# TP Module 9 — Collections et génériques : l'inventaire du magasin

**Durée :** 1 h — **Prérequis :** module 9

## Objectif

Choisir la bonne collection pour chaque besoin, puis écrire sa première
classe générique avec contrainte.

## Énoncé

### Partie A — List : le catalogue

1. Créer une `List<string>` de produits : `"clavier", "souris", "écran"`.
2. Ajouter `"webcam"`, insérer `"casque"` en position 1, supprimer `"souris"`.
3. Afficher : le nombre d'éléments, la liste triée (`Sort`), la position
   d'`"écran"` (`IndexOf`), et si `"souris"` est encore là (`Contains`).
4. Question : pourquoi `List<T>` plutôt qu'un tableau ici ?

### Partie B — Dictionary : le stock

5. Créer un `Dictionary<string, int>` associant chaque produit à sa quantité
   (clavier 12, casque 4, écran 7, webcam 0).
6. Écrire `RetirerDuStock(Dictionary<string, int> stock, string produit, int quantite)`
   → `bool` :
   - `false` si le produit est **absent** (utiliser `TryGetValue`, jamais
     `stock[produit]` à l'aveugle !) ou si le stock est insuffisant ;
   - sinon décrémenter et renvoyer `true`.
7. Tester : retirer 3 claviers (OK), 10 casques (refus), 1 souris (absent).
8. Afficher le stock complet avec `foreach` sur les paires clé/valeur,
   et la liste des produits **en rupture** (quantité 0).

### Partie C — HashSet, Queue, Stack : trois problèmes, trois structures

9. **Clients uniques** : dans le tableau
   `{ "alice", "bob", "alice", "chloé", "bob", "alice" }`, compter les clients
   distincts avec un `HashSet<string>` (2 lignes maximum).
10. **File du service après-vente** : `Queue<string>` — 3 clients arrivent,
    traiter les 2 premiers (`Dequeue`), afficher qui reste (`Peek`).
11. **Historique d'actions** : `Stack<string>` — empiler
    `"ajout clavier"`, `"retrait écran"`, `"ajout webcam"`, puis « annuler »
    les 2 dernières actions (`Pop`).
12. Question : pour chaque cas, pourquoi cette structure et pas une `List` ?

### Partie D — Générique maison : la pile à capacité limitée

13. Écrire une classe générique `PileBornee<T>` :
    - constructeur `(int capacite)`,
    - `bool Empiler(T element)` → `false` si pleine,
    - `bool TryDepiler(out T? element)` → `false` si vide,
    - propriété `Nombre`.
    En interne, utiliser une `List<T>`.
14. La tester avec des `int` **et** des `string` : même code, deux types.
15. Ajouter `AfficherTout(IEnumerable<T> elements)` générique qui fonctionne
    avec la liste, le dictionnaire (ses clés), le HashSet…

## Bonus

- Contrainte : `T PlusGrand<T>(List<T> liste) where T : IComparable<T>`
  → le max de la liste ; tester avec `int` et `string`.
- Que se passe-t-il si `PileBornee` interne utilisait un tableau ?
  Quel est l'intérêt pédagogique d'avoir les deux implémentations ?

> **Organisation en projet** : un type = un fichier (records, enums et structs
> compris). Le résultat attendu est dans `solution-projet/`
> (`dotnet run --project solution-projet`).
