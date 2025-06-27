enterNVL()
speak('ALISTAIR', "You’ve always had a chip on your shoulder against me. Ever since you were young. You couldn’t stand me.")
speak('BIANCA', "Alistair, that's enough.")
speak('ALISTAIR', "That’s why you had to go away, isn’t it? That’s why you had to abandon me? Leave me by myself?")
exitNVL()

setSpeed(25)
driveWait(100)

choose("...", ".")

driveWait(60)

choose("(I'm sorry Alistair)", "(You deserved it)")

enterNVL()
speak('ALISTAIR', "Blaming everyone else but yourself for the pain that you caused.")
speak('ALISTAIR', "For the ways that you wanted everyone to bow down to you. And to worship your feet.")
speak('BIANCA', "Honey. Ignore him. Hold my hand.")
speak('BIANCA', "Squeeze if you can hear me.")
choose("(I didn’t mean to abandon you)", "(Everyone wanted me gone)")
speak('ALISTAIR', "Yes, I didn’t call you. But you were the one who left.")
speak('ALISTAIR', "You didn’t even say goodbye. How am I supposed to deal with that?")
exitNVL()

wait(1)

enterNVL()
speak('ALISTAIR', "We were best friends.")
choose("(I know we were)", "(I wanted to say goodbye, but I couldn’t)")
speak('BIANCA', "Say you love me if you love me, Liam.")
speak('ALISTAIR', "You left me.")
choose("(I had no choice)", "(The pain was too much...)")
speak('BIANCA', "Liam.")
exitNVL()

wait(.4)

enterNVL()
speak('BIANCA', "I would die for you.")
exitNVL()

wait(.4)

enterNVL()
speak('ALISTAIR', "And you left your mother, too.")
speak('ALISTAIR', "You left all of us, but...")
speak('ALISTAIR', "You left her to die.")
exitNVL()

play('scene7_03')