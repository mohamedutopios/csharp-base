// =====================================================================
// Solution — TP Module 1 : ma première solution
// Exécution : dotnet run solution.cs
//
// Ce fichier est le Program.cs attendu en Partie C. Les parties A, B
// et D sont des commandes de terminal : elles sont détaillées en
// commentaires ci-dessous, dans l'ordre de l'énoncé.
// =====================================================================
//
// ---------------------------------------------------------------
// Partie A — Créer la structure
// ---------------------------------------------------------------
//   mkdir tp-ecosysteme && cd tp-ecosysteme
//   dotnet new sln -n TpEcosysteme
//   dotnet new console -n Application -o src/Application
//   dotnet new console -n Outils -o src/Outils
//   dotnet sln add src/Application/Application.csproj
//   dotnet sln add src/Outils/Outils.csproj
//   dotnet sln list
//       → src/Application/Application.csproj
//         src/Outils/Outils.csproj
//
// ---------------------------------------------------------------
// Partie B — Explorer et exécuter
// ---------------------------------------------------------------
//   Dans src/Application/Application.csproj :
//       <TargetFramework>net10.0</TargetFramework>
//
//   Exécuter depuis la racine (sans cd) :
//       dotnet run --project src/Application
//
//   Compiler toute la solution et trouver le binaire :
//       dotnet build
//       ls src/Application/bin/Debug/net10.0/
//       → Application.dll  (l'IL), Application (lanceur), *.json…
//
// ---------------------------------------------------------------
// Partie C — Personnaliser : le contenu de src/Application/Program.cs
// ---------------------------------------------------------------

Console.WriteLine("Bienvenue dans TpEcosysteme !");
Console.WriteLine($"Runtime : {Environment.Version}");
Console.WriteLine($"Machine : {Environment.MachineName}");

// ---------------------------------------------------------------
// Partie D — Git
// ---------------------------------------------------------------
//   git init
//   dotnet new gitignore
//   git add .
//   git commit -m "TP module 1 : structure solution + 2 projets"
//   git status     → bin/ et obj/ n'apparaissent pas : ignorés
//
// ---------------------------------------------------------------
// Réponses aux questions de compréhension
// ---------------------------------------------------------------
// 1. dotnet build compile (C# → IL dans un .dll) sans exécuter ;
//    dotnet run compile si nécessaire PUIS exécute (run = build + lancement).
// 2. Le .dll contient de l'IL (Intermediate Language), pas du code machine.
//    Le CLR le compile en code machine à l'exécution (JIT) → portabilité.
// 3. bin/ et obj/ sont des fichiers GÉNÉRÉS, reproductibles par dotnet build
//    sur n'importe quelle machine : les committer alourdit le dépôt et
//    crée des conflits inutiles.
