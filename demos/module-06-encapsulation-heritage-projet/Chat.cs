namespace DemoHeritage;

// sealed : personne ne pourra hériter de Chat
public sealed class Chat : Animal
{
    public Chat(string nom) : base(nom) { }

    public override void Crier() => Console.WriteLine($"{Nom} : Miaou !");
}
