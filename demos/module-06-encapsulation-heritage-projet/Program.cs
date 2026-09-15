// =====================================================================
// Module 6 — Démo (version projet multi-fichiers)
// Program.cs = le scénario ; la hiérarchie vit dans Animal.cs, Chien.cs,
// Chat.cs, et les autres classes dans CompteBancaire.cs et Point.cs.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using DemoHeritage;

Console.WriteLine("=== Démo Module 6 : encapsulation et héritage (projet) ===\n");

// 1. Encapsulation : l'état est protégé, on passe par des méthodes
var compte = new CompteBancaire("FR76-0000", 100m);
compte.Deposer(50m);
bool ok = compte.Retirer(500m);       // refusé : solde insuffisant
Console.WriteLine($"Solde : {compte.Solde:C} | Retrait de 500 € accepté ? {ok}");
// compte._solde = 1_000_000m;        // ← ERREUR : le champ est private. C'est le but !

// 2. Héritage : Chien et Chat SONT DES Animaux
var animaux = new List<Animal>
{
    new Chien("Rex"),
    new Chat("Félix"),
    new Chien("Médor")
};

Console.WriteLine("\n--- Polymorphisme : même appel, comportements différents ---");
foreach (Animal animal in animaux)     // on manipule le type de BASE…
{
    animal.Crier();                    // …mais c'est l'override du type RÉEL qui s'exécute
}

// 3. Classe abstraite : non instanciable
// var a = new Animal("X");            // ← ERREUR : Animal est abstract
Console.WriteLine("\nAnimal est abstract : on ne peut pas faire new Animal(...).");

// 4. ToString : la représentation texte d'un objet
var rex = animaux[0];
Console.WriteLine($"\nAvec override : {rex}");     // appelle ToString() implicitement

// 5. Equals : égalité de contenu vs égalité de référence
var p1 = new Point(2, 3);
var p2 = new Point(2, 3);
Console.WriteLine($"\np1 == p2 (référence)   : {ReferenceEquals(p1, p2)}  (deux objets distincts)");
Console.WriteLine($"p1.Equals(p2) (contenu): {p1.Equals(p2)}  (grâce à l'override)");

Console.WriteLine("\n→ À retenir : private par défaut, virtual/override pour spécialiser, " +
                  "et en projet : un fichier par classe de la hiérarchie.");
