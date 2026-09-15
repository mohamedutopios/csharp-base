// Une classe = un fichier, nommé comme la classe.
// Le compilateur inclut automatiquement tous les .cs du dossier du projet.
namespace DemoClasses;

public class Livre
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
