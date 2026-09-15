namespace TpInventaire;

// Classe générique : le type des éléments est un paramètre.
// Le fichier porte le nom de la classe, sans le <T>.
public class PileBornee<T>
{
    private readonly List<T> _elements = new();    // la List fait le stockage
    private readonly int _capacite;

    public PileBornee(int capacite) => _capacite = capacite;

    public int Nombre => _elements.Count;

    public bool Empiler(T element)
    {
        if (_elements.Count >= _capacite)
            return false;                           // pleine

        _elements.Add(element);                     // le sommet = fin de liste
        return true;
    }

    public bool TryDepiler(out T? element)
    {
        if (_elements.Count == 0)
        {
            element = default;                      // default : null ou 0 selon T
            return false;
        }
        element = _elements[^1];                    // ^1 : dernier élément
        _elements.RemoveAt(_elements.Count - 1);
        return true;
    }
}
