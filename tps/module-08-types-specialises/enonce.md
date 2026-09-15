# TP Module 8 — Types spécialisés : le suivi de colis

**Durée :** 45 min — **Prérequis :** module 8 (`struct`, `record`, `enum`, tuples)

## Objectif

Choisir le bon type pour chaque donnée : `record` pour les données,
`enum` pour les états, `struct` pour les petites valeurs, tuple pour
les retours multiples.

## Énoncé

### Partie A — enum : l'état d'un colis

1. Déclarer `enum EtatColis { EnPreparation, Expedie, EnTransit, Livre, Retourne }`.
2. Écrire `LibelleFrancais(EtatColis etat)` → `string` avec une **switch
   expression** (`"en préparation"`, `"expédié"`…).
3. Afficher toutes les valeurs possibles avec `Enum.GetValues<EtatColis>()`
   et leur valeur entière sous-jacente.

### Partie B — record : le colis

4. Déclarer un **record positionnel** `Colis(string Numero, string Destinataire,
   double PoidsKg, EtatColis Etat)`.
5. Créer un colis, puis simuler son avancement avec **`with`** :
   chaque étape crée une **copie** avec le nouvel état (l'original ne change pas).
   Afficher chaque version (le `ToString` généré suffit).
6. Vérifier l'égalité de contenu : deux colis créés avec les mêmes valeurs
   sont-ils `==` ? Et après un `with` qui ne change rien ?
7. Question : pourquoi un `record` plutôt qu'une `class` ici ?

### Partie C — struct : les dimensions

8. Déclarer une **struct** `Dimensions` avec `LongueurCm`, `LargeurCm`,
   `HauteurCm` (double) et une propriété calculée `VolumeLitres`
   (`L × l × h / 1000`).
9. Montrer la sémantique de **copie** : `var d2 = d1; d2.LongueurCm = 999;`
   → `d1` est intact.
10. Question : pourquoi une `struct` convient-elle bien à `Dimensions` ?

### Partie D — Tuples : le bilan d'une tournée

11. Écrire `AnalyserTournee(List<Colis> colis)` qui renvoie le tuple nommé
    `(int Total, int Livres, double PoidsTotal)` — sans créer de classe dédiée.
12. Appeler la méthode et **déconstruire** le résultat :
    `var (total, livres, poids) = AnalyserTournee(tournee);`
13. Déconstruire aussi un colis : `var (numero, dest, _, etat) = colis;`
    (le `_` ignore le poids).

## Bonus

- Donner des valeurs explicites à l'enum (`EnPreparation = 10, Expedie = 20…`)
  pour pouvoir intercaler des états plus tard : afficher les nouvelles valeurs.
- `record struct PointRelais(string Code, string Ville)` : le meilleur des
  deux mondes ? Tester l'égalité et la copie.
