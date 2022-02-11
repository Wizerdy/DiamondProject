VAR firstTimeTalking = false
VAR questionAsked = false

-> Start

== Start ==
{firstTimeTalking == false: Sherlock?excited "Welcome traveler. | Sherlock?happy "Oh, it's you again.}
{firstTimeTalking == false: Sherlock?explaining "Oh, it's your first time here ? I know a lot about this place. | Sherlock?happy "How are you doing since then ?}
Sherlock?explaining "So what do you need to know ?
-> Choices

== Choices ==
~firstTimeTalking = true
Chose a question.
+ [Where is the nearest hotel ?] -> HotelAnswer
+ [Have you seen a man with a snake tatoo on his left arm around ?] -> TatooQuestion
* [How do you do ?] -> Mood
+ {questionAsked == true}[Have a nice day] -> EndConversation

== Mood ==
RESPONSE
Sherlock?happy "I am fine thank you. 
~questionAsked = true
-> Choices

== HotelAnswer ==
RESPONSE
Sherlock?annoyed "The closest ? It would be the one at 55th Street but <color=blue>I wouldn't recommand it.</color> 
~questionAsked = true
-> Choices

== TatooQuestion ==
RESPONSE
Sherlock?thinking "If by man with a snake tatoo you mean the one with crimsom hair and tattered cloth, he passed by yesterday.
Sherlock?angry "He was pretty rude. It was a very memorable encounter.
Sherlock?deduction "If you are looking for him, I remember someone seeing him <color=green>around Joan's place this morning.</color>
~questionAsked = true
-> Choices

== EndConversation ==
END
Sherlock?happy "You too. # END CONVERSATION
-> END