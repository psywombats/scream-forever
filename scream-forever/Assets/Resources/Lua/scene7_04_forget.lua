playBGM('finale')

choose("....", "....")
enter('BIANCA')
enterNVL()
speak('BIANCA', "Liam.")
choose("....", "....")
speak('BIANCA', "Can you see the road ahead?")
choose("........", "........Yes.")
speak('BIANCA', "Hey. Liam. You're moving too fast.")
choose("(I abandoned my hope.)", "(I abandoned my muse.)")
speak('BIANCA', ".............You Find What You Search For.............")
choose("(I abandoned my connection.)", "(I abandoned my community.)")
exitNVL()

setSpeed(30)
driveWait(60)

enter('ALISTAIR')
enterNVL()
speak('ALISTAIR', "Steady on there, lad.")
speak('BIANCA', "............The Crows Delight In Their Circles............")
speak('BIANCA', "............Around And Around They Spin........................")
exitNVL()
choose("(I abandoned my love.)", "(I abandoned my pain.)")

setSpeed(33)
driveWait(60)

enter('CYN')
speak('CYN', "Liam! Please.")
speak('BIANCA', "............Over And Over The Rotations Devolve........................")
speak('CYN', "Bee?")
choose("(I abandoned my mother.)", "(I abandoned my friend.)")
speak('CYN', "No no no no no...")
speak('BIANCA', "...")
speak('BIANCA', "The Crows Circle Unto Themselves")
choose("(I abandoned myself.)", "(I abandoned myself.)")
speak('ALISTAIR', "It's okay, Cyn.")
speak('Cyn', "But...")
exitNVL()

setSpeed(35)
driveWait(70)
setSwitch('snap_cam', true)
wait(.5)
setSwitch('snap_cam', false)

driveWait(70)
setSwitch('snap_cam', true)
wait(.4)
setSwitch('snap_cam', false)
wait(.4)
setSwitch('snap_cam', true)
wait(.4)
setSwitch('snap_cam', false)
wait(.4)
setSwitch('snap_cam', true)
wait(.4)
setSwitch('snap_cam', false)
wait(.4)

setSpeed(38)
driveWait(100)

enterNVL()
speak('BIANCA', "Find Me Over And Over Again")
exitNVL()

play('scene7_05')