# PNS-SI4-S7_LetsGoBiking

## Comment lancer

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

- Si vous possédez Windows Terminal
  - lancez depuis un Windows PowerShell administrateur le fichier  
  `wt run java client.bat` à la racine du projet
- Sinon lancez le client avec la commande
  - Placez vous dans le dossier `Java\HeavyClient`  
    `cd Java\HeavyClient`
  - Lancez le client  
    `mvn compile exec:java`
