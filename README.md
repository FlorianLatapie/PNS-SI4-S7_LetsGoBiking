# PNS-SI4-S7_LetsGoBiking

## Comment lancer

Avant tout, lancez le serveur ActiveMQ (`apache-activemq-5.17.2\bin\win64`) avec l'exécutable `wraper.exe`.

### Serveurs `C#`

Une fois les exécutables `debug` générés avec Visual Studio

- Si vous possédez Windows Terminal
  - lancez depuis un Windows PowerShell administrateur le fichier  
  `wt run servers.bat`
- Sinon
  - Lancez les exécutables  
  `.\C#\LetGoBikingCS\ProxyServer\bin\Debug\ProxyServer.exe`  
  puis  
  `.\C#\LetGoBikingCS\RoutingServer\bin\Debug\RoutingServer.exe`  

### Client `Java`

#### Entrées du client

Les données d'entrée du client (adresse/coordonnée de départ, adresse/coordonnée d'arrivée) sont dans le fichier `Java\HeavyClient\input.txt`

##### Format du fichier

2 lignes : la première ligne est l'adresse de départ, la seconde est l'adresse d'arrivée.

Le format pour chaque entrée est le suivant : `addr:<adresse>` ou `coord:<latitude>,<longitude>`, il est possible de mélanger les deux formats dans le fichier.

exemple :

```txt
addr:rue pelisson villeurbanne
addr:rue tronchet lyon
```

ou encore :

```txt
coord:45.7737023,4.8868265
coord:45.7708222,4.8578873
```

et même :

```txt
addr:rue pelisson villeurbanne
coord:45.7708222,4.8578873
```

- Si vous possédez Windows Terminal
  - lancez depuis un Windows PowerShell administrateur le fichier  
  `wt run java client.bat` à la racine du projet
- Sinon lancez le client avec la commande
  - Placez vous dans le dossier `Java\HeavyClient`  
    `cd Java\HeavyClient`
  - Lancez le client  
    `mvn compile exec:java`
