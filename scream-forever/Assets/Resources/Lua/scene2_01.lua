wait(1.5)

enterNVL()
speak('BIANCA', "Wow, he’s running quite fast.")
speak('CYN', "Yeah. He’s got long legs, hasn’t he?")
exitNVL()

enter('ALISTAIR')
wait(.5)

enterNVL()
speak('ALISTAIR', "Hello, friends! How are thee all doing?")
speak('BIANCA', "Want me to slide my seat forward - ?")
speak('ALISTAIR', "No, you’re fine, I’ll just crouch in like a slimy eel back here. You’re good.")
speak('BIANCA', "I’ll pull my seat forward.")
exitNVL()

wait(1)

enterNVL()
speak('ALISTAIR', "This car’s vintage. Looks like it’s running on crude oil and pioneer sweat.")
speak('LIAM', "Alistair. How are you?")
speak('ALISTAIR', "Rather pristine, actually. Got some full-time work. It’s treating me well. Keeping me busy. And you?")

if choose("Could be better", "Really good") then
	speak('LIAM', "Well, I could be better.")
	speak('ALISTAIR', "Ah, surely it’s not that terrifying. You look okay. No five o’clock shadow present.")
else
	speak('LIAM', "I'm really good, actually.")
	speak('ALISTAIR', "I’m sure you are. Big successful man!")
end

speak('CYN', "I’m Cyn. Bianca’s friend.")
speak('ALISTAIR', "Cyn, like the collective moral failings? Atoned to meet you, Cyn.")
speak('CYN', "Likewise. And you are?")
speak('ALISTAIR', "Alistair. A very British name, although I’m not proud to be.")
speak('CYN', "Oh, what part of Britain do you come from?")
speak('ALISTAIR', "The poor part, but I moved over here when I was 13. And who is this lovely lady?")
speak('BIANCA', "I’m Bianca. Liam's girlfriend.")
speak('ALISTAIR', "I see. And I’m guessing... you two like each other a lot, is it?")
speak('BIANCA', "I mean, of course. I'm his chosen one.")
speak('ALISTAIR', "And he’s yours. A lot of couples don’t like each other nowadays. It’s like the new pandemic.")
speak('ALISTAIR', "Refreshing to see the monogamous devoted couple make its comeback in this sinful modern age.")
speak('LIAM', "...So you're all packed, Alistair?")
speak('ALISTAIR', "Got my little satchel. All I need for a couple days of fun.")
speak('CYN', "Same! You’re an efficient packer, too?")
speak('ALISTAIR', "Yes. Just one clothing, one deodorant and some other alcoholic substances. The whole festival package.")
speak('BIANCA', "Festival?")
speak('ALISTAIR', "Yeah, you know. A festival. The thing with the lights and the music and the people dancing their heads off.")
speak('CYN', "Is there a music festival happening near the campsite?")
speak('ALISTAIR', "That’s what we’re here for, isn’t it? Or did I get the wrong memo?")
speak('CYN', "That’s cool. I hope we get to go.")
speak('ALISTAIR', "Speaking of go, isn’t that what cars do, go?")
speak('LIAM', "It is. We should probably leave before it gets late.")
exitNVL()

allowDriving(true)
setSpeed(.05)
driveWait(10)

speak('ALISTAIR', "Hang on, this thing moves? A marvel of the industrial era! The people cheered and clapped!")
--speak('CYN', "Heehee.")
exitNVL()

teleport('Nighttime', true)
play('scene3_00')