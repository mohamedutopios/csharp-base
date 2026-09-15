// =====================================================================
// Program.cs — le POINT D'ENTRÉE du projet (top-level statements).
// Un seul fichier du projet peut en avoir ; les classes vivent chacune
// dans leur fichier (CompteBancaire.cs, Banque.cs) et sont compilées
// automatiquement avec celui-ci : `dotnet run` prend tous les .cs.
// =====================================================================

using TpBanque;   // le namespace où vivent nos classes

Console.WriteLine("=== TP Module 5 (version projet multi-fichiers) ===\n");

// --- Parties A & B — création et comportements ---
var compte = new CompteBancaire("Alice Martin", 100m);
compte.Afficher();

compte.Deposer(75m);
Console.WriteLine("Après dépôt de 75 € :");
compte.Afficher();

Console.WriteLine($"Retrait de 500 € accepté ? {compte.Retirer(500m)}");
Console.WriteLine($"Retrait de 50 € accepté ? {compte.Retirer(50m)}");
compte.Afficher();

// --- Partie C — membres statiques ---
var c2 = new CompteBancaire("Bob Durand", 500m);
var c3 = new CompteBancaire("Chloé Petit", -50m);
Console.WriteLine();
c2.Afficher();
c3.Afficher();
Console.WriteLine($"Nombre de comptes créés : {CompteBancaire.NombreDeComptes}");

// --- Partie D — la classe Banque ---
var banque = new Banque { Nom = "Banque Populaire du Code" };

Console.WriteLine($"\n{banque.Nom} ({banque.Devise})");
banque.OuvrirCompte("Dora Lopez", 1200m);
banque.OuvrirCompte("Émile Roy", 300m);
var dernier = banque.OuvrirCompte("Fatou Ba", 800m);
dernier?.Afficher();

Console.WriteLine($"Total des avoirs : {banque.TotalDesAvoirs():C}");

// --- Bonus ---
Console.WriteLine($"\nTaux livret : {CompteBancaire.TauxLivret} %");
Console.WriteLine($"Intérêts annuels du compte d'Alice : {compte.InteretsAnnuels():C}");
