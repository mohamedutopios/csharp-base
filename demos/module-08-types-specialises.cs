// =====================================================================
// Module 8 — Types spécialisés (Jour 3, 10h45–12h00)
// Démo : struct vs class, record, enum, tuples et déconstruction
// Exécution : dotnet run module-08-types-specialises.cs
// =====================================================================

Console.WriteLine("=== Démo Module 8 : types spécialisés ===\n");

// ---------------------------------------------------------------
// 1. struct vs class : copie de VALEUR vs partage de RÉFÉRENCE
// ---------------------------------------------------------------
var ps1 = new PointStruct(1, 1);
var ps2 = ps1;                 // COPIE complète (type valeur)
ps2.X = 99;
Console.WriteLine($"struct : ps1.X = {ps1.X} (intact), ps2.X = {ps2.X} → copies indépendantes");

var pc1 = new PointClass(1, 1);
var pc2 = pc1;                 // copie de la RÉFÉRENCE (même objet)
pc2.X = 99;
Console.WriteLine($"class  : pc1.X = {pc1.X} (modifié !), pc2.X = {pc2.X} → même objet partagé");
Console.WriteLine("→ struct pour les petites données « valeur » (point, montant, coordonnée).\n");

// ---------------------------------------------------------------
// 2. record : type conçu pour PORTER DES DONNÉES
// ---------------------------------------------------------------
var livre1 = new Livre("1984", "George Orwell", 1949);
var livre2 = new Livre("1984", "George Orwell", 1949);

// a) Égalité de CONTENU automatique (pas besoin d'overrider Equals)
Console.WriteLine($"record : livre1 == livre2 ? {livre1 == livre2} (égalité de contenu gratuite)");

// b) ToString lisible automatique
Console.WriteLine($"record : {livre1}");

// c) Immuable + « with » : créer une copie modifiée
var livreReedite = livre1 with { Annee = 2021 };
Console.WriteLine($"with   : {livreReedite}");
Console.WriteLine($"         l'original est intact : {livre1.Annee}\n");

// ---------------------------------------------------------------
// 3. enum : un ensemble FERMÉ de valeurs nommées
// ---------------------------------------------------------------
var etat = EtatEmprunt.EnCours;
Console.WriteLine($"enum : etat = {etat} (valeur entière sous-jacente : {(int)etat})");

// Fini les chaînes magiques "encours"/"en_cours"/"EnCours" : le compilateur vérifie
etat = EtatEmprunt.Rendu;

string description = etat switch          // enum + switch : duo parfait
{
    EtatEmprunt.EnCours  => "Le livre est sorti",
    EtatEmprunt.Rendu    => "Le livre est de retour",
    EtatEmprunt.EnRetard => "Relancer le membre !",
    _                    => "État inconnu"
};
Console.WriteLine($"switch sur enum : {description}");

// Lister toutes les valeurs :
Console.WriteLine($"Valeurs possibles : {string.Join(", ", Enum.GetNames<EtatEmprunt>())}\n");

// ---------------------------------------------------------------
// 4. Tuples : grouper des valeurs sans créer de type
// ---------------------------------------------------------------
(double min, double max) bornes = (2.5, 9.8);
Console.WriteLine($"tuple : min = {bornes.min}, max = {bornes.max}");

// Une méthode qui renvoie DEUX valeurs, sans out ni classe dédiée :
var stats = Analyser(new[] { 12, 7, 19, 3, 15 });
Console.WriteLine($"Analyser(...) → min {stats.Min}, max {stats.Max}, moyenne {stats.Moyenne:F1}");

// ---------------------------------------------------------------
// 5. Déconstruction : éclater un tuple (ou un record) en variables
// ---------------------------------------------------------------
var (min, max, moyenne) = Analyser(new[] { 100, 50, 75 });
Console.WriteLine($"Déconstruction : min={min}, max={max}, moyenne={moyenne:F1}");

var (titre, auteur, annee) = livre1;      // les records se déconstruisent aussi
Console.WriteLine($"Record déconstruit : « {titre} », {auteur}, {annee}");

// Astuce classique : échanger deux variables
int a = 1, b = 2;
(a, b) = (b, a);
Console.WriteLine($"Échange : a={a}, b={b}");

Console.WriteLine("\n→ À retenir : record pour les données, enum pour les états, " +
                  "tuple pour un retour multiple ponctuel.");

// Renvoie un TUPLE nommé : trois valeurs sans classe dédiée ni out.
// (Fonction locale : doit précéder les déclarations de types ci-dessous.)
static (int Min, int Max, double Moyenne) Analyser(int[] valeurs) =>
    (valeurs.Min(), valeurs.Max(), valeurs.Average());

// =====================================================================
// Types
// =====================================================================

struct PointStruct
{
    public int X, Y;
    public PointStruct(int x, int y) => (X, Y) = (x, y);
}

class PointClass
{
    public int X, Y;
    public PointClass(int x, int y) => (X, Y) = (x, y);
}

// UNE ligne = propriétés init, constructeur, Equals, GetHashCode,
// ToString, déconstruction… tout est généré.
record Livre(string Titre, string Auteur, int Annee);

enum EtatEmprunt
{
    EnCours,      // = 0
    Rendu,        // = 1
    EnRetard      // = 2
}
