namespace TpColis;

public struct Dimensions
{
    public double LongueurCm { get; set; }
    public double LargeurCm { get; set; }
    public double HauteurCm { get; set; }

    public double VolumeLitres => LongueurCm * LargeurCm * HauteurCm / 1000;
}
