namespace DemoHeritage;

public class Chien : Animal                        // Chien HÉRITE de Animal
{
    public Chien(string nom) : base(nom) { }       // base(...) appelle le constructeur parent

    public override void Crier() => Console.WriteLine($"{Nom} : Wouf !");
}
