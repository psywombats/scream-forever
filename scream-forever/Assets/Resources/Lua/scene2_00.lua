setSpeed(15)
enter('cyn')
enter('bianca')
playBGM('viral')
fade('normal')

driveWait(150)

enterNVL()
speak('BIANCA', "Do you like this song?")
speak('CYN', "This is great! Where'd you find it?")
speak('BIANCA', "I was just browsing and the algorithm served it to me. I loved it straight away.")
speak('CYN', "Is it on streaming? Where can I listen?")
speak('BIANCA', "Let me share it with you now.")
speak('CYN', "Oh wow. You’re a hipster, Bee.")

if choose("Tries way too hard", "I can't compare") then
	setSwitch('mockedBianca', true)
	speak('LIAM', "Total hipster - tries way too hard!")
	speak('BIANCA', "I do. I really do. But not too hard.")
	speak('CYN', "It shows. You always do a really good job of finding exciting songs. You’re so good at it.")
	speak('BIANCA', "Thanks, Cyn! I put a lot of work into my craft.")
	speak('CYN', "And how about you -- what’s your music taste, Liam?")
else
	speak('BIANCA', "Oh, come on -- don’t be so modest.")
	speak('CYN', "Yeah, I bet you have a really great way of finding songs too, Liam.")
	speak('CYN', "I bet you just find the best of them, and have the best taste.")
	speak('BIANCA', "Of course he does. He has great taste, I’m just kidding.")
	speak('BIANCA', "You listen to good songs, don’t you, Liam?")
end

if choose("Dire Straits", "Britney Spears") then
	speak('LIAM', "Do you like Dire Straits?")
	speak('CYN', "Yeah, they’re not bad. My dad listens to them. They’ve got great musical chops!")
else
	setSwitch('likesBritney', true)
	speak('LIAM', "Do you like Britney Spears?")
	speak('CYN', "Oh! My mom loved Britney. She used to be obsessed. That was a long time ago. When she used to listen to music.")
end
speak('CYN', "I listen to more modern music, generally. Not really, like- the... older stuff. Just new.")
speak('CYN', "Well, not- that you’re old, or anything! I mean you’re only, early... 30s, I’m guessing?")
speak('BIANCA', "Yeah, that’s about his age.")
speak('CYN', "Oh, good. So he’s not that much older than you, then.")
speak('BIANCA', "He’s a little bit older. I’m 28, and he’s 32.")
speak('CYN', "Oh, sorry! I didn’t mean to push you into saying that or anything.")
speak('BIANCA', "Cyn! You’re fine. Age is a thing. No biggie.")
speak('CYN', "Sorry, I know. You know I’m always saying sorry about saying sorry, and it goes on and on.")
speak('BIANCA', "Yeah, I know. It’s alright.")
speak('LIAM', "That should be where we're picking up Alistair.")
exitNVL()

setSwitch('spawn_house', true)
driveWait(90)
distBrake(25)

allowDriving(false)

play('scene2_01')
