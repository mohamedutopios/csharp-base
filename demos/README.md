# Démos par module — Les bases du C# (4 jours)

Une démo exécutable par module du programme, sous forme de **fichiers C# autonomes**
(« file-based apps », .NET 10) : pas de `.csproj`, pas de solution — un fichier = une démo.

> Une solution `CSharpBase.slnx` existe **à la racine de `csharp-base/`** : elle
> regroupe les 16 versions projet (démos + TP, modules 5 à 12). Ouvrir ce dossier
> racine dans VS Code / Rider donne accès à tout, et `dotnet build` à la racine
> compile l'ensemble d'un coup. Les démos mono-fichier de ce dossier n'en font
> pas partie : une file-based app ne s'inscrit pas dans une solution.

## Exécution

```bash
cd demos
dotnet run module-01-ecosysteme.cs
```

> La première exécution d'un fichier est un peu lente (compilation), les suivantes sont instantanées (cache).

## Comment ces démos ont été créées (il n'y a RIEN à créer)

C'est le point qui surprend : pour une démo mono-fichier, **aucune commande de
création de projet n'a été exécutée** — ni `dotnet new console`, ni solution.
Une *file-based app* (.NET 10), c'est :

1. créer un fichier `.cs` dans n'importe quel dossier (éditeur au choix) ;
2. `dotnet run monfichier.cs`.

C'est tout. Le SDK génère un projet **en mémoire** au moment du `run` (aucun
`.csproj`, aucun `bin/` ni `obj/` dans le dossier — le cache de compilation va
dans un dossier temporaire du système). Pour créer une nouvelle démo :

```bash
cd demos
```

```bash
touch module-13-ma-demo.cs   # ou créer le fichier depuis VS Code
```

```bash
dotnet run module-13-ma-demo.cs
```

