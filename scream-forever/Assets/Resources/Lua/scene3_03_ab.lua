speak('BIANCA', "Well, you had to have printed them off in quite high quality. This one’s laminated.")
if choose("We have a laminator at home.", "I used my friend’s printer.") then
	speak('BIANCA', "We don’t have a laminator at home.")
	if choose("It’s stored away.", "Well, I borrowed it from my friend.") then
		speak('BIANCA', "But... so you brought it out just to laminate these 4 different pamphlets individually, hid the laminator, and then gave them to us?")
		choose("I didn't hide the laminator. I just put it away.", "Three pamphlets. I printed three and sent one to Alistair via email.")
		speak('BIANCA', "But even if we have a laminator, we don’t have a printer!")
		speak('LIAM', "Not my problem.")
		speak('BIANCA', "So, you had access to a printer, and you laminated four different pamphlets, one each for yourself, your girlfriend, your girlfriend’s friend, and your friend you haven’t contacted in almost 10 years.")
		exitNVL()
		
		driveWait(70)
		
		enterNVL()
		speak('ALISTAIR', "I do think it’s matte lamination, too, which increases the likelihood he’s not telling the truth.")
		exitNVL()
		
		driveWait(100)
		
		enterNVL()
		speak('BIANCA', "Why did you print out 4 different pamphlets, Liam?")
		speak('LIAM', "I don't rememeber.")
		speak('BIANCA', "Right.")
		exitNVL()
		
		play('scene3_04')
	else
		play('scene3_03_friend')
	end
else
	play('scene3_03_friend')
end