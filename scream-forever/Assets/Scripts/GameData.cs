﻿using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// The generic stuff that used to be attached to the player party, such as location, gp, etc.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class GameData
{
    // switches and variables
    [JsonProperty] public Dictionary<string, int> Variables { get; private set; }
    [JsonProperty] public Dictionary<string, string> StringVariables { get; private set; }
    [JsonProperty] public Dictionary<string, bool> Switches { get; private set; }

    public int SwitchLastUpdatedFrame { get; set; } = 0;

    public GameData()
    {
        Variables = new Dictionary<string, int>();
        StringVariables = new Dictionary<string, string>();
        Switches = new Dictionary<string, bool>();
        SwitchLastUpdatedFrame = UnityEngine.Time.frameCount;
    }

    public bool GetSwitch(string switchName)
    {
        if (!Switches.ContainsKey(switchName))
        {
            return false;
        }
        return Switches[switchName];
    }

    public void SetSwitch(string switchName, bool value)
    {
        Switches[switchName] = value;
        SwitchLastUpdatedFrame = UnityEngine.Time.frameCount;
    }

    public int GetVariable(string variableName)
    {
        if (!Variables.ContainsKey(variableName))
        {
            Variables[variableName] = 0;
        }
        return Variables[variableName];
    }

    public string GetStringVariable(string variableName)
    {
        if (!StringVariables.ContainsKey(variableName))
        {
            StringVariables[variableName] = "";
        }
        return StringVariables[variableName];
    }

    public void IncrementVariable(string variableName)
    {
        Variables[variableName] = GetVariable(variableName) + 1;
    }

    public void DecrementVariable(string variableName)
    {
        Variables[variableName] = GetVariable(variableName) - 1;
    }

    public void SetVariable(string variableName, int value)
    {
        Variables[variableName] = value;
    }

    public void SetStringVariable(string variableName, string value)
    {
        StringVariables[variableName] = value;
    }
}
