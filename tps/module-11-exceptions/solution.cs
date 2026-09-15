// =====================================================================
// Solution — TP Module 11 : le distributeur de billets
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 11 ===\n");

// ---------------------------------------------------------------
// Partie A — Intercepter
// ---------------------------------------------------------------
string[] saisies = { "50", "abc", "" };
foreach (string saisie in saisies)
{
    try
    {
        decimal montant = LireMontant(saisie);
        Console.WriteLine($"« {saisie} » → {montant:C}");
    }
    catch (FormatException)                    // le cas PRÉCIS d'abord
    {
        Console.WriteLine($"« {saisie} » → Ce n'est pas un montant valide.");
    }
    catch (Exception ex)                       // le filet général en DERNIER
    {
        Console.WriteLine($"« {saisie} » → imprévu : {ex.GetType().Name}");
    }
}
// Question 3 : les catch sont testés DANS L'ORDRE. Exception attrape tout,
// donc placé en premier il masquerait les catch suivants — le compilateur
// le refuse d'ailleurs (CS0160 : clause catch inaccessible).

// ---------------------------------------------------------------
// Partie B — Signaler avec des exceptions métier
// ---------------------------------------------------------------
Console.WriteLine("\n--- Retraits ---");
var distributeur = new Distributeur(500m);

decimal[] retraits = { 120m, -5m, 35m, 9990m };
foreach (decimal montant in retraits)
{
    try
    {
        distributeur.Retirer(montant);
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine($"Retrait {montant} € : montant impossible (doit être > 0).");
    }
    catch (CoupureInvalideException ex)
    {
        Console.WriteLine($"Retrait {montant} € : {ex.Message}");
    }
    catch (SoldeInsuffisantException ex)
    {
        // Les propriétés de l'exception portent le contexte de l'erreur
        Console.WriteLine($"Retrait {montant} € : refusé, il manque {ex.Demande - ex.Solde:C}.");
    }
}
// Bonus : après les échecs, le solde est INTACT (l'exception part AVANT le débit)
Console.WriteLine($"Solde final : {distributeur.Solde:C} (les échecs n'ont rien débité)");

// ---------------------------------------------------------------
// Partie C — finally et using
// ---------------------------------------------------------------
Console.WriteLine("\n--- Session avec finally ---");
distributeur.Session(50m);        // OK      → carte rendue
distributeur.Session(35m);        // échec   → carte rendue QUAND MÊME

Console.WriteLine("\n--- using et IDisposable ---");
try
{
    using var journal = new JournalOperations();
    journal.Consigner("tentative de retrait de 9990 €");
    distributeur.Retirer(9990m);                 // lève !
}
catch (SoldeInsuffisantException)
{
    Console.WriteLine("Échec intercepté — et pourtant le journal est bien fermé au-dessus.");
}
// Question 9 : using = un try/finally écrit par le compilateur, qui appelle
// Dispose() à la sortie du bloc, exception ou pas. Moins de code, zéro oubli.

// Bonus throw : dans un catch, « throw; » relance l'exception EN CONSERVANT
// la pile d'appels d'origine ; « throw ex; » la RÉÉCRASE (on perd l'endroit
// réel de l'erreur). Toujours préférer « throw; ».

// =====================================================================
// Méthodes et classes
// =====================================================================

static decimal LireMontant(string saisie) => decimal.Parse(saisie);

// --- Exceptions métier : héritent d'Exception, nom en ...Exception ---

class SoldeInsuffisantException : Exception
{
    public decimal Solde { get; }
    public decimal Demande { get; }

    public SoldeInsuffisantException(decimal solde, decimal demande)
        : base($"Solde insuffisant : {solde:C} disponible, {demande:C} demandé.")
    {
        Solde = solde;
        Demande = demande;
    }
}

class CoupureInvalideException : Exception
{
    public CoupureInvalideException(decimal montant)
        : base($"{montant:C} n'est pas distribuable : multiples de 10 € uniquement.")
    {
    }
}

class Distributeur
{
    public decimal Solde { get; private set; }

    public Distributeur(decimal soldeInitial) => Solde = soldeInitial;

    public void Retirer(decimal montant)
    {
        // On valide TOUT avant de toucher à l'état : si une exception part,
        // le solde n'a pas bougé.
        if (montant <= 0)
            throw new ArgumentOutOfRangeException(nameof(montant), "Le montant doit être positif.");

        if (montant % 10 != 0)
            throw new CoupureInvalideException(montant);

        if (montant > Solde)
            throw new SoldeInsuffisantException(Solde, montant);

        Solde -= montant;
        int billets50 = (int)(montant / 50);
        int billets10 = (int)(montant % 50 / 10);
        Console.WriteLine($"Retrait {montant} € : {billets50} × 50 € + {billets10} × 10 €. Reste {Solde:C}.");
    }

    public void Session(decimal montant)
    {
        Console.WriteLine("Carte insérée");
        try
        {
            Retirer(montant);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Opération échouée : {ex.Message}");
        }
        finally
        {
            // Exécuté dans TOUS les cas : succès, échec, même un return
            Console.WriteLine("Carte rendue");
        }
    }
}

class JournalOperations : IDisposable
{
    public JournalOperations() => Console.WriteLine("[journal ouvert]");

    public void Consigner(string message) => Console.WriteLine($"[journal] {message}");

    public void Dispose() => Console.WriteLine("[journal fermé]");
}
