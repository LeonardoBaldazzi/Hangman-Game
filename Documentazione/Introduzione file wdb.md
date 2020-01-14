# Introduzione ai file wdb
I file ".wdb" (Word Database) rispondono all'esigenza di poter mantenere un database di parole/frasi facilmente aggiornabile e da gestire (si presta molto per applicazioni multilingua), sono scritti in un linguaggio interpretato direttamente dal programma che ne fa richiesta. (Provvederò poi ad inserire un'eventuale link al src dopo aver esteso le funzionalità basi)

## Sintassi
La sintassi segue le caratteristiche di un linguaggio di markup (aka CSS o HTML), di seguito i costrutti base:

Per una parola si utilizza  il costrutto:
```bash
<wd id=[NUMERO_IDENTIFICATIVO] content=[PAROLA]>
```
Il numero identificativo (id) serve per poter accedere successivamente al dato (Es. Descrizioni)

Per poter aggiungere delle descrizioni alla parola si usa il seguente costrutto:
```bash
<desc id=[NUMERO_IDENTIFICATIVO] content=[DESCRIZIONE]>
```
Le descrizioni vengono usate all'interno del progetto per poter definire gli aiuti


Invece, per poter raggruppare più costrutturi sotto ad un'unico gruppo, si usa il costrutto "gr":
```bash
<gr=[NOME AL GRUPPO]>
    ....CODICE....
</gr>
```
I gruppi vengono usati all'interno del gioco dell'impiccato per dividere le parole in difficoltà diverse

<u>E' possibilità dell'utente modificare i file di configurazione di WDB per poterne modificare i contenuti</u>

<b>I file .wdb si trovano, in questo progetto, dentro la directory "/impiccato v1/bin/Debug/lib"</b>
