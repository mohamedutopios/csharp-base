# TP Module 6 — Encapsulation et héritage : l'équipe de développement

**Durée :** 1 h — **Prérequis :** modules 5 et 6

## Objectif

Construire une hiérarchie de classes avec classe abstraite, redéfinition de
comportements et égalité de contenu.

## Énoncé

### Partie A — La classe de base abstraite

1. Créer une classe **abstraite** `Employe` :
   - propriétés `Nom` (lecture seule) et `SalaireBase` (`decimal`, lecture seule),
   - un constructeur qui les initialise,
   - une méthode **abstraite** `SalaireMensuel()` → `decimal` (pas de corps :
     chaque métier calcule différemment),
   - une méthode **virtuelle** `SePresenter()` qui affiche
     `"Je suis {Nom}, je gagne {SalaireMensuel():C} par mois."`.
2. Vérifier que `new Employe(...)` **ne compile pas**. Pourquoi ?

### Partie B — Les classes dérivées

3. `Developpeur : Employe` :
   - propriété supplémentaire `LangagePrincipal`,
   - `SalaireMensuel()` → salaire de base + 50 € par année d'ancienneté
     (propriété `Anciennete` en années),
   - **redéfinit** `SePresenter()` : appelle la version de base avec
     `base.SePresenter()` **puis** ajoute `"Je code en {LangagePrincipal}."`.
4. `Manager : Employe` :
   - propriété `TailleEquipe`,
   - `SalaireMensuel()` → salaire de base + 100 € par personne encadrée.
5. `Stagiaire : Employe`, marquée **`sealed`** :
   - `SalaireMensuel()` → montant fixe de 800 € (quel que soit le salaire de base).
6. Vérifier qu'on ne peut pas hériter de `Stagiaire` (essayer, lire l'erreur).

### Partie C — Le polymorphisme au travail

7. Créer une `List<Employe>` contenant 2 développeurs, 1 manager, 1 stagiaire.
8. Avec **une seule boucle** `foreach (Employe e in equipe)`, appeler
   `SePresenter()` : chaque type doit afficher SA version.
9. Calculer la masse salariale totale (somme des `SalaireMensuel()`), toujours
   via le type de base.

### Partie D — ToString et Equals

10. Redéfinir `ToString()` dans `Employe` : `"Developpeur Alice (3200,00 €)"`
    (utiliser `GetType().Name`).
11. Créer une classe `Badge` avec `Numero` (int) et `Societe` (string) ;
    redéfinir `Equals` (et `GetHashCode`) pour que deux badges de même numéro
    et même société soient égaux.
12. Vérifier : deux `new Badge(7, "ACME")` distincts → `Equals` vrai,
    `ReferenceEquals` faux.

## Bonus

- Ajouter dans `Manager` une méthode `Recruter()` qui incrémente `TailleEquipe` :
  que devient son salaire ? (montrer que le calcul est dynamique)
- Marquer `SalaireMensuel` de `Stagiaire` `sealed override` et expliquer la nuance.
