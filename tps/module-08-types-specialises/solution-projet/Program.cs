// =====================================================================
// Solution — TP Module 8 (version projet multi-fichiers)
// Un type = un fichier : EtatColis.cs, Colis.cs, Dimensions.cs,
// PointRelais.cs. Program.cs = le scénario.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using TpColis;

Console.WriteLine("=== TP Module 8 (projet) ===\n");

// --- Partie A — enum ---
Console.WriteLine("États possibles :");
foreach (EtatColis e in Enum.GetValues<EtatColis>())
    Console.WriteLine($"  {e,-14} = {(int)e,3} → « {LibelleFrancais(e)} »");

// --- Partie B — record + with ---
var colis = new Colis("COL-001", "Alice Martin", 2.4, EtatColis.EnPreparation);

Console.WriteLine("\nCycle de vie (chaque étape est une COPIE via with) :");
var expedie = colis with { Etat = EtatColis.Expedie };
var livre = expedie with { Etat = EtatColis.Livre };
Console.WriteLine($"  {colis}");
Console.WriteLine($"  {expedie}");
Console.WriteLine($"  {livre}");
Console.WriteLine($"L'original n'a pas bougé : {colis.Etat}");

var jumeau = new Colis("COL-001", "Alice Martin", 2.4, EtatColis.EnPreparation);
Console.WriteLine($"colis == jumeau : {colis == jumeau} (mêmes valeurs → égaux)");

// --- Partie C — struct : sémantique de valeur ---
var d1 = new Dimensions { LongueurCm = 40, LargeurCm = 30, HauteurCm = 20 };
var d2 = d1;                    // COPIE complète (type valeur)
d2.LongueurCm = 999;
Console.WriteLine($"\nVolume : {d1.VolumeLitres} L | d1.LongueurCm = {d1.LongueurCm} (intact)");

// --- Partie D — tuples et déconstruction ---
var tournee = new List<Colis>
{
    livre,
    new("COL-002", "Bob Durand", 1.1, EtatColis.Livre),
    new("COL-003", "Chloé Petit", 5.7, EtatColis.EnTransit),
};

var (total, livres, poids) = AnalyserTournee(tournee);
Console.WriteLine($"\nTournée : {total} colis, {livres} livrés, {poids:F1} kg au total");

var (numero, destinataire, _, etatFinal) = livre;
Console.WriteLine($"Déconstruit : {numero} pour {destinataire}, état {LibelleFrancais(etatFinal)}");

// --- Bonus — record struct ---
var p1 = new PointRelais("PR-07", "Lyon");
var p2 = new PointRelais("PR-07", "Lyon");
Console.WriteLine($"\nrecord struct : p1 == p2 ? {p1 == p2}");

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
