// =====================================================================
// Module 7 — Démo (version projet multi-fichiers)
// Program.cs = le scénario ; les contrats et classes vivent dans
// INotifiable.cs, Email.cs, Sms.cs, Drone.cs.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using DemoInterfaces;

Console.WriteLine("=== Démo Module 7 : interfaces et polymorphisme (projet) ===\n");

// 1. Une interface = un CONTRAT : « je sais faire », pas « je suis »
var canaux = new List<INotifiable>
{
    new Email("contact@exemple.fr"),
    new Sms("06 12 34 56 78"),
    new Email("admin@exemple.fr")
};

foreach (INotifiable canal in canaux)          // polymorphisme PAR INTERFACE
    canal.Envoyer("Votre commande est prête !");

// 2. Une classe peut implémenter PLUSIEURS interfaces
var drone = new Drone();
drone.Voler();
drone.Rouler();      // Drone est à la fois IVolant et IRoulant
Console.WriteLine();

// 3. is : tester le type réel (avec déclaration de variable)
object mystere = new Email("test@exemple.fr");

if (mystere is Email email)                     // test + cast en une fois
    Console.WriteLine($"is : c'est un Email vers {email.Adresse}");

if (mystere is not Sms)
    Console.WriteLine("is not : ce n'est pas un Sms");

// 4. as : cast « doux » qui renvoie null au lieu de planter
Sms? peutEtreSms = mystere as Sms;              // null ici, pas d'exception
Console.WriteLine($"as : mystere as Sms = {(peutEtreSms is null ? "null" : "un Sms")}");
// Sms crash = (Sms)mystere;                    // ← InvalidCastException !

// 5. Pattern matching sur types avec switch
Console.WriteLine("\n--- Pattern matching sur les types ---");
object[] objets = { new Email("a@b.fr"), new Sms("07 00 00 00 00"), 42, "bonjour", null! };

foreach (object? o in objets)
{
    string description = o switch
    {
        Email e            => $"Email → {e.Adresse}",
        Sms s              => $"SMS → {s.Numero}",
        int i when i > 10  => $"Grand entier : {i}",       // pattern + condition (when)
        int i              => $"Petit entier : {i}",
        string texte       => $"Chaîne de {texte.Length} caractères",
        null               => "null !",
        _                  => $"Type inconnu : {o.GetType().Name}"
    };
    Console.WriteLine($"  {description}");
}

Console.WriteLine("\n→ À retenir : héritage = « est un », interface = « sait faire » ; " +
                  "en projet, les interfaces ont leur fichier comme les classes.");
