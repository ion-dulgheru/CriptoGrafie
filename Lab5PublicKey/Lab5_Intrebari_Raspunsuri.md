# Lab 5: Criptografia cu chei publice. Întrebări și răspunsuri

Întrebări de recapitulare, grupate pe teme. Unde e cazul, răspunsurile se referă la calculele făcute în Wolfram (mesajul „Dulgheru Ion”). Întrebările de bază sunt marcate cu ⭐.

---

## 1. Noțiuni generale

**1. ⭐ Ce este criptografia cu chei publice?**
Fiecare utilizator are o pereche de chei: o **cheie publică**, pe care o dă oricui, și o **cheie privată**, pe care o ține secretă. Cu cheia publică oricine poate cripta, dar doar posesorul cheii private poate decripta.

**2. ⭐ Care e diferența față de criptarea simetrică (Caesar, Vigenère, DES, AES)?**
La criptarea simetrică aceeași cheie e folosită și la criptare, și la decriptare, deci cele două părți trebuie să o împartă mai întâi pe un canal sigur. La criptarea asimetrică cheile sunt diferite și cea publică poate circula liber, deci nu mai e nevoie de un canal secret pentru distribuirea cheilor.

**3. Atunci de ce nu folosim doar criptare asimetrică?**
Pentru că e de sute sau mii de ori mai lentă: calculează puteri cu numere de 2048 de biți. În practică se folosește **criptarea hibridă**: cu RSA sau Diffie-Hellman se stabilește o cheie simetrică, iar datele se criptează cu AES. Asta e exact ideea sarcinii 3.

**4. Cine a inventat criptografia cu chei publice?**
Ideea a fost publicată de **Diffie și Hellman în 1976** („New Directions in Cryptography”). **RSA** a apărut în **1977–1978** (Rivest, Shamir, Adleman), iar **ElGamal** în **1985**.

**5. Ce este o funcție unidirecțională cu trapă (trapdoor one-way function)?**
O funcție ușor de calculat, dar practic imposibil de inversat, **cu excepția** cazului în care cunoști un secret suplimentar (trapa). La RSA, funcția e m → m^e mod n, iar trapa e factorizarea lui n (p și q), din care se obține d.

**6. ⭐ Pe ce probleme matematice grele se bazează cei trei algoritmi?**
- RSA: **factorizarea** unui număr mare n = p·q.
- ElGamal și Diffie-Hellman: **logaritmul discret** modulo p (dacă știi g, p și g^x mod p, nu poți afla x).

**7. Ce înseamnă „mod n”?**
Restul împărțirii la n. De exemplu, 17 mod 5 = 2. Toate calculele se fac modulo un număr mare, deci rezultatele rămân mereu între 0 și n−1.

**8. Ce este inversul modular?**
Numărul a⁻¹ pentru care a·a⁻¹ ≡ 1 (mod n). Există doar dacă gcd(a, n) = 1. Se calculează cu algoritmul lui Euclid extins. În Wolfram se scrie `PowerMod[a, -1, n]`.

**9. Ce este exponențierea modulară rapidă?**
Calcularea lui a^b mod n prin ridicări repetate la pătrat și înmulțiri (square-and-multiply), reducând modulo n după fiecare pas. Așa se fac doar aproximativ 2·log₂(b) înmulțiri, nu b. Fără ea, m^d cu d de 2048 de biți ar fi imposibil de calculat. `PowerMod` face exact asta.

---

## 2. Reprezentarea mesajului

**10. ⭐ Cum ai transformat „Dulgheru Ion” în număr?**
Fiecare caracter → codul lui ASCII → scris în hex (2 cifre, adică 1 octet). Am lipit toate valorile hex: `44756c676865727520496f6e`. Numărul hex rezultat, convertit în zecimal, dă **m = 21186936911863710400463335278**.

**11. De ce `FromDigits[ToCharacterCode[...], 256]` dă același rezultat ca metoda cu hex?**
Pentru că două cifre hex înseamnă exact un octet (o cifră în baza 256). Concatenarea codurilor hex e același lucru cu scrierea mesajului ca număr în baza 256: m = c₁·256^(L−1) + … + c_L.

