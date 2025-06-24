enter('BIANCA')
enterNVL()
speak('BIANCA', "Hi my name is Bianca.")
speak('BIANCA', "I'm in the passenger seat.")
enter('CYN')
speak('CYN', "I'm Cyn. I'm going to talk a lot to stress the textbox wrapping and try to demonstrate how much is reasonable to fit in one line of dialog. Probably you could squeeze three whole lines in here. Impressive, right?")
enter('ALISTAIR')
speak('ALISTAIR', "Alistair here! All I'm here to do is leave.")
speak('LIAM', "Seriously? That's it?")
exit('ALISTAIR')
speak('LIAM', "Oh, you can't see me, but I'm the protagonist.")
speak('BIANCA', "Liam, I'm gonna call the pizza place. What do you want on yours?")
exitNVL()

if choose("Pepperoni", "Veggies") then
	enterNVL()
	speak('BIANCA', "Since when are you vegetarian?")
	exitNVL()
else
	enterNVL()
	speak('BIANCA', "Aren't you vegetarian?")
	exitNVL()
end