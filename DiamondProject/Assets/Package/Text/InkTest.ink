VAR numberOfDay = 0
VAR isIntroAlreadySaw = false
VAR isImportant = false
VAR isInteractible = false
VAR isInteracting = true

-> Start

== Start ==
{isIntroAlreadySaw == false: Il était une fois... Un fermier dans un champ. Qu'est-ce qu'il va faire aujourd'hui ? -> Choix | Le jour se lève, nous sommes au {numberOfDay} jour. Le fermier retourne dans son champ -> Choix }

== Choix ==
~isIntroAlreadySaw = true
 + [Partir] -> Partir
 + [Rester] -> Rester
 + [Répéter] -> Répéter
 
 == Partir ==
 Il parti rendre visite à son voisin.
{
    - numberOfDay == 0: 
        Sherlock?annoyed "Son voisin, Monsieur Carl, était entrain de <color=blue>plier bagage</color> en précipitation.
         {isInteracting == true: -> InteractionVoisin}
        Comme il avait l'air pressé, le fermier a descidé de le laisser tranquille -> End_Of_The_Day
    - numberOfDay > 0: 
        Il n'y trouva personne. -> End_Of_The_Day
}
 == InteractionVoisin ==
 ~isInteracting = false
 + [Pourquoi es-tu entrain de faire tes valises ?] -> Explication 
 
 == Explication ==
TESTESTEST.
 Sherlock?explaining "Ma cousine voyante m'a prévenu qu'une <color=red>calamité va s'abattre dans 3 jours</color>. 
  Sherlock?explaining "Je te recommande de faire de même et de déguerpir rapidement. Répondit il de manière paniqué
 -> End_Of_The_Day

 == Rester ==
 Il descida de profiter du soleil.
 -> End_Of_The_Day
 
 == Répéter == 
 J'ai demandé ce qu'il va faire aujourd'hui.
 -> Choix
 
 == End_Of_The_Day ==
 ~numberOfDay = numberOfDay + 1

 La nuit tomba et il rentra chez lui.
 
 {numberOfDay > 3: Une météorite s'écrase sur la ferme rasant les alentours sur plusieurs kilomètres.-> DONE|-> Start}
