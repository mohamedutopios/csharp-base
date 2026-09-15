// =====================================================================
// Program.cs — point d'entrée : uniquement le scénario.
// La hiérarchie vit dans Employe.cs, Developpeur.cs, Manager.cs,
// Stagiaire.cs, Badge.cs — compilés automatiquement avec ce fichier.
// =====================================================================

using TpEquipe;

Console.WriteLine("=== TP Module 6 (version projet multi-fichiers) ===\n");

// --- Partie C — Le polymorphisme au travail ---
var equipe = new List<Employe>
{
    new Developpeur("Alice", 2800m, "C#", anciennete: 4),
    new Developpeur("Bob", 2600m, "Python", anciennete: 2),
    new Manager("Chloé", 3200m, tailleEquipe: 5),
    new Stagiaire("David"),
};

foreach (Employe e in equipe)
{
    e.SePresenter();
    Console.WriteLine();
}

decimal masseSalariale = 0;
foreach (Employe e in equipe)
    masseSalariale += e.SalaireMensuel();
Console.WriteLine($"Masse salariale totale : {masseSalariale:C}");

// --- Partie D — ToString et Equals ---
Console.WriteLine("\n--- ToString ---");
foreach (Employe e in equipe)
    Console.WriteLine($"  {e}");

Console.WriteLine("\n--- Equals sur Badge ---");
var b1 = new Badge(7, "ACME");
var b2 = new Badge(7, "ACME");
var b3 = new Badge(8, "ACME");
Console.WriteLine($"b1.Equals(b2)          : {b1.Equals(b2)} (même contenu)");
Console.WriteLine($"ReferenceEquals(b1,b2) : {ReferenceEquals(b1, b2)} (objets distincts)");
Console.WriteLine($"b1.Equals(b3)          : {b1.Equals(b3)}");

// --- Bonus — le calcul de salaire est dynamique ---
var manager = new Manager("Chloé", 3200m, tailleEquipe: 5);
Console.WriteLine($"\nAvant recrutement : {manager.SalaireMensuel():C}");
manager.Recruter();
manager.Recruter();
Console.WriteLine($"Après 2 recrutements : {manager.SalaireMensuel():C} (recalculé à l'appel)");
