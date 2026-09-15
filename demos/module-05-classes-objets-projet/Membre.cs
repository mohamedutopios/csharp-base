namespace DemoClasses;

public class Membre
{
    public required string Nom { get; set; }   // required : obligatoire à la création
    public string Email { get; init; } = "";   // init : figé après construction
}
