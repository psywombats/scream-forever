enterNVL()
speak('BIANCA', "Liam. Why did you give us all different pamphlets?")
speak('ALISTAIR', "Yeah, who gave you these pamphlets, Liam?")

if choose("I think I printed them off.", "A man on the street gave them to me.") then
	speak('ALISTAIR', "You think? Or you know?")
	speak('BIANCA', "Come on, Alistair, he hasn’t got the best memory. Give him space.")
	speak('BIANCA', "Liam, you think? Or you know? Which is it?")
	if choose("I think.", "I know.") then
		speak('ALISTAIR', "Then that’s not good enough, is it?")
		speak('BIANCA', "What?")
		speak('ALISTAIR', "Well, it’s kind of vague. You don’t know whether you printed them off?")
		speak('BIANCA', "Liam, we don’t have a printer. Where did you print them?")
		if choose("I printed them at the local copy center.", "I don't know") then
			play('scene03_03_aaa')
		else
			speak('BIANCA', "You don’t know how you printed the pamphlets.")
			speak('BIANCA', "Do they just materialize from thin air? Where did they come from?")
			if choose("Sincerely, I have no idea.", "Maybe I printed them at the copy center. Maybe.") then
				play('scene3_03_ab')
			else
				play('scene3_03_aaa')
			end
		end
	else
		play('scene3_03_ab')
	end
else
	speak('ALISTAIR', "Right, right. And which street was it?")
	if choose("I don't know", "27th Street.") then
		speak('LIAM', "I don’t know. I was walking on a street somewhere.")
		speak('ALISTAIR', "Yes, we gathered you were walking on a street. And the destination was, indeed, \"somewhere\".")
		speak('BIANCA', "I’ve seen you. You never take flyers from strangers. You always just ignore them.")
		if choose("They always want money, anyway", "Should I be accepting them?") then
			speak("LIAM", "They’re always just trying to get your money, anyway.")
			speak("ALISTAIR", "That’s true.")
			speak("BIANCA", "You took 4 flyers from this man -- 4 different flyers with 4 different designs -- aiming to invite us all to 4 different things?")
		else
			speak('LIAM', "Is there a warmer way I should be accepting flyers from strangers?")
			speak('BIANCA', "No, but you barely look at them. You pretty much actively ignore people on the street. It’s not a bad thing, it’s just a fact!")
			exitNVL()
			
			wait(3)
			
			enterNVL()
			speak('BIANCA', "So you took a flyer from this man. And then took an extra three different flyers from him with 3 different designs than yours...")
			speak('BIANCA', "...aiming to invite us all to three different things than you?")
		end
		
		choose("I thought you might be interested", "I emailed Alistair the fourth.")
		
		speak('BIANCA', "So... you scanned the flyer and sent Alistair a copy?")
		speak('LIAM', "I can't recall.")
		speak('BIANCA', "But you don’t remember the invites being to separate events that have nothing to do with each other?")
		play('scene3_03_remember')
	else
		speak('ALISTAIR', "The 27th of what street?")
		speak('CYN', "That’s what they call streets in the city, Alistair. They refer to them by number.")
		speak('ALISTAIR', "Oh.")
		exitNVL()
		
		wait(2)
		
		enterNVL()
		speak('BIANCA', "So you took a flyer from him. And then took an extra three different flyers from him with three different designs than yours...")
		speak('BIANCA', "...aiming to invite us all to three different things than you?")
		play('scene3_03_remember')
	end
end