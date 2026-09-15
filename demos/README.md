# Démos par module — Les bases du C# (4 jours)

Une démo exécutable par module du programme, sous forme de **fichiers C# autonomes**
(« file-based apps », .NET 10) : pas de `.csproj`, pas de solution — un fichier = une démo.

## Exécution

```bash
cd demos
dotnet run module-01-ecosysteme.cs
```

> La première exécution d'un fichier est un peu lente (compilation), les suivantes sont instantanées (cache).

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
