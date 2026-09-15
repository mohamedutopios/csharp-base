// =====================================================================
// Module 5 — Démo (version projet multi-fichiers)
// Program.cs = le POINT D'ENTRÉE (top-level statements, un seul fichier
// du projet peut en avoir). Les classes vivent dans Livre.cs et Membre.cs.
// Exécution : dotnet run   (depuis ce dossier)
// =====================================================================

using DemoClasses;   // le namespace où vivent nos classes

Console.WriteLine("=== Démo Module 5 : classes et objets (projet) ===\n");

// 1. Instancier : new crée un OBJET (instance) à partir de la CLASSE (plan)
var l1 = new Livre("1984", "George Orwell");
var l2 = new Livre("Dune", "Frank Herbert");

Console.WriteLine($"l1 : {l1.Titre} de {l1.Auteur}");
Console.WriteLine($"l2 : {l2.Titre} de {l2.Auteur}");
Console.WriteLine("→ Deux objets distincts issus du même plan (la classe).\n");

// 2. Propriétés : lecture/écriture contrôlées
l1.NombreDePages = 328;               // set avec validation (voir Livre.cs)
Console.WriteLine($"{l1.Titre} : {l1.NombreDePages} pages");

l1.NombreDePages = -50;               // rejeté par le setter
Console.WriteLine($"Après tentative de -50 pages : {l1.NombreDePages} (la validation a protégé l'objet)\n");

// 3. Constructeurs : plusieurs façons de construire
var l3 = new Livre("Fondation");      // constructeur à 1 paramètre → auteur "Inconnu"
Console.WriteLine($"l3 : {l3.Titre} de {l3.Auteur} (constructeur chaîné avec this)\n");

// 4. Object initializer + init + required
var m1 = new Membre                   // syntaxe d'initialisation d'objet
{
    Nom = "Durand",                   // required : le compilateur EXIGE Nom
    Email = "durand@mail.fr"          // init : modifiable uniquement à la création
};
Console.WriteLine($"Membre : {m1.Nom} ({m1.Email})");
// m1.Email = "autre@mail.fr";        // ← ERREUR : init interdit la modification après création
// var m2 = new Membre { };           // ← ERREUR : Nom est required

// 5. Membres statiques : portés par la CLASSE, pas par l'instance
Console.WriteLine($"\nNombre de livres créés : {Livre.NombreDeLivres} (compteur statique)");
Console.WriteLine($"Math.Max est statique aussi : Math.Max(3, 7) = {Math.Max(3, 7)}");

Console.WriteLine("\n→ À retenir : une classe = un fichier ; tous les .cs du projet " +
                  "sont compilés ensemble ; un seul fichier a les top-level statements.");
