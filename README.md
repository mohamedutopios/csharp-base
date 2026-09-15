# Les bases du C# — Matériel de formation (4 jours)

Formation « Les bases du C# » sur .NET 10 / C# 14. Ce dépôt contient le
programme, les slides, les démos et les TP, regroupés dans une solution.

## Structure du dépôt

```
csharp-base/
├── README.md                              ← ce fichier
├── CSharpBase.slnx                        ← LA SOLUTION : les 16 projets démos + TP
├── .gitignore                             ← .gitignore .NET (bin/, obj/…)
├── programme-csharp-bases-4j-v1.0.md      ← programme détaillé des 4 jours (étape zéro incluse)
├── formation-csharp-bases-slides-v1.0.html← slides
├── demos/                                 ← 1 démo par module (voir demos/README.md)
│   ├── module-01-ecosysteme.cs … module-12-….cs        (mono-fichier, dotnet run fichier.cs)
│   └── module-05-…-projet/ … module-12-…-projet/       (versions projet, modules 5 à 12)
└── tps/                                   ← 1 TP par module (voir tps/README.md)
    └── module-XX-…/
        ├── enonce.md                      ← à distribuer aux apprenants
        ├── solution.cs                    ← solution mono-fichier exécutable
        └── solution-projet/               ← version projet (modules 5 à 12)
```

## La solution `CSharpBase.slnx`

Elle référence les **16 projets** du dépôt : les 8 versions projet des démos
(`DemoClasses` → `DemoFichiers`) et les 8 solutions projet des TP
(`TpBanque` → `TpContacts`), modules 5 à 12.

Depuis la racine :

```bash
dotnet sln list
```

```bash
dotnet build
```

```bash
dotnet run --project demos/module-05-classes-objets-projet
```

Ouvrir le dossier `csharp-base/` dans VS Code (extension C# Dev Kit) ou Rider
charge la solution entière.

Deux choses n'y figurent pas, et c'est normal :

- les **démos et solutions mono-fichier** (`.cs` autonomes / file-based apps) :
  pas de `.csproj`, donc rien à inscrire dans une solution — elles se lancent
  par `dotnet run fichier.cs` ;
- les modules **1 à 4** n'ont pas de version projet : ils ne définissent aucun
  type (la POO commence au module 5).

> `.slnx` ? C'est le nouveau format de solution (XML) généré par
> `dotnet new sln` depuis .NET 10 — même usage et mêmes commandes que
> l'ancien `.sln`. Les apprenants obtiendront aussi un `.slnx` à l'étape zéro.

## Comment cette solution a été créée

```bash
cd csharp-base
```

```bash
dotnet new sln -n CSharpBase
```

```bash
dotnet new gitignore
```

Puis le rattachement de chaque projet (les commandes qui ont créé les projets
eux-mêmes sont listées projet par projet dans `demos/README.md` et
`tps/README.md`, avec l'explication des options `-n`/`-o`) :

```bash
dotnet sln add demos/module-05-classes-objets-projet/DemoClasses.csproj
dotnet sln add demos/module-06-encapsulation-heritage-projet/DemoHeritage.csproj
dotnet sln add demos/module-07-interfaces-polymorphisme-projet/DemoInterfaces.csproj
dotnet sln add demos/module-08-types-specialises-projet/DemoTypes.csproj
dotnet sln add demos/module-09-collections-generiques-projet/DemoCollections.csproj
dotnet sln add demos/module-10-linq-projet/DemoLinq.csproj
dotnet sln add demos/module-11-exceptions-projet/DemoExceptions.csproj
dotnet sln add demos/module-12-nullabilite-fichiers-projet/DemoFichiers.csproj
dotnet sln add tps/module-05-classes-objets/solution-projet/TpBanque.csproj
dotnet sln add tps/module-06-encapsulation-heritage/solution-projet/TpEquipe.csproj
dotnet sln add tps/module-07-interfaces-polymorphisme/solution-projet/TpFormes.csproj
dotnet sln add tps/module-08-types-specialises/solution-projet/TpColis.csproj
dotnet sln add tps/module-09-collections-generiques/solution-projet/TpInventaire.csproj
dotnet sln add tps/module-10-linq/solution-projet/TpMediatheque.csproj
dotnet sln add tps/module-11-exceptions/solution-projet/TpDistributeur.csproj
dotnet sln add tps/module-12-nullabilite-fichiers/solution-projet/TpContacts.csproj
```

## Par où commencer

| Besoin | Aller à |
|---|---|
| Dérouler le programme, horaires, évaluation | `programme-csharp-bases-4j-v1.0.md` |
| Projeter une démo en séance | `demos/README.md` (sommaire + conseils d'animation) |
| Distribuer un TP, consulter sa solution | `tps/README.md` (sommaire + conseils) |
| Tout compiler avant une session | `dotnet build` à la racine |
