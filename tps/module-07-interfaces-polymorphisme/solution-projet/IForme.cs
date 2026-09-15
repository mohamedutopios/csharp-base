namespace TpFormes;

// Les interfaces aussi ont leur fichier : IForme.cs
public interface IForme
{
    double Aire();
    double Perimetre();

    // Bonus : implémentation PAR DÉFAUT (C# 8+) — héritée par tous
    string Resume() => $"aire {Aire():F2}, périmètre {Perimetre():F2}";
}
