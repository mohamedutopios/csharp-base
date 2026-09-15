namespace DemoExceptions;

// Exception personnalisée : hérite d'Exception, nom finissant par Exception,
// et son fichier — comme toute classe.
public class LivreIndisponibleException : Exception
{
    public string Titre { get; }

    public LivreIndisponibleException(string titre)
        : base($"Le livre « {titre} » est déjà emprunté.")   // message vers la base
    {
        Titre = titre;
    }
}
