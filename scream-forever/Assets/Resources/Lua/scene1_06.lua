enterNVL()
speak('BIANCA', "Cyn! So good to see you!")
speak('CYN', "Bee! How are you, Bee?")
speak('BIANCA', "I'm feeling very good, thank you for asking. This is going to be such a fun trip.")
speak('BIANCA', "I'm so excited. You're looking great this evening.")
speak('CYN', "Thank you! You’re looking awesome as well. Prepped nicely.")
speak('BIANCA', "That’s a lovely coat you have on.")
speak('CYN', "Picked it up at a vintage shop last week.")
speak('BIANCA', "I haven’t been clothes shopping in a while. We should go sometime!")
speak('CYN', "We really should. I’ve been lazy about that.")
speak('BIANCA', "Should we pop the trunk? Where's your stuff?")
speak('CYN', "Oh, no, I’ve got everything.")
speak('BIANCA', "You have?")
speak('CYN', "Yeah, it’s all in here. This little bag. I keep it all with me. Gotta travel light.")
speak('BIANCA', "Are you sure you don’t need some, like -- towels or something -- or a yoga mat? You know, for the exercises?")
speak('CYN', "Well, this isn’t, like, a wellness retreat, is it?")
exitNVL()

wait(1.3)

enterNVL()
speak('BIANCA', "Yeah, Cyn, there’s going to be a spa! You’re gonna want all your stuff -- for the pampering and all that.")
speak('LIAM', "I thought we were going for a nature hike in the woods.")
speak('BIANCA', "Where did you get that from? That wasn’t what was advertised to me.")
speak('CYN', "As long as I get to meet new people, I’m 100% game.")
speak('CYN', "And if there’s a spa, too, well then that’s extra, isn’t it?")
speak('BIANCA', "And yoga sessions, and drinks, and a sauna!")
speak('CYN', "Ooh, exciting!")
exitNVL()

wait(2.2)

enterNVL()
speak('BIANCA', "Oh! I forgot to introduce! As you know, or -- may have heard, this is my boyfriend, Liam. This is Cyn. Cyn, Liam - ")
speak('BIANCA', "Ah, who am I kidding, you both know each other by proxy.")
speak('CYN', "I've heard the stories, for sure!")

if choose("Hopefully the good ones", "I've heard about you too") then
	speak('LIAM', "Hopefully only the good ones.")
	speak('CYN', "Sure. And maybe some of the worse ones, maybe.")
	speak('LIAM', "Not too bad, of course! Just… you know, the fun ones.")
else
	speak('LIAM', "And I've heard stories about you.")
	speak('CYN', "Hopefully only the good ones!")
end

if getSwitch("wantsMassage") then
	speak('BIANCA', "Well, I haven’t told you yet about the massage he practically begged me for!")
else
	speak('BIANCA', "Well, he practically begged me not to touch him this whole trip, so we’ll see how that story goes!")
end
speak('CYN', "Ha!")
speak('BIANCA', "Let's be off.")
exitNVL()

allowDriving(true)
fadeOutBGM()
fade('black')
teleport('Residential2', true)
play('scene2_00')