using System.Text.Json;

namespace TpContacts;

// En version projet, la persistance devient une classe dédiée : c'est elle
// qu'on réutilisera (et qu'on testera) — Program.cs ne garde que le scénario.
public static class CarnetContacts
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static void Sauvegarder(List<Contact> contacts, string chemin)
    {
        string json = JsonSerializer.Serialize(contacts, Options);
        File.WriteAllText(chemin, json);
        Console.WriteLine($"{contacts.Count} contacts sauvegardés dans {Path.GetFileName(chemin)}");
    }

    public static List<Contact> Charger(string chemin)
    {
        // Contrat : ne renvoie JAMAIS null → l'appelant n'a rien à vérifier
        if (!File.Exists(chemin))
        {
            Console.WriteLine($"({Path.GetFileName(chemin)} introuvable → liste vide)");
            return new List<Contact>();
        }

        try
        {
            string json = File.ReadAllText(chemin);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new();
        }
        catch (JsonException)
        {
            Console.WriteLine($"({Path.GetFileName(chemin)} illisible → liste vide)");
            return new List<Contact>();
        }
    }
}
