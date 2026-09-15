// =====================================================================
// Module 4 — Méthodes (Jour 2, 9h15–10h45)
// Démo : signature, ref/out/params, paramètres optionnels et nommés,
//        surcharge, expression-bodied, récursivité
// Exécution : dotnet run module-04-methodes.cs
// =====================================================================

Console.WriteLine("=== Démo Module 4 : méthodes ===\n");

// ---------------------------------------------------------------
// 1. Méthode simple : signature = nom + paramètres (+ type de retour)
// ---------------------------------------------------------------
int somme = Additionner(3, 4);
Console.WriteLine($"Additionner(3, 4) = {somme}");

Saluer("Alice");                       // void : ne renvoie rien

// ---------------------------------------------------------------
// 2. Passage par VALEUR (défaut) vs par RÉFÉRENCE (ref)
// ---------------------------------------------------------------
int nombre = 10;
Doubler(nombre);                       // copie : l'original ne bouge pas
Console.WriteLine($"\nAprès Doubler(nombre)     : {nombre}  (inchangé, passage par valeur)");

DoublerRef(ref nombre);                // ref : la méthode modifie l'original
Console.WriteLine($"Après DoublerRef(ref n)   : {nombre}  (modifié !)");

// ---------------------------------------------------------------
// 3. out : renvoyer PLUSIEURS résultats (le modèle de TryParse)
// ---------------------------------------------------------------
if (DiviserEuclidien(17, 5, out int quotient, out int reste))
    Console.WriteLine($"\n17 = 5 × {quotient} + {reste}   (deux résultats via out)");

// ---------------------------------------------------------------
// 4. params : nombre variable d'arguments
// ---------------------------------------------------------------
Console.WriteLine($"\nMoyenne(12)          = {Moyenne(12)}");
Console.WriteLine($"Moyenne(10, 15, 20)  = {Moyenne(10, 15, 20)}");

// ---------------------------------------------------------------
// 5. Paramètres optionnels et arguments nommés
// ---------------------------------------------------------------
Console.WriteLine();
AfficherTicket("Café");                          // prix et quantité par défaut
AfficherTicket("Thé", 2.50m);
AfficherTicket("Jus", quantite: 3);              // argument NOMMÉ : on saute prix

// ---------------------------------------------------------------
// 6. Surcharge : même nom, paramètres différents
//    (dans une classe : les fonctions locales, elles, ne se surchargent pas)
// ---------------------------------------------------------------
Console.WriteLine($"\nGeometrie.Aire(5.0)      = {Geometrie.Aire(5.0):F2} (cercle)");
Console.WriteLine($"Geometrie.Aire(4.0, 3.0) = {Geometrie.Aire(4.0, 3.0):F2} (rectangle)");

// ---------------------------------------------------------------
// 7. Récursivité : une méthode qui s'appelle elle-même
// ---------------------------------------------------------------
Console.WriteLine($"\nFactorielle(5) = {Factorielle(5)}");
Console.WriteLine($"Fibonacci : {string.Join(", ", Enumerable.Range(0, 10).Select(Fibonacci))}");

Console.WriteLine("\n→ À retenir : out pour multi-retour, params pour n arguments, " +
                  "surcharge = même nom / signatures différentes.");

// =====================================================================
// Définitions (en file-based app, les méthodes/classes viennent APRÈS
// les top-level statements)
// =====================================================================

static int Additionner(int a, int b)
{
    return a + b;
}

// Expression-bodied : corps réduit à une expression avec =>
static void Saluer(string nom) => Console.WriteLine($"Bonjour, {nom} !");

static void Doubler(int valeur) => valeur *= 2;          // n'agit que sur la copie

static void DoublerRef(ref int valeur) => valeur *= 2;   // agit sur l'original

static bool DiviserEuclidien(int dividende, int diviseur, out int quotient, out int reste)
{
    if (diviseur == 0)
    {
        quotient = 0;
        reste = 0;
        return false;
    }
    quotient = dividende / diviseur;
    reste = dividende % diviseur;
    return true;
}

static double Moyenne(params int[] valeurs) => valeurs.Average();

static void AfficherTicket(string produit, decimal prix = 1.20m, int quantite = 1)
    => Console.WriteLine($"  {quantite} × {produit,-6} à {prix:C} = {quantite * prix:C}");

static long Factorielle(int n) => n <= 1 ? 1 : n * Factorielle(n - 1);

static int Fibonacci(int n) => n <= 1 ? n : Fibonacci(n - 1) + Fibonacci(n - 2);

// Surcharges : même nom, signatures différentes — le compilateur
// choisit selon les arguments. (Nécessite une classe : les fonctions
// locales des top-level statements ne peuvent pas être surchargées.)
static class Geometrie
{
    public static double Aire(double rayon) => Math.PI * rayon * rayon;
    public static double Aire(double largeur, double hauteur) => largeur * hauteur;
}
