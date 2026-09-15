// =====================================================================
// Solution — TP Module 4 : la boîte à outils du développeur
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 4 ===\n");

// --- Partie A ---
Console.WriteLine($"EstPair(4) : {EstPair(4)} | EstPair(7) : {EstPair(7)}");
Console.WriteLine($"Repeter(\"ab\", 3) : {Repeter("ab", 3)}");
AfficherEncadre("Bonjour");

// --- Partie B ---
if (TryDiviser(17, 5, out int q, out int r))
    Console.WriteLine($"\n17 / 5 → quotient {q}, reste {r}");

if (!TryDiviser(17, 0, out _, out _))          // _ : on ignore les sorties
    Console.WriteLine("17 / 0 → division impossible, gérée sans crash");

// --- Partie C ---
Console.WriteLine($"\nMoyenne()            : {Moyenne()}");
Console.WriteLine($"Moyenne(10, 15, 20)  : {Moyenne(10, 15, 20):F2}");
Console.WriteLine($"FormaterPrix(12.5m)                        : {FormaterPrix(12.5m)}");
Console.WriteLine($"FormaterPrix(12.5m, \"$\", avantLeNombre..) : {FormaterPrix(12.5m, "$", avantLeNombre: true)}");

// --- Partie D ---
Console.WriteLine($"\n{Outils.Decrire(42)}");
Console.WriteLine(Outils.Decrire("bonjour"));
Console.WriteLine(Outils.Decrire(new[] { 1, 2, 3 }));

// --- Partie E ---
Console.WriteLine($"\nPuissance(2, 10) : {Puissance(2, 10)}");
Console.WriteLine($"Puissance(5, 0)  : {Puissance(5, 0)}");
Console.Write("CompteARebours(5) : ");
CompteARebours(5);

// --- Bonus ---
Console.WriteLine($"\nInverser(\"chat\")  : {Inverser("chat")}");
Console.WriteLine($"Inverser(\"radar\") : {Inverser("radar")} (palindrome !)");
Console.WriteLine($"Puissance(2, -2)  : {Puissance(2, -2)} (cas négatif géré : 1/x^n)");

// =====================================================================
// Partie A — Bases
// =====================================================================

// Expression-bodied : le corps est une seule expression
static bool EstPair(int n) => n % 2 == 0;

static string Repeter(string texte, int fois)
{
    var resultat = "";
    for (int i = 0; i < fois; i++)
        resultat += texte;
    return resultat;
    // (variante maligne : return string.Concat(Enumerable.Repeat(texte, fois));)
}

static void AfficherEncadre(string titre)
{
    string bordure = "+" + new string('-', titre.Length + 4) + "+";
    Console.WriteLine(bordure);
    Console.WriteLine($"|  {titre}  |");
    Console.WriteLine(bordure);
}

// =====================================================================
// Partie B — out : le modèle de TryParse
// =====================================================================

static bool TryDiviser(int a, int b, out int quotient, out int reste)
{
    // Contrat de out : TOUTES les sorties doivent être affectées avant return
    if (b == 0)
    {
        quotient = 0;
        reste = 0;
        return false;
    }
    quotient = a / b;
    reste = a % b;
    return true;
}

// =====================================================================
// Partie C — params et paramètres optionnels
// =====================================================================

static double Moyenne(params double[] valeurs)
{
    if (valeurs.Length == 0) return 0;

    double somme = 0;
    foreach (double v in valeurs)
        somme += v;
    return somme / valeurs.Length;
}

static string FormaterPrix(decimal prix, string devise = "€", bool avantLeNombre = false) =>
    avantLeNombre ? $"{devise} {prix:F2}" : $"{prix:F2} {devise}";

// =====================================================================
// Partie D — Surcharge : même nom, signatures différentes.
// (Dans une classe : les fonctions locales ne se surchargent pas.)
// =====================================================================

// (la classe Outils est déclarée tout en bas : les types doivent venir
//  APRÈS toutes les instructions et fonctions locales)

// =====================================================================
// Partie E — Récursivité : cas de base + cas récursif
// =====================================================================

static double Puissance(double x, int n)
{
    if (n == 0) return 1;                       // cas de base
    if (n < 0) return 1 / Puissance(x, -n);     // bonus : exposant négatif
    return x * Puissance(x, n - 1);             // cas récursif
}

static void CompteARebours(int n)
{
    if (n == 0)                                 // cas de base : on s'arrête
    {
        Console.WriteLine("Décollage !");
        return;
    }
    Console.Write($"{n}, ");
    CompteARebours(n - 1);                      // la méthode s'appelle elle-même
}

static string Inverser(string texte) =>
    texte.Length <= 1 ? texte : Inverser(texte[1..]) + texte[0];

static class Outils
{
    public static string Decrire(int n) => $"entier : {n}";
    public static string Decrire(string s) => $"chaîne de {s.Length} caractères";
    public static string Decrire(int[] t) => $"tableau de {t.Length} éléments";
}
