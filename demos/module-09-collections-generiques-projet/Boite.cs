namespace DemoCollections;

// Classe générique : le type du contenu est un paramètre.
// Le fichier porte le nom de la classe, sans le <T> : Boite.cs
public class Boite<T>
{
    public T Contenu { get; }
    public Boite(T contenu) => Contenu = contenu;
}
