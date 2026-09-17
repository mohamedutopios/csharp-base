// =====================================================================
// Solution — TP Module 2 : la caisse enregistreuse
// Exécution : dotnet run solution.cs
// =====================================================================

using System.Text;

Console.WriteLine("=== TP Module 2 : caisse enregistreuse ===\n");

// ---------------------------------------------------------------
// Partie A — Les bons types
// ---------------------------------------------------------------
string nomArticle = "Café moulu";
decimal prixUnitaire = 4.80m;      // decimal pour l'argent : exact (le m est obligatoire)
int quantite = 3;
bool enPromotion = false;

const double TauxTva = 0.20;       // const : connue à la compilation, jamais modifiée

decimal totalHt = prixUnitaire * quantite;
decimal montantTva = totalHt * (decimal)TauxTva;   // cast explicite double → decimal
decimal totalTtc = totalHt + montantTva;

Console.WriteLine($"Article   : {nomArticle} (promo : {enPromotion})");
Console.WriteLine($"Total HT  : {totalHt:C}");
Console.WriteLine($"TVA       : {montantTva:C}");
Console.WriteLine($"Total TTC : {totalTtc:C}");

// ---------------------------------------------------------------
// Partie B — Saisie sécurisée
// ---------------------------------------------------------------
Console.Write("\nQuantité souhaitée ? ");
string? saisie = Console.ReadLine();               // ReadLine peut renvoyer null

// TryParse : false si la saisie n'est pas un entier → pas de crash.
// On rejette aussi les valeurs négatives ou nulles.
if (!int.TryParse(saisie, out int quantiteSaisie) || quantiteSaisie <= 0)
{
    Console.WriteLine($"Saisie invalide (« {saisie} ») → quantité 1 par défaut.");
    quantiteSaisie = 1;
}

totalTtc = prixUnitaire * quantiteSaisie * (1 + (decimal)TauxTva);
Console.WriteLine($"{quantiteSaisie} × {prixUnitaire:C} = {totalTtc:C} TTC");

// Question 6 : int.Parse("abc") lève une FormatException et ARRÊTE le
// programme. TryParse renvoie false à la place : c'est pour ça qu'on
// l'utilise systématiquement sur les saisies utilisateur.

// ---------------------------------------------------------------
// Partie C — Le nom de l'article au microscope
// ---------------------------------------------------------------
string brut = "  café MOULU bio  ";

string propre = brut.Trim().ToLower();                       // "café moulu bio"
propre = char.ToUpper(propre[0]) + propre.Substring(1);      // "Café moulu bio"

Console.WriteLine($"\nNettoyé   : « {propre} »");
Console.WriteLine($"Longueur  : {propre.Length}");
Console.WriteLine($"Bio ?     : {propre.Contains("bio")}");
Console.WriteLine($"Tirets    : {propre.Replace(' ', '-')}");

// ---------------------------------------------------------------
// Partie D — Le ticket avec StringBuilder
// ---------------------------------------------------------------
// string est immuable : chaque += créerait une nouvelle chaîne.
// StringBuilder modifie un tampon interne : efficace ligne par ligne.
var sb = new StringBuilder();
sb.AppendLine("===== TICKET =====");
sb.AppendLine(propre);
sb.AppendLine($"{quantiteSaisie} x {prixUnitaire:C} ....... {prixUnitaire * quantiteSaisie:C}");
sb.AppendLine($"TVA ({TauxTva:P0}) ....... {prixUnitaire * quantiteSaisie * (decimal)TauxTva:C}");
sb.AppendLine($"TOTAL TTC ........ {totalTtc:C}");
sb.AppendLine("==================");

Console.WriteLine();
Console.WriteLine(sb.ToString());

// ---------------------------------------------------------------
// Partie E — Par valeur ou par référence ?
// ---------------------------------------------------------------
Console.WriteLine("\n--- Partie E : par valeur ou par référence ? ---");

// 11a. int est un type VALEUR : l'affectation COPIE la donnée
int prixA = 10;
int prixB = prixA;
prixB = 99;
Console.WriteLine($"int   : prixA = {prixA} (intact — prixB était une copie indépendante)");

// 11b. Un tableau est un type RÉFÉRENCE : l'affectation copie l'ADRESSE
int[] stockA = { 10, 20, 30 };
int[] stockB = stockA;          // deux variables… UN SEUL tableau
stockB[0] = 99;
Console.WriteLine($"int[] : stockA[0] = {stockA[0]} (modifié ! stockA et stockB désignent le même objet)");

// 12. En une phrase : affecter un type valeur COPIE la donnée ; affecter un
//     type référence copie l'ADRESSE, donc les deux variables désignent le
//     même OBJET et une modification via l'une se voit via l'autre.

// 13. string : type référence, mais nomB = "thé" ne MODIFIE pas l'objet
//     "café" — ça RÉ-AFFECTE la variable nomB vers un AUTRE objet.
//     (Et string est immuable : aucune méthode ne modifie l'objet en place.)
string nomA = "café";
string nomB = nomA;
nomB = "thé";
Console.WriteLine($"string: nomA = {nomA} (intact — on a ré-affecté nomB, pas modifié l'objet)");

// ---------------------------------------------------------------
// Bonus
// ---------------------------------------------------------------
Console.WriteLine($"\n0.1 + 0.2 == 0.3 en double  : {0.1 + 0.2 == 0.3}   (approximation binaire !)");
Console.WriteLine($"0.1 + 0.2 == 0.3 en decimal : {0.1m + 0.2m == 0.3m} → decimal pour l'argent");
Console.WriteLine($"Aligné à droite sur 10 : |{prixUnitaire,10:C}|");

int auMax = int.MaxValue;
Console.WriteLine($"int.MaxValue + 1 = {auMax + 1} (dépassement SILENCIEUX : repart à MinValue)");
// checked { Console.WriteLine(auMax + 1); }   // ← décommenter : OverflowException
