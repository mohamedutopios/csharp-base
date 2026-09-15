namespace TpDistributeur;

// Exception métier : hérite d'Exception, nom en ...Exception, son fichier.
public class SoldeInsuffisantException : Exception
{
    public decimal Solde { get; }
    public decimal Demande { get; }

    public SoldeInsuffisantException(decimal solde, decimal demande)
        : base($"Solde insuffisant : {solde:C} disponible, {demande:C} demandé.")
    {
        Solde = solde;
        Demande = demande;
    }
}
