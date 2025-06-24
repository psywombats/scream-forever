if getSwitch("broughtInLaundry") then
	speak("BIANCA", "By any chance, did you also make sure that the oven was off?")
else
	speak("BIANCA", "Gah! Now I’m worried that the oven’s on as well.")
end

if choose("Check your photos", "I didn't turn it off") then
	speak("LIAM", "Check your photos. You took photos of it, remember?")
	speak("BIANCA", "I did. You’re right.")
	exitNVL()
	wait(2)
	enterNVL()
	speak("BIANCA", "There it is Oven off. Good. Phew.")
	speak("BIANCA", "Glad that's settled.")
else
	speak("LIAM", "I didn't turn it off. It should be off, though.")
	speak("BIANCA", "Yeah, it should be off. I think I remember turning it off.")
	exitNVL()
	wait(1.0)
	enterNVL()
	speak("BIANCA", "But it’s fine. It’s forgettable! I’ve put it out of the brain.")
	speak("BIANCA", "Whoosh! There it goes.")
end

exitNVL()