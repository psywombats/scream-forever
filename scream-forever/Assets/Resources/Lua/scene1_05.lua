enterNVL()
speak('LIAM', "Where does your friend live, again?")
speak('LIAM', "We're getting close to the area but I've forgotten.")
speak('BIANCA', "I'll pull it up on the GPS for you.")
exitNVL()

wait(1)
setSwitch('gpsOn', true)
wait(1)

enterNVL()
speak('BIANCA', "It’s 21 Birchfield Road.")
speak('BIANCA', "I think I remember coming here, it’s... it’s by that trash can up there.")
speak('LIAM', "Over there?")
speak('BIANCA', "Yup. I think I see Cynthia.")
exitNVL()

smoothBrake()

wait(2)