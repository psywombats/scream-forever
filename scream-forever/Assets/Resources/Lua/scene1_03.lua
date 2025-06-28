video(.3)
wait(2)

enterNVL()
speak('BIANCA', "So...")
speak('BIANCA', "Do you think our friends are gonna get along okay?")
exitNVL()

video(2)
wait(2)

enterNVL()
speak('LIAM', "...")
choose("Yeah. I hope Cynthia has thick skin. Alistair can be a massive jerk.", "Surely. As long as Alistair behaves like he really should.")
speak('BIANCA', "Hopefully he’s not as bad as the tales you’ve been spinning.")
speak('BIANCA', "No offense.")
speak('LIAM', "None taken.")
speak('BIANCA', "Right.")
exitNVL()

wait(4)

enterNVL()
speak('BIANCA', "And you’re gonna be okay hanging out with him? It's been a long time.")
if choose("I'll be fine. There's nothing for you to worry about.", "Is there a reason for this sudden concern?") then
	speak('BIANCA', "Good.")
	exitNVL()
	wait(1.5)
	enterNVL()
	speak('BIANCA', "Like, as long as you're okay with it, I'm okay with it.")
	exitNVL()
	wait(2)
	enterNVL()
	speak('BIANCA', "Because that's mostly what matters, anyway.")
	speak('BIANCA', "Being okay.")
else
	speak('BIANCA', "No reason. I just know that tensions can arise when people are apart for too long.")
	speak('BIANCA', "People change, that kind of thing.")
end

exitNVL()

driveWait(100)
play('scene1_04')