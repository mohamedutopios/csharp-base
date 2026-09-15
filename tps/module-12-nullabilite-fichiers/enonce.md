# TP Module 12 — Nullabilité et fichiers : le carnet de contacts persistant

**Durée :** 45 min — **Prérequis :** modules 8 à 12

## Objectif

Traquer les `null` avec le compilateur, manipuler fichiers et chemins,
et persister des objets en JSON — la mécanique exacte de l'étape 3 du fil rouge.

## Énoncé

### Partie A — Apprivoiser le null

1. Déclarer un `record Contact(string Nom, string? Telephone, string? Email);`
   — téléphone et email sont **optionnels** (nullables), le nom non.
2. Écrire `DecrireContact(Contact c)` → `string` qui affiche :
   - le téléphone s'il existe, sinon `"(pas de téléphone)"` — avec `??`,
   - la **longueur** de l'email via `c.Email?.Length` sans jamais crasher.
3. Écrire `NormaliserTelephone(string? brut)` → `string?` :
   - `null` ou blanc (`string.IsNullOrWhiteSpace`) → `null`,
   - sinon le numéro sans espaces (`Replace`).
4. Tester avec un contact complet, un contact sans téléphone ni email.
5. Question : que signifie le warning CS8602 que le compilateur affiche si
   on écrit `c.Email.Length` sans vérification ?

### Partie B — Chemins et fichiers texte

6. Construire avec `Path.Combine` (jamais de `"/"` en dur !) le chemin
   `<temp>/tp-contacts/export.txt` (`Path.GetTempPath()`).
7. Créer le dossier (`Directory.CreateDirectory`), puis écrire dans le fichier
   une ligne par contact au format `Nom;Telephone;Email`
   (champs nulls → vide). Utiliser `File.WriteAllLines`.
8. Relire avec `File.ReadAllLines` et reconstruire des `Contact` avec `Split`
   (champ vide → `null`). Afficher les contacts relus.
9. Vérifier `File.Exists` avant la lecture ; que renvoyer si le fichier
   n'existe pas ? (une liste vide, pas `null` !)

### Partie C — Persistance JSON (la vraie solution)

10. Sérialiser la liste de contacts avec `JsonSerializer.Serialize`
    (option `WriteIndented = true`) et l'écrire dans `contacts.json`.
    Afficher le JSON produit : où sont passés les `null` ?
11. Recharger avec `JsonSerializer.Deserialize<List<Contact>>` :
    le résultat est un `List<Contact>?` — le sécuriser avec `?? new()`.
12. Écrire le cycle complet dans deux méthodes réutilisables :
    - `Sauvegarder(List<Contact> contacts, string chemin)`,
    - `Charger(string chemin)` → `List<Contact>` (jamais null : fichier
      absent ou JSON invalide → liste vide + message).
13. Prouver la persistance : sauvegarder 3 contacts, recharger dans une
    **nouvelle** variable, vérifier le nombre et l'égalité du premier
    (`record` → égalité de contenu !).

## Bonus

- `??=` : compléter les téléphones manquants avec `"(à renseigner)"` en une
  ligne par contact.
- Que se passe-t-il si le JSON du fichier est corrompu (`"{ oops"`) ?
  Protéger `Charger` avec un `try/catch (JsonException)`.

> **Organisation en projet** : un type = un fichier (records, enums et structs
> compris). Le résultat attendu est dans `solution-projet/`
> (`dotnet run --project solution-projet`).
