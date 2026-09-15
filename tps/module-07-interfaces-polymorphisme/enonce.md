# TP Module 7 — Interfaces et polymorphisme : formes et exports

**Durée :** 1 h — **Prérequis :** modules 6 et 7

## Objectif

Programmer contre des contrats (interfaces), combiner plusieurs interfaces,
et aiguiller par type avec `is`/`as` et le pattern matching.

## Énoncé

### Partie A — Le contrat IForme

1. Déclarer une interface `IForme` avec deux méthodes :
   `double Aire();` et `double Perimetre();`.
2. L'implémenter dans trois classes **sans lien d'héritage entre elles** :
   - `Cercle(double rayon)`,
   - `Rectangle(double largeur, double hauteur)`,
   - `TriangleRectangle(double base_, double hauteur)`
     (aire = base × hauteur / 2 ; périmètre avec l'hypoténuse `Math.Sqrt`).
3. Dans le programme : une `List<IForme>` avec les trois formes ; pour chacune,
   afficher aire et périmètre **via le type interface**.
4. Question : pourquoi une interface plutôt qu'une classe de base abstraite ici ?

### Partie B — Une deuxième interface : IExportable

5. Déclarer `IExportable` avec `string ExporterCsv();`.
6. `Cercle` et `Rectangle` l'implémentent (`"cercle;3.0"` , `"rectangle;4.0;3.0"`),
   mais **pas** `TriangleRectangle` : une classe choisit ses contrats.
7. Parcourir la `List<IForme>` et, pour chaque forme **qui est aussi**
   `IExportable` (test `is`), afficher son CSV. Le triangle doit être sauté
   sans erreur.

### Partie C — is, as et pattern matching

8. Écrire `Decrire(object objet)` → `string` avec une **switch expression** :
   - `Cercle c` → `"cercle de rayon 3"`,
   - `Rectangle r when r.Largeur == r.Hauteur` → `"carré de côté 4"` (pattern + `when`),
   - `Rectangle r` → `"rectangle 4x3"`,
   - `IForme` → `"autre forme d'aire ..."`,
   - `null` → `"rien du tout"`,
   - défaut → `"objet inconnu : {type}"`.
9. La tester avec : un cercle, un rectangle 4×3, un rectangle 4×4, un triangle,
   la chaîne `"bonjour"` et `null`.
10. Montrer la différence entre `(Cercle)objet` (crash possible) et
    `objet as Cercle` (renvoie `null`) sur un objet qui n'est **pas** un cercle.

## Bonus

- Ajouter à `IForme` une **implémentation par défaut** :
  `string Resume() => $"aire {Aire():F2}, périmètre {Perimetre():F2}";`
  et constater que les trois classes en profitent sans rien écrire.
- Trier la liste par aire croissante (`List.Sort` + `CompareTo`… ou patienter
  jusqu'au module 10 et LINQ `OrderBy`).

> **Organisation en projet** : un fichier par contrat et par classe
> (`IForme.cs`, `IExportable.cs`, `Cercle.cs`, `Rectangle.cs`,
> `TriangleRectangle.cs`), le scénario et `Decrire` dans `Program.cs`.
> Le résultat attendu est dans `solution-projet/` (`dotnet run --project solution-projet`).
