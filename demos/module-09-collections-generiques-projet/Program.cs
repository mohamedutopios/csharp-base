// =====================================================================
// Module 9 — Démo (version projet multi-fichiers)
// La classe générique vit dans Boite.cs ; les méthodes utilitaires du
// scénario restent des fonctions locales de Program.cs.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using DemoCollections;

Console.WriteLine("=== Démo Module 9 : collections et génériques (projet) ===\n");

// 1. List<T> : le tableau dynamique — LA collection par défaut
var villes = new List<string> { "Paris", "Lyon" };
villes.Add("Nantes");
villes.Insert(1, "Lille");
villes.Remove("Lyon");
Console.WriteLine($"List<string> ({villes.Count}) : {string.Join(", ", villes)}");

// 2. Dictionary<K,V> : accès instantané PAR CLÉ
var stock = new Dictionary<string, int> { ["pomme"] = 12, ["poire"] = 5 };
stock["banane"] = 8;
if (stock.TryGetValue("kiwi", out int qte))
    Console.WriteLine($"kiwi : {qte}");
else
    Console.WriteLine("kiwi absent → TryGetValue évite le crash");

// 3. HashSet<T> : ensemble SANS DOUBLONS
var tags = new HashSet<string> { "roman", "sf" };
Console.WriteLine($"HashSet : re-Add(\"sf\") → {tags.Add("sf")} (doublon ignoré)");

// 4. Queue<T> (FIFO) et Stack<T> (LIFO)
var fileAttente = new Queue<string>(["Alice", "Bob", "Chloé"]);
Console.WriteLine($"Queue : Dequeue → {fileAttente.Dequeue()} (premier arrivé, premier servi)");

var historique = new Stack<string>(["page1", "page2", "page3"]);
Console.WriteLine($"Stack : Pop → {historique.Pop()} (= bouton Retour)");

// 5. IEnumerable<T> : programmer contre l'interface
Console.WriteLine("\nIEnumerable<T> accepte tout ce qui se parcourt :");
AfficherTout(villes);
AfficherTout(tags);
AfficherTout(stock.Keys);

// 6. La classe générique du projet (Boite.cs)
var boiteNotes = new Boite<int>(42);              // T = int
var boiteMot = new Boite<string>("bonjour");      // T = string
Console.WriteLine($"\nBoite<int> : {boiteNotes.Contenu} | Boite<string> : {boiteMot.Contenu}");

// 7. Méthode générique avec contrainte where
Console.WriteLine($"Max(3, 7) = {Max(3, 7)} | Max(\"pomme\", \"poire\") = {Max("pomme", "poire")}");

Console.WriteLine("\n→ À retenir : List par défaut, Dictionary pour chercher par clé ; " +
                  "les classes génériques ont leur fichier (Boite.cs).");

// Paramètre le plus GÉNÉRAL possible : tout ce qui s'énumère convient
static void AfficherTout(IEnumerable<string> elements) =>
    Console.WriteLine($"  [{string.Join(" · ", elements)}]");

// Contrainte where : T doit savoir se comparer
static T Max<T>(T a, T b) where T : IComparable<T> =>
    a.CompareTo(b) >= 0 ? a : b;
