VAR firstTimeTalking = false

== Start ==
{firstTimeTalking == false: ->FirstTalk | ->OtherTalk}

== FirstTalk ==
~firstTimeTalking = true
Narcisse?excited "Hello
Narcisse?excited "Welcome to this small town
Narcisse?excited "There not a lot of thing going around here but I hope you will enjoy your stay
-> ContinueTalk

== OtherTalk ==
Narcisse?excited "Oh it's you again.
-> ContinueTalk

== ContinueTalk ==
#interacttext
Narcisse?happy "The weather is pretty good today, I recommand you to go for a stroll around the <color=green>Balto monument</color>.
Narcisse?happy "I wish you a nice day.
#ENDING
-> END


== BlueChoice ==
Narcisse?thinking "The Balto monument is a famous spot around here.
Narcisse?explaining "Back in the day, people used to send prayer to the status for succes in the arena.
Narcisse?explaining "Nowaday it's just an old relic good to entertain the local.
Narcisse?happy "Anyway, I wish you a nice day.
#ENDING
-> END

== GreenChoice ==
green
->END
