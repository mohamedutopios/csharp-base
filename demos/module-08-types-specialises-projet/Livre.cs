namespace DemoTypes;

// Un record aussi a son fichier, même s'il tient en une ligne :
// on le retrouve par son nom, et il pourra grandir sans déménager.
// UNE ligne = propriétés init, constructeur, Equals, GetHashCode,
// ToString, déconstruction… tout est généré.
public record Livre(string Titre, string Auteur, int Annee);
