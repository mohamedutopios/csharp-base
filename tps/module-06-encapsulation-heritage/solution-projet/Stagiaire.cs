namespace TpEquipe;

// sealed : fin de lignée, personne n'hérite de Stagiaire.
// class SuperStagiaire : Stagiaire { }  ← erreur CS0509
public sealed class Stagiaire : Employe
{
    public Stagiaire(string nom) : base(nom, 0m) { }

    public override decimal SalaireMensuel() => 800m;
}
