// =====================================================================
// Solution — TP Module 10 (version projet multi-fichiers)
// Le record Film vit dans Film.cs ; Program.cs = les requêtes.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using TpMediatheque;

Console.WriteLine("=== TP Module 10 (projet) ===\n");

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

// --- Partie A — Filtrer et projeter ---
var sf = films.Where(f => f.Genre == "SF");
Console.WriteLine($"1. SF : {string.Join(", ", sf.Select(f => f.Titre))}");

var topTitres = films.Where(f => f.Note >= 4.5).Select(f => f.Titre.ToUpper());
Console.WriteLine($"2. Notés ≥ 4.5 : {string.Join(", ", topTitres)}");

var annees2000 = films
    .Where(f => f.Annee >= 2000 && f.Annee < 2010)
    .OrderByDescending(f => f.Annee);
Console.WriteLine($"3. Années 2000 : {string.Join(" ; ", annees2000.Select(f => $"{f.Annee} {f.Titre}"))}");

var podium = films.OrderByDescending(f => f.Note).Take(3);
Console.WriteLine($"4. Podium : {string.Join(" ; ", podium.Select(f => $"{f.Titre} ({f.Note})"))}");

// --- Partie B — Agréger ---
Console.WriteLine($"\n5. Total : {films.Count}, dont Nolan : {films.Count(f => f.Realisateur == "Christopher Nolan")}");
Console.WriteLine($"6. Avant 1980 ? {films.Any(f => f.Annee < 1980)} | Noté 5 ? {films.Any(f => f.Note == 5)}");

var western = films.FirstOrDefault(f => f.Genre == "Western");
Console.WriteLine($"7. Western : {(western is null ? "aucun (FirstOrDefault → null)" : western.Titre)}");
Console.WriteLine($"8. Durée moyenne : {films.Average(f => f.Duree):F0} min | Note max : {films.Max(f => f.Note)}");

// --- Partie C — Grouper ---
Console.WriteLine("\n9-10. Réalisateurs par note moyenne :");
var classement = films
    .GroupBy(f => f.Realisateur)
    .Select(g => new { Nom = g.Key, Nombre = g.Count(), NoteMoyenne = g.Average(f => f.Note) })
    .OrderByDescending(x => x.NoteMoyenne);
foreach (var real in classement)
    Console.WriteLine($"   {real.Nom,-20} {real.Nombre} film(s), moyenne {real.NoteMoyenne:F2}");

// --- Partie D — Exécution différée ---
var recents = films.Where(f => f.Annee > 2000);
films.Add(new("Oppenheimer", "Christopher Nolan", 2023, "Biopic", 4.4, 180));
Console.WriteLine($"\n11. Différé : Oppenheimer vu par la requête ? {recents.Any(f => f.Titre == "Oppenheimer")}");

var figes = films.Where(f => f.Annee > 2000).ToList();
films.Add(new("Dune 2", "Denis Villeneuve", 2024, "SF", 4.3, 166));
Console.WriteLine($"12. Figé (ToList) : Dune 2 inclus ? {figes.Any(f => f.Titre == "Dune 2")}");
Console.WriteLine("13. Règle : la requête ne s'exécute qu'à l'énumération, et à chaque énumération.");
