# Les bases du C# — Programme 4 jours (28 h)

**Version :** 1.1
**Durée :** 4 jours × 7 h (9h00–12h30 / 13h30–17h00)
**Modalité :** présentiel ou distanciel, 50 % théorie / 50 % pratique
**Stack :** .NET 10 (LTS) — C# 14 — VS Code + extension C# Dev Kit (ou Visual Studio 2022 / Rider)

---

## Public visé

Développeurs débutants ou issus d'un autre langage (Java, Python, JavaScript), étudiants M1/M2, reconvertis.

## Prérequis

- Notions d'algorithmique (variables, boucles, conditions)
- Aisance avec un terminal et un éditeur de texte
- Aucune connaissance préalable de C# ou .NET requise

## Objectifs pédagogiques

À l'issue de la formation, l'apprenant est capable de :

1. Installer et utiliser l'environnement .NET (SDK, CLI, IDE)
2. Écrire des programmes C# structurés en maîtrisant types, opérateurs et structures de contrôle
3. Concevoir des classes, interfaces et hiérarchies d'objets selon les principes de la POO
4. Manipuler des collections avec les génériques et LINQ
5. Gérer les erreurs, les fichiers et la nullabilité
6. Livrer une application console complète et structurée

---

## Étape zéro — Mise en place de l'environnement (J1, 9h00–10h00)

Réalisée en séance, machine par machine, avant tout contenu.

| Étape | Commande / action | Vérification |
|---|---|---|
| Installer le SDK .NET 10 | https://dotnet.microsoft.com/download (Windows/macOS/Linux) | `dotnet --version` → `10.x.x` |
| Installer VS Code + C# Dev Kit | Extensions → « C# Dev Kit » | Coloration et IntelliSense actifs sur un `.cs` |
| Créer le dossier de travail | `mkdir formation-csharp && cd formation-csharp` | — |
| Créer une solution | `dotnet new sln -n FormationCSharp` | Fichier `.sln` présent |
| Créer un premier projet console | `dotnet new console -n Jour1 -o src/Jour1` | Dossier `src/Jour1` |
| Rattacher le projet à la solution | `dotnet sln add src/Jour1/Jour1.csproj` | `dotnet sln list` |
| Exécuter | `dotnet run --project src/Jour1` | `Hello, World!` affiché |
| Initialiser Git | `git init && dotnet new gitignore && git add . && git commit -m "init"` | `git log` |

Chaque jour, un nouveau projet est créé sur le même modèle (`dotnet new console -n JourN -o src/JourN` + `dotnet sln add`). Le projet fil rouge est créé en J2.

---

## Jour 1 — Prise en main et syntaxe de base

| Horaire | Module | Contenu | Format |
|---|---|---|---|
| 9h00–10h00 | 0. Environnement | Étape zéro (ci-dessus) | Atelier |
| 10h00–11h00 | 1. Écosystème .NET | Historique, .NET vs .NET Framework, CLR, IL, JIT, NuGet, structure d'un projet (`.csproj`, `Program.cs`, top-level statements), CLI `dotnet` | Théorie + démo |
| 11h00–12h30 | 2. Types et variables | Types valeur / référence, types primitifs, `var`, constantes, conversions implicites/explicites, `Parse`/`TryParse` | Théorie + exercices |
| 13h30–14h30 | 2. Types et variables (suite) | Chaînes, interpolation, méthodes usuelles de `string`, `StringBuilder`, `Console.ReadLine`/`WriteLine` | Théorie + exercices |
| 14h30–16h00 | 3. Opérateurs et structures de contrôle | Opérateurs arithmétiques/logiques/`??`/`?.`/ternaire, `if`/`switch` (instruction et expression), boucles `for`/`foreach`/`while`/`do`, `break`/`continue` | Théorie + exercices |
| 16h00–17h00 | TP 1 | Convertisseur d'unités console : lecture d'entrée, parsing sécurisé, menu en boucle avec `switch` | Atelier |

**Livrable J1 :** projet `Jour1` committé, TP 1 fonctionnel.

---

## Jour 2 — Méthodes et introduction à la POO

| Horaire | Module | Contenu | Format |
|---|---|---|---|
| 9h00–9h15 | Rappel J1 | Quiz 10 questions | Quiz |
| 9h15–10h45 | 4. Méthodes | Signature, paramètres (`ref`, `out`, `params`, optionnels, nommés), valeur de retour, surcharge, expressions-bodied, récursivité | Théorie + exercices |
| 10h45–12h30 | 5. Classes et objets | Classe, instance, champs, propriétés (auto, `init`, `required`), constructeurs, `this`, membres statiques, `object initializer` | Théorie + exercices |
| 13h30–15h00 | 6. Encapsulation et héritage | Modificateurs d'accès, `base`, `virtual`/`override`/`sealed`, classes abstraites, `ToString`/`Equals` | Théorie + exercices |
| 15h00–17h00 | TP 2 — Fil rouge (étape 1) | Création du projet `Bibliotheque` : classes `Livre`, `Membre`, `Emprunt`, classe `Bibliotheque` avec tableau interne, menu console (ajouter, lister, emprunter, rendre) | Atelier |

