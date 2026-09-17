# TP par module — Les bases du C# (4 jours)

Un TP par module du programme, chacun dans son dossier avec :

- `enonce.md` — l'énoncé à distribuer aux apprenants (parties progressives A → D + bonus) ;
- `solution.cs` — la solution complète, commentée, **exécutable telle quelle**
  (file-based app .NET 10 : `dotnet run solution.cs`). Pour le module 1, le code
  exécutable est celui de la Partie C ; les commandes CLI des parties A, B et D
  sont en commentaires dans le fichier.

Ces TP **complètent** les 4 grands TP du programme (convertisseur d'unités et fil
rouge Bibliothèque) : ce sont les exercices de mise en pratique de chaque créneau
« Théorie + exercices ».

## Exécution d'une solution

```bash
cd tps/module-04-methodes
dotnet run solution.cs
```

Les solutions des modules 2 et 3 sont interactives (`Console.ReadLine`) : les
lancer dans un vrai terminal.

## Mono-fichier vs projet multi-fichiers

Les `solution.cs` tiennent en un fichier pour être projetables et exécutables
d'un seul `dotnet run solution.cs`. **En séance, dès le module 5, la règle
devient : une classe = un fichier**, dans le projet console de l'apprenant :

- le compilateur inclut automatiquement **tous les `.cs` du dossier du projet**,
  il n'y a aucun « import de fichier » à écrire ;
- un seul fichier (`Program.cs`) porte les top-level statements : c'est le point
  d'entrée ; les autres fichiers ne contiennent que des types ;
- `dotnet run` compile l'ensemble et exécute.

Les TP 5 à 12 contiennent chacun un dossier `solution-projet/` : la même
solution éclatée en un fichier par type (+ `.csproj`), à lancer avec
`dotnet run --project solution-projet` :

- TP 5 : `Program.cs` + `CompteBancaire.cs` + `Banque.cs` — et l'énoncé a une
  partie E qui fait faire cette réorganisation aux apprenants ;
- TP 6 : un fichier par classe de la hiérarchie (`Employe.cs`, `Developpeur.cs`,
  `Manager.cs`, `Stagiaire.cs`, `Badge.cs`) ;
- TP 7 : un fichier par contrat et par implémentation (`IForme.cs`,
  `IExportable.cs`, `Cercle.cs`, `Rectangle.cs`, `TriangleRectangle.cs`) ;
- TP 8 : records, enum et structs aussi ont leur fichier (`Colis.cs`,
  `EtatColis.cs`, `Dimensions.cs`, `PointRelais.cs`) ;
- TP 9 : la classe générique dans `PileBornee.cs` (sans le `<T>` dans le nom) ;
- TP 10 : le modèle `Film.cs`, les requêtes dans `Program.cs` ;
- TP 11 : un fichier par exception métier (`SoldeInsuffisantException.cs`,
  `CoupureInvalideException.cs`) + `Distributeur.cs`, `JournalOperations.cs` ;
- TP 12 : `Contact.cs` + `CarnetContacts.cs` (la persistance devient une classe
  dédiée) — et plus besoin de la directive `#:property` du mono-fichier.

### Les commandes pour créer un projet comme ceux-ci

Les commandes exactes qui ont créé les 8 `solution-projet/`, exécutées depuis
`tps/` (`-n` = nom du projet et du namespace, `-o` = dossier de destination) :

```bash
dotnet new console -n TpBanque       -o module-05-classes-objets/solution-projet
dotnet new console -n TpEquipe       -o module-06-encapsulation-heritage/solution-projet
dotnet new console -n TpFormes       -o module-07-interfaces-polymorphisme/solution-projet
dotnet new console -n TpColis        -o module-08-types-specialises/solution-projet
dotnet new console -n TpInventaire   -o module-09-collections-generiques/solution-projet
dotnet new console -n TpMediatheque  -o module-10-linq/solution-projet
dotnet new console -n TpDistributeur -o module-11-exceptions/solution-projet
dotnet new console -n TpContacts     -o module-12-nullabilite-fichiers/solution-projet
```

Puis le `.gitignore` .NET dans chaque projet — dossier par dossier
(`cd module-05-classes-objets/solution-projet && dotnet new gitignore`),
ou en une fois pour les huit :

```bash
for d in module-*/solution-projet; do (cd "$d" && dotnet new gitignore); done
```

Le nom passé à `-n` est aussi le `namespace` en tête de chaque fichier du
projet (`namespace TpBanque;`, `namespace TpEquipe;`, etc.).

Voici maintenant la séquence détaillée que les apprenants doivent connaître,
sur l'exemple du TP 5.

**Anatomie des commandes** (à expliquer en séance, pas seulement à taper) :

