enterNVL()
speak('LIAM', "Where does your friend live, again?")
speak('LIAM', "We're getting close to the area but I've forgotten.")
speak('BIANCA', "I'll pull it up on the GPS for you.")
exitNVL()

wait(1)
setSwitch('gps_on', true)
wait(1)

enterNVL()
speak('BIANCA', "It’s 21 Birchfield Road.")
speak('BIANCA', "I think I remember coming here, it’s... it’s by that trash can up there.")
speak('LIAM', "Over there?")
speak('BIANCA', "Yup. I think I see Cynthia.")
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