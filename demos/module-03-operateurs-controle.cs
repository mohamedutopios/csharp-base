// =====================================================================
// Module 3 — Opérateurs et structures de contrôle (Jour 1, 14h30–16h00)
// Démo : opérateurs, ??, ?., ternaire, if/switch, boucles, break/continue
// Exécution : dotnet run module-03-operateurs-controle.cs
// =====================================================================

Console.WriteLine("=== Démo Module 3 : opérateurs et structures de contrôle ===\n");

// ---------------------------------------------------------------
// 1. Opérateurs arithmétiques — le piège de la division entière
// ---------------------------------------------------------------
Console.WriteLine($"7 / 2   = {7 / 2}    (division ENTIÈRE : les deux opérandes sont int)");
Console.WriteLine($"7 / 2.0 = {7 / 2.0}  (dès qu'un opérande est double → division réelle)");
Console.WriteLine($"7 % 2   = {7 % 2}    (modulo : le reste)");
Console.WriteLine($"10 % 2 == 0 → nombre pair ? {10 % 2 == 0}");

int n = 5;
Console.WriteLine($"\nn++ post-incrément : affiche {n++} puis n vaut {n}");
Console.WriteLine($"++n pré-incrément  : affiche {++n}");

// ---------------------------------------------------------------
// 2. Opérateurs logiques (&&, ||, !) — évaluation court-circuit
// ---------------------------------------------------------------
int age = 25;
bool aPermis = true;
Console.WriteLine($"\nage >= 18 && aPermis : {age >= 18 && aPermis}");
// && s'arrête dès qu'un false apparaît ; || dès qu'un true apparaît.
// C'est ce qui rend sûr : if (texte != null && texte.Length > 0)

// ---------------------------------------------------------------
// 3. Ternaire, ?? (null-coalescing) et ?. (null-conditionnel)
// ---------------------------------------------------------------
string statut = age >= 18 ? "majeur" : "mineur";      // ternaire
Console.WriteLine($"\nTernaire : {age} ans → {statut}");

string? surnom = null;
string affichage = surnom ?? "(aucun surnom)";        // ?? : valeur de repli si null
Console.WriteLine($"?? : {affichage}");

int? longueur = surnom?.Length;                       // ?. : ne déréférence pas si null
Console.WriteLine($"?. : surnom?.Length = {(longueur.HasValue ? longueur : "null")} (pas de crash !)");

// ---------------------------------------------------------------
// 4. if / else if / else
// ---------------------------------------------------------------
int note = 14;
if (note >= 16)      Console.WriteLine("\nMention très bien");
else if (note >= 14) Console.WriteLine("\nMention bien");
else if (note >= 10) Console.WriteLine("\nAdmis");
else                 Console.WriteLine("\nAjourné");

// ---------------------------------------------------------------
// 5. switch INSTRUCTION (classique) vs switch EXPRESSION (moderne)
// ---------------------------------------------------------------
string jour = "samedi";

// Forme instruction
switch (jour)
{
    case "samedi":
    case "dimanche":
        Console.WriteLine($"\n{jour} : week-end !");
        break;
    default:
        Console.WriteLine($"\n{jour} : jour ouvré");
        break;
}

// Forme expression : renvoie une valeur, plus concise
string categorie = note switch
{
    >= 16 => "très bien",
    >= 14 => "bien",
    >= 10 => "admis",
    _     => "ajourné"          // _ = cas par défaut, obligatoire ici
};
Console.WriteLine($"switch expression : note {note} → {categorie}");

// ---------------------------------------------------------------
// 6. Boucles : for, while, do-while, foreach
// ---------------------------------------------------------------
Console.Write("\nfor      : ");
for (int i = 1; i <= 5; i++)
    Console.Write($"{i} ");

Console.Write("\nwhile    : ");
int compte = 5;
while (compte > 0)
{
    Console.Write($"{compte} ");
    compte--;
}

Console.Write("\ndo-while : ");
int k = 10;
do
{
    Console.Write($"{k} ");     // exécuté AU MOINS une fois, même si condition fausse
    k++;
} while (k < 10);

Console.Write("\nforeach  : ");
string[] villes = { "Paris", "Lyon", "Nantes" };
foreach (string ville in villes)   // pas d'indice, pas de dépassement possible
    Console.Write($"{ville} ");
Console.WriteLine();

// ---------------------------------------------------------------
// 7. break et continue
// ---------------------------------------------------------------
Console.Write("\ncontinue (sauter les pairs) : ");
for (int i = 1; i <= 10; i++)
{
    if (i % 2 == 0) continue;   // passe directement à l'itération suivante
    Console.Write($"{i} ");
}

Console.Write("\nbreak (stop au premier > 6) : ");
for (int i = 1; i <= 10; i++)
{
    if (i > 6) break;           // sort complètement de la boucle
    Console.Write($"{i} ");
}
Console.WriteLine();

// ---------------------------------------------------------------
// 8. Mini-synthèse : le squelette du TP 1 (menu en boucle)
// ---------------------------------------------------------------
Console.WriteLine("\n--- Squelette de menu (le modèle du TP 1) ---");
bool quitter = false;
var choixSimules = new Queue<string>(["1", "9", "0"]);   // simule des saisies

while (!quitter)
{
    Console.WriteLine("1) Dire bonjour  0) Quitter");
    string choix = choixSimules.Dequeue();               // en vrai : Console.ReadLine()
    Console.WriteLine($"> {choix}");

    switch (choix)
    {
        case "1": Console.WriteLine("Bonjour !"); break;
        case "0": quitter = true; break;
        default:  Console.WriteLine("Choix inconnu, réessayez."); break;
    }
}
Console.WriteLine("Au revoir !");

Console.WriteLine("\n→ À retenir : 7/2=3, switch expression pour mapper une valeur, foreach par défaut.");