**12. Ce cod are spațiul? Dar „D”?**
Spațiul are 32 (hex 20). „D” are 68 (hex 44). Literele mici încep de la „a” = 97 (hex 61).

**13. Câți biți are mesajul?**
Are 12 caractere × 8 biți = 96 de biți. Primul octet (0x44) începe cu un bit 0, deci de fapt m are 95 de biți.

**14. ⭐ De ce trebuie ca m < n (sau m < p)?**
Pentru că rezultatele sunt calculate modulo n. Dacă m ≥ n, la decriptare obții m mod n, nu m, și mesajul se pierde. Mesajul meu are 95 de biți, iar n are 2048, deci încape într-un singur bloc.

**15. Ce faci dacă mesajul e mai lung decât n?**
Îl împarți în blocuri mai mici decât n și criptezi fiecare bloc separat. În practică, cu RSA criptezi doar o cheie AES, iar mesajul îl criptezi cu AES.

**16. Cum ai transformat numărul înapoi în text?**
Cu `IntegerDigits[m, 256]` am obținut lista de octeți, iar cu `FromCharacterCode` am obținut caracterele.

---

## 3. RSA

**17. ⭐ Care sunt pașii generării cheilor RSA?**
1. Alegi două numere prime mari p ≠ q.
2. Calculezi n = p·q.
3. Calculezi φ(n) = (p−1)(q−1).
4. Alegi e cu 1 < e < φ(n) și gcd(e, φ(n)) = 1.
5. Calculezi d = e⁻¹ mod φ(n).

Cheia publică este (e, n), iar cea privată (d, n).

**18. ⭐ Formulele de criptare și decriptare?**
Criptare: c = m^e mod n. Decriptare: m = c^d mod n.

**19. ⭐ De ce funcționează decriptarea?**
Pentru că e·d = 1 + t·φ(n), iar după teorema lui Euler m^φ(n) ≡ 1 (mod n). Rezultă că c^d = m^(ed) = m·(m^φ(n))^t ≡ m·1 = m (mod n).

**20. Ce este φ(n) (funcția lui Euler)?**
Numărul de numere din intervalul 1..n prime cu n. Pentru un număr prim p, φ(p) = p − 1. Pentru n = p·q, φ(n) = (p−1)(q−1).

**21. ⭐ Cum se alege e?**
Trebuie să respecte două condiții: 1 < e < φ(n) și **gcd(e, φ(n)) = 1**, ca să existe d. Cum φ(n) e mereu par, e trebuie să fie impar. Valoarea standard e **65537**.

**22. ⭐ De ce 65537?**
- E prim, deci aproape sigur prim cu φ(n). Am verificat totuși: `GCD[e, phi]` = 1.
- E egal cu 2¹⁶ + 1 și are doar doi biți de 1 în binar, deci criptarea e foarte rapidă.
- E destul de mare ca să evite atacurile cunoscute pentru e foarte mic (de exemplu e = 3, fără padding).

**23. Ce se întâmplă dacă gcd(e, φ(n)) ≠ 1?**
Atunci e nu are invers modulo φ(n), deci nu poți calcula d și nu mai poți decripta. Trebuie ales alt e.

**24. Poți alege e aleatoriu?**
Da. Alegi un număr aleatoriu și, dacă gcd ≠ 1, încerci altul:
`While[GCD[e, phi] != 1, e = RandomInteger[{3, phi - 1}]]`.

**25. ⭐ De ce p și q au câte 1024 de biți?**
Enunțul cere n ≥ 2048 de biți. Produsul a două numere de câte 1024 de biți are 2047 sau 2048 de biți. Am ales p și q din intervalul [3·2¹⁰²², 2¹⁰²⁴ − 1], deci p·q ≥ 9·2²⁰⁴⁴ > 2²⁰⁴⁷. Așa n are **exact 2048** de biți (`BitLength[n]` = 2048).

**26. De ce p ≠ q?**
Dacă p = q, atunci n = p² și p = √n se află imediat, deci sistemul e spart.

