enterNVL()
speak("BIANCA", "Oh man! Liam, I forgot to bring the laundry in. Damn!")
if choose("Well, you must've, right?", "Don't worry! I did it for you.") then
	speak("BIANCA", "I don't know. I'm not sure I did. Gah, I knew I was missing something!")
	speak("BIANCA", "Damn...")
	setSwitch("broughtInLaundry", false)
else
	speak("BIANCA", "Oh, thank you! That's good. Phew.")
	setSwitch("broughtInLaundry", true)
end
exitNVL()

driveWait(40)
play('scene1_02')