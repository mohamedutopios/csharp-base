// =====================================================================
// Solution — TP Module 12 (version projet multi-fichiers)
// Contact.cs (le modèle), CarnetContacts.cs (la persistance JSON),
// Program.cs (le scénario). En projet classique, le JSON par réflexion
// fonctionne sans la directive #:property de la version mono-fichier.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using TpContacts;

Console.WriteLine("=== TP Module 12 (projet) ===\n");

// --- Partie A — Apprivoiser le null ---
var alice = new Contact("Alice Martin", "06 12 34 56 78", "alice@mail.fr");
var bob = new Contact("Bob Durand", null, null);          // optionnels absents

Console.WriteLine(DecrireContact(alice));
Console.WriteLine(DecrireContact(bob));

// --- Parties B/C — chemins et persistance JSON ---
var contacts = new List<Contact>
{
    alice,
    bob,
    new("Chloé Petit", "07 98 76 54 32", null),
};

string dossier = Path.Combine(Path.GetTempPath(), "tp-contacts-projet");
string cheminJson = Path.Combine(dossier, "contacts.json");
Directory.CreateDirectory(dossier);

CarnetContacts.Sauvegarder(contacts, cheminJson);

List<Contact> recharges = CarnetContacts.Charger(cheminJson);
Console.WriteLine($"Rechargés : {recharges.Count} contacts");
Console.WriteLine($"Premier identique à l'original ? {recharges[0] == contacts[0]} (record → égalité de contenu)");

// Fichier absent et JSON corrompu : le contrat « jamais null » tient
List<Contact> inexistants = CarnetContacts.Charger(Path.Combine(dossier, "absent.json"));
Console.WriteLine($"Fichier absent → {inexistants.Count} contact(s), pas de crash");

File.WriteAllText(cheminJson, "{ oops");
List<Contact> corrompus = CarnetContacts.Charger(cheminJson);
Console.WriteLine($"JSON corrompu → {corrompus.Count} contact(s), programme intact");

Directory.Delete(dossier, recursive: true);
Console.WriteLine("\n(Dossier temporaire supprimé.)");

static string DecrireContact(Contact c)
{
    // ?? : valeur de repli si null ; ?. : accès sans crash
    string telephone = c.Telephone ?? "(pas de téléphone)";
    int? tailleEmail = c.Email?.Length;

    return $"{c.Nom} — {telephone} — email : " +
           (tailleEmail is null ? "(aucun)" : $"{c.Email} ({tailleEmail} car.)");
}
