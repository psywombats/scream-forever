enterNVL()
speak('CYN', "Hm... Fireworks, drinks, DJs... I know none of the artists but I’m keen to go along and meet some new people.")
speak('CYN', "Maybe we can bring some people from the camp.")
speak('ALISTAIR', "What camp?")
speak('CYN', "Here, let me show you.")
exitNVL()

pamphlet('friends')

enterNVL()
speak('ALISTAIR', "What in the world is this crap?")
speak('BIANCA', "Whoa. Yeah, this is a bit full-on.")
speak('CYN', "Oh, Bee, it’s nice! It’s got sports, kayaking, a petting zoo... more.")
speak('ALISTAIR', "Oh, it’s got ‘more’.")
speak('BIANCA', "They really turned the kitsch setting up to 11 on this one.")
speak('ALISTAIR', "To be fair, Bianca, your pamphlet talked about a James from GuestAdvisor saying that \"Grand Getaway\" made him a \"changed man\". Do you know what that means?")
speak('BIANCA', "No. What does it mean?")
speak('ALISTAIR', "Nothing, is what it means. It’s just words for the sake of words!")
speak('ALISTAIR', "And I’m pretty sure those were stock images used in the pamphlet so I think Cyn’s pamphlet beats your pamphlet, hands down.")
exitNVL()
wait(1)
enterNVL()
speak('ALISTAIR', "At least hers has funny shapes.")
speak('LIAM', "They're all pretty weird pamphlets.")
speak('BIANCA', "Liam, show us yours?")
exitNVL()

pamphlet('nature')

enterNVL()
speak('CYN', "This one’s... nice, Liam.")
speak('ALISTAIR', "I changed my mind. Friendship Camp looks better than this one.")
speak('LIAM', "None of you are outdoors people?")
speak('ALISTAIR', "Not this kind of outdoors.")
speak('CYN', "I wouldn’t mind going for a walk in the bush.")
speak('BIANCA', "Liam, you’re not an outdoors person either. One time we tried going on that hike and you couldn’t stand it.")
speak('BIANCA', "You were just fidgeting around the entire time as if you didn’t want to be there.")

if choose("I like the outdoors", "Guess it's not my thing") then
	speak('LIAM', "No, I like the outdoors.")
	speak('BIANCA', "No, you don't.")
else
	speak('LIAM', "Fine, it’s not exactly my thing. But I only went because you wanted to go.")
	speak('BIANCA', "There we go. The truth comes out. You only went because I wanted to go.")
	if choose("That’s what I said.", "Yes, you’re right, Bianca.") then
		speak('BIANCA', "Right.")
	else
		speak('BIANCA', "Okay.")
	end
end
exitNVL()

driveWait(300)
play('scene3_03')
