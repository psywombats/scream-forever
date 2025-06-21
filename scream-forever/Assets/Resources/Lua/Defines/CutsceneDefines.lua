-- global defines for cutscenes

function teleportCoords(mapName, x, y)
    cs_teleportCoords(mapName, x, y)
    await()
end

function teleport(mapName, eventName, dir, raw)
    cs_teleport(mapName, eventName, dir, raw)
    await()
end

function fadeOutBGM(seconds)
    cs_fadeOutBGM(seconds)
    await()
end

function speak(speaker, line)
    cs_speak(speaker, line)
    await()
end

function radio(speaker, line, qual)
	cs_radio(speaker, line, qual)
	await()
end

function fade(fadeType, dur)
    cs_fade(fadeType, dur)
    await()
end

function enterNVL(arg)
    cs_enterNVL(arg)
    await()
end

function exitNVL()
    cs_exitNVL()
    await()
end

function expr(speaker, expression)
	cs_expr(speaker, expression)
	await()
end

