speak('BIANCA', "A local copy center? What \"local copy center?\"")
choose("You know, the one near our house.", "I don't know.")
exitNVL()

driveWait(250)

enterNVL()
speak('BIANCA', "I don’t think there’s a copy center near our house.")
speak('BIANCA', "And it wouldn’t be a place where you’d print and laminate 4 different pamphlets, one each for yourself, your girlfriend, your girlfriend’s friend, and your friend you haven’t contacted in almost 10 years.")
exitNVL()

driveWait(200)

enterNVL()
speak('LIAM', "Fine. A man on the street gave them to me.")
speak('BIANCA', "Now it’s a man on the street?")
speak('LIAM', "What difference does it make?")
exitNVL()

driveWait(150)

enterNVL()
speak('BIANCA', "A big difference.")
exitNVL()

play('scene3_04')