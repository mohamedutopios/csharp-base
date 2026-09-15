# TP Module 11 — Exceptions : le distributeur de billets

**Durée :** 45 min — **Prérequis :** modules 5, 6 et 11

## Objectif

Gérer les erreurs proprement : intercepter, signaler avec des exceptions
métier, garantir le nettoyage avec `finally` et `using`.

## Énoncé

### Partie A — Intercepter

1. Écrire `LireMontant(string saisie)` → `decimal` qui utilise
   `decimal.Parse` (volontairement, pour l'exercice).
2. Dans le programme, appeler cette méthode sur les saisies
   `"50"`, `"abc"` et `""` dans un `try` :
   - `catch (FormatException)` → `"Ce n'est pas un montant valide."`,
   - `catch (Exception ex)` en dernier filet → afficher `ex.GetType().Name`.
3. Question : pourquoi le `catch (Exception)` doit-il être en **dernier** ?
   (inverser l'ordre et lire l'erreur du compilateur)

### Partie B — Signaler : les exceptions métier

4. Créer deux exceptions personnalisées :
   - `SoldeInsuffisantException(decimal solde, decimal demande)` — expose les
     deux montants en propriétés et fabrique un message clair,
   - `CoupureInvalideException(decimal montant)` — pour un montant non multiple
     de 10.
5. Créer une classe `Distributeur` avec un solde initial de 500 € et une
   méthode `Retirer(decimal montant)` qui **lève** :
   - `ArgumentOutOfRangeException` si montant ≤ 0,
   - `CoupureInvalideException` si non multiple de 10,
   - `SoldeInsuffisantException` si montant > solde,
   - sinon débite et affiche les billets distribués.
6. Tester 4 retraits : `120` (OK), `-5`, `35`, `9990` — chacun dans un `try`
   avec le `catch` **le plus précis possible**. Le programme survit aux 4.

### Partie C — finally et using

7. Ajouter à `Distributeur` une méthode `Session(decimal montant)` qui :
   - affiche `"Carte insérée"`,
   - appelle `Retirer(montant)` (qui peut lever !),
   - et garantit l'affichage de `"Carte rendue"` **dans tous les cas**
     (`finally`).
   Vérifier avec un retrait OK puis un retrait qui échoue : la carte est
   toujours rendue.
8. Créer une classe `JournalOperations : IDisposable` qui affiche
   `"[journal ouvert]"` à la construction et `"[journal fermé]"` dans
   `Dispose()`. L'utiliser avec `using` autour d'un retrait qui échoue :
   `Dispose` doit s'exécuter quand même.
9. Question : que fait `using` que `finally` faisait à la main ?

## Bonus

- Ajouter une propriété `Solde` et re-tester : après un échec, le solde
  doit être **intact** (une exception bien placée protège l'état).
- `throw;` vs `throw ex;` dans un catch : chercher la différence (pile d'appels).
