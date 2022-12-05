# PNS-SI4-S7_LetsGoBiking

## Comment lancer

Avant tout, il est supposé que vous ayez lancé le serveur ActiveMQ depuis son dossier `apache-activemq-5.17.2\bin\win64` l'exécutable `wraper.exe` en tant qu'administrateur.

### Serveurs `C#`

Une fois les exécutables `debug` générés avec Visual Studio

- Si vous possédez Windows Terminal
  - lancez depuis un Windows PowerShell administrateur le fichier  
  `wt run servers.bat` à la racine du projet, une copie des fichiers ActiveMQ est aussi contenu dans le dossier `lib`, il permet a ce script de tourner sur nimporte quelle machine Windows les 2 serveurs et ActiveMQ en une ligne de commande
- Sinon
  - Lancez les exécutables  
  `.\C#\LetGoBikingCS\RoutingServer\bin\Debug\RoutingServer.exe`  
  et  
  `.\C#\LetGoBikingCS\ProxyServer\bin\Debug\ProxyServer.exe`

### Client `Java`

#### Entrées du client

Les données d'entrée du client (adresse/coordonnée de départ, adresse/coordonnée d'arrivée) sont dans le fichier `Java\HeavyClient\input.txt`

##### Format du fichier

2 lignes : la première ligne est l'adresse de départ, la seconde est l'adresse d'arrivée.

le format pour chaque entrée est le suivant : `addr:<adresse>` ou `coord:<latitude>,<longitude>`, il est possible de mélanger les deux formats dans le fichier.

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
