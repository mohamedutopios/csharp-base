namespace TpColis;

// Record positionnel : propriétés init, constructeur, égalité, ToString,
// déconstruction… générés en une ligne.
public record Colis(string Numero, string Destinataire, double PoidsKg, EtatColis Etat);
