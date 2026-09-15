// =====================================================================
// Module 5 — Classes et objets (Jour 2, 10h45–12h30)
// Démo : classe, instance, champs, propriétés (auto, init, required),
//        constructeurs, this, membres statiques, object initializer
// Exécution : dotnet run module-05-classes-objets.cs
// Version projet (une classe = un fichier) : module-05-classes-objets-projet/
// =====================================================================

Console.WriteLine("=== Démo Module 5 : classes et objets ===\n");

// ---------------------------------------------------------------
// 1. Instancier : new crée un OBJET (instance) à partir de la CLASSE (plan)
// ---------------------------------------------------------------
var l1 = new Livre("1984", "George Orwell");
var l2 = new Livre("Dune", "Frank Herbert");

Console.WriteLine($"l1 : {l1.Titre} de {l1.Auteur}");
Console.WriteLine($"l2 : {l2.Titre} de {l2.Auteur}");
Console.WriteLine("→ Deux objets distincts issus du même plan (la classe).\n");

// ---------------------------------------------------------------
// 2. Propriétés : lecture/écriture contrôlées
// ---------------------------------------------------------------
l1.NombreDePages = 328;               // set avec validation (voir la classe)
Console.WriteLine($"{l1.Titre} : {l1.NombreDePages} pages");

l1.NombreDePages = -50;               // rejeté par le setter
Console.WriteLine($"Après tentative de -50 pages : {l1.NombreDePages} (la validation a protégé l'objet)\n");

// ---------------------------------------------------------------
// 3. Constructeurs : plusieurs façons de construire
// ---------------------------------------------------------------
var l3 = new Livre("Fondation");      // constructeur à 1 paramètre → auteur "Inconnu"
Console.WriteLine($"l3 : {l3.Titre} de {l3.Auteur} (constructeur chaîné avec this)\n");

// ---------------------------------------------------------------
// 4. Object initializer + init + required
// ---------------------------------------------------------------
var m1 = new Membre                   // syntaxe d'initialisation d'objet
{
    Nom = "Durand",                   // required : le compilateur EXIGE Nom
    Email = "durand@mail.fr"          // init : modifiable uniquement à la création
};
Console.WriteLine($"Membre : {m1.Nom} ({m1.Email})");
// m1.Email = "autre@mail.fr";        // ← ERREUR : init interdit la modification après création
// var m2 = new Membre { };           // ← ERREUR : Nom est required

// ---------------------------------------------------------------
// 5. Membres statiques : portés par la CLASSE, pas par l'instance
// ---------------------------------------------------------------
Console.WriteLine($"\nNombre de livres créés : {Livre.NombreDeLivres} (compteur statique)");
Console.WriteLine($"Math.Max est statique aussi : Math.Max(3, 7) = {Math.Max(3, 7)}");

Console.WriteLine("\n→ À retenir : propriétés plutôt que champs publics, " +
                  "required/init pour construire des objets valides, static = partagé.");

// =====================================================================
// Les classes
// =====================================================================

class Livre
{
    // Champ privé : l'état interne, invisible de l'extérieur
    private int _nombreDePages;

    // Champ STATIQUE : une seule valeur pour toute la classe
    public static int NombreDeLivres { get; private set; }

    // Propriétés automatiques : le compilateur génère le champ caché
    public string Titre { get; set; }
    public string Auteur { get; set; }

    // Propriété avec validation dans le setter
    public int NombreDePages
    {
        get => _nombreDePages;
        set
        {
            if (value > 0)                    // value = la valeur affectée
                _nombreDePages = value;
        }
    }

    // Constructeur principal
    public Livre(string titre, string auteur)
    {
        Titre = titre;          // this.Titre implicite ; this utile si ambiguïté
        Auteur = auteur;
        NombreDeLivres++;       // chaque new incrémente le compteur partagé
    }

    // Constructeur secondaire chaîné avec : this(...)
    public Livre(string titre) : this(titre, "Inconnu")
    {
    }
}

class Membre
{
    public required string Nom { get; set; }   // required : obligatoire à la création
    public string Email { get; init; } = "";   // init : figé après construction
}
