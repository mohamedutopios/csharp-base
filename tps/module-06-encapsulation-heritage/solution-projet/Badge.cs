namespace TpEquipe;

public class Badge
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
