namespace TpBanque;

public class Banque
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
