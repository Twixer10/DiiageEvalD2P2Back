# EvalD2P2.AzureFunctions

Ce projet est une application Azure Functions qui fournit une API pour gérer des événements. Il est écrit en C# et utilise le runtime Azure Functions en mode isolé.

## Fonctionnalités

L'application fournit les fonctionnalités suivantes :

- Récupérer tous les événements avec pagination
- Récupérer un événement spécifique par son ID
- Créer un nouvel événement
- Mettre à jour un événement existant
- Supprimer un événement existant

Ces fonctionnalités sont exposées via des fonctions HTTP déclenchées par des requêtes GET.

## Design Pattern Factory

Dans ce projet, nous utilisons le design pattern Factory. Le pattern Factory est un pattern de création qui fournit une interface pour créer des objets dans une superclasse, mais permet aux sous-classes de modifier le type d'objets qui seront créés.

Nous utilisons le pattern Factory pour déléguer la logique de création d'objet à des classes spécifiques et nous assurer que ces classes sont responsables de la création de l'objet. Cela nous permet de fournir aux utilisateurs de notre classe une manière de créer des objets sans exposer la logique de création. De plus, cela nous permet d'implémenter des principes de programmation tels que l'encapsulation, l'abstraction et la polymorphisme.

## Configuration

Le projet utilise les fichiers `host.json` et `local.settings.json` pour configurer le comportement de l'application Azure Functions et définir les paramètres de l'application.

## Base de donnée.

Malheureusement, je n'ai pas eu le temps de mettre en le systeme de migration automatique. Vous devez changer la chaine de connection dans `EvalD2P2.AzureFunctions/Program.cs` ligne 16 et dans `EvalD2P2.DAL/EvalD2P2DbContextFactory.cs` ligne 13, par votre chaine de connection à votre base de donnée MSSQL.
Une fois que vous avez fait cela, vous pouvez lancer la commande `dotnet ef database update` pour créer la base de donnée.

## Dépendances

Le projet dépend de plusieurs packages NuGet, notamment :

- Microsoft.Azure.Functions.Worker : pour le runtime Azure Functions
- Newtonsoft.Json : pour la sérialisation et la désérialisation JSON
- NUnit et Moq : pour les tests unitaires

## Tests

Le projet comprend des tests unitaires qui utilisent le framework NUnit et la bibliothèque d'objets fictifs Moq.
Malheureusement, je n'ai pas eu le temps de faire les tests unitaires pour les fonctions, l'idée est là, mais le test ne fonctionne pas.
D'autre part, je ne comprend pas l'intérêt de tester une azure function, ou même de tester du CRUD...
