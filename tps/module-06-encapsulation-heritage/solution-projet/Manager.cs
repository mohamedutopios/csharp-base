namespace TpEquipe;

public class Manager : Employe
{
    public int TailleEquipe { get; private set; }

    public Manager(string nom, decimal salaireBase, int tailleEquipe)
        : base(nom, salaireBase)
    {
        TailleEquipe = tailleEquipe;
    }

    public override decimal SalaireMensuel() => SalaireBase + 100m * TailleEquipe;

    public void Recruter() => TailleEquipe++;        // bonus
}
