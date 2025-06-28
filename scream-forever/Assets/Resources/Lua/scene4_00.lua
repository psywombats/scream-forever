setSwitch('gps_on', true)
setSpeed(20)
enter('cyn')
enter('bianca')
enter('alistair')


playBGM('ride')
fade('normal', 3)

driveWait(300)

enterNVL()
speak('BIANCA', "Alistair.")
speak('ALISTAIR', "Yo.")
speak('BIANCA', "I’ve heard you restore old cars in the garage.")
speak('ALISTAIR', "Yeah. My car’s in the shop at the moment, actually, otherwise I wouldn’t have asked for a ride in.")
speak('CYN', "You didn’t want to hang out with three of the most exciting people in the world having the time of their lives?")
speak('ALISTAIR', "Ha ha. Very funny. I didn’t realize it would be such a fun and dandy time, then, did I?")
speak('CYN', "No awkward moments at all.")
exitNVL()

wait(1)

enterNVL()
speak('BIANCA', "Liam says you used to slay the club. You were the life of the party!")
if choose("\"Slay the club\"? Is that the term for it?", "Alistair was the biggest attention grabber.") then
	speak('BIANCA', "Yeah, like... I don’t know, bring the party. Heighten the excitement.")
	speak('ALISTAIR', "Don’t worry, I knew what you meant, Bianca. Common slang.")
	speak('ALISTAIR', "We both had... a certain way about ourselves. Wouldn’t say we \"slayed\" anything though.")
else
	speak('LIAM', "He’d always try to find a way to spotlight himself.")
	speak('ALISTAIR', "Well, yeah. But neither of us really were that successful at it, were we?")
	speak('ALISTAIR', "Largely because I was the Brit with the funny accent, and Liam was the guy who refused to chatter one bit.")
end
speak('BIANCA', "Actually, that’s believable.")
exitNVL()
choose("Alright...", "Okay...")


driveWait(250)

enterNVL()
speak('CYN', "I think you two must’ve been pretty cute when you were younger.")
speak('ALISTAIR', "Not now?")
speak('CYN', "I mean... yeah. You’re still pretty nice.")
speak('ALISTAIR', "I guess I was fishing for that one.")
speak('CYN', "Yeah, and when you guys were friends, I just think that you would’ve been a great team!")
speak('CYN', "You must’ve got up to lots of fun stuff in your youth... You two would’ve been really cool...")
exitNVL()

wait(1)

enterNVL()
speak('ALISTAIR', "And what do you do for fun, Cyn?")
speak('CYN', "Oh, nothing. I like puzzles.")
speak('ALISTAIR', "Hmm. What kind of puzzles?")
speak('BIANCA', "You like puzzles?")
speak('CYN', "Oh yeah, Bianca. You should try one with me. It’s been something that’s been hitting me recently.")
speak('CYN', "There’s this awesome puzzle that’s like a super long river and it goes around and it’s only, like, one tile wide but it goes in all these loops and it’s fantastic.")
speak('BIANCA', "I think I saw something like that online.")
speak('CYN', "Yeah, I got it off some person’s online puzzle store. I want to buy more. They’re very fun!")
speak('ALISTAIR', "And you don’t get bored of sitting inside playing with puzzles all day?")
speak('CYN', "No, not at all. Well, it’s more therapeutic than it is an intellectual exercise. I’m simply more excited about achieving flow state more than anything.")
speak('LIAM', "And what's flow state?")
speak('BIANCA', "Flow state, Liam, is when your subconscious takes over completely, and your conscious mind goes totally offline.")
speak('BIANCA', "It’s basically like a spiritual autopilot.")
speak('CYN', "I think you might be right.")
speak('CYN', "I thought, though, that it had something less to do with shutting down your mind completely, and more to do with being present but, like, focused. With all the weird bits calmed down.")
exitNVL()

driveWait(200)

enterNVL()
speak('BIANCA', "I think there’s something very... spiritual... about putting together puzzles.")
speak('CYN', "That’s right! The idea of putting two and two together. I think that can be, like, logically spiritual. Maybe.")
speak('BIANCA', "Well, if it’s hard to explain, then it’s spiritual, right?")
speak('ALISTAIR', "That sounds like an excuse for ignorance.")
speak('BIANCA', "That’s not very kind to spirituality.")
speak('ALISTAIR', "On the contrary. I think that spirituality doesn’t have to be ignorant. It can be very cognizant of its surroundings.")
speak('BIANCA', "Well, I don’t think many people can claim to take care of that space.")
speak('ALISTAIR', "You’re talking about it as if it’s a personal garden that you take care of. It doesn’t belong to you.")
speak('BIANCA', "Our minds are personal gardens connected by paths. So we take care of them, connect them, and enjoy their patterns.")
speak('BIANCA', "They need nutrients, exercise and water to survive... so that’s why it’s paramount to take care of the mind, body and soul.")
exitNVL()
driveWait(20)
enterNVL()
speak('ALISTAIR', "No, I don’t think I will...")
speak('CYN', "Oh, when we get to the camp, we should get some puzzles and just have a great big puzzle-off!")
speak('ALISTAIR', "Sure. Sounds like fun.")

if choose("Can't tell if you're joking.", "(Stay silent.)") then
	speak('ALISTAIR', "Maybe you don’t know me, then. I like a good puzzle.")
	if choose("No, you don't.", "(Stay silent.)") then
		speak('ALISTAIR', "Oh. Whoops! I don’t, then. My mistake. All’s cleared up, then.")
	end
end

exitNVL()

driveWait(200)

play('scene4_01')