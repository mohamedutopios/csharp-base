namespace TpFormes;

public class TriangleRectangle : IForme                   // PAS IExportable : c'est un choix
{
    public double Base { get; }
    public double Hauteur { get; }
    public TriangleRectangle(double base_, double hauteur) => (Base, Hauteur) = (base_, hauteur);

    public double Aire() => Base * Hauteur / 2;
    public double Perimetre() => Base + Hauteur + Math.Sqrt(Base * Base + Hauteur * Hauteur);
}
