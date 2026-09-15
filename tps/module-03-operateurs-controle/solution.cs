// =====================================================================
// Solution — TP Module 3 : opérateurs et structures de contrôle
// Exécution : dotnet run solution.cs
// =====================================================================

Console.WriteLine("=== TP Module 3 ===");

// ---------------------------------------------------------------
// Partie D — Le menu en boucle (qui appelle les parties A, B, C)
// ---------------------------------------------------------------
bool quitter = false;

while (!quitter)
{
    Console.WriteLine("\n--- MENU ---");
    Console.WriteLine("1) FizzBuzz (1 à 30)");
    Console.WriteLine("2) Table de multiplication");
    Console.WriteLine("3) Statistiques des notes");
    Console.WriteLine("0) Quitter");
    Console.Write("Votre choix : ");

    string? choix = Console.ReadLine();

    switch (choix)
    {
        case "1":
            FizzBuzz();
            break;
        case "2":
            TableDeMultiplication();
            break;
        case "3":
            Statistiques();
            break;
        case "0":
            quitter = true;
            Console.WriteLine("Au revoir !");
            break;
        default:
            Console.WriteLine("Choix inconnu.");    // aucune saisie ne plante
            break;
    }
}

// ---------------------------------------------------------------
// Partie A — FizzBuzz
// ---------------------------------------------------------------
static void FizzBuzz()
{
    // Version 1 : if / else if / else
    // ATTENTION à l'ordre : tester 3 ET 5 en PREMIER, sinon jamais atteint !
    Console.WriteLine("\n-- if/else --");
    for (int n = 1; n <= 30; n++)
    {
        if (n % 3 == 0 && n % 5 == 0) Console.Write("FizzBuzz ");
        else if (n % 3 == 0)          Console.Write("Fizz ");
        else if (n % 5 == 0)          Console.Write("Buzz ");
        else                          Console.Write($"{n} ");
    }

    // Version 2 : switch expression sur un tuple de booléens
    Console.WriteLine("\n-- switch expression --");
    for (int n = 1; n <= 30; n++)
    {
        string resultat = (n % 3 == 0, n % 5 == 0) switch
        {
            (true, true)  => "FizzBuzz",
            (true, false) => "Fizz",
            (false, true) => "Buzz",
            _             => n.ToString()
        };
        Console.Write($"{resultat} ");
    }
    Console.WriteLine();
}

// ---------------------------------------------------------------
// Partie B — Table de multiplication
// ---------------------------------------------------------------
static void TableDeMultiplication()
{
    int nombre;

    // do/while : on demande AU MOINS une fois, et on redemande tant que c'est invalide
    bool valide;
    do
    {
        Console.Write("Nombre entre 1 et 10 ? ");
        valide = int.TryParse(Console.ReadLine(), out nombre)
                 && nombre >= 1 && nombre <= 10;
        if (!valide) Console.WriteLine("Saisie invalide, réessayez.");
    } while (!valide);

    for (int i = 1; i <= 10; i++)
        Console.WriteLine($"{nombre} x {i,2} = {nombre * i}");

    Console.WriteLine("Résultats pairs uniquement :");
    for (int i = 1; i <= 10; i++)
    {
        if (nombre * i % 2 != 0) continue;      // impair → on saute l'affichage
        Console.WriteLine($"{nombre} x {i,2} = {nombre * i}");
    }
}

// ---------------------------------------------------------------
// Partie C — Statistiques (foreach seulement, LINQ arrive au module 10)
// ---------------------------------------------------------------
static void Statistiques()
{
    int[] notes = { 12, 8, 17, 5, 14, 9, 20, 11 };

    int somme = 0;
    int min = notes[0];         // on part du premier élément, pas de 0 !
    int max = notes[0];
    int nbAdmis = 0;

    foreach (int note in notes)
    {
        somme += note;
        if (note < min) min = note;
        if (note > max) max = note;
        if (note >= 10) nbAdmis++;
    }

    double moyenne = (double)somme / notes.Length;   // cast sinon division entière !

    Console.WriteLine($"\nNotes      : {string.Join(", ", notes)}");
    Console.WriteLine($"Somme      : {somme}");
    Console.WriteLine($"Moyenne    : {moyenne:F2}");
    Console.WriteLine($"Min / Max  : {min} / {max}");
    Console.WriteLine($"Notes ≥ 10 : {nbAdmis} sur {notes.Length}");

    // Ternaire : condition ? valeurSiVrai : valeurSiFaux
    Console.WriteLine($"Verdict    : {(moyenne >= 10 ? "Admis" : "Ajourné")}");

    // Bonus : le sapin (boucles imbriquées)
    Console.WriteLine("\nBonus, sapin de hauteur 3 :");
    int hauteur = 3;
    for (int ligne = 1; ligne <= hauteur; ligne++)
    {
        // (hauteur - ligne) espaces puis (2*ligne - 1) étoiles
        Console.WriteLine(new string(' ', hauteur - ligne) + new string('*', 2 * ligne - 1));
    }
}
