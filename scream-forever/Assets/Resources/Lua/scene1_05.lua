enterNVL()
speak('LIAM', "Where does Cyn live again?")
speak('LIAM', "I think it was around here, but everything looks the same in these parts.")
speak('BIANCA', "One sec. Lemme pull it up.")
exitNVL()

wait(1)
setSwitch('gps_on', true)
wait(1)

enterNVL()
speak('BIANCA', "21 Birchfield Road.")
speak('BIANCA', "Ooh! Ooh! Right up there.")
speak('LIAM', "Over there?")
speak('BIANCA', "Yup. Right here! That's Cyn. There, there!")
exitNVL()

setSwitch('scene1_cyn' , true)
driveWait(10)
distBrake(20)
allowDriving(false)

wait(2)

setSwitch('scene1_cyn' , false)
wait(.7)
playSFX('door')
wait(.8)
enter('CYN')

play('scene1_06')