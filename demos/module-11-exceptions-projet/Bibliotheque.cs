namespace DemoExceptions;

public class Bibliotheque
{
    private readonly HashSet<string> _empruntes = new();

    public void Emprunter(string titre)
    {
        if (_empruntes.Contains(titre))
            throw new LivreIndisponibleException(titre);     // throw = signaler l'erreur

        _empruntes.Add(titre);
        Console.WriteLine($"Emprunt de « {titre} » enregistré.");
    }
}
