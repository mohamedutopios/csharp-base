// =====================================================================
// Solution — TP Module 7 : formes et exports
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 7 ===\n");

// ---------------------------------------------------------------
// Partie A — polymorphisme par interface
// ---------------------------------------------------------------
var formes = new List<IForme>
{
    new Cercle(3.0),
    new Rectangle(4.0, 3.0),
    new TriangleRectangle(6.0, 4.0),
};

foreach (IForme forme in formes)
    Console.WriteLine($"{forme.GetType().Name,-17} aire = {forme.Aire():F2}, périmètre = {forme.Perimetre():F2}");

// Question 4 : une classe abstraite imposerait un lien de parenté (« est un »)
// et interdirait un autre parent. L'interface est un simple contrat (« sait
// faire ») : des types sans rapport peuvent l'implémenter, et une classe
// peut en implémenter PLUSIEURS.

// ---------------------------------------------------------------
// Partie B — deuxième contrat, implémenté par certaines classes seulement
// ---------------------------------------------------------------
Console.WriteLine("\n--- Export CSV (seulement les IExportable) ---");
foreach (IForme forme in formes)
{
    if (forme is IExportable exportable)          // test + cast en une fois
        Console.WriteLine($"  {exportable.ExporterCsv()}");
    else
        Console.WriteLine($"  ({forme.GetType().Name} : pas exportable, sauté)");
}

// ---------------------------------------------------------------
// Partie C — is, as, pattern matching
// ---------------------------------------------------------------
Console.WriteLine("\n--- Decrire ---");
object?[] objets = { new Cercle(3), new Rectangle(4, 3), new Rectangle(4, 4),
                     new TriangleRectangle(6, 4), "bonjour", null };
foreach (object? o in objets)
    Console.WriteLine($"  {Decrire(o)}");

// Cast dur vs as :
object pasUnCercle = new Rectangle(2, 2);
Cercle? tentative = pasUnCercle as Cercle;        // null, pas d'exception
Console.WriteLine($"\nas Cercle sur un Rectangle : {(tentative is null ? "null (échec doux)" : "cercle")}");
// Cercle crash = (Cercle)pasUnCercle;            // ← InvalidCastException !

// ---------------------------------------------------------------
// Bonus — implémentation par défaut + tri par aire
// ---------------------------------------------------------------
Console.WriteLine("\n--- Resume (implémentation par défaut de l'interface) ---");
foreach (IForme forme in formes)
    Console.WriteLine($"  {forme.GetType().Name} : {forme.Resume()}");

formes.Sort((a, b) => a.Aire().CompareTo(b.Aire()));
Console.WriteLine($"\nTri par aire : {string.Join(" < ", formes.Select(f => $"{f.GetType().Name} ({f.Aire():F1})"))}");

// =====================================================================
// Méthode d'aiguillage (pattern matching)
// =====================================================================

static string Decrire(object? objet) => objet switch
{
    Cercle c                                   => $"cercle de rayon {c.Rayon}",
    Rectangle r when r.Largeur == r.Hauteur    => $"carré de côté {r.Largeur}",   // le when AVANT le cas général !
    Rectangle r                                => $"rectangle {r.Largeur}x{r.Hauteur}",
    IForme f                                   => $"autre forme d'aire {f.Aire():F2}",
    null                                       => "rien du tout",
    _                                          => $"objet inconnu : {objet.GetType().Name}"
};

// =====================================================================
// Contrats et classes
// =====================================================================

interface IForme
{
    double Aire();
    double Perimetre();

    // Bonus : implémentation PAR DÉFAUT (C# 8+) — héritée par tous
    string Resume() => $"aire {Aire():F2}, périmètre {Perimetre():F2}";
}

interface IExportable
{
    string ExporterCsv();
}

class Cercle : IForme, IExportable                 // deux contrats à la fois
{
    public double Rayon { get; }
    public Cercle(double rayon) => Rayon = rayon;

    public double Aire() => Math.PI * Rayon * Rayon;
    public double Perimetre() => 2 * Math.PI * Rayon;
    public string ExporterCsv() => $"cercle;{Rayon}";
}

class Rectangle : IForme, IExportable
{
    public double Largeur { get; }
    public double Hauteur { get; }
    public Rectangle(double largeur, double hauteur) => (Largeur, Hauteur) = (largeur, hauteur);

    public double Aire() => Largeur * Hauteur;
    public double Perimetre() => 2 * (Largeur + Hauteur);
    public string ExporterCsv() => $"rectangle;{Largeur};{Hauteur}";
}

class TriangleRectangle : IForme                   // PAS IExportable : c'est un choix
{
    public double Base { get; }
    public double Hauteur { get; }
    public TriangleRectangle(double base_, double hauteur) => (Base, Hauteur) = (base_, hauteur);

    public double Aire() => Base * Hauteur / 2;
    public double Perimetre() => Base + Hauteur + Math.Sqrt(Base * Base + Hauteur * Hauteur);
}
