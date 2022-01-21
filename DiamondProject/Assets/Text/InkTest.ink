-> Start
VAR numberOfDay = 0
VAR isIntroAlreadySaw = false

== Start ==
{isIntroAlreadySaw == false: Il était une fois... Un fermier dans un champ. Qu'est-ce qu'il va faire aujourd'hui ? -> Choix | Le jour se lève, le fermier retourne dans son champ -> Choix }

== Choix ==
~isIntroAlreadySaw = true
 * [Partir] -> Partir
 + [Rester] -> Rester
 + [Répéter] -> Répéter
 
 == Partir ==
 Il marcha jusqu'au bout de la route.
 -> End_Of_The_Day
 
 == Rester ==
 Il descida de profiter du soleil.
 -> End_Of_The_Day
 
 #color blue
 == Répéter == 
 J'ai demandé ce qu'il va faire aujourd'hui.
 -> Choix
 
 == End_Of_The_Day ==
 ~numberOfDay = numberOfDay + 1

 La nuit tomba et il rentra chez lui.
 
 {numberOfDay > 3: Une météorite s'écrase sur la ferme. -> DONE|-> Start}
