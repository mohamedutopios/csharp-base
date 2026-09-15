namespace TpFormes;

public class Cercle : IForme, IExportable                 // deux contrats à la fois
{
    public double Rayon { get; }
    public Cercle(double rayon) => Rayon = rayon;

    public double Aire() => Math.PI * Rayon * Rayon;
    public double Perimetre() => 2 * Math.PI * Rayon;
    public string ExporterCsv() => $"cercle;{Rayon}";
}
