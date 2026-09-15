namespace TpContacts;

// Nom obligatoire ; Telephone et Email optionnels : le ? le DIT au compilateur
public record Contact(string Nom, string? Telephone, string? Email);
