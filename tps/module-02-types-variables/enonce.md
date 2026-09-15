# TP Module 2 — Types et variables : la caisse enregistreuse

**Durée :** 45 min — **Prérequis :** module 2 (types, conversions, `TryParse`, chaînes)

## Objectif

Manipuler les types primitifs, les conversions sécurisées et les chaînes
en construisant une mini caisse enregistreuse console.

## Énoncé

### Partie A — Les bons types

1. Déclarer les données d'un article avec le type le plus adapté :
   - un nom d'article (`"Café moulu"`),
   - un prix unitaire de `4.80` € — **attention : quel type pour de l'argent ?**
   - une quantité de `3`,
   - un article en promotion : oui/non.
2. Déclarer une **constante** `TauxTva` valant `0.20`.
3. Calculer et afficher :
   - le total HT (`prix × quantité`),
   - le montant de TVA,
   - le total TTC,
   en utilisant l'**interpolation** et le format monétaire `{...:C}`.

### Partie B — Saisie sécurisée

4. Demander à l'utilisateur une quantité via `Console.ReadLine()`.
5. La convertir avec `int.TryParse` :
   - si la saisie est invalide **ou** négative, afficher un message et utiliser `1` par défaut ;
   - sinon, recalculer le total TTC avec cette quantité.
6. Question piège à tester : que se passe-t-il avec `int.Parse("abc")` ?
   (essayer, constater le crash, puis remettre `TryParse`).

### Partie C — Le nom de l'article au microscope

7. À partir de la chaîne `"  café MOULU bio  "` , produire proprement `"Café moulu bio"` :
   `Trim`, mise en minuscules, puis première lettre en majuscule.
8. Afficher : sa longueur, s'il contient `"bio"`, sa version sans les espaces internes
   remplacés par des tirets (`Replace`).

### Partie D — Le ticket avec StringBuilder

9. Construire le ticket ligne par ligne avec un `StringBuilder` :

```
===== TICKET =====
Café moulu bio
3 x 4,80 € ....... 14,40 €
TVA (20 %) ....... 2,88 €
TOTAL TTC ........ 17,28 €
==================
```

10. L'afficher en une seule fois avec `Console.WriteLine(sb.ToString())`.

## Bonus

- Vérifier avec `0.1 + 0.2 == 0.3` pourquoi `double` est banni pour l'argent.
- Afficher le prix aligné à droite sur 10 caractères : `{prix,10:C}`.
