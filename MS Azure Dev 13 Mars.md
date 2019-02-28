# Etapes de l'étude de cas

## Explication métier

* Durée: 15 minutes
* Qui sommes nous (TrainTrain)
* Explication de l'architecture actuel (dessin)
* Explicaiton des règles métiers
* Nos dépendances HasanCef

## Livecode 1 "All in one container"

* Durée: 20 minutes
* Code de départ AMDDD sans cache
* Demo locale
* Explication du code actuel
* WebTicketManager de dépard
* Exécution dans 3 containers Docker

## Expliquer l'architecture K8s 1er étape

* Durée: 5 minutes
* Explication de l'archecture K8s avec l'application (dessin)

## Livecode 2 "Tester WebTickerManager"

* Durée: 20 minutes
* Casse une dépendance (TrainDataServiceAdapter)
* On switch sur un résultat où toutes les dépendances sont cassée + Test vert happy path
* On pousse dans K8s

## Break - Déjeuner

## C'est quoi un microservice

* Durée: 10 minutes
* Présenter quelques slides MS

## Livecode 3 Problème métier en production

* Durée: 20 minutes
* On reçoit un message du support: avec la description
* Création d'un test => rouge
* Creation du type coach
* Test corriger
* On repousse à nouveau dans le K8s

## Conclusion: Peut-on aller plus loin

* Durée: 20 minutes

Pour les microservices on doit respecter quelques règles:

* Un ou deux comportements par microservice
* Complètement testé
* Une architecture logicielle hexagonale
* Montrer le solution avec Hexagone