using DG.Tweening;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IInputListener
{
    [SerializeField] public new Camera camera;
    [Space]
    [SerializeField] [Range(0f, 9f)] float mouseRotateSensitivity = 2f;
    [SerializeField] Vector2 RotationYBounds = new Vector2(-70, 70);
    [Space]
    [SerializeField] private RenderTexture lapTex;
    [SerializeField] private RenderTexture transTex;
    
    private int pauseCount;
    private bool selfPaused;
    public bool IsPaused => pauseCount > 0;

    private void Start()
    {
        Global.Instance.Maps.Avatar = this;
    }

    private void Update()
    {
        if (!IsPaused)
        {
            HandleFPC();
        }
    }

    public void OnEnable()
    {
        InputManager.Instance.PushListener(this);
    }

    public void OnDisable()
    {
        InputManager.Instance?.RemoveListener(this);
    }

    public void PauseInput()
    {
        pauseCount += 1;
    }

    public void UnpauseInput()
    {
        if (pauseCount > 0)
        {
            pauseCount -= 1;
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private Vector2Int GetMouse()
    {
        var pos = Mouse.current.position;
        return new Vector2Int((int)pos.x.ReadValue(), (int)pos.y.ReadValue());
    }

    public void OnTeleport()
    {

    }

    public IEnumerator RotateTowardRoutine(GameObject mover, GameObject target, GameObject rotater)
    {
        var targetPos = target.transform.position + new Vector3(0, 1f, 0);
        var dir = (targetPos - mover.transform.position).normalized;
        if (mover == rotater)
        {
            dir *= -1;
        }
        var lookAngles = Quaternion.LookRotation(dir).eulerAngles;
        if (mover == rotater)
        {
            lookAngles.x = 0f;
        }

        return CoUtils.RunTween(rotater.transform.DORotate(lookAngles, .5f));
    }

    public void Screenshot()
    {
        camera.enabled = false;
        camera.targetTexture = lapTex;
        camera.Render();
        camera.targetTexture = transTex;
        camera.Render();
        camera.targetTexture = null;
        camera.enabled = true;
    }

    public bool OnCommand(InputManager.Command command, InputManager.Event eventType)
    {
        /*
        if (command == InputManager.Command.Menu && eventType == InputManager.Event.Up)
        {
            if (selfPaused)
            {
                UnpauseInput();
                selfPaused = false;
            }
            else
            {
                PauseInput();
                selfPaused = true;
            }
        }
        */
        
        if (IsPaused)
        {
            return true;
        }
        switch (eventType)
        {
            
        }
        return true;
    }

    private void HandleFPC()
    {
        var mouse = Mouse.current.delta;
        var inX = mouse.x.ReadValue();
        var inY = mouse.y.ReadValue();

        var trans = camera.transform;

        var sense = mouseRotateSensitivity;
        if (Application.platform == RuntimePlatform.WebGLPlayer) sense *= 10;
        trans.rotation *= Quaternion.AngleAxis(inY * mouseRotateSensitivity, Vector3.left);

        var ang = trans.eulerAngles.x;
        while (ang > 180) ang -= 360;
        while (ang < -180) ang += 360;

        var xcom = inX * mouseRotateSensitivity;

        trans.rotation = Quaternion.Euler(
            Mathf.Clamp(ang, RotationYBounds.x, RotationYBounds.y),
            trans.eulerAngles.y + xcom,
            0
        );
    }
}
