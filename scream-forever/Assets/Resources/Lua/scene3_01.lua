

enterNVL()
speak('ALISTAIR', "So what’s this whole thing about, anyway?")
speak('BIANCA', "What thing?")
speak('ALISTAIR', "The trip. One says it’s a concert, the other says it’s a bath-house retreat.")
speak('ALISTAIR', "Surely we can’t be having all these sorts of different views about it, can we?")
speak('BIANCA', "Well, you received the pamphlet, didn’t you?")
speak('ALISTAIR', "And where did we get those pamphlets from, anyway?")
speak('CYN', "Bee gave me mine.")
speak('BIANCA', "And Liam gave me both of mine, one to give to Cyn.")
speak('ALISTAIR', "Oh, right, because you two are living together.")
speak('BIANCA', "Yes.")
speak('ALISTAIR', "Co-habitation. You’ve found a nice spot for yourself, haven’t you, Liam?")

if choose("Yeah, it's not bad.", "How about yourself? How’s your digs?") then
	--hi
else
	speak('ALISTAIR', "Ahh, don’t worry about me, lad, I’m just handling myself quite nicely. Quite nicely, indeed.")
end

speak('ALISTAIR', "Honestly, you’d always struck me as a guy who’d have a simple life.")
speak('ALISTAIR', "Then you went and moved to the city and left me all by my lonesome. Funny how life works.")
exitNVL()

driveWait(50)

enterNVL()
speak('BIANCA', "Oh, I found it. It's the pamphlet Liam gave me.")
exitNVL()

pamphlet('retreat')

enterNVL()
speak('ALISTAIR', "The Perfect Getaway? What?")
speak('BIANCA', "What do you mean? It looks nice. What are you talking about?")
speak('ALISTAIR', "This isn’t at all what I got for my invitation.")
speak('CYN', "It isn’t what I got either, although some parts are close.")
speak('LIAM', "That's different than what I got too.")
speak('BIANCA', "I thought you guys wanted to join me on a healing journey.")
speak('ALISTAIR', "Heck no. Not about that crap. Although a pool does sound nice.. actually, it doesn’t look that bad, now that I...")
speak('ALISTAIR', "...tilt my head a little. Sorry. I was just lookin’ at it Australian-like.")
speak('BIANCA', "So you don’t want to join us on a healing journey of self-discovery.")
speak('ALISTAIR', "Nah, those all go around in circles. You never learn much that you couldn’t learn anywhere else.")
speak('CYN', "It does look fun, though. Lots of people. Those guys in the hot springs look pretty cute.")
speak('ALISTAIR', "Well unfortunately, my dear Cynthia, they are models and do not come with the package.")
speak('CYN', "I don’t think so. They look like they’re just enjoying things naturally. Maybe they asked the visitors to pose for the camera.")
speak('BIANCA', "They look like they could work out a bit more. Maybe diet.")
speak('CYN', "Oh, don’t be mean, Bee!")
speak('BIANCA', "What? I’m not insulting them or anything. It’s for their own health.")
speak('CYN', "They probably didn’t ask to have their picture taken or their bodies criticized.")
speak('BIANCA', "Fine. Well, Alistair, show us yours, then.")
speak('ALISTAIR', "What, my body? Very forward of you, resident femme misogynist.")
speak('BIANCA', "No, your pamphlet, the one you got.")
speak('ALISTAIR', "Thought you were one of them open-relationship polygamists there for a second. Asking for my body and all.")
speak('BIANCA', "Now, you are a real jokester, aren’t you? Just pass up your pamphlet so Liam and I can see.")
exitNVL()

pamphlet('music')

enterNVL()
speak('CYN', "Wow. I can see what you mean now. This is totally different.")
speak('ALISTAIR', "Yeah, a much better time, is what I call it.")
speak('BIANCA', "Whoa! Jason’s Not Here is performing?")
speak('ALISTAIR', "You’re a JNH fan?")
speak('BIANCA', "I’m in love with them. We should go to this as well.")
speak('BIANCA', "Where did you get this from?")
speak('ALISTAIR', "Liam sent it to me over message. Then soon he followed it up with a bloody call out of nowhere asking me if I wanted to reconnect. A call?")
speak('ALISTAIR', "Honestly, it was surprising, considering he’d never contacted me until now... even given our seasoned history in high school.")

if choose("I’m sorry, dude, what can I say?", "It's not like you ever contacted me, either.") then
	speak('ALISTAIR', "Yup. Sorry’s a good start.")
else
	speak('ALISTAIR', "You’re right, I didn’t. But I didn’t leave, did I?")
end
exitNVL()

driveWait(50)
play('scene3_02')