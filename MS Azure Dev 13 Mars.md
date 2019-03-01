# Étapes de l'étude de cas

## Explication métier

* Durée: 15 minutes
* Qui sommes-nous (TrainTrain)
* Explication de l'architecture actuelle (dessin)
* Explication des règles métiers

## Livecode 1 "All in one container"

* Durée: 20 minutes
* Code: branche LiveCode1
* Demo: exécution locale puis Clean the deck
* K8s: 3 containers Docker (sans K8s)

## Expliquer l'architecture K8s 1er étape

* Durée: 5 minutes
* Explication de l'archecture K8s avec l'application (dessin)

## Livecode 2 "Tester WebTickerManager"

* Durée: 20 minutes
* Code: LiveCode2
* Demo: poser un 1er test => casser une dépendance (TrainDataServiceAdapter)
* Demo: LiveCode2_after_2_tests & monter le résultat
* K8s: On pousse dans K8s deux containers séparés
	* TrainTrain
	* HassanCehef

## Break - Déjeuner

## C'est quoi un microservice

* Durée: 10 minutes
* Présenter quelques slides MS

## Livecode 3 Problème métier en production

* Durée: 20 minutes
* On reçoit un message du support avec la description: 
	* train with 2 coaches and 9 seats already reserved in the first coach
* Création d'un test => rouge
* Création du type coach en TDD
* Test corriger
* K8s: On repousse à nouveau dans le K8s

## Conclusion: Peut-on aller plus loin

* Durée: 20 minutes
* Echanges: Pour les microservices on doit respecter quelques règles:
	* Un ou deux comportements par microservice
	* Complètement testé
	* Une architecture logicielle hexagonale
* Montrer la solution avec Hexagone
