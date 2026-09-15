namespace DemoInterfaces;

// Convention : les interfaces commencent par I, et ont leur fichier aussi
public interface INotifiable
{
    void Envoyer(string message);       // pas de corps : c'est un contrat
}
