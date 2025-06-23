using System;
using UnityEngine;

public class GateComponent : MonoBehaviour
{
    [SerializeField] private string luaCommand;

    private bool triggered;

    public void Update()
    {
        if (!triggered && Global.Instance.Avatar.transform.position.z >= transform.position.z)
        {
            triggered = true;
            var script = new LuaScript(Global.Instance.Lua, luaCommand);
            StartCoroutine(script.RunRoutine(canBlock: false));
        }
    }
}