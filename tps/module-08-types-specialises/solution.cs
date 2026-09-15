// =====================================================================
// Solution — TP Module 8 : le suivi de colis
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 8 ===\n");

// ---------------------------------------------------------------
// Partie A — enum
// ---------------------------------------------------------------
Console.WriteLine("États possibles :");
foreach (EtatColis etat in Enum.GetValues<EtatColis>())
    Console.WriteLine($"  {etat,-14} = {(int)etat,3} → « {LibelleFrancais(etat)} »");

// ---------------------------------------------------------------
// Partie B — record + with
// ---------------------------------------------------------------
var colis = new Colis("COL-001", "Alice Martin", 2.4, EtatColis.EnPreparation);

Console.WriteLine("\nCycle de vie (chaque étape est une COPIE via with) :");
Console.WriteLine($"  {colis}");                       // ToString généré par le record

var expedie = colis with { Etat = EtatColis.Expedie };
var enTransit = expedie with { Etat = EtatColis.EnTransit };
var livre = enTransit with { Etat = EtatColis.Livre };
Console.WriteLine($"  {expedie}");
Console.WriteLine($"  {enTransit}");
Console.WriteLine($"  {livre}");
Console.WriteLine($"L'original n'a pas bougé : {colis.Etat}");

// Égalité de contenu
var jumeau = new Colis("COL-001", "Alice Martin", 2.4, EtatColis.EnPreparation);
Console.WriteLine($"\ncolis == jumeau            : {colis == jumeau} (mêmes valeurs → égaux)");
Console.WriteLine($"colis == (colis with {{ }})  : {colis == (colis with { })} (copie identique → égale)");
// Question 7 : un record fournit gratuitement l'égalité de contenu, un
// ToString lisible, l'immuabilité et with — exactement ce qu'il faut pour
// une donnée qu'on suit et compare. Une class ne donne rien de tout ça.

// ---------------------------------------------------------------
// Partie C — struct : sémantique de valeur
// ---------------------------------------------------------------
var d1 = new Dimensions { LongueurCm = 40, LargeurCm = 30, HauteurCm = 20 };
Console.WriteLine($"\nVolume : {d1.VolumeLitres} L");

var d2 = d1;                    // COPIE complète (type valeur)
d2.LongueurCm = 999;
Console.WriteLine($"d1.LongueurCm = {d1.LongueurCm} (intact) | d2.LongueurCm = {d2.LongueurCm}");
// Question 10 : Dimensions est une petite donnée « valeur » : copier est
// naturel et peu coûteux, on ne veut PAS que deux variables partagent le
// même objet. C'est le cas d'école du struct.

// ---------------------------------------------------------------
// Partie D — tuples et déconstruction
// ---------------------------------------------------------------
var tournee = new List<Colis>
{
    livre,
    new("COL-002", "Bob Durand", 1.1, EtatColis.Livre),
    new("COL-003", "Chloé Petit", 5.7, EtatColis.EnTransit),
    new("COL-004", "David Lopez", 0.8, EtatColis.Retourne),
};

// Retour multiple sans classe dédiée : le tuple nommé
var (total, livres, poids) = AnalyserTournee(tournee);
Console.WriteLine($"\nTournée : {total} colis, {livres} livrés, {poids:F1} kg au total");

// Déconstruction d'un record ; _ ignore une valeur
var (numero, destinataire, _, etatFinal) = livre;
Console.WriteLine($"Déconstruit : {numero} pour {destinataire}, état {LibelleFrancais(etatFinal)}");

// ---------------------------------------------------------------
// Bonus — record struct
// ---------------------------------------------------------------
var p1 = new PointRelais("PR-07", "Lyon");
var p2 = new PointRelais("PR-07", "Lyon");
Console.WriteLine($"\nrecord struct : p1 == p2 ? {p1 == p2} (égalité de contenu + copie par valeur)");

// =====================================================================
// Méthodes et types
// =====================================================================

static string LibelleFrancais(EtatColis etat) => etat switch
{
    EtatColis.EnPreparation => "en préparation",
    EtatColis.Expedie       => "expédié",
    EtatColis.EnTransit     => "en transit",
    EtatColis.Livre         => "livré",
    EtatColis.Retourne      => "retourné à l'expéditeur",
    _                       => "état inconnu"
};

static (int Total, int Livres, double PoidsTotal) AnalyserTournee(List<Colis> colis)
{
    int livres = 0;
    double poids = 0;
    foreach (var c in colis)
    {
        if (c.Etat == EtatColis.Livre) livres++;
        poids += c.PoidsKg;
    }
    return (colis.Count, livres, poids);
}

// Bonus : valeurs explicites espacées de 10 → on peut intercaler plus tard
enum EtatColis
{
    EnPreparation = 10,
    Expedie = 20,
    EnTransit = 30,
    Livre = 40,
    Retourne = 50
}

// Record positionnel : propriétés init, constructeur, égalité, ToString,
// déconstruction… générés en une ligne.
record Colis(string Numero, string Destinataire, double PoidsKg, EtatColis Etat);

struct Dimensions
{
    public double LongueurCm { get; set; }
    public double LargeurCm { get; set; }
    public double HauteurCm { get; set; }

    public double VolumeLitres => LongueurCm * LargeurCm * HauteurCm / 1000;
}

// record struct : égalité de contenu du record + sémantique de valeur du struct
record struct PointRelais(string Code, string Ville);
