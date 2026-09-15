namespace DemoInterfaces;

public class Email : INotifiable        // « : » = implémente
{
    public string Adresse { get; }
    public Email(string adresse) => Adresse = adresse;

    public void Envoyer(string message) =>
        Console.WriteLine($"[EMAIL → {Adresse}] {message}");
}
