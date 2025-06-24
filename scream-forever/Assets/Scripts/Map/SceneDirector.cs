using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    [Serializable]
    public class SceneData
    {
        public string luaName;
    }
    [SerializeField] private List<SceneData> scenes;
}