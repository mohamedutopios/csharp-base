// =====================================================================
// Solution — TP Module 5 : le compte bancaire
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 5 ===\n");

// ---------------------------------------------------------------
// Parties A & B — création et comportements
// ---------------------------------------------------------------
var compte = new CompteBancaire("Alice Martin", 100m);
compte.Afficher();

compte.Deposer(75m);
Console.WriteLine("Après dépôt de 75 € :");
compte.Afficher();

bool retrait500 = compte.Retirer(500m);
Console.WriteLine($"Retrait de 500 € accepté ? {retrait500}");

bool retrait50 = compte.Retirer(50m);
Console.WriteLine($"Retrait de 50 € accepté ? {retrait50}");
compte.Afficher();

// Question 7 : compte.Solde = 1_000_000m; ne compile pas car Solde n'a pas
// de setter public. C'est l'ENCAPSULATION : le solde ne peut évoluer que par
// Deposer/Retirer, qui garantissent les règles métier (pas de solde négatif).

// ---------------------------------------------------------------
// Partie C — membres statiques
// ---------------------------------------------------------------
var c2 = new CompteBancaire("Bob Durand", 500m);
var c3 = new CompteBancaire("Chloé Petit", -50m);    // solde négatif → ramené à 0
Console.WriteLine();
c2.Afficher();
c3.Afficher();
Console.WriteLine($"Nombre de comptes créés : {CompteBancaire.NombreDeComptes}");
// Question 10 : un membre d'instance appartient à UN objet ; chaque compte
// aurait SON compteur à 1. static = une seule valeur partagée par la classe.

// ---------------------------------------------------------------
// Partie D — la classe Banque
// ---------------------------------------------------------------
var banque = new Banque { Nom = "Banque Populaire du Code" };   // object initializer
// var oups = new Banque { };        // ← ne compile pas : Nom est required !
// banque.Devise = "USD";            // ← ne compile pas : Devise est init

Console.WriteLine($"\n{banque.Nom} ({banque.Devise})");
banque.OuvrirCompte("Dora Lopez", 1200m);
banque.OuvrirCompte("Émile Roy", 300m);
var dernier = banque.OuvrirCompte("Fatou Ba", 800m);
dernier?.Afficher();

Console.WriteLine($"Total des avoirs : {banque.TotalDesAvoirs():C}");

// ---------------------------------------------------------------
// Bonus — propriété statique + calcul d'instance
// ---------------------------------------------------------------
Console.WriteLine($"\nTaux livret : {CompteBancaire.TauxLivret} %");
Console.WriteLine($"Intérêts annuels du compte d'Alice : {compte.InteretsAnnuels():C}");

// =====================================================================
// Les classes
// =====================================================================

class CompteBancaire
{
    // Champ privé : l'état réel, inaccessible de l'extérieur
    private decimal _solde;

    // static : porté par la CLASSE, partagé par toutes les instances.
    // set privé : lisible partout, modifiable seulement ici.
    public static int NombreDeComptes { get; private set; }

    public static decimal TauxLivret { get; set; } = 3.0m;      // bonus

    public string Titulaire { get; set; }
    public string Iban { get; }                 // get seul : figé après le constructeur
    public decimal Solde => _solde;             // lecture seule : expose le champ privé

    public CompteBancaire(string titulaire, decimal soldeInitial)
    {
        Titulaire = titulaire;
        _solde = soldeInitial < 0 ? 0 : soldeInitial;

        NombreDeComptes++;                              // compteur partagé
        Iban = $"FR76-{NombreDeComptes:D4}";            // D4 : 0001, 0002…
    }

    public void Deposer(decimal montant)
    {
        if (montant > 0)
            _solde += montant;
    }

    public bool Retirer(decimal montant)
    {
        if (montant <= 0 || montant > _solde)
            return false;

        _solde -= montant;
        return true;
    }

    public void Afficher() =>
        Console.WriteLine($"{Iban} | {Titulaire} | {Solde:C}");

    public decimal InteretsAnnuels() => Solde * TauxLivret / 100;   // bonus
}

class Banque
{
    private readonly CompteBancaire?[] _comptes = new CompteBancaire?[10];
    private int _nombreDeComptes;

    public required string Nom { get; set; }        // required : exigé à la création
    public string Devise { get; init; } = "EUR";    // init : figé après construction

    public CompteBancaire? OuvrirCompte(string titulaire, decimal depot)
    {
        if (_nombreDeComptes >= _comptes.Length)
            return null;                            // banque pleine

        var compte = new CompteBancaire(titulaire, depot);
        _comptes[_nombreDeComptes] = compte;
        _nombreDeComptes++;
        return compte;
    }

    public decimal TotalDesAvoirs()
    {
        decimal total = 0;
        foreach (var compte in _comptes)
        {
            if (compte is not null)
                total += compte.Solde;
        }
        return total;
    }
}
