// =====================================================================
// Solution — TP Module 10 : la médiathèque en chiffres
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 10 ===\n");

var films = new List<Film>
{
    new("Inception", "Christopher Nolan", 2010, "SF", 4.5, 148),
    new("Interstellar", "Christopher Nolan", 2014, "SF", 4.6, 169),
    new("Le Voyage de Chihiro", "Hayao Miyazaki", 2001, "Animation", 4.7, 125),
    new("Mon Voisin Totoro", "Hayao Miyazaki", 1988, "Animation", 4.4, 86),
    new("Pulp Fiction", "Quentin Tarantino", 1994, "Policier", 4.5, 154),
    new("Kill Bill", "Quentin Tarantino", 2003, "Action", 4.0, 111),
    new("Alien", "Ridley Scott", 1979, "SF", 4.3, 117),
    new("Blade Runner", "Ridley Scott", 1982, "SF", 4.2, 117),
    new("Amélie Poulain", "Jean-Pierre Jeunet", 2001, "Comédie", 4.1, 122),
};

// ---------------------------------------------------------------
// Partie A — Filtrer et projeter
// ---------------------------------------------------------------
// 1. Where : filtrer
var sf = films.Where(f => f.Genre == "SF");
Console.WriteLine($"1. SF : {string.Join(", ", sf.Select(f => f.Titre))}");

// 2. Where + Select : filtrer PUIS transformer
var topTitres = films.Where(f => f.Note >= 4.5).Select(f => f.Titre.ToUpper());
Console.WriteLine($"2. Notés ≥ 4.5 : {string.Join(", ", topTitres)}");

// 3. Années 2000, du plus récent au plus ancien
var annees2000 = films
    .Where(f => f.Annee >= 2000 && f.Annee < 2010)
    .OrderByDescending(f => f.Annee);
Console.WriteLine("3. Années 2000 :");
foreach (var f in annees2000)
    Console.WriteLine($"   {f.Annee} — {f.Titre}");

// 4. Top 3 par note
var podium = films.OrderByDescending(f => f.Note).Take(3);
Console.WriteLine($"4. Podium : {string.Join(" ; ", podium.Select(f => $"{f.Titre} ({f.Note})"))}");

// ---------------------------------------------------------------
// Partie B — Agréger
// ---------------------------------------------------------------
Console.WriteLine($"\n5. Total : {films.Count} films, dont Nolan : {films.Count(f => f.Realisateur == "Christopher Nolan")}");
Console.WriteLine($"6. Avant 1980 ? {films.Any(f => f.Annee < 1980)} | Noté 5 ? {films.Any(f => f.Note == 5)}");

// First : plante si aucun résultat ; FirstOrDefault : renvoie null (référence)
var premierAnime = films.First(f => f.Genre == "Animation");
var western = films.FirstOrDefault(f => f.Genre == "Western");
Console.WriteLine($"7. Premier animation : {premierAnime.Titre} | Western : {(western is null ? "aucun (FirstOrDefault → null, pas de crash)" : western.Titre)}");

Console.WriteLine($"8. Durée moyenne : {films.Average(f => f.Duree):F0} min | " +
                  $"Total : {films.Sum(f => f.Duree) / 60.0:F1} h | " +
                  $"Meilleure note : {films.Max(f => f.Note)}");

// ---------------------------------------------------------------
// Partie C — Grouper
// ---------------------------------------------------------------
Console.WriteLine("\n9. Films par genre :");
foreach (var groupe in films.GroupBy(f => f.Genre))
    Console.WriteLine($"   {groupe.Key,-10} : {groupe.Count()}");

Console.WriteLine("10. Réalisateurs par note moyenne :");
var classement = films
    .GroupBy(f => f.Realisateur)
    .Select(g => new { Nom = g.Key, Nombre = g.Count(), NoteMoyenne = g.Average(f => f.Note) })
    .OrderByDescending(x => x.NoteMoyenne);
foreach (var real in classement)
    Console.WriteLine($"   {real.Nom,-20} {real.Nombre} film(s), moyenne {real.NoteMoyenne:F2}");

// ---------------------------------------------------------------
// Partie D — Exécution différée
// ---------------------------------------------------------------
Console.WriteLine("\n--- Exécution différée ---");

// 11. La requête est une RECETTE, pas un résultat
var recents = films.Where(f => f.Annee > 2000);

films.Add(new("Oppenheimer", "Christopher Nolan", 2023, "Biopic", 4.4, 180));

// L'énumération se fait ICI → Oppenheimer, ajouté APRÈS, est vu !
Console.WriteLine($"11. Différé : {recents.Count()} films récents (Oppenheimer inclus : " +
                  $"{recents.Any(f => f.Titre == "Oppenheimer")})");

// 12. ToList exécute IMMÉDIATEMENT et fige le résultat
var figes = films.Where(f => f.Annee > 2000).ToList();
films.Add(new("Dune 2", "Denis Villeneuve", 2024, "SF", 4.3, 166));
Console.WriteLine($"12. Figé : {figes.Count} films (Dune 2 inclus : {figes.Any(f => f.Titre == "Dune 2")})");

// 13. Règle : une requête LINQ ne s'exécute qu'au moment où on l'énumère
//     (foreach, Count, ToList…) — et se ré-exécute à CHAQUE énumération.

// ---------------------------------------------------------------
// Bonus
// ---------------------------------------------------------------
var chaine = films
    .Where(f => f.Genre == "SF" && f.Note >= 4.3)
    .OrderBy(f => f.Annee)
    .Select(f => f.Titre)
    .ToList();
Console.WriteLine($"\nBonus chaîne : {string.Join(" → ", chaine)}");

// SelectMany : aplatit une collection de collections
var mots = films.SelectMany(f => f.Titre.Split(' ')).ToList();
Console.WriteLine($"Bonus SelectMany : {mots.Count} mots dans les titres");

// Syntaxe de requête : équivalent exact de la requête 3
var requete3 = from f in films
               where f.Annee >= 2000 && f.Annee < 2010
               orderby f.Annee descending
               select f.Titre;
Console.WriteLine($"Bonus syntaxe requête : {string.Join(", ", requete3)}");

// =====================================================================
record Film(string Titre, string Realisateur, int Annee, string Genre, double Note, int Duree);
