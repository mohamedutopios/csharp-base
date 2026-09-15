// =====================================================================
// Module 11 — Démo (version projet multi-fichiers)
// LivreIndisponibleException.cs, Bibliotheque.cs, RessourceDemo.cs ;
// Program.cs = le scénario.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using DemoExceptions;

Console.WriteLine("=== Démo Module 11 : gestion des exceptions (projet) ===\n");

// 1. try / catch : intercepter et continuer
try
{
    int valeur = int.Parse("abc");
    Console.WriteLine($"Jamais affiché : {valeur}");
}
catch (FormatException ex)
{
    Console.WriteLine($"catch FormatException : « {ex.Message} » — le programme continue !");
}

// 2. finally : s'exécute TOUJOURS
try
{
    Console.WriteLine("\ntry    : ouverture d'une ressource fictive…");
    throw new InvalidOperationException("problème en plein traitement");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"catch  : {ex.Message}");
}
finally
{
    Console.WriteLine("finally: nettoyage exécuté quoi qu'il arrive");
}

// 3. Exception métier personnalisée (celle du fil rouge !)
var bibliotheque = new Bibliotheque();
Console.WriteLine();
bibliotheque.Emprunter("Dune");                  // OK

try
{
    bibliotheque.Emprunter("Dune");              // déjà sorti → exception métier
}
catch (LivreIndisponibleException ex)
{
    Console.WriteLine($"Exception métier interceptée : {ex.Message} (livre : {ex.Titre})");
}

// 4. using + IDisposable : libération GARANTIE
Console.WriteLine("\n--- using : Dispose appelé automatiquement ---");
using (var ressource = new RessourceDemo("connexion A"))
{
    ressource.Utiliser();
}   // ← Dispose() appelé ICI, même en cas d'exception

Console.WriteLine("\n→ À retenir : chaque exception métier a son fichier, " +
                  "comme n'importe quelle classe.");
