namespace TpEquipe;

// abstract : ne peut pas être instanciée, sert de socle commun à la hiérarchie
public abstract class Employe
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
