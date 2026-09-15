// =====================================================================
// Module 8 — Démo (version projet multi-fichiers)
// Chaque type a son fichier : Livre.cs (record), EtatEmprunt.cs (enum),
// PointStruct.cs, PointClass.cs. Program.cs = le scénario.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using DemoTypes;

Console.WriteLine("=== Démo Module 8 : types spécialisés (projet) ===\n");

// 1. struct vs class : copie de VALEUR vs partage de RÉFÉRENCE
var ps1 = new PointStruct(1, 1);
var ps2 = ps1;                 // COPIE complète (type valeur)
ps2.X = 99;
Console.WriteLine($"struct : ps1.X = {ps1.X} (intact), ps2.X = {ps2.X} → copies indépendantes");

var pc1 = new PointClass(1, 1);
var pc2 = pc1;                 // copie de la RÉFÉRENCE (même objet)
pc2.X = 99;
Console.WriteLine($"class  : pc1.X = {pc1.X} (modifié !), pc2.X = {pc2.X} → même objet partagé\n");

// 2. record : type conçu pour PORTER DES DONNÉES
var livre1 = new Livre("1984", "George Orwell", 1949);
var livre2 = new Livre("1984", "George Orwell", 1949);

Console.WriteLine($"record : livre1 == livre2 ? {livre1 == livre2} (égalité de contenu gratuite)");
Console.WriteLine($"record : {livre1}");

var livreReedite = livre1 with { Annee = 2021 };
Console.WriteLine($"with   : {livreReedite} — l'original est intact : {livre1.Annee}\n");

// 3. enum : un ensemble FERMÉ de valeurs nommées
var etat = EtatEmprunt.Rendu;
string description = etat switch
{
    EtatEmprunt.EnCours  => "Le livre est sorti",
    EtatEmprunt.Rendu    => "Le livre est de retour",
    EtatEmprunt.EnRetard => "Relancer le membre !",
    _                    => "État inconnu"
};
Console.WriteLine($"enum + switch : {description}");
Console.WriteLine($"Valeurs possibles : {string.Join(", ", Enum.GetNames<EtatEmprunt>())}\n");

// 4. Tuples et déconstruction
var stats = Analyser(new[] { 12, 7, 19, 3, 15 });
Console.WriteLine($"Analyser(...) → min {stats.Min}, max {stats.Max}, moyenne {stats.Moyenne:F1}");

var (titre, auteur, annee) = livre1;      // les records se déconstruisent aussi
Console.WriteLine($"Record déconstruit : « {titre} », {auteur}, {annee}");

Console.WriteLine("\n→ À retenir : en projet, record et enum ont leur fichier " +
                  "comme les classes — un type = un fichier.");

// Le tuple nommé : retour multiple ponctuel, pas besoin de type dédié
static (int Min, int Max, double Moyenne) Analyser(int[] valeurs) =>
    (valeurs.Min(), valeurs.Max(), valeurs.Average());
