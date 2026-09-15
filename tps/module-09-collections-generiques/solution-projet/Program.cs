// =====================================================================
// Solution — TP Module 9 (version projet multi-fichiers)
// La classe générique vit dans PileBornee.cs ; Program.cs = le scénario
// et ses fonctions locales.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using TpInventaire;

Console.WriteLine("=== TP Module 9 (projet) ===\n");

// --- Partie A — List : le catalogue ---
var catalogue = new List<string> { "clavier", "souris", "écran" };
catalogue.Add("webcam");
catalogue.Insert(1, "casque");
catalogue.Remove("souris");
catalogue.Sort();
Console.WriteLine($"Catalogue trié ({catalogue.Count}) : {string.Join(", ", catalogue)}");

// --- Partie B — Dictionary : le stock ---
var stock = new Dictionary<string, int>
{
    ["clavier"] = 12,
    ["casque"] = 4,
    ["écran"] = 7,
    ["webcam"] = 0,
};

Console.WriteLine($"\nRetirer 3 claviers : {RetirerDuStock(stock, "clavier", 3)}");
Console.WriteLine($"Retirer 10 casques : {RetirerDuStock(stock, "casque", 10)} (stock insuffisant)");
Console.WriteLine($"Retirer 1 souris   : {RetirerDuStock(stock, "souris", 1)} (produit absent)");

foreach (var (produit, quantite) in stock)
    Console.WriteLine($"  {produit,-8} → {quantite}");

// --- Partie C — HashSet, Queue, Stack ---
string[] visites = { "alice", "bob", "alice", "chloé", "bob", "alice" };
var clientsUniques = new HashSet<string>(visites);
Console.WriteLine($"\nClients uniques : {clientsUniques.Count}");

var sav = new Queue<string>(["Alice", "Bob", "Chloé"]);
Console.WriteLine($"SAV : {sav.Dequeue()}, puis {sav.Dequeue()} ; reste {sav.Peek()}");

var historique = new Stack<string>(["ajout clavier", "retrait écran", "ajout webcam"]);
Console.WriteLine($"Annuler : {historique.Pop()}, puis {historique.Pop()}");

// --- Partie D — la classe générique du projet (PileBornee.cs) ---
Console.WriteLine("\n--- PileBornee<T> ---");
var pileEntiers = new PileBornee<int>(3);
Console.WriteLine($"Empiler 1, 2, 3 : {pileEntiers.Empiler(1)}, {pileEntiers.Empiler(2)}, {pileEntiers.Empiler(3)}");
Console.WriteLine($"Empiler 4 (pleine) : {pileEntiers.Empiler(4)}");
pileEntiers.TryDepiler(out var sommet);
Console.WriteLine($"Dépilé : {sommet} ; il en reste {pileEntiers.Nombre}");

var pileMots = new PileBornee<string>(2);          // MÊME code, autre type
pileMots.Empiler("bonjour");
pileMots.TryDepiler(out string? mot);
Console.WriteLine($"PileBornee<string> : dépilé « {mot} »");

AfficherTout(catalogue);
AfficherTout(clientsUniques);

// --- Bonus — contrainte where ---
Console.WriteLine($"\nPlusGrand([3, 9, 4]) : {PlusGrand(new List<int> { 3, 9, 4 })}");
Console.WriteLine($"PlusGrand(mots)      : {PlusGrand(new List<string> { "poire", "pomme", "abricot" })}");

static bool RetirerDuStock(Dictionary<string, int> stock, string produit, int quantite)
{
    if (!stock.TryGetValue(produit, out int disponible))
        return false;                       // produit inconnu

    if (quantite <= 0 || quantite > disponible)
        return false;

    stock[produit] = disponible - quantite;
    return true;
}

static void AfficherTout<T>(IEnumerable<T> elements) =>
    Console.WriteLine($"  [{string.Join(" · ", elements)}]");

static T PlusGrand<T>(List<T> liste) where T : IComparable<T>
{
    T max = liste[0];
    foreach (T element in liste)
    {
        if (element.CompareTo(max) > 0)
            max = element;
    }
    return max;
}
