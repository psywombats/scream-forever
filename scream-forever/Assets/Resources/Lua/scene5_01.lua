setSpeed(20)
fade('normal', .1)

driveWait(350)

enterNVL()
speak('ALISTAIR', "I need to smoke.")
speak('BIANCA', "Well then, smoke away. Nobody’s stopping you.")
speak('ALISTAIR', "Cyn told me she doesn't like the smell of smoke.")
speak('CYN', "It’s fine! It’s fine.")
speak('ALISTAIR', "Not according to what you told me earlier.")
speak('ALISTAIR', "I’ll do it outside the car and spare your sweet lungs.")
exitNVL()

driveWait(15)

enterNVL()
speak('ALISTAIR', "Could we stop at the nearest station, monsieur driver? This thing ain’t gonna smoke itself.")
speak('LIAM', "Maybe you should just avoid smoking for a bit. It'd be better for you.")
speak('ALISTAIR', "Alright.")
exitNVL()

driveWait(12)

enterNVL()
speak('ALISTAIR', "A bit preachy.")
speak('BIANCA', "We do need to fill up. There’s not enough fuel. It’s running on empty.")
speak('LIAM', "I remember. I saw the blinking light, just as you did.")
speak('BIANCA', "Yeah. You did.")
exitNVL()

driveWait(12)

enterNVL()
speak('ALISTAIR', "So maybe we do stop at the petrol station.")
exitNVL()

driveWait(500)

play('scene5_02')