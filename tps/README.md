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

Pour migrer un fichier autonome vers un vrai projet : `dotnet project convert solution.cs`.

## Sommaire

| Jour | Dossier | TP | Notions travaillées |
|---|---|---|---|
| J1 | `module-01-ecosysteme/` | Ma première solution | CLI `dotnet`, sln/projets, build vs run, .gitignore |
| J1 | `module-02-types-variables/` | La caisse enregistreuse | `decimal`, `const`, `TryParse`, chaînes, `StringBuilder` |
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
