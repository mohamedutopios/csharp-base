// =====================================================================
// Module 10 — LINQ (Jour 4, 9h15–10h45)
// Démo : lambdas Func/Action, Where/Select/OrderBy/GroupBy/Count/
//        Any/First, exécution différée, ToList
// Exécution : dotnet run module-10-linq.cs
// =====================================================================

Console.WriteLine("=== Démo Module 10 : LINQ ===\n");

// Jeu de données : les livres de la bibliothèque (fil rouge !)
var livres = new List<Livre>
{
    new("1984", "George Orwell", 1949, Disponible: true),
    new("Dune", "Frank Herbert", 1965, Disponible: false),
    new("Fondation", "Isaac Asimov", 1951, Disponible: true),
    new("La Ferme des animaux", "George Orwell", 1945, Disponible: true),
    new("Hypérion", "Dan Simmons", 1989, Disponible: false),
    new("Les Robots", "Isaac Asimov", 1950, Disponible: true),
};

// ---------------------------------------------------------------
// 1. Lambdas : une fonction anonyme en une expression
// ---------------------------------------------------------------
Func<int, int> carre = x => x * x;              // Func : prend, RENVOIE
Func<int, int, int> addition = (a, b) => a + b;
Action<string> afficher = texte => Console.WriteLine($"  {texte}");  // Action : ne renvoie rien

Console.WriteLine($"carre(5) = {carre(5)} | addition(3, 4) = {addition(3, 4)}");
afficher("Une Action affiche sans renvoyer.");

// LINQ n'est QUE ça : des méthodes qui prennent des lambdas en paramètre.

// ---------------------------------------------------------------
// 2. Where : filtrer (garde les éléments qui passent le test)
// ---------------------------------------------------------------
var disponibles = livres.Where(l => l.Disponible).ToList();
Console.WriteLine($"\nWhere → {disponibles.Count} livres disponibles :");
foreach (var l in disponibles)
    Console.WriteLine($"  {l.Titre}");

// ---------------------------------------------------------------
// 3. Select : transformer (projeter chaque élément vers autre chose)
// ---------------------------------------------------------------
var titres = livres.Select(l => l.Titre.ToUpper()).ToList();
Console.WriteLine($"\nSelect → {string.Join(" | ", titres)}");

// ---------------------------------------------------------------
// 4. OrderBy / ThenBy : trier
// ---------------------------------------------------------------
Console.WriteLine("\nOrderBy(Auteur).ThenBy(Annee) :");
foreach (var l in livres.OrderBy(l => l.Auteur).ThenBy(l => l.Annee))
    Console.WriteLine($"  {l.Auteur,-15} {l.Annee}  {l.Titre}");

// ---------------------------------------------------------------
// 5. Count / Any / First / FirstOrDefault
// ---------------------------------------------------------------
Console.WriteLine($"\nCount(av. 1960)     : {livres.Count(l => l.Annee < 1960)}");
Console.WriteLine($"Any(Orwell)         : {livres.Any(l => l.Auteur == "George Orwell")}");
Console.WriteLine($"First(dispo)        : {livres.First(l => l.Disponible).Titre}");

// First plante si rien ne correspond ; FirstOrDefault renvoie null
var deVerne = livres.FirstOrDefault(l => l.Auteur == "Jules Verne");
Console.WriteLine($"FirstOrDefault(Verne): {(deVerne is null ? "null (aucun match, pas de crash)" : deVerne.Titre)}");

// ---------------------------------------------------------------
// 6. GroupBy : regrouper par clé
// ---------------------------------------------------------------
Console.WriteLine("\nGroupBy(Auteur) :");
foreach (var groupe in livres.GroupBy(l => l.Auteur))
    Console.WriteLine($"  {groupe.Key,-15} → {groupe.Count()} livre(s) : {string.Join(", ", groupe.Select(l => l.Titre))}");

// ---------------------------------------------------------------
// 7. Chaîner : le vrai pouvoir de LINQ
// ---------------------------------------------------------------
var topAncien = livres
    .Where(l => l.Disponible)          // 1. filtrer
    .OrderBy(l => l.Annee)             // 2. trier
    .Select(l => $"{l.Titre} ({l.Annee})")  // 3. transformer
    .Take(2)                           // 4. limiter
    .ToList();                         // 5. matérialiser
Console.WriteLine($"\nChaînage → 2 disponibles les plus anciens : {string.Join(" ; ", topAncien)}");

// ---------------------------------------------------------------
// 8. EXÉCUTION DIFFÉRÉE : la requête ne tourne que quand on l'énumère
// ---------------------------------------------------------------
Console.WriteLine("\n--- Exécution différée (le piège à comprendre) ---");
var nombres = new List<int> { 1, 2, 3 };

var pairs = nombres.Where(x => x % 2 == 0);    // AUCUN calcul ici : juste une « recette »

nombres.Add(4);                                // on modifie la source APRÈS la requête
nombres.Add(6);

// L'énumération se fait MAINTENANT → la requête voit 4 et 6 !
Console.WriteLine($"pairs énuméré après les Add : {string.Join(", ", pairs)}");

var pairsFiges = nombres.Where(x => x % 2 == 0).ToList();  // ToList = photo immédiate
nombres.Add(8);
Console.WriteLine($"ToList figé avant Add(8)    : {string.Join(", ", pairsFiges)}");

Console.WriteLine("\n→ À retenir : Where filtre, Select transforme, la requête est paresseuse, " +
                  "ToList l'exécute et fige le résultat.");

// =====================================================================
record Livre(string Titre, string Auteur, int Annee, bool Disponible);
