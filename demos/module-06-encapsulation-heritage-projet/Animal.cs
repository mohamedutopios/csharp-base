namespace DemoHeritage;

// abstract : ne peut pas être instanciée, sert de socle commun
public abstract class Animal
{
    public string Nom { get; }

    // protected : visible ici et dans les classes filles uniquement
    protected Animal(string nom) => Nom = nom;

    // virtual : les filles PEUVENT redéfinir
    public virtual void Crier() => Console.WriteLine($"{Nom} fait un bruit.");

    public override string ToString() => $"{GetType().Name} nommé {Nom}";
}
