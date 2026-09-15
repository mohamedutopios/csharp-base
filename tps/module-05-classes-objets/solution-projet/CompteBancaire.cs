// Une classe = un fichier, nommé comme la classe : CompteBancaire.cs
// Le namespace regroupe les types du projet ; même namespace partout
// → les fichiers se voient entre eux sans rien importer.
namespace TpBanque;

public class CompteBancaire
{
    // Champ privé : l'état réel, inaccessible de l'extérieur
    private decimal _solde;

    // static : porté par la CLASSE, partagé par toutes les instances.
    public static int NombreDeComptes { get; private set; }

    public static decimal TauxLivret { get; set; } = 3.0m;      // bonus

    public string Titulaire { get; set; }
    public string Iban { get; }                 // get seul : figé après le constructeur
    public decimal Solde => _solde;             // lecture seule : expose le champ privé

    public CompteBancaire(string titulaire, decimal soldeInitial)
    {
        Titulaire = titulaire;
        _solde = soldeInitial < 0 ? 0 : soldeInitial;

        NombreDeComptes++;
        Iban = $"FR76-{NombreDeComptes:D4}";
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
