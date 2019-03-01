# Préparer la modernization de vos applications avec les containers

* https://github.com/42skillz/AzureDevCamp20190313

## Informations

* Le code est tiré d'un livecoding 42 skillz (@tpierrain & @brunoboucard)
	* Code en C# sur .NET Core 2.1 https://github.com/42skillz/liveCoding-LegacyTrain
	* Une version Java 1.8 est disponible https://github.com/42skillz/liveCoding-LegacyTrain-java
	* Qui est lui même inspiré par https://github.com/emilybache/KataTrainReservation

* Vidéos
	* Après midi du DDD https://www.youtube.com/watch?v=qzygjKpFSq4&t=4311s
	* How To Distill The Core Domain From Your Legacy App - Explore DDD https://www.youtube.com/watch?v=mZzPwt9vhHM&t=4301s
	* Distill the Core Domain from Your Legacy App - DDD Europe 2018 https://www.youtube.com/watch?v=F3DV9YDeA6Q&t=1527s

* Services
	* TrainDataService: http://localhost:50680/api/data_for_train/341RTFA
	* BookingReferenceService: http://localhost:51691/booking_reference
	* TrainTrain: http://localhost:53801/api/reservations?trainId=express_20008&numberOfSeats=3

## Explication du métier

* 11H00
* Durée: 15 minutes
* Qui sommes-nous (TrainTrain)
* Expliquer de l'architecture actuelle (skechnotes)
* Illustrer les règles métiers

## Démo "All in one container"

* 11H15
* Durée: 20 minutes
* Branche: Livecode1
* Démo: exécution locale puis Clean the deck
* K8s: 3 containers Docker (sans K8s)

## Expliquer l'architecture K8s

* 11H35
* Durée: 5 minutes
* Explication de l'architecture K8s avec les service Train & HassanCehef (dessin)

## Démo "Tester WebTickerManager"

* 11H40
* Durée: 20 minutes
* Branche: Livecode2
* Demo: Poser un 1er test => casser une dépendance (TrainDataServiceAdapter)
* Demo: LiveCode2_after_2_tests & monter le résultat
* K8s: On pousse dans K8s deux containers séparés
  * TrainTrain
  * HassanCehef

## 12H00 "Déjeuner"

## C'est quoi un microservice

* 13H00
* Durée: 10 minutes
* Présenter quelques slides MS

## Démo "Problème métier en production"

* 13H10
* Durée: 30 minutes
* Branche: Livecode3_issue_in_production
* On reçoit un message du support avec la description: 
  * train with 2 coaches and 9 seats already reserved in the first coach
* Création d'un test => rouge
* Création du type coach en TDD
* Test corriger
* Branche: Livecode3_bug_fixed
* K8s: On repousse à nouveau dans le K8s

## Conclusion "Peut-on aller plus loin ...""

* 13H40
* Durée: 20 minutes
* Echanges: Pour les microservices on doit respecter quelques règles:
  * Un ou deux comportements par microservice
  * Complètement testé
  * Une architecture logicielle hexagonale
* Branche: Livecode4