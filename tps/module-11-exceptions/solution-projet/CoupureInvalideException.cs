namespace TpDistributeur;

public class CoupureInvalideException : Exception
{
    public CoupureInvalideException(decimal montant)
        : base($"{montant:C} n'est pas distribuable : multiples de 10 € uniquement.")
    {
    }
}
