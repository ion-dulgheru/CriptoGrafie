# Lab 6: rezultate așteptate în Wolfram

Varianta k = 10, deci i = (10 mod 24) + 1 = 11:
- Sarcina 2 (RSA): **SHA-512**
- Sarcina 3 (ElGamal): **SHA-384**

Mesajul m este textul descifrat la Lab 2 (pasajul despre Vigenère, 5 paragrafe unite prin „\n”).
Blocurile din `Lab6_cod_de_lipit.txt` se rulează în ordine, fiecare în celula lui (Shift+Enter). Niciun bloc nu se sare: fără blocul 15, x și y rămân nedefinite, iar blocurile 21–24 se afișează neevaluate (`Mod[...]`).

| Bloc | Rezultat așteptat |
|---|---|
| 1 | `{3343, 3345}`: numărul de caractere și de octeți UTF-8 (liniuța „—” ocupă 3 octeți) |
| 2 | `Vigenere was born in the village of Saint-Pourcain, ...` |
| 3 | `{1536, 1536, True}` |
| 4 | `3072`: n are 3072 de biți, cât cere sarcina |
| 5 | `1`: gcd(e, φ(n)) = 1 |
| 6 | `1`: e·d ≡ 1 mod φ(n) |
| 7 | `688cd59594c4a2658e51b974c4bcb23ef4891ced949a20d10eb5c32b16dad9591adbe2414cacfea7b2b003464f8681392cd873971df53c1a297e452a97220114` |
| 8 | `5475734829017884322610136832428659962514688255197415449702528113976087551365592728385043068011937576247424594880599244179092213783447323473350292177813780` |
| 9 | `{511, True}`: hash-ul începe cu un bit 0, deci are 511 biți semnificativi; h < n |
| 10 | semnătura s (un număr mare, diferit la fiecare rulare) |
| 11 | `True`: semnătura RSA este validă |
| 12 | `True` |
| 13 | `False`: pentru mesajul modificat semnătura nu mai este validă |
| 14 | `{2048, True}` |
| 15 | ~2048 (lungimea în biți a cheii publice y) |
| 16 | `45f64a30082189fb38bc6d68f24311422984c420a2076657726df4eea2b6ea7a6ed3ce494fb4e0fcb328b1112341998f` |
| 17 | `10768148033108956180769706000163834178301961467087930503933147337426105119553066011732353117940986593848555812788623` |
| 18 | `1` |
| 19 | r (un număr mare) |
| 20 | `1` |
| 21 | s (un număr mare) |
| 22 | `{True, True}` |
| 23 | `True`: semnătura ElGamal este validă |
| 24 | `False`: pentru mesajul modificat semnătura nu mai este validă |

Hash-urile (blocurile 7, 8, 16 și 17) trebuie să iasă **exact** ca mai sus, pentru că mesajul e fix.
Dacă diferă, textul lui m s-a modificat la copiere.
Cheile, k, r și semnăturile sunt aleatoare, deci se schimbă la fiecare rulare.
