video(.3)
wait(2)

enterNVL()
speak('BIANCA', "So...")
speak('BIANCA', "Do you think our friends will get along alright?")
exitNVL()


if choose("If Cynthia has thick skin", "If Alistair behaves") then
	speak('LIAM', "Yeah, I hope Cynthia has some thick skin. Alistair can be really something, sometimes.")
else
	speak('LIAM', "Surely. That is, as long as Alistair behaves like he really should.")
end

speak('BIANCA', "Hopefully he’s not as bad as the tales you’ve spun.")
speak('BIANCA', "No offense to you, being his friend and all.")
speak('LIAM', "None taken.")
exitNVL()

wait(4)

enterNVL()
speak('BIANCA', "And you’re gonna be alright with hanging out with him? It's been a while.")
if choose("It'll be fine", "Why are you concerned?") then
	speak('LIAM', "It'll be fine. There's nothing for you to worry about.")
	speak('BIANCA', "Good.")
	speak('BIANCA', "As long as you're okay with it, I'm okay with it.")
	speak('BIANCA', "That's mostly what matters, anyway. Being okay.")
else
	speak('LIAM', "What, is there a reason for your concern?")
	speak('BIANCA', "No reason. It’s just been a while, and sometimes tensions can rise.")
	speak('BIANCA', "People change, that sort of thing.")
end

exitNVL()

driveWait(100)
play('scene1_04')