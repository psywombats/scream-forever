enter('???')
playBGM('mom')

driveWait(100)

exit('CYN')
wait(2)
exit('BIANCA')
wait(2)
exit('ALISTAIR')
wait(3)

enterNVL()
speak('???', "So Say It Now")
speak('???', "Say Goodbye")
speak('???', "Your Life Is Not Yours Anymore")
speak('???', "The Crows Have Appeared")
speak('???', "And They Circle Now")
speak('???', "They Circle Around The Body")
speak('???', "Perching")
speak('???', "Observing")
exitNVL()

driveWait(200)

enterNVL()
speak('???', "So The World Will Know Where You Stood")
speak('???', "When Your Final Day Passed")
speak('???', "Make Your Vow")
exitNVL()

if choose("(Please forgive me)", "(I don't need to apologize)") then
	play("scene7_04_forgive")
else
	play("scene7_04_forget")
end