**27. Ce trebuie păstrat secret în RSA?**
d, dar și p, q și φ(n). Oricare dintre ele permite calcularea lui d.

**28. ⭐ Cum ar sparge cineva RSA?**
Ar trebui să-l factorizeze pe n, ca să obțină p, q, apoi φ(n) și d. Pentru n de 2048 de biți nu se cunoaște niciun algoritm care să termine într-un timp rezonabil. Cel mai bun e GNFS (General Number Field Sieve).

**29. Care e cel mai mare număr RSA factorizat public?**
RSA-250, de 829 de biți, în 2020, după aproximativ 2700 de ani-nucleu de calcul. 2048 de biți e mult peste asta.

**30. De ce RSA „simplu” (textbook RSA) nu e sigur în practică?**
Pentru că e **determinist**: același m dă mereu același c, deci un atacator poate ghici și verifica mesaje. Mai e și **maleabil**: c·2^e se decriptează la 2m. De aceea în practică se folosește un padding aleatoriu (OAEP).

**31. Cum se face o semnătură digitală cu RSA?**
Invers față de criptare: semnezi cu cheia privată, s = h(m)^d mod n. Oricine verifică cu cheia publică: s^e mod n = h(m).

**32. ⭐ Ce ai verificat în Wolfram la RSA?**
- `{BitLength[p], BitLength[q], PrimeQ[p], PrimeQ[q]}` = {1024, 1024, True, True};
- `BitLength[n]` = 2048;
- `GCD[e, phi]` = 1;
- `Mod[e*d, phi]` = 1;
- `m2 == m` = True;
- textul decriptat este „Dulgheru Ion”.

**33. Ce face `RandomPrime[{a, b}]`?**
Returnează un număr prim pseudoaleator din intervalul [a, b].

**34. Cum știi că `PrimeQ` spune adevărul pentru numere atât de mari?**
Folosește teste probabilistice de primalitate (Miller-Rabin și Lucas, adică BPSW). Pentru acestea nu se cunoaște niciun contraexemplu, deci în practică rezultatul e sigur.

**35. De ce ciphertextul c nu seamănă deloc cu mesajul?**
Pentru că m^e e redus modulo n, un număr de 2048 de biți, iar rezultatul arată ca un număr aleatoriu de aproximativ 617 cifre. Fără d nu se poate face legătura înapoi.

---

## 4. ElGamal

**36. ⭐ Care sunt pașii la ElGamal?**
- **Chei:** alegi x aleatoriu (privat) și calculezi y = g^x mod p (public).
- **Criptare:** alegi k aleatoriu și calculezi c₁ = g^k mod p și c₂ = m·y^k mod p.
- **Decriptare:** s = c₁^x mod p și m = c₂·s⁻¹ mod p.

**37. ⭐ De ce funcționează decriptarea ElGamal?**
Pentru că s = c₁^x = g^(kx) = (g^x)^k = y^k. Deci c₂·s⁻¹ = m·y^k·y^(−k) = m.

**38. Ce rol are y^k?**
E o „mască” de unică folosință care ascunde mesajul. Destinatarul o poate recalcula din c₁ cu cheia lui privată x.

**39. ⭐ De ce k trebuie să fie nou la fiecare mesaj?**
Dacă folosești același k pentru m₁ și m₂, masca y^k e aceeași. Atunci c₂/c₂' = m₁/m₂, iar dacă un mesaj e cunoscut, îl afli și pe celălalt. Pe lângă asta, k trebuie ținut secret: dacă k se află, m = c₂·(y^k)⁻¹ se poate calcula fără x.

**40. Din ce interval se aleg x și k?**
Din [2, p−2]. Am exclus 0, 1 și p−1, pentru că dau valori triviale: g⁰ = 1, g¹ = g și g^(p−1) = 1.

**41. ⭐ De ce ciphertextul ElGamal e de două ori mai lung decât mesajul?**
Pentru că e format din două numere, c₁ și c₂, fiecare de 2048 de biți. Acesta e prețul randomizării.

**42. Care e avantajul randomizării?**
Același mesaj criptat de două ori dă ciphertexturi diferite. Un atacator nu poate verifica un mesaj ghicit, așa cum poate la textbook RSA.

