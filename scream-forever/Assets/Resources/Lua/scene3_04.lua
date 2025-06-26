wait(5)

enterNVL()
speak('BIANCA', "Well, then. That might just be the strangest, most unlikely story I’ve ever heard.")
exitNVL()

driveWait(400)

setSwitch('needs_gas', true)

driveWait(200)

enterNVL()
speak('BIANCA', "The fuel light's on, Liam.")
exitNVL()

driveWait(200)

enterNVL()
speak('LIAM', "I know.")
exitNVL()

fade('black', 0.1)
teleport('Midnight1', true)
play('scene4_01')