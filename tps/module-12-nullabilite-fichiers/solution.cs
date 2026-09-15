// =====================================================================
// Solution — TP Module 12 : le carnet de contacts persistant
// Exécution : dotnet run solution.cs
// =====================================================================

// Les file-based apps visent l'AOT par défaut, ce qui coupe la sérialisation
// JSON par réflexion ; on la réactive (inutile dans un projet console classique).
#:property PublishAot=false

using System.Text.Json;

Console.WriteLine("=== TP Module 12 ===\n");

// ---------------------------------------------------------------
// Partie A — Apprivoiser le null
// ---------------------------------------------------------------
var alice = new Contact("Alice Martin", "06 12 34 56 78", "alice@mail.fr");
var bob = new Contact("Bob Durand", null, null);          // optionnels absents

Console.WriteLine(DecrireContact(alice));
Console.WriteLine(DecrireContact(bob));

Console.WriteLine($"\nNormaliser(\"06 12 34 56 78\") : {NormaliserTelephone("06 12 34 56 78")}");
Console.WriteLine($"Normaliser(\"   \")            : {NormaliserTelephone("   ") ?? "null"}");
Console.WriteLine($"Normaliser(null)             : {NormaliserTelephone(null) ?? "null"}");

// Question 5 : CS8602 = « déréférencement d'une référence possiblement
// nulle ». Le compilateur a suivi le flux et voit que Email peut être null
// à cet endroit : c.Email.Length peut lever NullReferenceException.

// ---------------------------------------------------------------
// Partie B — Chemins et fichiers texte
// ---------------------------------------------------------------
var contacts = new List<Contact>
{
    alice,
    bob,
    new("Chloé Petit", "07 98 76 54 32", null),
};

// Path.Combine gère le séparateur (/ ou \) selon l'OS
string dossier = Path.Combine(Path.GetTempPath(), "tp-contacts");
string cheminTexte = Path.Combine(dossier, "export.txt");
string cheminJson = Path.Combine(dossier, "contacts.json");

Directory.CreateDirectory(dossier);        // idempotent : ne plante pas si présent
Console.WriteLine($"\nDossier de travail : {dossier}");

// Écriture : une ligne par contact, null → champ vide
var lignes = contacts.Select(c => $"{c.Nom};{c.Telephone};{c.Email}");
File.WriteAllLines(cheminTexte, lignes);
Console.WriteLine($"export.txt écrit ({new FileInfo(cheminTexte).Length} octets)");

// Relecture : Split + champ vide → null
Console.WriteLine("Relecture du texte :");
if (File.Exists(cheminTexte))              // toujours vérifier avant de lire
{
    foreach (string ligne in File.ReadAllLines(cheminTexte))
    {
        string[] champs = ligne.Split(';');
        var relu = new Contact(
            champs[0],
            champs[1] == "" ? null : champs[1],
            champs[2] == "" ? null : champs[2]);
        Console.WriteLine($"  {DecrireContact(relu)}");
    }
}
// Question 9 : fichier absent → renvoyer une liste VIDE. Renvoyer null
// obligerait chaque appelant à re-vérifier : le null se propage comme
// une maladie ; la liste vide se manipule normalement.

// ---------------------------------------------------------------
// Partie C — Persistance JSON
// ---------------------------------------------------------------
Sauvegarder(contacts, cheminJson);

Console.WriteLine("\nJSON produit :");
Console.WriteLine(File.ReadAllText(cheminJson));
// Les propriétés nulles apparaissent comme "telephone": null — le JSON
// sait représenter l'absence.

// Rechargement dans une NOUVELLE variable : la preuve de la persistance
List<Contact> recharges = Charger(cheminJson);
Console.WriteLine($"Rechargés : {recharges.Count} contacts");
Console.WriteLine($"Premier identique à l'original ? {recharges[0] == contacts[0]} (record → égalité de contenu)");

// Fichier absent : liste vide, pas de crash
List<Contact> inexistants = Charger(Path.Combine(dossier, "absent.json"));
Console.WriteLine($"Fichier absent → {inexistants.Count} contact(s), pas de crash");

// ---------------------------------------------------------------
// Bonus
// ---------------------------------------------------------------
// ??= sur une variable locale (les propriétés d'un record positionnel sont init)
Console.WriteLine("\nBonus ??= :");
foreach (var c in recharges)
{
    string? tel = c.Telephone;
    tel ??= "(à renseigner)";              // n'affecte QUE si null
    Console.WriteLine($"  {c.Nom,-14} {tel}");
}

// JSON corrompu : JsonException interceptée par Charger
File.WriteAllText(cheminJson, "{ oops");
List<Contact> corrompus = Charger(cheminJson);
Console.WriteLine($"JSON corrompu → {corrompus.Count} contact(s), programme intact");

Directory.Delete(dossier, recursive: true);        // nettoyage du TP
Console.WriteLine("\n(Dossier temporaire supprimé.)");

// =====================================================================
// Méthodes
// =====================================================================

static string DecrireContact(Contact c)
{
    // ?? : valeur de repli si null ; ?. : accès sans crash
    string telephone = c.Telephone ?? "(pas de téléphone)";
    int? tailleEmail = c.Email?.Length;

    return $"{c.Nom} — {telephone} — email : " +
           (tailleEmail is null ? "(aucun)" : $"{c.Email} ({tailleEmail} car.)");
}

static string? NormaliserTelephone(string? brut)
{
    if (string.IsNullOrWhiteSpace(brut))
        return null;

    return brut.Replace(" ", "");
}

static void Sauvegarder(List<Contact> contacts, string chemin)
{
    var options = new JsonSerializerOptions { WriteIndented = true };
    string json = JsonSerializer.Serialize(contacts, options);
    File.WriteAllText(chemin, json);
    Console.WriteLine($"\n{contacts.Count} contacts sauvegardés dans {Path.GetFileName(chemin)}");
}

static List<Contact> Charger(string chemin)
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

// Nom obligatoire ; Telephone et Email optionnels : le ? le DIT au compilateur
record Contact(string Nom, string? Telephone, string? Email);
