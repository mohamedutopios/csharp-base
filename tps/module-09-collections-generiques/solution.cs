// =====================================================================
// Solution — TP Module 9 : l'inventaire du magasin
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 9 ===\n");

// ---------------------------------------------------------------
// Partie A — List : le catalogue
// ---------------------------------------------------------------
var catalogue = new List<string> { "clavier", "souris", "écran" };
catalogue.Add("webcam");
catalogue.Insert(1, "casque");
catalogue.Remove("souris");

Console.WriteLine($"Catalogue ({catalogue.Count}) : {string.Join(", ", catalogue)}");
catalogue.Sort();
Console.WriteLine($"Trié : {string.Join(", ", catalogue)}");
Console.WriteLine($"IndexOf(\"écran\") : {catalogue.IndexOf("écran")} | Contains(\"souris\") : {catalogue.Contains("souris")}");
// Question 4 : le nombre de produits varie (ajouts, retraits) ; un tableau a
// une taille FIXE. List<T> grandit toute seule et offre Insert/Remove/Sort.

// ---------------------------------------------------------------
// Partie B — Dictionary : le stock
// ---------------------------------------------------------------
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

Console.WriteLine("\nStock :");
foreach (var (produit, quantite) in stock)
    Console.WriteLine($"  {produit,-8} → {quantite}");

Console.Write("En rupture : ");
foreach (var (produit, quantite) in stock)
{
    if (quantite == 0)
        Console.Write($"{produit} ");
}
Console.WriteLine();

// ---------------------------------------------------------------
// Partie C — HashSet, Queue, Stack
// ---------------------------------------------------------------
// 9. HashSet : l'unicité est GRATUITE, les doublons sont ignorés à l'Add
string[] visites = { "alice", "bob", "alice", "chloé", "bob", "alice" };
var clientsUniques = new HashSet<string>(visites);
Console.WriteLine($"\nClients uniques : {clientsUniques.Count} ({string.Join(", ", clientsUniques)})");

// 10. Queue : premier arrivé, premier servi (FIFO)
var sav = new Queue<string>();
sav.Enqueue("Alice");
sav.Enqueue("Bob");
sav.Enqueue("Chloé");
Console.WriteLine($"SAV : traitement de {sav.Dequeue()}, puis {sav.Dequeue()} ; reste en tête : {sav.Peek()}");

// 11. Stack : dernier arrivé, premier sorti (LIFO) → parfait pour « annuler »
var historique = new Stack<string>();
historique.Push("ajout clavier");
historique.Push("retrait écran");
historique.Push("ajout webcam");
Console.WriteLine($"Annuler : {historique.Pop()}, puis {historique.Pop()} ; reste : {historique.Peek()}");
// Question 12 : List obligerait à gérer soi-même les indices d'entrée/sortie ;
// HashSet garantit l'unicité, Queue impose l'ordre d'arrivée, Stack l'ordre
// inverse — la structure PORTE la règle du problème.

// ---------------------------------------------------------------
// Partie D — classe générique maison
// ---------------------------------------------------------------
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

var pileVide = new PileBornee<string>(2);
Console.WriteLine($"TryDepiler sur pile vide : {pileVide.TryDepiler(out _)} (pas de crash)");

// AfficherTout : générique, accepte tout IEnumerable<T>
Console.WriteLine("\n--- AfficherTout (générique) ---");
AfficherTout(catalogue);              // List<string>
AfficherTout(stock.Keys);             // les clés du dictionnaire
AfficherTout(clientsUniques);         // HashSet<string>
AfficherTout(new[] { 1, 2, 3 });      // tableau d'int : T = int

// ---------------------------------------------------------------
// Bonus — contrainte where
// ---------------------------------------------------------------
Console.WriteLine($"\nPlusGrand([3, 9, 4]) : {PlusGrand(new List<int> { 3, 9, 4 })}");
Console.WriteLine($"PlusGrand(mots)      : {PlusGrand(new List<string> { "poire", "pomme", "abricot" })}");

// =====================================================================
// Méthodes
// =====================================================================

static bool RetirerDuStock(Dictionary<string, int> stock, string produit, int quantite)
{
    // TryGetValue : jamais stock[produit] sans vérifier (KeyNotFoundException)
    if (!stock.TryGetValue(produit, out int disponible))
        return false;                       // produit inconnu

    if (quantite <= 0 || quantite > disponible)
        return false;                       // demande invalide ou stock insuffisant

    stock[produit] = disponible - quantite;
    return true;
}

// <T> : la méthode marche pour N'IMPORTE quel type d'éléments
static void AfficherTout<T>(IEnumerable<T> elements) =>
    Console.WriteLine($"  [{string.Join(" · ", elements)}]");

// where : T doit savoir se comparer, sinon CompareTo n'existerait pas
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

// =====================================================================
// Classe générique : le type des éléments est un paramètre
// =====================================================================

class PileBornee<T>
{
    private readonly List<T> _elements = new();    // la List fait le stockage
    private readonly int _capacite;

    public PileBornee(int capacite) => _capacite = capacite;

    public int Nombre => _elements.Count;

    public bool Empiler(T element)
    {
        if (_elements.Count >= _capacite)
            return false;                           // pleine

        _elements.Add(element);                     // le sommet = fin de liste
        return true;
    }

    public bool TryDepiler(out T? element)
    {
        if (_elements.Count == 0)
        {
            element = default;                      // default : null ou 0 selon T
            return false;
        }
        element = _elements[^1];                    // ^1 : dernier élément
        _elements.RemoveAt(_elements.Count - 1);
        return true;
    }
}
