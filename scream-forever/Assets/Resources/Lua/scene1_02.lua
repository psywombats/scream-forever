enterNVL()
if getSwitch("broughtInLaundry") then
	speak("BIANCA", "By chance, did you make sure the oven was off?")
else
	speak("BIANCA", "Now I’m worried that the oven’s on too.")
end

if choose("Check your photos. You took photos of it, remember?", "I didn't turn it off. It should be off, though.") then
	speak("BIANCA", "I did! You’re right. Lemme check.")
	exitNVL()
	wait(2)
	enterNVL()
	speak("BIANCA", "Ah, there it is. Oven off. Good. Phew.")
	speak("BIANCA", "Glad that's done and dusted.")
	exitNVL()
	wait(3)
	enterNVL()
	speak("BIANCA", "Although part of me still thinks it's on, for some reason.")
else
	speak("BIANCA", "Yeah, it should be off.")
	speak("BIANCA", "I think I remember turning it off.")
	exitNVL()
	wait(1.0)
	enterNVL()
	speak("BIANCA", "But it’s fine. It’s forgettable! I’ve put it out of the brain.")
	speak("BIANCA", "Whoosh! There it goes.")
end

exitNVL()

play('scene1_03')

