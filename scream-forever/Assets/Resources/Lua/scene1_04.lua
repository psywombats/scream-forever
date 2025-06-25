enterNVL()
speak('BIANCA', "It's pretty quiet in here, isn't it?")
exitNVL()

wait(2)

enterNVL()
speak('LIAM', "Okay, I might be worried. I haven’t seen him in a long time.")
speak('LIAM', "I don’t know why he flew all the way out to the city all the way from Fuseville just to catch a ride with us to the middle of nowhere.")
speak('BIANCA', "It's a retreat, Liam. It's not the middle of nowhere.")
speak('BIANCA', "It’ll be a place where you can feel safe. And calm.")
exitNVL()

wait(1)

enterNVL()
speak('BIANCA', "Do you need a massage later?")
if choose("I might", "No thanks") then
	setSwitch('wantsMassage', true)
	speak('LIAM', "I might like one, yeah. Thanks.")
	speak('BIANCA', "Then I’ll give you one later to ease your mind.")
	speak('BIANCA', "A special one just for you.")
else
	speak('LIAM', "Sorry, no massage, please. Too tender.")
	speak('BIANCA', "Oh, was it the workout?")
	speak('BIANCA', "Yeah, okay. I thought a massage might solve that, but...")
end
exitNVL()

driveWait(60)
play('scene1_05')