**43. Cum se calculează s⁻¹?**
Cu `PowerMod[s, -1, pp]` (Euclid extins). Variantă: s^(p−2) mod p, după mica teoremă a lui Fermat.

**44. Pe ce se bazează securitatea ElGamal?**
Pe logaritmul discret: din y = g^x mod p nu poți afla x. Mai precis, pe problema Diffie-Hellman: din g^x și g^k nu poți calcula g^(xk).

**45. ⭐ Ce se știe despre p-ul din enunț?**
- Are 2048 de biți și este prim (`{BitLength[pp], PrimeQ[pp]}` = {2048, True}).
- E primul standard **ffdhe2048** din **RFC 7919**, folosit în TLS.
- E un **safe prime**: și (p−1)/2 e prim.

**46. Ce este un safe prime și de ce contează?**
Un număr prim p pentru care q = (p−1)/2 e și el prim. Atunci subgrupurile posibile au ordinele 1, 2, q și 2q, deci nu există subgrupuri mici în care logaritmul discret să fie ușor (de exemplu atacul Pohlig-Hellman).

**47. Ce înseamnă că g este generator?**
Că puterile lui g (g¹, g², …) parcurg toate elementele unui grup mare. La ffdhe2048, g = 2 generează subgrupul de ordin q = (p−1)/2, care e destul de mare pentru securitate.

**48. De ce ai numit variabila `pp`, nu `p`?**
Ca să nu suprascriu numărul prim p de la RSA din același notebook.

**49. Ce eroare ai avut la ElGamal și cum ai rezolvat-o?**
Am scris două expresii pe același rând fără separator. Wolfram le-a interpretat ca **înmulțire**, iar `FromCharacterCode` a primit un număr greșit (eroare + `$RecursionLimit`). Am rulat `Clear[s, mE]` și am pus fiecare comandă pe rândul ei.

**50. Poate ElGamal fi folosit și pentru semnături?**
Da. Din schema de semnătură ElGamal derivă **DSA**, standardul american de semnătură digitală.

---

## 5. Diffie-Hellman

**51. ⭐ Ce face protocolul Diffie-Hellman?**
Permite ca două persoane care nu au niciun secret comun să stabilească o cheie secretă comună comunicând **doar pe un canal public**. Protocolul nu criptează mesaje, ci doar stabilește cheia.

**52. ⭐ Care sunt pașii?**
1. p și g sunt publice.
2. Alisa alege a secret și îi trimite lui Bob A = g^a mod p.
3. Bob alege b secret și îi trimite Alisei B = g^b mod p.
4. Alisa calculează K = B^a mod p, iar Bob calculează K = A^b mod p.

**53. ⭐ De ce obțin amândoi aceeași cheie?**
B^a = (g^b)^a = g^(ab) = (g^a)^b = A^b (mod p). În Wolfram, `kAlisa == kBob` a dat **True**.

**54. Ce vede un atacator pasiv și de ce nu poate afla K?**
Vede p, g, A și B. Ca să calculeze g^(ab) ar trebui să afle a sau b din A sau B, adică să rezolve logaritmul discret, ceea ce e imposibil practic pentru p de 2048 de biți.

**55. ⭐ Ce este atacul man-in-the-middle?**
Atacatorul (Eva) interceptează A și B și le înlocuiește cu propriile valori. Alisa stabilește o cheie cu Eva, iar Bob altă cheie cu Eva, și Eva citește tot. DH simplu **nu autentifică** părțile. Soluția: valorile A și B se semnează (cu certificate sau semnături digitale).

**56. Ce înseamnă „a și b aleși aleatoriu conform cerințelor algoritmului”?**
Să fie secrete, să fie generate de un generator aleatoriu și să se afle în intervalul [2, p−2]. Am folosit `RandomInteger[{2, pp - 2}]`.

**57. De ce nu se afișează a și b?**
Pentru că sunt secrete și nu se transmit niciodată. Pe canal circulă doar A și B. De aceea în notebook comenzile pentru a și b se termină cu „;”.

