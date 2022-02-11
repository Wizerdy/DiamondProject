VAR numberOfDay = 0
VAR isIntroAlreadySaw = false
VAR isImportant = false
VAR isInteractible = false
VAR isInteracting = true
VAR hasInteractedWithNeighbour = false

-> Start

== Start ==

{
    -isIntroAlreadySaw == false: Narrator?neutral "As-tu déjà entendu parler des livres dont tu es le héro ? 
        Narrator?happy "En voila un exemple. 
        Narrator?happy "Il était une fois... Un fermier dans un champ. Qu'est-ce qu'il va faire aujourd'hui ? -> Choix 
    -else:  Narrator?neutral "Le jour se lève, nous sommes au {numberOfDay} jour. 
        Narrator?neutral "Le fermier retourne dans son champ -> Choix 
}

== Choix ==
~isIntroAlreadySaw = true
 + [Partir] -> Partir
 + [Rester] -> Rester
 + [Répéter] -> Répéter
 + {hasInteractedWithNeighbour == true} [Partir au loin] -> PartirLoin
 
 == Partir ==
 RESPONSE
 Narrator?pleased "Il parti rendre visite à son voisin.
{
    - numberOfDay == 0: 
        Sherlock?annoyed "Son voisin, Sherlock, était entrain de <color=blue>plier bagage</color> en précipitation.
         {isInteracting == true: -> InteractionVoisin}
        Narrator?sad "Comme il avait l'air pressé, le fermier a descidé de le laisser tranquille -> End_Of_The_Day
    - numberOfDay > 0: 
        Narrator?pained "Il n'y trouva personne. -> End_Of_The_Day
}
 == InteractionVoisin ==
 ~isInteracting = false
 + [Pourquoi es-tu entrain de faire tes valises ?] -> Explication 
 
 == Explication ==
TESTESTEST.
~hasInteractedWithNeighbour = true
 Sherlock?explaining "Ma cousine voyante m'a prévenu qu'une <color=red>calamité va s'abattre dans 3 jours</color>. 
  Sherlock?explaining "Je te recommande de faire de même et de déguerpir rapidement. Répondit il de manière paniqué
 -> End_Of_The_Day

 == Rester ==
 RESTER
 Narrator?worried "Il descida de profiter du soleil.
 -> End_Of_The_Day
 
 == Répéter == 
 Narrator?annoyed "J'ai demandé ce qu'il va faire aujourd'hui.
 -> Choix
 
 == PartirLoin ==
 REACTION
 Narrator?smirk "Le fermier c'est empressé de partir.
 {hasInteractedWithNeighbour == true: Narrator?smirk "Il a choisi de croire au mauvais présentiment de la cousine de Sherlock.}
 {
    -numberOfDay == 0: Narrator?triumphant "Trois jours plus tard, une météorite s'écrasa sur sa ferme, il l'a échappé belle. -> End_Of_Dialogue
    -numberOfDay == 1: Narrator?triumphant "Deux jours plus tard, une météorite s'écrasa sur sa ferme il l'a échappé belle. -> End_Of_Dialogue
    -numberOfDay == 2: Narrator?triumphant "Le lendemain, une météorite s'écrasa sur sa ferme il l'a échappé belle. -> End_Of_Dialogue
    -numberOfDay == 3: Narrator?triumphant "Le jour même, dans la soirée, une météorite s'écrasa sur sa ferme il l'a échappé belle. -> End_Of_Dialogue
}
 == End_Of_The_Day ==
 ~numberOfDay = numberOfDay + 1

 Narrator?neutral "La nuit tomba et il rentra chez lui.
 
 {numberOfDay == 3: Narrator?shocked "Une météorite s'écrase sur la ferme rasant les alentours sur plusieurs kilomètres.-> End_Of_Dialogue|-> Start}

== End_Of_Dialogue ==
Narrator?happy "C'était un example de ce que peut faire ces plugins.
# ENDING
-> END