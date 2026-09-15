namespace TpEquipe;

public class Developpeur : Employe
{
    public string LangagePrincipal { get; }
    public int Anciennete { get; }

    public Developpeur(string nom, decimal salaireBase, string langage, int anciennete)
        : base(nom, salaireBase)                    // délègue au constructeur parent
    {
        LangagePrincipal = langage;
        Anciennete = anciennete;
    }

    public override decimal SalaireMensuel() => SalaireBase + 50m * Anciennete;

    public override void SePresenter()
    {
        base.SePresenter();                          // la version du parent…
        Console.WriteLine($"Je code en {LangagePrincipal}.");   // …plus la spécialité
    }
}
