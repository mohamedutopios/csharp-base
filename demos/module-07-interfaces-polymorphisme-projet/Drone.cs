namespace DemoInterfaces;

// Deux petits contrats peuvent partager un fichier quand ils vont ensemble —
// la règle « un type public majeur = un fichier » reste la norme.
public interface IVolant { void Voler(); }
public interface IRoulant { void Rouler(); }

// Implémentation MULTIPLE : impossible avec l'héritage de classes
public class Drone : IVolant, IRoulant
{
    public void Voler() => Console.WriteLine("Le drone décolle.");
    public void Rouler() => Console.WriteLine("Le drone roule au sol.");
}
