// =====================================================================
// Module 12 — Nullabilité et fichiers (Jour 4, 11h45–12h30)
// Démo : nullable reference types, ?, !, ??=, System.IO (File, Path),
//        System.Text.Json
// Exécution : dotnet run module-12-nullabilite-fichiers.cs
// =====================================================================

// Les file-based apps visent l'AOT par défaut, ce qui coupe la sérialisation
// JSON par réflexion ; on la réactive (inutile dans un projet console classique).
#:property PublishAot=false

using System.Text.Json;

Console.WriteLine("=== Démo Module 12 : nullabilité et fichiers ===\n");

// ---------------------------------------------------------------
// 1. Nullable reference types : le compilateur traque les null
// ---------------------------------------------------------------
string toujoursRempli = "valeur";      // ne DOIT jamais être null
string? peutEtreNull = null;           // le ? déclare : « null possible »

// Console.WriteLine(peutEtreNull.Length);   // ← WARNING du compilateur : déréférencement possible de null

// Le compilateur est content si on VÉRIFIE d'abord :
if (peutEtreNull is not null)
    Console.WriteLine(peutEtreNull.Length);  // plus de warning : flux analysé

// ---------------------------------------------------------------
// 2. Les opérateurs de survie en terrain nullable
// ---------------------------------------------------------------
string? saisie = null;

// ?.  : n'appelle que si non-null (sinon renvoie null)
int? longueur = saisie?.Length;
Console.WriteLine($"?.  → saisie?.Length = {(longueur?.ToString() ?? "null")}");

// ??  : valeur de repli
string affichage = saisie ?? "(vide)";
Console.WriteLine($"??  → {affichage}");

// ??= : n'affecte QUE si la variable est null
saisie ??= "valeur par défaut";
Console.WriteLine($"??= → saisie vaut maintenant « {saisie} »");

// !   : « fais-moi confiance, ce n'est pas null » — à utiliser AVEC PARCIMONIE
string? config = ChargerConfig();
string valeurSure = config!;           // on supprime le warning… à nos risques
Console.WriteLine($"!   → {valeurSure} (le ! ne protège de rien à l'exécution !)");

// ---------------------------------------------------------------
// 3. Path : construire des chemins PORTABLES (Windows/macOS/Linux)
// ---------------------------------------------------------------
string dossier = Path.Combine(Path.GetTempPath(), "demo-csharp");
string fichierTexte = Path.Combine(dossier, "notes.txt");
string fichierJson = Path.Combine(dossier, "bibliotheque.json");

Console.WriteLine($"\nPath.Combine        : {fichierTexte}");
Console.WriteLine($"Path.GetFileName    : {Path.GetFileName(fichierTexte)}");
Console.WriteLine($"Path.GetExtension   : {Path.GetExtension(fichierTexte)}");

Directory.CreateDirectory(dossier);    // ne plante pas si le dossier existe déjà

// ---------------------------------------------------------------
// 4. File : écrire et lire du texte
// ---------------------------------------------------------------
File.WriteAllText(fichierTexte, "Ligne 1 : bonjour\n");
File.AppendAllText(fichierTexte, "Ligne 2 : depuis C# !\n");

Console.WriteLine($"\nFile.Exists : {File.Exists(fichierTexte)}");
string contenu = File.ReadAllText(fichierTexte);
Console.WriteLine($"File.ReadAllText :\n{contenu}");

string[] lignes = File.ReadAllLines(fichierTexte);
Console.WriteLine($"File.ReadAllLines : {lignes.Length} lignes, la 2e est « {lignes[1]} »");

// ---------------------------------------------------------------
// 5. System.Text.Json : sérialiser des objets (la persistance du fil rouge)
// ---------------------------------------------------------------
var livres = new List<Livre>
{
    new("1984", "George Orwell", 1949),
    new("Dune", "Frank Herbert", 1965),
};

// Objet → JSON (sérialisation)
var options = new JsonSerializerOptions { WriteIndented = true };
string json = JsonSerializer.Serialize(livres, options);
Console.WriteLine($"\nSerialize →\n{json}");

// Sauvegarder puis recharger : le cycle complet
File.WriteAllText(fichierJson, json);
Console.WriteLine($"Sauvegardé dans {fichierJson}");

// JSON → objets (désérialisation) — le résultat PEUT être null : on gère !
string jsonRelu = File.ReadAllText(fichierJson);
List<Livre> relus = JsonSerializer.Deserialize<List<Livre>>(jsonRelu) ?? new();
Console.WriteLine($"Deserialize → {relus.Count} livres relus, le 1er : {relus[0]}");

// Nettoyage de la démo
Directory.Delete(dossier, recursive: true);
Console.WriteLine("\n(Dossier temporaire de démo supprimé.)");

Console.WriteLine("\n→ À retenir : ? déclare le null, ?? le rattrape, ! le masque (danger) ; " +
                  "Path.Combine toujours ; Serialize/Deserialize pour persister.");

// =====================================================================
static string? ChargerConfig() => "config chargée";   // simule une valeur peut-être absente

record Livre(string Titre, string Auteur, int Annee);
