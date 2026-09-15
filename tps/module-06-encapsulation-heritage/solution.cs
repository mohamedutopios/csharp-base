// =====================================================================
// Solution — TP Module 6 : l'équipe de développement
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 6 ===\n");

// Question 2 : new Employe(...) ne compile pas car la classe est abstract :
// elle sert de socle commun, elle n'a pas de sens seule (quel salaire ?).

// ---------------------------------------------------------------
// Partie C — Le polymorphisme au travail
// ---------------------------------------------------------------
var equipe = new List<Employe>
{
    new Developpeur("Alice", 2800m, "C#", anciennete: 4),
    new Developpeur("Bob", 2600m, "Python", anciennete: 2),
    new Manager("Chloé", 3200m, tailleEquipe: 5),
    new Stagiaire("David"),
};

// UNE boucle sur le type de base : chaque objet exécute SA version
foreach (Employe e in equipe)
{
    e.SePresenter();
    Console.WriteLine();
}

// Masse salariale : toujours via le type de base
decimal masseSalariale = 0;
foreach (Employe e in equipe)
    masseSalariale += e.SalaireMensuel();
Console.WriteLine($"Masse salariale totale : {masseSalariale:C}");

// ---------------------------------------------------------------
// Partie D — ToString et Equals
// ---------------------------------------------------------------
Console.WriteLine("\n--- ToString ---");
foreach (Employe e in equipe)
    Console.WriteLine($"  {e}");            // appelle ToString() implicitement

Console.WriteLine("\n--- Equals sur Badge ---");
var b1 = new Badge(7, "ACME");
var b2 = new Badge(7, "ACME");
var b3 = new Badge(8, "ACME");
Console.WriteLine($"b1.Equals(b2)          : {b1.Equals(b2)} (même contenu)");
Console.WriteLine($"ReferenceEquals(b1,b2) : {ReferenceEquals(b1, b2)} (objets distincts)");
Console.WriteLine($"b1.Equals(b3)          : {b1.Equals(b3)}");

// ---------------------------------------------------------------
// Bonus — le calcul de salaire est dynamique
// ---------------------------------------------------------------
var manager = new Manager("Chloé", 3200m, tailleEquipe: 5);
Console.WriteLine($"\nAvant recrutement : {manager.SalaireMensuel():C}");
manager.Recruter();
manager.Recruter();
Console.WriteLine($"Après 2 recrutements : {manager.SalaireMensuel():C} (recalculé à l'appel)");

// =====================================================================
// La hiérarchie
// =====================================================================

abstract class Employe
{
    public string Nom { get; }
    public decimal SalaireBase { get; }

    protected Employe(string nom, decimal salaireBase)
    {
        Nom = nom;
        SalaireBase = salaireBase;
    }

    // abstract : pas de corps, chaque classe fille DOIT fournir le sien
    public abstract decimal SalaireMensuel();

    // virtual : comportement par défaut, redéfinissable
    public virtual void SePresenter() =>
        Console.WriteLine($"Je suis {Nom}, je gagne {SalaireMensuel():C} par mois.");

    // ToString commun à toute la hiérarchie ; GetType().Name donne le type RÉEL
    public override string ToString() =>
        $"{GetType().Name} {Nom} ({SalaireMensuel():C})";
}

class Developpeur : Employe
{
    public string LangagePrincipal { get; }
    public int Anciennete { get; }

    public Developpeur(string nom, decimal salaireBase, string langage, int anciennete)
        : base(nom, salaireBase)                    // délègue au constructeur parent
    {
        LangagePrincipal = langage;
        Anciennete = anciennete;
    }

    public override decimal SalaireMensuel() => SalaireBase + 50m * Anciennete;

    public override void SePresenter()
    {
        base.SePresenter();                          // la version du parent…
        Console.WriteLine($"Je code en {LangagePrincipal}.");   // …plus la spécialité
    }
}

class Manager : Employe
{
    public int TailleEquipe { get; private set; }

    public Manager(string nom, decimal salaireBase, int tailleEquipe)
        : base(nom, salaireBase)
    {
        TailleEquipe = tailleEquipe;
    }

    public override decimal SalaireMensuel() => SalaireBase + 100m * TailleEquipe;

    public void Recruter() => TailleEquipe++;        // bonus
}

// sealed : fin de lignée, personne n'hérite de Stagiaire.
// class SuperStagiaire : Stagiaire { }  ← erreur CS0509
sealed class Stagiaire : Employe
{
    public Stagiaire(string nom) : base(nom, 0m) { }

    // Bonus : « sealed override » sur une méthode (dans une classe non sealed)
    // interdirait SEULEMENT de re-redéfinir cette méthode plus bas,
    // tout en laissant la classe héritable. Ici la classe entière est sealed.
    public override decimal SalaireMensuel() => 800m;
}

class Badge
{
    public int Numero { get; }
    public string Societe { get; }

    public Badge(int numero, string societe)
    {
        Numero = numero;
        Societe = societe;
    }

    // Égalité de CONTENU (par défaut : égalité de référence)
    public override bool Equals(object? obj) =>
        obj is Badge autre && autre.Numero == Numero && autre.Societe == Societe;

    // Règle d'or : Equals redéfini ⇒ GetHashCode redéfini avec les MÊMES champs
    public override int GetHashCode() => HashCode.Combine(Numero, Societe);
}
