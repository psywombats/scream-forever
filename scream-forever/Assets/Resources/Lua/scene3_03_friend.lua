speak('BIANCA', "Which \"friend\", Liam?")
if choose("You know. Someone you haven't met.", "John.") then
	speak('BIANCA', "I didn't know you had friends that I haven't met. Who is it?")
	if not choose("John.", "Philip.") then
		setSwitch("friendsNameIsPhilip", true)
	end
end

if getSwitch("friendsNameIsPhilip") then
	speak('BIANCA', "You have a friend named Philip, then. That I haven't met.")
else
	speak('BIANCA', "You have a friend named John, then. That I haven't met.")
end

if choose("He's a good friend.", "No, no I don't.") then
	speak('BIANCA', "Right.")
	exitNVL()
	wait(.5)
	enterNVL()
	speak('BIANCA', "Okay then.")
	if getSwitch("friendsNameIsPhilip") then
		speak('BIANCA', "A good friend named Philip that I haven't met.")
	else
		speak('BIANCA', "A good friend named John that I haven't met.")
	end
	exitNVL()
	
	wait(7)
	
	enterNVL()
	speak('LIAM', "Fine, I printed them off. No friend involved.")
	exitNVL()
	
	wait(5)
	
	enterNVL()
	speak('BIANCA', "No you didn't.")
else
	speak('BIANCA', "No, no you do not.")
	exitNVL()
	
	wait(5)
	
	enterNVL()
	speak('BIANCA', "So, you had access to a printer, and you laminated four different pamphlets, one each for yourself, your girlfriend, your girlfriend’s friend, and your friend you haven’t contacted in almost 10 years.")
end
exitNVL()

wait(3.5)

enterNVL()
speak('BIANCA', "Why did you print out four different pamphlets, Liam?")
speak('LIAM', "I don't remember.")
speak('BIANCA', "Right.")
exitNVL()

play('scene3_04')