| Élément | Signification |
|---|---|
| `dotnet new console` | génère un projet à partir du **modèle** `console` (lister les modèles : `dotnet new list`) |
| `-n TpBanque` | **name** : le nom du projet → fichier `TpBanque.csproj`, namespace par défaut, nom de l'exécutable |
| `-o solution-projet` | **output** : le dossier où créer les fichiers (créé s'il n'existe pas). Sans `-o`, la CLI crée un dossier portant le nom donné à `-n` ; sans `-n` ni `-o`, elle utilise le dossier courant et son nom |
| `dotnet new gitignore` | même mécanique : `gitignore` est un modèle qui génère un `.gitignore` .NET dans le dossier courant |
| `dotnet run` | compile **le projet du dossier courant** puis l'exécute |
| `dotnet run --project chemin/` | désigne le dossier du projet à exécuter quand on n'est **pas** dedans |
| `dotnet run fichier.cs` | tout autre chose : exécute un **fichier autonome** (file-based app), sans projet |
| `dotnet sln add chemin/X.csproj` | inscrit le projet dans le fichier `.sln` du dossier courant (le retirer : `dotnet sln remove`) |
| `dotnet project convert fichier.cs` | transforme un fichier autonome en projet : crée le dossier, le `.csproj`, et convertit les directives `#:` |

```bash
cd tps/module-05-classes-objets
```

Créer le projet console dans un sous-dossier :

```bash
dotnet new console -n TpBanque -o solution-projet
```

Générer le `.gitignore` .NET dans le projet :

```bash
cd solution-projet
```

```bash
dotnet new gitignore
```

Créer ensuite un fichier par type (`CompteBancaire.cs`, `Banque.cs`…), tous
avec la même ligne `namespace TpBanque;` en tête et des types `public` ;
`Program.cs` garde uniquement le scénario (top-level statements). Exécuter :

```bash
dotnet run
```

(ou depuis le dossier du TP : `dotnet run --project solution-projet`)

Dans le cadre de la formation (solution `FormationCSharp` de l'étape zéro),
on rattache le projet à la solution :

```bash
dotnet sln add solution-projet/TpBanque.csproj
```

> C'est déjà fait pour ce dépôt : la solution `CSharpBase.slnx` **à la racine de
> `csharp-base/`** référence les 16 projets (démos + TP). `dotnet build` à la
> racine compile tout ; `dotnet sln list` les liste. À noter : avec .NET 10,
> `dotnet new sln` génère le nouveau format `.slnx` (XML) — même usage que
> l'ancien `.sln`, et c'est ce que les apprenants obtiendront aussi à l'étape zéro.

Enfin, pour migrer une solution mono-fichier existante vers un vrai projet,
la CLI fait le squelette toute seule (elle crée le dossier, le `.csproj` et
convertit les directives `#:` en propriétés MSBuild) :

```bash
dotnet project convert solution.cs
```

## Sommaire

| Jour | Dossier | TP | Notions travaillées |
|---|---|---|---|
| J1 | `module-01-ecosysteme/` | Ma première solution | CLI `dotnet`, sln/projets, build vs run, .gitignore |
| J1 | `module-02-types-variables/` | La caisse enregistreuse | `decimal`, `const`, `TryParse`, chaînes, `StringBuilder`, par valeur vs par référence, overflow |
| J1 | `module-03-operateurs-controle/` | Le petit train des multiples | FizzBuzz, modulo, `do/while`, `foreach`, menu + `switch` |
| J2 | `module-04-methodes/` | La boîte à outils | `out`, `params`, optionnels/nommés, surcharge, récursivité |
| J2 | `module-05-classes-objets/` | Le compte bancaire | Encapsulation, propriétés validées, `static`, `required`/`init` |
| J2 | `module-06-encapsulation-heritage/` | L'équipe de développement | `abstract`/`virtual`/`override`/`sealed`, `base`, `ToString`/`Equals` |
| J3 | `module-07-interfaces-polymorphisme/` | Formes et exports | Interfaces multiples, `is`/`as`, pattern matching + `when` |
| J3 | `module-08-types-specialises/` | Le suivi de colis | `record` + `with`, `enum`, `struct`, tuples, déconstruction |
| J3 | `module-09-collections-generiques/` | L'inventaire du magasin | `List`/`Dictionary`/`HashSet`/`Queue`/`Stack`, classe générique + `where` |
| J4 | `module-10-linq/` | La médiathèque en chiffres | `Where`/`Select`/`OrderBy`/`GroupBy`/agrégats, exécution différée |
| J4 | `module-11-exceptions/` | Le distributeur de billets | `try`/`catch`/`finally`, exceptions métier, `using`/`IDisposable` |
| J4 | `module-12-nullabilite-fichiers/` | Le carnet de contacts persistant | `?`/`??`/`??=`/`?.`, `File`/`Path`, `System.Text.Json` |

## Conseils d'animation

- Les parties A → D sont **progressives** : tout le monde termine A et B ;
  C et D différencient les rythmes ; les bonus occupent les plus rapides.
- Les énoncés contiennent des **questions de compréhension** (pourquoi `decimal` ?
  pourquoi `static` ? pourquoi ce `catch` en dernier ?) : les corriger à l'oral,
  les réponses sont dans les commentaires des solutions.
- Plusieurs TP préparent directement le fil rouge : le menu du module 3 (TP 1),
  les classes des modules 5-6 (étape 1), les collections du module 9 (étape 2),
  LINQ/exceptions/JSON des modules 10-12 (étape 3).
- Les solutions des modules 2 à 12 tiennent dans un seul fichier pour la
  projection ; en séance, les apprenants travaillent dans leur projet `JourN`
  habituel (`dotnet new console`).
