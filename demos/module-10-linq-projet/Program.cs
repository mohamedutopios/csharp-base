// =====================================================================
// Module 10 — Démo (version projet multi-fichiers)
// Le record Film vit dans Film.cs ; Program.cs = les requêtes.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using DemoLinq;

Console.WriteLine("=== Démo Module 10 : LINQ (projet) ===\n");

var films = new List<Film>
{
    new("Inception", "Christopher Nolan", 2010, "SF", 4.5),
    new("Interstellar", "Christopher Nolan", 2014, "SF", 4.6),
    new("Le Voyage de Chihiro", "Hayao Miyazaki", 2001, "Animation", 4.7),
    new("Pulp Fiction", "Quentin Tarantino", 1994, "Policier", 4.5),
    new("Alien", "Ridley Scott", 1979, "SF", 4.3),
};

// Where : filtrer | Select : transformer | OrderBy : trier
var titresSf = films
    .Where(f => f.Genre == "SF")
    .OrderBy(f => f.Annee)
    .Select(f => $"{f.Titre} ({f.Annee})")
    .ToList();
Console.WriteLine($"SF par année : {string.Join(" ; ", titresSf)}");

// Agrégats
Console.WriteLine($"Count(Nolan) : {films.Count(f => f.Realisateur == "Christopher Nolan")}");
Console.WriteLine($"Any(av. 1980) : {films.Any(f => f.Annee < 1980)} | Note max : {films.Max(f => f.Note)}");

// GroupBy
Console.WriteLine("\nPar réalisateur :");
foreach (var groupe in films.GroupBy(f => f.Realisateur))
    Console.WriteLine($"  {groupe.Key,-20} → {groupe.Count()} film(s), moyenne {groupe.Average(f => f.Note):F2}");

// Exécution différée : la requête est une recette, pas un résultat
var recents = films.Where(f => f.Annee > 2000);
films.Add(new("Oppenheimer", "Christopher Nolan", 2023, "Biopic", 4.4));
Console.WriteLine($"\nDifféré : la requête voit le film ajouté après coup → {recents.Count()} récents");

var figes = films.Where(f => f.Annee > 2000).ToList();     // ToList = photo immédiate
films.Add(new("Dune 2", "Denis Villeneuve", 2024, "SF", 4.3));
Console.WriteLine($"ToList figé avant l'ajout → {figes.Count} récents");

Console.WriteLine("\n→ À retenir : Where filtre, Select transforme, la requête est " +
                  "paresseuse ; le modèle (Film) a son fichier.");
