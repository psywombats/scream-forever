wait(1)
exit('ALISTAIR')
setSwitch('scene5_alistair', true)
enterNVL()
speak('ALISTAIR', "Now do you any of you idiots want a drink or a snack?", 'no_highlight')
if choose("No thanks.", "Just an energy drink.") then
	speak('ALISTAIR', "Fine. Bianca?", 'no_highlight')
	setSwitch('askedForDrink', true)
else
	speak('ALISTAIR', "Noted. Bianca?", 'no_highlight')
end
speak('BIANCA', "Uh, water for me, thanks.", 'no_anim')
speak('ALISTAIR', "Not a fan of the tap?", 'no_highlight')
speak('BIANCA', "I am. I just didn’t pack a bottle of water with me.", 'no_anim')
speak('ALISTAIR', "And you, Cynthia?", 'no_highlight')
speak('CYN', "I’ll come in with you, actually. Need the fresh air.")
speak('ALISTAIR', "Understandable. Let’s adventure.", 'no_highlight')
exitNVL()

exit('CYN')
wait(1)

enterNVL()
speak('ALISTAIR', "And the petrol's on me.", 'no_highlight')
choose("No, it's not", "No thanks, I'll cover.")
speak('ALISTAIR', "Come on. You're the one driving! It's on me.", 'no_highlight')
speak('BIANCA', "He thanks you for the kind offer. And he accepts it.", 'no_anim')
speak('ALISTAIR', "That would be better if it came out of his mouth.", 'no_highlight')
speak('ALISTAIR', "But I won’t push my luck. Tends to bite me in the bum. Ciao for now!", 'no_highlight')
exitNVL()
setSwitch('scene5_alistair', false)

wait(3.5)

enterNVL()
speak('BIANCA', "How are you? How’s your back?")
speak('LIAM', "It's fine.")
speak('BIANCA', "We should really go to that chiropractor, Shea Toulouse.")
speak('LIAM', "I heard my mother say that name once.")
speak('BIANCA', "Your mother?")
speak('LIAM', "Yeah. Shea Toulouse.")
exitNVL()

wait(2)

enterNVL()
speak('BIANCA', "You're treating him unkindly, but Alistair seems alright.")
speak('LIAM', "Yeah. Maybe he’s great.")
speak('BIANCA', "Didn’t say that.")
exitNVL()

wait(3)
enterNVL()
speak('BIANCA', "Did you really get pulled in by a naturist retreat?")
speak('LIAM', "If it were a naturist retreat, Bianca, we’d be naked.")
speak('BIANCA', "You know what I mean. You don’t like all that nature crap. So why?")
speak('BIANCA', "And please cut out the deception, Liam. I want the truth.")
speak('BIANCA', "Tell me now or this is going to grow into a serious problem.")
exitNVL()

if choose("(Tell her)", "(Don't tell her)") then
	setSwitch("youToldHer", true)

	wait(3)
	
	enterNVL()
	speak('LIAM', "The nature thing isn't the reason.")
	speak('BIANCA', "Well then, what is it? Please tell me.")
	exitNVL()
	
	wait(1)
	setSwitch('view_note', true)
	pamphlet('note')
	setSwitch('view_note', false)
	wait(3)
	
	enterNVL()
	speak('BIANCA', "So they sent you this second letter to draw you in?")
	exitNVL()
	
	choose("Nothing Else Could", "Nothing Else Could")
	wait(.5)
	
	enterNVL()
	speak('BIANCA', "I'm sorry?")
	exitNVL()
	
	wait(.5)
	choose("You Heard Me", "You Heard Me")
	wait(1)
	
	enterNVL()
	speak('BIANCA', "I see.")
	exitNVL()
	
	wait(2.5)
	
	choose("I Saw Crows When She Died", "I Saw Crows When She Died")
	wait(1.0)
	
	enterNVL()
	speak('BIANCA', "Crows?")
	exit('BIANCA', false)
else
	exitNVL()
	
	wait(3)
	
	enterNVL()
	speak('LIAM', "I just like trees.")
	speak('BIANCA', "I'm sorry?")
	speak('BIANCA', "Did you just say that?")
	choose("Did I just say that?", "No, I didn't.")
	speak('BIANCA', "What the...")
	exitNVL()
	
	wait(1)
	
	enterNVL()
	speak('BIANCA', "I think you need time alone.")
	exitNVL()
	
	exit('BIANCA')
	
	wait(2.5)
	setSwitch('view_note', true)
	pamphlet('note')
	setSwitch('view_note', false)
	wait(3)
	
	choose("Nothing Else Could", "Nothing Else Could")
	wait(2)
	choose("You Heard Me", "You Heard Me")
	wait(2)
	choose("I Saw Crows When She Died", "I Saw Crows When She Died")
	wait(2)
end

play('scene5_04')