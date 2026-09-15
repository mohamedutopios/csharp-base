namespace DemoInterfaces;

public class Sms : INotifiable
{
    public string Numero { get; }
    public Sms(string numero) => Numero = numero;

    public void Envoyer(string message) =>
        Console.WriteLine($"[SMS → {Numero}] {message}");
}
