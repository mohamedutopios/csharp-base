// =====================================================================
// Module 6 — Encapsulation et héritage (Jour 2, 13h30–15h00)
// Démo : modificateurs d'accès, base, virtual/override/sealed,
//        classes abstraites, ToString/Equals
// Exécution : dotnet run module-06-encapsulation-heritage.cs
// Version projet (une classe = un fichier) : module-06-encapsulation-heritage-projet/
// =====================================================================

Console.WriteLine("=== Démo Module 6 : encapsulation et héritage ===\n");

// ---------------------------------------------------------------
// 1. Encapsulation : l'état est protégé, on passe par des méthodes
// ---------------------------------------------------------------
var compte = new CompteBancaire("FR76-0000", 100m);
compte.Deposer(50m);
bool ok = compte.Retirer(500m);       // refusé : solde insuffisant
Console.WriteLine($"Solde : {compte.Solde:C} | Retrait de 500 € accepté ? {ok}");
// compte._solde = 1_000_000m;        // ← ERREUR : le champ est private. C'est le but !

// ---------------------------------------------------------------
// 2. Héritage : Chien et Chat SONT DES Animaux
// ---------------------------------------------------------------
var animaux = new List<Animal>
{
    new Chien("Rex"),
    new Chat("Félix"),
    new Chien("Médor")
};

Console.WriteLine("\n--- Polymorphisme : même appel, comportements différents ---");
foreach (Animal animal in animaux)     // on manipule le type de BASE…
{
    animal.Crier();                    // …mais c'est l'override du type RÉEL qui s'exécute
}

// ---------------------------------------------------------------
// 3. Classe abstraite : non instanciable, impose un contrat partiel
// ---------------------------------------------------------------
// var a = new Animal("X");            // ← ERREUR : Animal est abstract
Console.WriteLine("\nAnimal est abstract : on ne peut pas faire new Animal(...).");

// ---------------------------------------------------------------
// 4. ToString : la représentation texte d'un objet
// ---------------------------------------------------------------
var rex = animaux[0];
Console.WriteLine($"\nSans override, ToString donnerait le nom du type.");
Console.WriteLine($"Avec override : {rex}");     // appelle ToString() implicitement

// ---------------------------------------------------------------
// 5. Equals : égalité de contenu vs égalité de référence
// ---------------------------------------------------------------
var p1 = new Point(2, 3);
var p2 = new Point(2, 3);
Console.WriteLine($"\np1 == p2 (référence)   : {ReferenceEquals(p1, p2)}  (deux objets distincts)");
Console.WriteLine($"p1.Equals(p2) (contenu): {p1.Equals(p2)}  (grâce à l'override)");

Console.WriteLine("\n→ À retenir : private par défaut, virtual/override pour spécialiser, " +
                  "abstract = contrat, sealed = fin de lignée.");

// =====================================================================
// Les classes
// =====================================================================

class CompteBancaire
{
    private decimal _solde;                        // private : invisible dehors

    public string Iban { get; }                    // get seul : lecture seule
    public decimal Solde => _solde;                // propriété calculée en lecture

    public CompteBancaire(string iban, decimal soldeInitial)
    {
        Iban = iban;
        _solde = soldeInitial;
    }

    public void Deposer(decimal montant)
    {
        if (montant > 0) _solde += montant;
    }

    public bool Retirer(decimal montant)
    {
        if (montant <= 0 || montant > _solde) return false;
        _solde -= montant;
        return true;
    }
}

// abstract : ne peut pas être instanciée, sert de socle commun
abstract class Animal
{
    public string Nom { get; }

    // protected serait visible ici et dans les classes filles uniquement
    protected Animal(string nom) => Nom = nom;

    // virtual : les filles PEUVENT redéfinir
    public virtual void Crier() => Console.WriteLine($"{Nom} fait un bruit.");

    public override string ToString() => $"{GetType().Name} nommé {Nom}";
}

class Chien : Animal                               // Chien HÉRITE de Animal
{
    public Chien(string nom) : base(nom) { }       // base(...) appelle le constructeur parent

    public override void Crier() => Console.WriteLine($"{Nom} : Wouf !");
}

// sealed : personne ne pourra hériter de Chat
sealed class Chat : Animal
{
    public Chat(string nom) : base(nom) { }

    public override void Crier() => Console.WriteLine($"{Nom} : Miaou !");
}

class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) => (X, Y) = (x, y);

    // Égalité de CONTENU au lieu de l'égalité de référence par défaut
    public override bool Equals(object? obj) =>
        obj is Point autre && autre.X == X && autre.Y == Y;

    // Règle d'or : qui override Equals doit overrider GetHashCode
    public override int GetHashCode() => HashCode.Combine(X, Y);
}
