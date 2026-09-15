// =====================================================================
// Solution — TP Module 11 (version projet multi-fichiers)
// SoldeInsuffisantException.cs, CoupureInvalideException.cs,
// Distributeur.cs, JournalOperations.cs. Program.cs = le scénario.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using TpDistributeur;

Console.WriteLine("=== TP Module 11 (projet) ===\n");

// --- Partie B — Signaler avec des exceptions métier ---
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
        Console.WriteLine($"Retrait {montant} € : refusé, il manque {ex.Demande - ex.Solde:C}.");
    }
}
Console.WriteLine($"Solde final : {distributeur.Solde:C} (les échecs n'ont rien débité)");

// --- Partie C — finally et using ---
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
