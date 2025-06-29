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

function speak(speaker, line, mode)
    cs_speak(speaker, line, mode)
    await()
end

function fade(fadeType, dur)
    cs_fade(fadeType, dur)
    await()
end

function enterNVL()
    cs_enterNVL()
    await()
end

function exitNVL()
    cs_exitNVL()
    await()
end

function enter(speaker, expression)
	cs_enter(speaker, expression)
	await()
end

function exit(arg, useAnim)
	cs_exit(arg, useAnim)
	await()
end

function choose(a, b)
    cs_choose(a, b)
    await()
    return selection
end

function smoothBrake(duration)
	cs_smoothBrake(duration)
	await()
end

function distBrake(duration)
	cs_distBrake(duration)
	await()
end

function expr(speaker, expression)
	cs_expr(speaker, expression)
	await()
end

function driveWait(dist)
	cs_driveWait(dist)
	await()
end

function pamphlet(tag)
	cs_pamphlet(tag)
	await()
end

function video(arg)
	cs_video(arg)
	await()
end