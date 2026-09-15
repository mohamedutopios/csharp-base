namespace TpFormes;

public class Rectangle : IForme, IExportable
{
    public double Largeur { get; }
    public double Hauteur { get; }
    public Rectangle(double largeur, double hauteur) => (Largeur, Hauteur) = (largeur, hauteur);

    public double Aire() => Largeur * Hauteur;
    public double Perimetre() => 2 * (Largeur + Hauteur);
    public string ExporterCsv() => $"rectangle;{Largeur};{Hauteur}";
}
