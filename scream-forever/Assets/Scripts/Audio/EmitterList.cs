using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class EmitterList : MonoBehaviour
{
    [Serializable]
    public class TaggedEmitter
    {
        public string tag;
        public StudioEventEmitter emitter;
    }

    [SerializeField] public List<TaggedEmitter> emitters;

    public StudioEventEmitter TryPlay(string tag)
    {
        foreach (var emitter in emitters)
        {
            if (emitter.tag == tag)
            {
                emitter.emitter.Play();
                return emitter.emitter;
            }
        }

        return null;
    }
}