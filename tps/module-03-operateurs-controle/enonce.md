# TP Module 3 — Opérateurs et structures de contrôle : le petit train des multiples

**Durée :** 45 min — **Prérequis :** module 3 (opérateurs, `if`/`switch`, boucles)

## Objectif

Pratiquer les boucles, les conditions, le modulo et le `switch` — puis assembler
un menu en boucle, le squelette exact du TP 1 (convertisseur d'unités).

## Énoncé

### Partie A — FizzBuzz (le classique des entretiens)

1. Afficher les nombres de 1 à 30, un par ligne, mais :
   - multiples de 3 → afficher `Fizz` à la place,
   - multiples de 5 → afficher `Buzz`,
   - multiples de 3 **et** 5 → afficher `FizzBuzz`.
2. L'écrire une première fois avec `if / else if / else`.
3. Le réécrire avec une **switch expression** sur le tuple
   `(n % 3 == 0, n % 5 == 0)`. Comparer la lisibilité.

### Partie B — La table de multiplication

4. Demander un nombre entre 1 et 10 (`TryParse` + redemander tant que la saisie
   est invalide : boucle `do/while`).
5. Afficher sa table de multiplication de 1 à 10 avec une boucle `for` :
   `7 x 1 = 7`, `7 x 2 = 14`, etc.
6. Variante : n'afficher que les résultats **pairs** (avec `continue`).

### Partie C — Statistiques d'un tableau

7. Sur le tableau `int[] notes = { 12, 8, 17, 5, 14, 9, 20, 11 }` , calculer
   **avec un `foreach`** (sans LINQ, il arrive au module 10 !) :
   - la somme et la moyenne,
   - le minimum et le maximum,
   - le nombre de notes ≥ 10.
8. Afficher un verdict avec l'opérateur **ternaire** :
   `moyenne >= 10 ? "Admis" : "Ajourné"`.

### Partie D — Le menu en boucle (préparation directe du TP 1)

9. Afficher en boucle le menu :

```
--- MENU ---
1) FizzBuzz (1 à 30)
2) Table de multiplication
3) Statistiques des notes
0) Quitter
Votre choix :
```

10. Aiguiller avec un `switch` **instruction** vers les parties A, B, C.
11. `0` quitte la boucle ; tout autre choix affiche `Choix inconnu`.
12. Le programme ne doit **jamais planter**, quelle que soit la saisie.

## Bonus

- Dessiner un sapin de hauteur saisie (boucles imbriquées) :

```
  *
 ***
*****
```
