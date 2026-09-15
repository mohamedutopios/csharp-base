// =====================================================================
// Module 2 — Types et variables (Jour 1, 11h00–14h30)
// Démo : types valeur/référence, var, conversions, Parse/TryParse,
//        chaînes, interpolation, StringBuilder, Console
// Exécution : dotnet run module-02-types-variables.cs
// =====================================================================

using System.Text;

Console.WriteLine("=== Démo Module 2 : types et variables ===\n");

// ---------------------------------------------------------------
// 1. Types primitifs (types VALEUR : la variable contient la donnée)
// ---------------------------------------------------------------
int age = 30;                 // entier 32 bits
long population = 8_000_000_000; // entier 64 bits (séparateur _ pour lisibilité)
double prix = 19.99;          // flottant 64 bits (approximatif !)
decimal solde = 1234.56m;     // décimal exact → pour l'argent, toujours decimal
bool actif = true;
char initiale = 'M';

Console.WriteLine($"int: {age} | long: {population} | double: {prix} | decimal: {solde} | bool: {actif} | char: {initiale}");

// Chaque type a des bornes :
Console.WriteLine($"int va de {int.MinValue} à {int.MaxValue}");

// Piège classique du double (à montrer absolument) :
Console.WriteLine($"\n0.1 + 0.2 == 0.3 ?  {0.1 + 0.2 == 0.3}  (double = approximation binaire)");
Console.WriteLine($"En decimal : {0.1m + 0.2m == 0.3m}  → pour l'argent : decimal !");

// ---------------------------------------------------------------
// 2. var : inférence de type (le type est FIXÉ à la compilation)
// ---------------------------------------------------------------
var message = "Bonjour";   // string, décidé par le compilateur
var compteur = 0;          // int
// compteur = "texte";     // ← ERREUR de compilation : var ≠ typage dynamique !
Console.WriteLine($"\nvar message est un {message.GetType().Name}, compteur un {compteur.GetType().Name}");

// ---------------------------------------------------------------
// 3. Constantes
// ---------------------------------------------------------------
const double TauxTva = 0.20;
// TauxTva = 0.10;         // ← ERREUR : une const ne change jamais
Console.WriteLine($"TVA : {TauxTva:P0}");

// ---------------------------------------------------------------
// 4. Conversions
// ---------------------------------------------------------------
int petit = 42;
long grand = petit;           // implicite : aucun risque de perte
double d = petit;             // implicite aussi

double pi = 3.99;
int tronque = (int)pi;        // explicite (cast) : perte assumée → 3, pas 4 !
Console.WriteLine($"\n(int)3.99 = {tronque}  (troncature, pas d'arrondi)");
Console.WriteLine($"Math.Round(3.99) = {Math.Round(3.99)}");

// ---------------------------------------------------------------
// 5. Parse vs TryParse : convertir du texte en nombre
// ---------------------------------------------------------------
string saisieOk = "123";
int valeur = int.Parse(saisieOk);          // OK… mais lève une exception si invalide
Console.WriteLine($"\nint.Parse(\"123\") = {valeur}");

string saisieUtilisateur = "abc";          // ce qu'un utilisateur peut vraiment taper
// int.Parse(saisieUtilisateur);           // ← FormatException : crash !

// TryParse = la façon SÛRE : renvoie false au lieu de planter
if (int.TryParse(saisieUtilisateur, out int resultat))
    Console.WriteLine($"Conversion réussie : {resultat}");
else
    Console.WriteLine($"\"{saisieUtilisateur}\" n'est pas un nombre → TryParse renvoie false, pas de crash");

// ---------------------------------------------------------------
// 6. Types référence : la variable contient une ADRESSE
// ---------------------------------------------------------------
int[] a = { 1, 2, 3 };
int[] b = a;              // b pointe vers LE MÊME tableau
b[0] = 99;
Console.WriteLine($"\na[0] = {a[0]}  ← modifié via b : a et b référencent le même objet !");

int x = 1;
int y = x;                // copie de la VALEUR
y = 99;
Console.WriteLine($"x = {x}  ← intact : les types valeur sont copiés");

// ---------------------------------------------------------------
// 7. Chaînes : immuables + méthodes usuelles
// ---------------------------------------------------------------
string nom = "  Marie Curie  ";
Console.WriteLine($"\nLength: {nom.Length} | Trim: '{nom.Trim()}' | Upper: {nom.ToUpper()}");
Console.WriteLine($"Contains(\"Curie\"): {nom.Contains("Curie")} | Replace: {nom.Trim().Replace("Marie", "Ève")}");
Console.WriteLine($"Substring(2, 5): '{nom.Substring(2, 5)}'");

string csv = "pomme;poire;banane";
string[] fruits = csv.Split(';');
Console.WriteLine($"Split → {fruits.Length} éléments, le 2e est '{fruits[1]}'");
Console.WriteLine($"Join  → {string.Join(" | ", fruits)}");

// Interpolation ($) et formats
double montant = 1234.5678;
Console.WriteLine($"\nFormats : {montant:F2} (F2) | {montant:C} (C) | {montant:N0} (N0) | {0.856:P1} (P1)");

// ---------------------------------------------------------------
// 8. StringBuilder : concaténation efficace en boucle
// ---------------------------------------------------------------
// string est immuable : chaque « += » crée une NOUVELLE chaîne en mémoire.
var sb = new StringBuilder();
for (int i = 1; i <= 5; i++)
    sb.Append($"[{i}]");
Console.WriteLine($"\nStringBuilder : {sb}");

// ---------------------------------------------------------------
// 9. Console.ReadLine : entrée utilisateur (décommenter en démo live)
// ---------------------------------------------------------------
// Console.Write("Votre âge ? ");
// string? reponse = Console.ReadLine();          // peut être null !
// if (int.TryParse(reponse, out int ageSaisi))
//     Console.WriteLine($"Dans 10 ans vous aurez {ageSaisi + 10} ans.");
// else
//     Console.WriteLine("Saisie invalide.");

Console.WriteLine("\n→ À retenir : decimal pour l'argent, TryParse pour les saisies, string est immuable.");
