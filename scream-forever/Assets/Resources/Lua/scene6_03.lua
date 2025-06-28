setSwitch('gps_on', false)
playSFX('impact')
impact()
allowDriving(false)
wait(3.5)
enterNVL()


enterNVL()
speak('BIANCA', "Liam! Are you okay?")
speak('ALISTAIR', "What in the world was THAT?")
speak('BIANCA', "I think we've hit... something.")
speak('CYN', "No! Poor thing. I don't want to see any carnage!")
speak('ALISTAIR', "We have to check the car for damage. Come on guys.")
exitNVL()

exit('ALISTAIR')
wait(1.5)
exit('CYN')
wait(.5)
setSwitch('alistar_scene6', true)
wait(.8)
setSwitch('cyn_scene6', true)

wait(1.7)

enterNVL()
speak('BIANCA', "I think I need to go...")
speak('BIANCA', "...see what has happened.")
exitNVL()

wait(.8)

enterNVL()
speak('LIAM', "Yes.")
exitNVL()

wait(2)

enterNVL()
speak('BIANCA', "I'll be back.")
exitNVL()

exit('BIANCA')

wait(2.0)

mom(true)
wait(5)

setSwitch('cyn_scene6', false)
wait(.8)
setSwitch('alistar_scene6', false)
wait(2)

setSwitch('kill_lights', true)
wait(.05)
setSwitch('kill_lights', false)
wait(.05)
setSwitch('kill_lights', true)
wait(2)

play('scene6_04')