**Livrable J2 :** projet `Bibliotheque` avec modèle objet et menu fonctionnel.

---

## Jour 3 — POO avancée et collections

| Horaire | Module | Contenu | Format |
|---|---|---|---|
| 9h00–9h15 | Rappel J2 | Quiz 10 questions | Quiz |
| 9h15–10h45 | 7. Interfaces et polymorphisme | Déclaration et implémentation, polymorphisme par interface vs héritage, `is`/`as`, pattern matching sur types | Théorie + exercices |
| 10h45–12h00 | 8. Types spécialisés | `struct` vs `class`, `record`, `enum`, tuples et déconstruction | Théorie + exercices |
| 12h00–12h30 | 9. Collections et génériques | Tableaux vs `List<T>`, `Dictionary<K,V>`, `HashSet<T>` | Théorie + démo |
| 13h30–15h00 | 9. Collections et génériques (suite) | `Queue<T>`, `Stack<T>`, `IEnumerable<T>` / `IList<T>`, classes et méthodes génériques, contraintes `where` | Théorie + exercices |
| 15h00–17h00 | TP 3 — Fil rouge (étape 2) | Interface `IBibliotheque`, remplacement du tableau par `List<T>` et `Dictionary<K,V>`, `record` pour `Livre`, `enum` pour l'état d'un emprunt | Atelier |

**Livrable J3 :** fil rouge refactoré sur interfaces et collections génériques.

---

## Jour 4 — LINQ, erreurs, fichiers et projet final

| Horaire | Module | Contenu | Format |
|---|---|---|---|
| 9h00–9h15 | Rappel J3 | Quiz 10 questions | Quiz |
| 9h15–10h45 | 10. LINQ | Lambdas `Func`/`Action`, syntaxe méthode, `Where`/`Select`/`OrderBy`/`GroupBy`/`Count`/`Any`/`First`, exécution différée, `ToList` | Théorie + exercices |
| 10h45–11h45 | 11. Gestion des exceptions | `try`/`catch`/`finally`, exceptions personnalisées, `throw`, `using` et `IDisposable` | Théorie + exercices |
| 11h45–12h30 | 12. Nullabilité et fichiers | Nullable reference types, `?`, `!`, `??=`, `System.IO` (`File`, `Path`), `System.Text.Json` | Théorie + démo |
| 13h30–15h45 | TP 4 — Fil rouge (étape 3, évaluation) | Recherches et statistiques LINQ (livres disponibles, top emprunteurs, retards), sauvegarde/chargement JSON, exception `LivreIndisponibleException` | Atelier évalué |
| 15h45–16h45 | Restitution | Démonstration individuelle (5 min/personne) | Présentation |
| 16h45–17h00 | Clôture | Bilan, ressources pour continuer (tests xUnit, async, ASP.NET Core), évaluation à chaud | — |

**Livrable J4 :** dépôt Git complet avec fil rouge fonctionnel et persistance JSON.

---

## Modalités d'évaluation

| Type | Moment | Pondération |
|---|---|---|
| Quiz de rappel (3 × 10 questions) | J2, J3, J4 | 30 % |
| Projet fil rouge (fonctionnel, structuré) | J4 | 50 % |
| Restitution orale | J4 | 20 % |

Seuil de validation : 60 %.

## Grille d'évaluation du fil rouge

| Critère | Points |
|---|---|
| Compilation sans warning, exécution sans crash | 4 |
| Modèle objet cohérent (encapsulation, interface, record, enum) | 5 |
| Utilisation pertinente des collections génériques | 3 |
| Requêtes LINQ correctes | 3 |
| Gestion des exceptions et de la nullabilité | 3 |
| Persistance JSON fonctionnelle | 2 |
| **Total** | **20** |

---

## Structure du dépôt attendu en fin de formation

```
formation-csharp/
├── FormationCSharp.sln
├── .gitignore
└── src/
    ├── Jour1/            # TP 1 — convertisseur d'unités
    ├── Jour2/            # exercices méthodes / classes
    ├── Jour3/            # exercices interfaces / collections
    ├── Jour4/            # exercices LINQ / exceptions
    └── Bibliotheque/     # fil rouge
```

## Ressources

- Documentation officielle C# : https://learn.microsoft.com/dotnet/csharp/
- Parcours « C# for beginners » : https://learn.microsoft.com/training/paths/csharp-first-steps/
- Référence .NET CLI : https://learn.microsoft.com/dotnet/core/tools/