**58. ⭐ Cum ai obținut cheia AES de 256 de biți din secretul de 2048 de biți?**
Am aplicat **SHA-256** pe cei 256 de octeți ai secretului K:
`Hash[ByteArray[IntegerDigits[kAlisa, 256, 256]], "SHA256"]`.
Rezultatul are exact 256 de biți (64 de cifre hex). Cheia mea: `6431ea8227eccdad6839ee417aaeac7dbcdb7139a42bf9c9af5d975a06bcf0a1`.

**59. De ce SHA-256 și nu pur și simplu primii sau ultimii 256 de biți?**
Ambele variante funcționează (alternativa e `Mod[kAlisa, 2^256]`). Hash-ul e însă mai corect: K nu e uniform distribuit pe biți, iar funcția hash „amestecă” toți cei 2048 de biți, deci cheia AES rezultată e uniformă. Asta face o **funcție de derivare a cheii** (KDF), de exemplu HKDF în TLS.

**60. De ce `IntegerDigits[kAlisa, 256, 256]` și nu doar `IntegerDigits[kAlisa, 256]`?**
Al treilea argument fixează lungimea la exact 256 de octeți, cu zerouri în față dacă e nevoie. Așa Alisa și Bob hash-uiesc aceeași secvență de octeți, indiferent de mărimea lui K.

**61. Cum continuă comunicarea după DH?**
Alisa și Bob criptează mesajele cu **AES-256** folosind cheia comună. DH a rezolvat doar problema distribuirii cheii.

**62. Ce este „forward secrecy” (DHE)?**
Dacă la fiecare sesiune se aleg a și b noi și apoi se șterg, compromiterea ulterioară a unei chei pe termen lung nu dezvăluie sesiunile trecute. Așa funcționează TLS 1.3.

**63. Cât de mare e numărul de chei AES-256 posibile?**
2²⁵⁶ ≈ 1,16·10⁷⁷, imposibil de încercat prin forță brută.

---

## 6. Wolfram și desfășurarea lucrării

**64. ⭐ De ce Wolfram Cloud și nu wolframalpha.com?**
WolframAlpha răspunde la întrebări independente și nu ține minte variabilele de la un pas la altul. Cu numere de 2048 de biți ar trebui să le copiezi manual la fiecare pas. În notebook-ul din Wolfram Cloud (Mathematica în browser) variabilele rămân în memorie.

**65. Ce face „;” la finalul unei comenzi?**
Comanda se calculează, dar rezultatul nu se afișează.

**66. De ce nu ai folosit variabilele N, E, C, D?**
Pentru că în Wolfram literele mari singure sunt rezervate: `N` e funcție de evaluare numerică, `E` e constanta e, `I` e unitatea imaginară, iar `C` și `D` sunt și ele simboluri predefinite (`D` e derivata). De aceea am folosit `n`, `e`, `c`, `d`.

**67. Explică `PowerMod[a, b, n]`.**
Calculează a^b mod n eficient, fără să calculeze mai întâi a^b, care ar avea miliarde de cifre. Cu b = −1 returnează inversul modular.

**68. Ce face `GCD`?**
Calculează cel mai mare divizor comun. La RSA îl folosim ca să verificăm că e e prim cu φ(n).

**69. Ce face `BitLength[n]`?**
Returnează numărul de biți necesari pentru a scrie n în binar, adică ⌊log₂ n⌋ + 1.

**70. Dacă rulezi din nou notebook-ul, obții aceleași numere?**
Nu. p, q, x, k, a și b sunt aleatorii, deci cheile și ciphertexturile se schimbă. Mesajul decriptat e însă mereu „Dulgheru Ion”, iar `kAlisa == kBob` e mereu True.

**71. Cum ai verifica decriptarea fără să te uiți la text?**
Comparând numerele: `m2 == m` dă True.

---

## 7. Comparații

**72. ⭐ Compară RSA cu ElGamal.**

| | RSA | ElGamal |
|---|---|---|
| Problemă grea | factorizarea | logaritmul discret |
| Ciphertext | un număr (cât n) | două numere (de 2× lungime) |
| Determinist? | da (textbook) | nu (folosește k aleatoriu) |
| Parametri comuni | fiecare are propriul n | p și g pot fi comuni tuturor |

