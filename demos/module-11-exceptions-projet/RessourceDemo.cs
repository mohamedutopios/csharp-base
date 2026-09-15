namespace DemoExceptions;

// IDisposable : le contrat « je détiens une ressource à libérer »
public class RessourceDemo : IDisposable
{
    private readonly string _nom;

    public RessourceDemo(string nom)
    {
        _nom = nom;
        Console.WriteLine($"  [{_nom}] ouverte");
    }

    public void Utiliser() => Console.WriteLine($"  [{_nom}] en cours d'utilisation");

    public void Dispose() => Console.WriteLine($"  [{_nom}] libérée (Dispose)");
}