Quand le fichier a besoin de configuration, elle se déclare **dans le fichier
lui-même** avec les directives `#:` en tête (l'équivalent du `.csproj`) :

- `#:property PublishAot=false` — utilisé par `module-12` pour réactiver la
  sérialisation JSON par réflexion ;
- `#:package Nom@Version` — référencer un paquet NuGet.

**À ne pas confondre** avec les deux autres contextes de création, qui eux
passent par `dotnet new` :

| Contexte | Où c'est défini | Commandes |
|---|---|---|
| Démos mono-fichier (ce dossier) | ci-dessus | aucune — un fichier + `dotnet run fichier.cs` |
| Versions projet des démos et TP | section suivante + `tps/README.md` | `dotnet new console -n Nom -o dossier`, `dotnet new gitignore` |
| Projets des apprenants (Jour1…Jour4, fil rouge) | **étape zéro** de `programme-csharp-bases-4j-v1.0.md` | `dotnet new sln`, `dotnet new console`, `dotnet sln add`, `dotnet new gitignore` |

## Versions projet multi-fichiers (modules 5 à 12)

Toutes les démos qui définissent des types (classes, mais aussi interfaces,
records, enums, structs, exceptions) existent aussi en **vrai projet console**,
avec la structure professionnelle *un type = un fichier* — à utiliser en séance
pour montrer comment on organise réellement le code :

| Dossier | Fichiers |
|---|---|
| `module-05-classes-objets-projet/` | `Program.cs`, `Livre.cs`, `Membre.cs` |
| `module-06-encapsulation-heritage-projet/` | `Program.cs`, `Animal.cs`, `Chien.cs`, `Chat.cs`, `CompteBancaire.cs`, `Point.cs` |
| `module-07-interfaces-polymorphisme-projet/` | `Program.cs`, `INotifiable.cs`, `Email.cs`, `Sms.cs`, `Drone.cs` |
| `module-08-types-specialises-projet/` | `Program.cs`, `Livre.cs` (record), `EtatEmprunt.cs` (enum), `PointStruct.cs`, `PointClass.cs` |
| `module-09-collections-generiques-projet/` | `Program.cs`, `Boite.cs` (classe générique) |
| `module-10-linq-projet/` | `Program.cs`, `Film.cs` (record) |
| `module-11-exceptions-projet/` | `Program.cs`, `LivreIndisponibleException.cs`, `Bibliotheque.cs`, `RessourceDemo.cs` |
| `module-12-nullabilite-fichiers-projet/` | `Program.cs`, `Livre.cs` — sans la directive `#:property` (inutile en projet) |

```bash
dotnet run --project module-05-classes-objets-projet
```

Points à faire passer en démo : le compilateur inclut automatiquement **tous
les `.cs` du dossier du projet** (aucun « import de fichier ») ; un seul fichier
(`Program.cs`) porte les top-level statements ; même `namespace` partout et
classes `public`. Le fichier mono-fichier reste pratique pour dérouler la
théorie ; la version projet montre l'organisation cible (celle du fil rouge).

### Comment ces projets ont été créés (à refaire en live)

Les commandes exactes, **projet par projet**, exécutées depuis `demos/`
(`-n` = nom du projet et du namespace, `-o` = dossier de destination) :

```bash
dotnet new console -n DemoClasses     -o module-05-classes-objets-projet
dotnet new console -n DemoHeritage    -o module-06-encapsulation-heritage-projet
dotnet new console -n DemoInterfaces  -o module-07-interfaces-polymorphisme-projet
dotnet new console -n DemoTypes       -o module-08-types-specialises-projet
dotnet new console -n DemoCollections -o module-09-collections-generiques-projet
dotnet new console -n DemoLinq        -o module-10-linq-projet
dotnet new console -n DemoExceptions  -o module-11-exceptions-projet
dotnet new console -n DemoFichiers    -o module-12-nullabilite-fichiers-projet
```

Puis le `.gitignore` .NET dans chaque projet — soit dossier par dossier
(`cd module-05-classes-objets-projet && dotnet new gitignore`), soit en une
fois pour les huit :

```bash
for d in module-*-projet; do (cd "$d" && dotnet new gitignore); done
```

Le nom passé à `-n` est aussi le `namespace` à mettre en tête de chaque
fichier du projet (`namespace DemoClasses;`, `namespace DemoHeritage;`, etc.).

**Anatomie des commandes** :

| Élément | Signification |
|---|---|
| `dotnet new console` | génère un projet à partir du **modèle** `console` (lister les modèles : `dotnet new list`) |
| `-n DemoClasses` | **name** : le nom du projet → `DemoClasses.csproj`, namespace par défaut, nom de l'exécutable |
| `-o module-05-...-projet` | **output** : le dossier où créer les fichiers (créé s'il n'existe pas). Sans `-o`, la CLI crée un dossier portant le nom donné à `-n` |
| `dotnet new gitignore` | même mécanique : le modèle `gitignore` génère un `.gitignore` .NET dans le dossier courant |
| `dotnet run` | compile **le projet du dossier courant** puis l'exécute |
| `dotnet run --project chemin/` | désigne le dossier du projet quand on n'est **pas** dedans |
| `dotnet run fichier.cs` | exécute un **fichier autonome** (file-based app), sans projet — le mode des démos mono-fichier |
| `dotnet project convert fichier.cs` | transforme un fichier autonome en projet (dossier + `.csproj` + conversion des directives `#:`) |

Une fois le projet créé : un fichier par type (`Livre.cs`, `Membre.cs`…),
les top-level statements dans `Program.cs` seulement, puis `dotnet run`
depuis le dossier du projet (ou `dotnet run --project <dossier>` depuis `demos/`).

Autre chemin possible en live : partir de la démo mono-fichier et laisser la
CLI générer le projet, puis éclater les types en fichiers :

```bash
dotnet project convert module-05-classes-objets.cs
```

## Sommaire

| Jour | Fichier | Module | Contenu démontré |
|---|---|---|---|
| J1 | `module-01-ecosysteme.cs` | 1. Écosystème .NET | Runtime, CLR, top-level statements, infos d'environnement |
| J1 | `module-02-types-variables.cs` | 2. Types et variables | Types valeur/référence, `var`, conversions, `Parse`/`TryParse`, chaînes, `StringBuilder` |
| J1 | `module-03-operateurs-controle.cs` | 3. Opérateurs et contrôle | `??`, `?.`, ternaire, `if`/`switch` (instruction + expression), boucles, `break`/`continue`, squelette de menu |
| J2 | `module-04-methodes.cs` | 4. Méthodes | Signature, `ref`/`out`/`params`, optionnels/nommés, surcharge, expression-bodied, récursivité |
| J2 | `module-05-classes-objets.cs` | 5. Classes et objets | Champs, propriétés (`init`, `required`), constructeurs, `this`, `static`, object initializer |
| J2 | `module-06-encapsulation-heritage.cs` | 6. Encapsulation et héritage | Modificateurs d'accès, `base`, `virtual`/`override`/`sealed`, `abstract`, `ToString`/`Equals` |
| J3 | `module-07-interfaces-polymorphisme.cs` | 7. Interfaces et polymorphisme | Contrats, implémentation multiple, `is`/`as`, pattern matching sur types |
| J3 | `module-08-types-specialises.cs` | 8. Types spécialisés | `struct` vs `class`, `record` (+ `with`), `enum`, tuples et déconstruction |
| J3 | `module-09-collections-generiques.cs` | 9. Collections et génériques | Tableaux, `List`, `Dictionary`, `HashSet`, `Queue`, `Stack`, `IEnumerable`, génériques + `where` |
| J4 | `module-10-linq.cs` | 10. LINQ | Lambdas, `Where`/`Select`/`OrderBy`/`GroupBy`/`Count`/`Any`/`First`, exécution différée, `ToList` |
| J4 | `module-11-exceptions.cs` | 11. Exceptions | `try`/`catch`/`finally`, exception personnalisée (`LivreIndisponibleException`), `throw`, `using`/`IDisposable` |
| J4 | `module-12-nullabilite-fichiers.cs` | 12. Nullabilité et fichiers | Types référence nullables, `?`/`!`/`??=`, `File`/`Path`, `System.Text.Json` |

## Conseils d'animation

- **Dérouler section par section** : chaque fichier est découpé en blocs numérotés qui suivent
  l'ordre du programme ; commenter/décommenter les lignes marquées « décommenter » pour
  provoquer les erreurs de compilation ou les crashs en live (c'est le moment le plus pédagogique).
- **Fil rouge** : les démos 5, 8, 10, 11 et 12 réutilisent volontairement `Livre` / `Bibliotheque`
  pour préparer les TP fil rouge (`LivreIndisponibleException` du module 11 est exactement
  celle demandée au TP 4).
- **Module 1** : avant de lancer le fichier, montrer dans le terminal `dotnet --info`,
  `dotnet new console`, `dotnet build`, `dotnet run` (voir l'en-tête du fichier).
- Chaque démo se termine par une ligne « → À retenir » qui peut servir de synthèse au tableau.
