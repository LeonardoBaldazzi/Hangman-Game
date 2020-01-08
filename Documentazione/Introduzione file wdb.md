# Introduzione ai file wdb
I file ".wdb" (Word Database) rispondono all'esigenza di poter mantenere un database di parole/frasi facilmente aggiornabile e da gestire (si presta molto per applicazioni multilingua), sono scritti in un linguaggio interpretato direttamente dal programma che ne fa richiesta. (Provvederò popi ad inserire un'eventuale link al src dopo aver steso le funzionalità basi)

## Sintassi
La sintassi segue le caratteristiche di un linguaggio di markup (aka CSS o HTML), di seguito i costrutti base:

Per una parola si utilizza  il costrutto:
```bash
<wd id=[NUMERO_IDENTIFICATIVO] content=[PAROLA] -Altri Argomenti>
```
Il numero identificativo (id) serve per poter accedere successivamente al dato (Es. Descrizioni)

Per poter aggiungere delle descrizioni alla parola si usa il seguente costrutto:
```bash
<desc id=[NUMERO_IDENTIFICATIVO]>
    <Numero della descrizione>[DESCRIZIONE]</Numero della descrizione>
</desc>
```

Le descrizioni vengono usate all'interno del progetto per poter definire gli aiuti

I file .wdb si trovano dentro la directory "/impiccato v1/wdb/src"