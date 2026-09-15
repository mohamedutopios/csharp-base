# TP Module 5 — Classes et objets : le compte bancaire

**Durée :** 1 h — **Prérequis :** module 5 (classes, propriétés, constructeurs, static)

## Objectif

Concevoir une première classe complète avec état protégé, propriétés validées,
constructeurs et membres statiques.

## Énoncé

### Partie A — La classe CompteBancaire

1. Créer une classe `CompteBancaire` avec :
   - une propriété `Titulaire` (`string`, lecture/écriture),
   - une propriété `Iban` (`string`, **lecture seule** : affectée une fois au constructeur),
   - un **champ privé** `_solde` (`decimal`) exposé par une propriété `Solde`
     en **lecture seule** (pas de setter public !).
2. Un constructeur `(string titulaire, decimal soldeInitial)` :
   - si `soldeInitial` est négatif, le ramener à `0` ;
   - l'IBAN est généré automatiquement (voir partie C).

### Partie B — Comportements

3. `Deposer(decimal montant)` : ignore les montants ≤ 0.
4. `Retirer(decimal montant)` → `bool` : refuse (renvoie `false`) si le montant
   est ≤ 0 **ou** dépasse le solde ; sinon débite et renvoie `true`.
5. `Afficher()` : `"FR76-0001 | Alice Martin | 150,00 €"`.
6. Dans le programme principal : créer un compte avec 100 €, déposer 75 €,
   tenter de retirer 500 € (refus), retirer 50 € (accepté), afficher à chaque étape.
7. Vérifier que `compte.Solde = 1000000;` **ne compile pas**. Pourquoi est-ce
   une bonne chose ?

### Partie C — Membres statiques

8. Ajouter un compteur **statique** `NombreDeComptes`, incrémenté à chaque
   création ; l'IBAN est généré à partir de lui : `"FR76-0001"`, `"FR76-0002"`…
9. Créer 3 comptes et afficher `CompteBancaire.NombreDeComptes`.
10. Pourquoi ce compteur ne peut-il pas être un membre d'instance ?

### Partie D — La classe Banque (une classe qui en contient d'autres)

11. Créer une classe `Banque` avec :
    - une propriété `Nom` marquée **`required`**,
    - une propriété `Devise` avec `init` et la valeur par défaut `"EUR"`,
    - un tableau privé de 10 `CompteBancaire` et un compteur interne.
12. `OuvrirCompte(string titulaire, decimal depot)` → le `CompteBancaire` créé
    (ou `null` si la banque est pleine).
13. `TotalDesAvoirs()` → la somme des soldes (boucle `foreach`).
14. Créer la banque avec un **object initializer** :
    `new Banque { Nom = "Banque Populaire du Code" }`, ouvrir 3 comptes,
    afficher le total des avoirs.

## Bonus

- `TauxLivret` : propriété statique (`3.0m` %) et méthode d'instance
  `InteretsAnnuels()` → `Solde × TauxLivret / 100`.
- Que se passe-t-il si on oublie `Nom` dans l'object initializer ? Tester.
