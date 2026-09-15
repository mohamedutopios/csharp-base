namespace TpDistributeur;

public class JournalOperations : IDisposable
{
    public JournalOperations() => Console.WriteLine("[journal ouvert]");

    public void Consigner(string message) => Console.WriteLine($"[journal] {message}");

    public void Dispose() => Console.WriteLine("[journal fermé]");
}
