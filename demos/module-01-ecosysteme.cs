// =====================================================================
// Module 1 — Écosystème .NET (Jour 1, 10h00–11h00)
// Démo : ce que le runtime sait de lui-même + top-level statements
// Exécution : dotnet run module-01-ecosysteme.cs
// =====================================================================
//
// Points à montrer en live AVANT ce fichier (dans un terminal) :
//   dotnet --info                  → SDK, runtimes installés
//   dotnet new console -n Demo     → structure d'un projet (.csproj, Program.cs)
//   dotnet build                   → compilation vers IL (dossier bin/)
//   dotnet run                     → JIT + exécution
//
// Ce fichier est une "file-based app" (.NET 10) : pas de .csproj,
// le SDK en génère un en mémoire. Idéal pour les démos.

using System.Runtime.InteropServices;

// --- Top-level statements : pas de classe Program ni de méthode Main visibles.
// Le compilateur les génère pour nous. Un seul fichier par projet peut en avoir.

Console.WriteLine("=== Démo Module 1 : l'écosystème .NET vu de l'intérieur ===\n");

// Le runtime (CLR) expose des infos sur lui-même
Console.WriteLine($"Version du runtime : {Environment.Version}");
Console.WriteLine($"Description        : {RuntimeInformation.FrameworkDescription}");
Console.WriteLine($"OS                 : {RuntimeInformation.OSDescription}");
Console.WriteLine($"Architecture       : {RuntimeInformation.OSArchitecture}");
Console.WriteLine($"Dossier courant    : {Environment.CurrentDirectory}");

// C# est compilé en IL (Intermediate Language), puis JIT-compilé en code machine.
// On peut le prouver : le même binaire tourne sur Windows, macOS et Linux.
Console.WriteLine($"\nProcesseurs logiques : {Environment.ProcessorCount}");
Console.WriteLine($"Utilisateur          : {Environment.UserName}");

// NuGet : le gestionnaire de paquets. En file-based app, on peut référencer
// un paquet avec la directive « #:package Nom@Version » en tête de fichier.
// (À montrer en démo bonus si le temps le permet.)

Console.WriteLine("\n→ À retenir : code C# → compilation → IL → JIT (CLR) → code machine.");
