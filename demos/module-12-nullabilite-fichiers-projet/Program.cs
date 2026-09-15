// =====================================================================
// Module 12 — Démo (version projet multi-fichiers)
// Le record Livre vit dans Livre.cs ; Program.cs = le scénario.
// NB : dans un projet console classique, la sérialisation JSON par
// réflexion marche telle quelle — la directive #:property PublishAot=false
// n'était nécessaire que pour la version file-based app.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using System.Text.Json;
using DemoFichiers;

Console.WriteLine("=== Démo Module 12 : nullabilité et fichiers (projet) ===\n");

// 1. Les opérateurs de survie en terrain nullable
string? saisie = null;
Console.WriteLine($"?.  → saisie?.Length = {(saisie?.Length)?.ToString() ?? "null"}");
Console.WriteLine($"??  → {saisie ?? "(vide)"}");
saisie ??= "valeur par défaut";
Console.WriteLine($"??= → saisie vaut maintenant « {saisie} »");

// 2. Path : construire des chemins PORTABLES
string dossier = Path.Combine(Path.GetTempPath(), "demo-csharp-projet");
string fichierJson = Path.Combine(dossier, "bibliotheque.json");
Directory.CreateDirectory(dossier);
Console.WriteLine($"\nPath.Combine : {fichierJson}");

// 3. File : écrire et lire du texte
string fichierTexte = Path.Combine(dossier, "notes.txt");
File.WriteAllText(fichierTexte, "Ligne 1 : bonjour\n");
File.AppendAllText(fichierTexte, "Ligne 2 : depuis C# !\n");
Console.WriteLine($"File.ReadAllLines : {File.ReadAllLines(fichierTexte).Length} lignes");

// 4. System.Text.Json : sérialiser des objets
var livres = new List<Livre>
{
    new("1984", "George Orwell", 1949),
    new("Dune", "Frank Herbert", 1965),
};

var options = new JsonSerializerOptions { WriteIndented = true };
File.WriteAllText(fichierJson, JsonSerializer.Serialize(livres, options));
Console.WriteLine($"\n{livres.Count} livres sauvegardés en JSON");

// JSON → objets — le résultat PEUT être null : on gère avec ??
List<Livre> relus = JsonSerializer.Deserialize<List<Livre>>(File.ReadAllText(fichierJson)) ?? new();
Console.WriteLine($"Deserialize → {relus.Count} livres relus, le 1er : {relus[0]}");
Console.WriteLine($"Égalité de contenu (record) : {relus[0] == livres[0]}");

Directory.Delete(dossier, recursive: true);
Console.WriteLine("\n(Dossier temporaire supprimé.)");

Console.WriteLine("\n→ À retenir : en projet classique, le JSON par réflexion " +
                  "fonctionne sans configuration.");
