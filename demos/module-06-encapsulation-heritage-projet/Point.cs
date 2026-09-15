namespace DemoHeritage;

public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y) => (X, Y) = (x, y);

    // Égalité de CONTENU au lieu de l'égalité de référence par défaut
    public override bool Equals(object? obj) =>
        obj is Point autre && autre.X == X && autre.Y == Y;

    // Règle d'or : qui override Equals doit overrider GetHashCode
    public override int GetHashCode() => HashCode.Combine(X, Y);
}
