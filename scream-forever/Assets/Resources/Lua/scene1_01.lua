enterNVL()
speak("BIANCA", "Oh man! Liam, I forgot to bring the laundry in. Shit!")
if choose("You must’ve brought it in, right?", "Don't worry. I did it for you.") then
	speak("BIANCA", "No, I don’t know... I don’t think I did. I knew I was missing something! Damn...")
	setSwitch("broughtInLaundry", false)
else
	speak("BIANCA", "Oh, thank you! Much appreciated, Liam.")
	setSwitch("broughtInLaundry", true)
end
exitNVL()

driveWait(40)
play('scene1_02')