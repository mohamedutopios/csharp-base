# TP Module 10 — LINQ : la médiathèque en chiffres

**Durée :** 1 h — **Prérequis :** modules 9 et 10

## Objectif

Interroger une collection avec LINQ : filtrer, projeter, trier, grouper,
agréger — et comprendre l'exécution différée.

## Jeu de données (fourni)

```csharp
record Film(string Titre, string Realisateur, int Annee, string Genre, double Note, int Duree);

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
```

## Énoncé

### Partie A — Filtrer et projeter

1. Les films de SF (`Where`) : afficher leurs titres.
2. Les titres en MAJUSCULES des films notés ≥ 4.5 (`Where` + `Select`).
3. Les films des années 2000 (2000 ≤ année < 2010), triés du plus récent
   au plus ancien (`OrderByDescending`).
4. Les 3 films les mieux notés (`OrderByDescending` + `Take`), affichés
   `"Titre (note)"`.

### Partie B — Agréger

5. Nombre de films par `Count` : total, et nombre de films de Nolan.
6. `Any` : y a-t-il un film d'avant 1980 ? Un film noté 5 ?
7. `First` vs `FirstOrDefault` : le premier film d'animation ; puis chercher
   un film de genre `"Western"` **sans crash**.
8. Durée moyenne (`Average`), durée totale en heures (`Sum`), note maximale (`Max`).

### Partie C — Grouper

9. `GroupBy(Genre)` : afficher chaque genre avec son nombre de films.
10. Par réalisateur : nombre de films **et** note moyenne, trié par note
    moyenne décroissante. Qui arrive en tête ?

### Partie D — Exécution différée

11. Construire la requête `var recents = films.Where(f => f.Annee > 2000);`
    **puis** ajouter un film de 2023 à la liste, **puis** énumérer `recents` :
    le film ajouté apparaît-il ? Pourquoi ?
12. Refaire la même chose avec `.ToList()` au moment de la construction :
    qu'est-ce qui change ?
13. Formuler la règle en une phrase.

## Bonus

- La chaîne complète : les titres des films de SF notés ≥ 4.3, triés par année,
  en une seule instruction chaînée terminée par `ToList`.
- `SelectMany` : la liste de tous les mots de tous les titres.
- Réécrire la requête 3 en **syntaxe de requête** (`from f in films where … select f`).

> **Organisation en projet** : un type = un fichier (records, enums et structs
> compris). Le résultat attendu est dans `solution-projet/`
> (`dotnet run --project solution-projet`).
