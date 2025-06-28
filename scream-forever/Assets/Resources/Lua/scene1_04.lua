enterNVL()
speak('BIANCA', "It's pretty quiet in here, isn't it?")
exitNVL()

wait(1)
playSFX('knob')
wait(.8)
playBGM('radio_tune')

wait(3)

enterNVL()
speak('LIAM', "...")
exitNVL()
if choose("Okay, I might be worried. I haven’t seen him in a long time.", "(Stay silent.)") then
	enterNVL()
	speak('LIAM', "I don’t know why he flew out to the city all the way from Fuseville to DC, just to catch a ride with us to the middle of nowhere.")
	speak('BIANCA', "Liam, it's a retreat. It's not nowhere.")
	speak('BIANCA', "It's a place where you can feel safe and calm.")
	exitNVL()
end


wait(3)

enterNVL()
speak('BIANCA', "So, you want a massage later? You seem tense.")
if choose("I might like one, yeah. Thanks.", "Sorry. No massage, please. Too tender.") then
	setSwitch('wantsMassage', true)
	speak('BIANCA', "Great! I’ll give you one later to ease your mind.")
else
	speak('BIANCA', "Oh, you worked out today?")
	speak('BIANCA', "Thought you might need the muscle relaxation, but... I guess tonight's not the night.")
end
exitNVL()

driveWait(60)
play('scene1_05')
