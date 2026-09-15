// =====================================================================
// Module 11 — Gestion des exceptions (Jour 4, 10h45–11h45)
// Démo : try/catch/finally, exceptions personnalisées, throw,
//        using et IDisposable
// Exécution : dotnet run module-11-exceptions.cs
// Version projet (un type = un fichier) : module-11-exceptions-projet/
// =====================================================================

Console.WriteLine("=== Démo Module 11 : gestion des exceptions ===\n");

// ---------------------------------------------------------------
// 1. Sans try/catch : une exception non gérée ARRÊTE le programme
// ---------------------------------------------------------------
// int boom = int.Parse("abc");        // ← décommenter : FormatException, crash immédiat

// ---------------------------------------------------------------
// 2. try / catch : intercepter et continuer
// ---------------------------------------------------------------
try
{
    int valeur = int.Parse("abc");
    Console.WriteLine($"Jamais affiché : {valeur}");
}
catch (FormatException ex)
{
    Console.WriteLine($"catch FormatException : « {ex.Message} »");
    Console.WriteLine("Le programme continue !");
}

// ---------------------------------------------------------------
// 3. Plusieurs catch : du PLUS SPÉCIFIQUE au PLUS GÉNÉRAL
// ---------------------------------------------------------------
string[] essais = { "42", "abc", "999999999999999999999" };
foreach (string essai in essais)
{
    try
    {
        int n = int.Parse(essai);
        Console.WriteLine($"\n\"{essai}\" → {n}");
    }
    catch (FormatException)
    {
        Console.WriteLine($"\n\"{essai}\" → pas un nombre (FormatException)");
    }
    catch (OverflowException)
    {
        Console.WriteLine($"\n\"{essai}\" → trop grand pour un int (OverflowException)");
    }
    catch (Exception ex)                 // filet de sécurité, TOUJOURS en dernier
    {
        Console.WriteLine($"\nImprévu : {ex.GetType().Name}");
    }
}

// ---------------------------------------------------------------
// 4. finally : s'exécute TOUJOURS (succès, échec ou return)
// ---------------------------------------------------------------
Console.WriteLine();
try
{
    Console.WriteLine("try    : ouverture d'une ressource fictive…");
    throw new InvalidOperationException("problème en plein traitement");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"catch  : {ex.Message}");
}
finally
{
    Console.WriteLine("finally: nettoyage exécuté quoi qu'il arrive (fermeture, libération…)");
}

// ---------------------------------------------------------------
// 5. throw : signaler soi-même une erreur + exception PERSONNALISÉE
// ---------------------------------------------------------------
var bibliotheque = new Bibliotheque();
bibliotheque.Emprunter("Dune");                  // OK

try
{
    bibliotheque.Emprunter("Dune");              // déjà sorti → exception métier
}
catch (LivreIndisponibleException ex)            // celle du fil rouge !
{
    Console.WriteLine($"\nException métier interceptée : {ex.Message} (livre : {ex.Titre})");
}

// ---------------------------------------------------------------
// 6. using + IDisposable : libération GARANTIE des ressources
// ---------------------------------------------------------------
Console.WriteLine("\n--- using : Dispose appelé automatiquement ---");
using (var ressource = new RessourceDemo("connexion A"))
{
    ressource.Utiliser();
}   // ← Dispose() appelé ICI, même en cas d'exception (= try/finally caché)

// Forme moderne « using declaration » : libéré en fin de bloc englobant
using var ressource2 = new RessourceDemo("connexion B");
ressource2.Utiliser();

Console.WriteLine("\n→ À retenir : catch du spécifique au général, exceptions métier nommées " +
                  "en ...Exception, using pour tout ce qui est IDisposable.");
// (Dispose de « connexion B » s'affiche après cette ligne, en sortie de programme.)

// =====================================================================
// Types
// =====================================================================

// Exception personnalisée : hérite d'Exception, nom finissant par Exception
class LivreIndisponibleException : Exception
{
    public string Titre { get; }

    public LivreIndisponibleException(string titre)
        : base($"Le livre « {titre} » est déjà emprunté.")   // message vers la base
    {
        Titre = titre;
    }
}

class Bibliotheque
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

// IDisposable : le contrat « je détiens une ressource à libérer »
class RessourceDemo : IDisposable
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
