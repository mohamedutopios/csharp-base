namespace TpDistributeur;

public class Distributeur
{
    public decimal Solde { get; private set; }

    public Distributeur(decimal soldeInitial) => Solde = soldeInitial;

    public void Retirer(decimal montant)
    {
        // On valide TOUT avant de toucher à l'état : si une exception part,
        // le solde n'a pas bougé.
        if (montant <= 0)
            throw new ArgumentOutOfRangeException(nameof(montant), "Le montant doit être positif.");

        if (montant % 10 != 0)
            throw new CoupureInvalideException(montant);

        if (montant > Solde)
            throw new SoldeInsuffisantException(Solde, montant);

        Solde -= montant;
        int billets50 = (int)(montant / 50);
        int billets10 = (int)(montant % 50 / 10);
        Console.WriteLine($"Retrait {montant} € : {billets50} × 50 € + {billets10} × 10 €. Reste {Solde:C}.");
    }

    public void Session(decimal montant)
    {
        Console.WriteLine("Carte insérée");
        try
        {
            Retirer(montant);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Opération échouée : {ex.Message}");
        }
        finally
        {
            // Exécuté dans TOUS les cas : succès, échec, même un return
            Console.WriteLine("Carte rendue");
        }
    }
}
