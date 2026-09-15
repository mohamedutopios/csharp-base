// =====================================================================
// Program.cs — point d'entrée : le scénario et la fonction d'aiguillage.
// Les contrats et les classes vivent dans IForme.cs, IExportable.cs,
// Cercle.cs, Rectangle.cs, TriangleRectangle.cs.
// =====================================================================

using TpFormes;

Console.WriteLine("=== TP Module 7 (version projet multi-fichiers) ===\n");

// --- Partie A — polymorphisme par interface ---
var formes = new List<IForme>
{
    new Cercle(3.0),
    new Rectangle(4.0, 3.0),
    new TriangleRectangle(6.0, 4.0),
};

foreach (IForme forme in formes)
    Console.WriteLine($"{forme.GetType().Name,-17} aire = {forme.Aire():F2}, périmètre = {forme.Perimetre():F2}");

// --- Partie B — deuxième contrat, implémenté par certaines classes seulement ---
Console.WriteLine("\n--- Export CSV (seulement les IExportable) ---");
foreach (IForme forme in formes)
{
    if (forme is IExportable exportable)          // test + cast en une fois
        Console.WriteLine($"  {exportable.ExporterCsv()}");
    else
        Console.WriteLine($"  ({forme.GetType().Name} : pas exportable, sauté)");
}

// --- Partie C — is, as, pattern matching ---
Console.WriteLine("\n--- Decrire ---");
object?[] objets = { new Cercle(3), new Rectangle(4, 3), new Rectangle(4, 4),
                     new TriangleRectangle(6, 4), "bonjour", null };
foreach (object? o in objets)
    Console.WriteLine($"  {Decrire(o)}");

object pasUnCercle = new Rectangle(2, 2);
Cercle? tentative = pasUnCercle as Cercle;        // null, pas d'exception
Console.WriteLine($"\nas Cercle sur un Rectangle : {(tentative is null ? "null (échec doux)" : "cercle")}");

// --- Bonus — implémentation par défaut + tri par aire ---
Console.WriteLine("\n--- Resume (implémentation par défaut de l'interface) ---");
foreach (IForme forme in formes)
    Console.WriteLine($"  {forme.GetType().Name} : {forme.Resume()}");

formes.Sort((a, b) => a.Aire().CompareTo(b.Aire()));
Console.WriteLine($"\nTri par aire : {string.Join(" < ", formes.Select(f => $"{f.GetType().Name} ({f.Aire():F1})"))}");

// Fonction d'aiguillage (pattern matching) — une fonction locale du
// point d'entrée, pas une classe : elle reste donc dans Program.cs
static string Decrire(object? objet) => objet switch
{
    Cercle c                                   => $"cercle de rayon {c.Rayon}",
    Rectangle r when r.Largeur == r.Hauteur    => $"carré de côté {r.Largeur}",
    Rectangle r                                => $"rectangle {r.Largeur}x{r.Hauteur}",
    IForme f                                   => $"autre forme d'aire {f.Aire():F2}",
    null                                       => "rien du tout",
    _                                          => $"objet inconnu : {objet.GetType().Name}"
};
