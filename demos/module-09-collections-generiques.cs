// =====================================================================
// Module 9 — Collections et génériques (Jour 3, 12h00–15h00)
// Démo : tableaux vs List<T>, Dictionary, HashSet, Queue, Stack,
//        IEnumerable/IList, classes et méthodes génériques, where
// Exécution : dotnet run module-09-collections-generiques.cs
// Version projet (un type = un fichier) : module-09-collections-generiques-projet/
// =====================================================================

Console.WriteLine("=== Démo Module 9 : collections et génériques ===\n");

// ---------------------------------------------------------------
// 1. Tableau : taille FIXE, décidée à la création
// ---------------------------------------------------------------
string[] tableau = { "Paris", "Lyon", "Nantes" };
tableau[1] = "Marseille";               // modifier : oui
// tableau.Add("Lille");                // ajouter : NON, taille figée !
Console.WriteLine($"Tableau ({tableau.Length}) : {string.Join(", ", tableau)}");

// ---------------------------------------------------------------
// 2. List<T> : le tableau dynamique — LA collection par défaut
// ---------------------------------------------------------------
var villes = new List<string> { "Paris", "Lyon" };
villes.Add("Nantes");
villes.Insert(1, "Lille");
villes.Remove("Lyon");
Console.WriteLine($"List<string> ({villes.Count}) : {string.Join(", ", villes)}");
Console.WriteLine($"Contains(\"Paris\") : {villes.Contains("Paris")} | IndexOf(\"Nantes\") : {villes.IndexOf("Nantes")}");

// <T> = paramètre de type : List<int>, List<Livre>… même code, typé et sûr.
// villes.Add(42);                      // ← ERREUR de compilation : c'est une List<string>

// ---------------------------------------------------------------
// 3. Dictionary<K,V> : accès instantané PAR CLÉ
// ---------------------------------------------------------------
var stock = new Dictionary<string, int>
{
    ["pomme"] = 12,
    ["poire"] = 5
};
stock["banane"] = 8;                    // ajout (ou remplacement si la clé existe)
stock["pomme"] = 10;                    // remplacement

Console.WriteLine($"\nDictionary : stock[\"pomme\"] = {stock["pomme"]}");

// Ne JAMAIS lire une clé sans vérifier : stock["kiwi"] → KeyNotFoundException
if (stock.TryGetValue("kiwi", out int qte))
    Console.WriteLine($"kiwi : {qte}");
else
    Console.WriteLine("kiwi absent → TryGetValue évite le crash");

foreach (var (fruit, quantite) in stock)          // parcours clé/valeur
    Console.WriteLine($"  {fruit,-8} → {quantite}");

// ---------------------------------------------------------------
// 4. HashSet<T> : ensemble SANS DOUBLONS
// ---------------------------------------------------------------
var tags = new HashSet<string> { "roman", "sf" };
bool ajoute1 = tags.Add("policier");    // true : nouvel élément
bool ajoute2 = tags.Add("sf");          // false : déjà présent, ignoré
Console.WriteLine($"\nHashSet : {string.Join(", ", tags)} (re-Add(\"sf\") → {ajoute2})");

// ---------------------------------------------------------------
// 5. Queue<T> (FIFO) et Stack<T> (LIFO)
// ---------------------------------------------------------------
var fileAttente = new Queue<string>();
fileAttente.Enqueue("Alice");
fileAttente.Enqueue("Bob");
fileAttente.Enqueue("Chloé");
Console.WriteLine($"\nQueue (premier arrivé, premier servi) : Dequeue → {fileAttente.Dequeue()}, puis {fileAttente.Dequeue()}");

var historique = new Stack<string>();
historique.Push("page1");
historique.Push("page2");
historique.Push("page3");
Console.WriteLine($"Stack (dernier arrivé, premier sorti)  : Pop → {historique.Pop()}, puis {historique.Pop()} (= bouton Retour)");

// ---------------------------------------------------------------
// 6. IEnumerable<T> / IList<T> : programmer contre l'interface
// ---------------------------------------------------------------
// AfficherTout accepte List, tableau, HashSet, Queue… tout ce qui s'énumère.
Console.WriteLine("\nIEnumerable<T> accepte tout ce qui se parcourt :");
AfficherTout(villes);
AfficherTout(tableau);
AfficherTout(tags);

// ---------------------------------------------------------------
// 7. Méthode générique : un algorithme, tous les types
// ---------------------------------------------------------------
Console.WriteLine($"\nPremierEtDernier(villes)  : {PremierEtDernier(villes)}");
Console.WriteLine($"PremierEtDernier([1..5])  : {PremierEtDernier(new List<int> { 1, 2, 3, 4, 5 })}");

// ---------------------------------------------------------------
// 8. Classe générique avec contrainte where
// ---------------------------------------------------------------
var boiteNotes = new Boite<int>(42);              // T = int
var boiteMot = new Boite<string>("bonjour");      // T = string
Console.WriteLine($"\nBoite<int> : {boiteNotes.Contenu} | Boite<string> : {boiteMot.Contenu}");

// where T : IComparable<T> garantit que CompareTo existe → Max compile
Console.WriteLine($"Max(3, 7) = {Max(3, 7)} | Max(\"pomme\", \"poire\") = {Max("pomme", "poire")}");

Console.WriteLine("\n→ À retenir : List par défaut, Dictionary pour chercher par clé, " +
                  "HashSet pour l'unicité, IEnumerable en paramètre.");

// =====================================================================
// Méthodes et classes génériques
// =====================================================================

// Paramètre le plus GÉNÉRAL possible : tout ce qui s'énumère convient
static void AfficherTout(IEnumerable<string> elements) =>
    Console.WriteLine($"  [{string.Join(" · ", elements)}]");

// <T> après le nom : méthode générique
static string PremierEtDernier<T>(IList<T> liste) =>
    $"premier = {liste[0]}, dernier = {liste[^1]}";     // ^1 = dernier élément

// Contrainte where : T doit savoir se comparer
static T Max<T>(T a, T b) where T : IComparable<T> =>
    a.CompareTo(b) >= 0 ? a : b;

// Classe générique : le type du contenu est un paramètre
class Boite<T>
{
    public T Contenu { get; }
    public Boite(T contenu) => Contenu = contenu;
}
