# TP Module 1 — Écosystème .NET : ma première solution

**Durée :** 30 min — **Prérequis :** étape zéro terminée (`dotnet --version` fonctionne)

## Objectif

Manipuler la CLI `dotnet` pour créer, organiser et exécuter des projets, sans IDE.
Tout se fait **au terminal**.

## Énoncé

### Partie A — Créer la structure

1. Créer un dossier `tp-ecosysteme` et s'y placer.
2. Y créer une solution nommée `TpEcosysteme`.
3. Créer un projet console `Application` dans `src/Application`.
4. Créer un second projet console `Outils` dans `src/Outils`.
5. Rattacher les **deux** projets à la solution.
6. Vérifier avec `dotnet sln list` : les deux projets doivent apparaître.

### Partie B — Explorer et exécuter

7. Ouvrir `src/Application/Application.csproj` : identifier le `TargetFramework`.
8. Exécuter le projet `Application` **depuis la racine** (sans `cd`).
9. Compiler toute la solution avec `dotnet build` : où sont les fichiers produits ?
   Trouver le fichier `.dll` généré pour `Application`.

### Partie C — Personnaliser

10. Modifier `src/Application/Program.cs` pour afficher :
    - une ligne de bienvenue,
    - la version du runtime (`Environment.Version`),
    - le nom de la machine (`Environment.MachineName`).
11. Relancer et vérifier l'affichage.

### Partie D — Git

12. Initialiser un dépôt Git, générer le `.gitignore` .NET avec la CLI,
    puis faire un premier commit.
13. Vérifier avec `git status` que `bin/` et `obj/` sont bien **ignorés**.

## Résultat attendu (exemple)

```
Bienvenue dans TpEcosysteme !
Runtime : 10.0.x
Machine : MacBook-de-Sam
```

## Questions de compréhension (à l'oral)

- Quelle est la différence entre `dotnet build` et `dotnet run` ?
- Que contient le fichier `.dll` produit par la compilation ? (indice : IL)
- Pourquoi ignorer `bin/` et `obj/` dans Git ?
