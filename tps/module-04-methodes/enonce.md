# TP Module 4 — Méthodes : la boîte à outils du développeur

**Durée :** 45 min — **Prérequis :** module 4 (signatures, `ref`/`out`/`params`, surcharge)

## Objectif

Écrire une petite bibliothèque de méthodes utilitaires en couvrant toutes les
formes de paramètres, la surcharge et la récursivité.

## Énoncé

Toutes les méthodes sont appelées et vérifiées depuis le programme principal.

### Partie A — Bases

1. `EstPair(int n)` → `bool`. L'écrire en **expression-bodied** (`=>`).
2. `Repeter(string texte, int fois)` → `string` qui concatène `texte` `fois` fois.
3. `AfficherEncadre(string titre)` → `void` qui affiche :

```
+----------+
|  Bonjour |
+----------+
```

### Partie B — out : le multi-retour

4. `TryDiviser(int a, int b, out int quotient, out int reste)` → `bool` :
   - renvoie `false` (et des sorties à 0) si `b == 0`,
   - sinon `true` avec quotient et reste.
5. L'appeler deux fois : avec `(17, 5)` et `(17, 0)`, et afficher le résultat
   des deux cas **sans crash**.

### Partie C — params et optionnels

6. `Moyenne(params double[] valeurs)` → `double` ; renvoie `0` si aucun argument.
7. `FormaterPrix(decimal prix, string devise = "€", bool avantLeNombre = false)` :
   - `FormaterPrix(12.5m)` → `"12,50 €"`,
   - `FormaterPrix(12.5m, "$", avantLeNombre: true)` → `"$ 12,50"` (argument **nommé**).

### Partie D — Surcharge

8. Trois surcharges de `Decrire` :
   - `Decrire(int n)` → `"entier : 42"`,
   - `Decrire(string s)` → `"chaîne de 7 caractères"`,
   - `Decrire(int[] t)` → `"tableau de 3 éléments"`.
9. Les appeler avec `42`, `"bonjour"` et `{ 1, 2, 3 }` : montrer que le
   compilateur choisit seul la bonne version.

### Partie E — Récursivité

10. `Puissance(double x, int n)` → `x^n` en récursif (`x^n = x × x^(n-1)`, `x^0 = 1`).
11. `CompteARebours(int n)` : affiche `n, n-1, … 1, Décollage !` en récursif,
    **sans aucune boucle**.

## Bonus

- `Inverser(string texte)` en récursif : `"radar"` → `"radar"`, `"chat"` → `"tahc"`.
- Que se passe-t-il si `Puissance` reçoit `n = -1` ? Protéger le cas.
