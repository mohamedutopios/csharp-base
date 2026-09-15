// =====================================================================
// Module 7 — Interfaces et polymorphisme (Jour 3, 9h15–10h45)
// Démo : déclaration/implémentation, interface vs héritage,
//        is / as, pattern matching sur types
// Exécution : dotnet run module-07-interfaces-polymorphisme.cs
// Version projet (une classe = un fichier) : module-07-interfaces-polymorphisme-projet/
// =====================================================================

Console.WriteLine("=== Démo Module 7 : interfaces et polymorphisme ===\n");

// ---------------------------------------------------------------
// 1. Une interface = un CONTRAT : « je sais faire », pas « je suis »
// ---------------------------------------------------------------
// Email et Sms n'ont AUCUN parent commun, mais remplissent le même contrat.
var canaux = new List<INotifiable>
{
    new Email("contact@exemple.fr"),
    new Sms("06 12 34 56 78"),
    new Email("admin@exemple.fr")
};

foreach (INotifiable canal in canaux)          // polymorphisme PAR INTERFACE
    canal.Envoyer("Votre commande est prête !");

// ---------------------------------------------------------------
// 2. Une classe peut implémenter PLUSIEURS interfaces
//    (alors qu'elle n'hérite que d'UNE classe)
// ---------------------------------------------------------------
var drone = new Drone();
drone.Voler();
drone.Rouler();      // Drone est à la fois IVolant et IRoulant
Console.WriteLine();

// ---------------------------------------------------------------
// 3. is : tester le type réel (avec déclaration de variable)
// ---------------------------------------------------------------
object mystere = new Email("test@exemple.fr");

if (mystere is Email email)                     // test + cast en une fois
    Console.WriteLine($"is : c'est un Email vers {email.Adresse}");

if (mystere is not Sms)
    Console.WriteLine("is not : ce n'est pas un Sms");

// ---------------------------------------------------------------
// 4. as : cast « doux » qui renvoie null au lieu de planter
// ---------------------------------------------------------------
Sms? peutEtreSms = mystere as Sms;              // null ici, pas d'exception
Console.WriteLine($"as : mystere as Sms = {(peutEtreSms is null ? "null" : "un Sms")}");
// Sms crash = (Sms)mystere;                    // ← InvalidCastException !

// ---------------------------------------------------------------
// 5. Pattern matching sur types avec switch
// ---------------------------------------------------------------
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
                  "is pour tester, as pour tenter, switch pour aiguiller par type.");

// =====================================================================
// Interfaces et classes
// =====================================================================

// Convention : les interfaces commencent par I
interface INotifiable
{
    void Envoyer(string message);       // pas de corps : c'est un contrat
}

class Email : INotifiable               // « : » = implémente
{
    public string Adresse { get; }
    public Email(string adresse) => Adresse = adresse;

    public void Envoyer(string message) =>
        Console.WriteLine($"[EMAIL → {Adresse}] {message}");
}

class Sms : INotifiable
{
    public string Numero { get; }
    public Sms(string numero) => Numero = numero;

    public void Envoyer(string message) =>
        Console.WriteLine($"[SMS → {Numero}] {message}");
}

interface IVolant { void Voler(); }
interface IRoulant { void Rouler(); }

// Implémentation MULTIPLE : impossible avec l'héritage de classes
class Drone : IVolant, IRoulant
{
    public void Voler() => Console.WriteLine("Le drone décolle.");
    public void Rouler() => Console.WriteLine("Le drone roule au sol.");
}
