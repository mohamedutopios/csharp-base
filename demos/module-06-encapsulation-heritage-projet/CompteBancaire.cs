namespace DemoHeritage;

public class CompteBancaire
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