**73. Compară ElGamal cu Diffie-Hellman.**
ElGamal e practic Diffie-Hellman „cu un pas în plus”. Destinatarul publică y = g^x (ca A din DH), iar expeditorul face un DH efemer cu k (c₁ = g^k). Secretul comun y^k e folosit ca mască pentru mesaj.

**74. Este cheia publică suficientă pentru a decripta?**
Nu. Cu cheia publică doar criptezi. Pentru decriptare e nevoie de cheia privată (d sau x).

**75. Dacă cineva află φ(n), poate sparge RSA?**
Da. Calculează imediat d = e⁻¹ mod φ(n). În plus, din n și φ(n) se pot afla p și q, rezolvând o ecuație de gradul 2.

**76. Ce dimensiune de cheie e recomandată azi?**
Pentru RSA și DH, minim 2048 de biți (NIST: sigur până în aproximativ 2030), iar pentru siguranță pe termen lung, 3072 de biți. AES-256 e considerat sigur pe termen lung.

**77. Ce fac calculatoarele cuantice cu acești algoritmi?**
Algoritmul lui Shor ar rezolva eficient atât factorizarea, cât și logaritmul discret, deci RSA, ElGamal și DH ar fi sparte. De aceea se dezvoltă criptografia post-cuantică (de exemplu ML-KEM, standardizat de NIST în 2024). AES-256 rămâne sigur, pentru că Grover îi reduce securitatea doar la aproximativ 128 de biți.

**78. Se poate folosi RSA pentru schimbul de chei?**
Da. Alisa generează o cheie AES și o criptează cu cheia publică RSA a lui Bob. Diferența față de DH: cheia e aleasă de o singură parte și nu există forward secrecy.

**79. De ce trebuie ca numerele aleatorii să fie generate criptografic sigur?**
Dacă p, q, x, k, a sau b sunt previzibile, atacatorul le poate ghici și sparge sistemul fără să rezolve problema matematică.

**80. Ce concluzii rezultă din lucrare?**
- Criptografia asimetrică rezolvă problema distribuirii cheilor.
- RSA se bazează pe factorizare, iar ElGamal și DH pe logaritmul discret.
- Toate operațiile sunt exponențieri modulare cu numere de 2048 de biți.
- În practică, cheia publică servește la stabilirea unei chei simetrice (AES-256), cu care se criptează apoi datele.

---

## 8. Mini-exemple de calcul de mână

**81. RSA cu p = 5, q = 11, e = 3, m = 4.**
- n = 55 și φ = 4·10 = 40; gcd(3, 40) = 1.
- d = 27, pentru că 3·27 = 81 = 2·40 + 1.
- c = 4³ mod 55 = 64 mod 55 = **9**.
- Decriptare: 9²⁷ mod 55 = **4** ✓.

**82. ElGamal cu p = 23, g = 5, x = 6, k = 3, m = 10.**
- y = 5⁶ mod 23 = 8.
- c₁ = 5³ mod 23 = 125 mod 23 = **10**.
- y^k = 8³ mod 23 = 512 mod 23 = 6, deci c₂ = 10·6 mod 23 = 60 mod 23 = **14**.
- Decriptare: s = 10⁶ mod 23 = 6, s⁻¹ = 4 (6·4 = 24 ≡ 1), m = 14·4 mod 23 = 56 mod 23 = **10** ✓.

**83. Diffie-Hellman cu p = 23, g = 5, a = 6, b = 15.**
- A = 5⁶ mod 23 = **8** și B = 5¹⁵ mod 23 = **19**.
- Alisa: K = 19⁶ mod 23 = **2**. Bob: K = 8¹⁵ mod 23 = **2** ✓.

**84. Cum calculezi rapid 5⁶ mod 23 de mână?**
- 5² = 25 ≡ 2.
- 5⁴ = 2² = 4.
- 5⁶ = 5⁴·5² = 4·2 = **8**.

Asta e ideea exponențierii rapide: ridici la pătrat și reduci modulo p după fiecare pas.
