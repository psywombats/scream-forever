using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IInputListener
{
    [SerializeField] public new Camera camera;
    [Space]
    [SerializeField] [Range(0f, 9f)] private float mouseRotateSensitivity = .1f;
    [SerializeField] private Vector2 rotationXBounds = new Vector2(-70, 70);
    [SerializeField] private Vector2 rotationYBounds = new Vector2(-70, 70);
    [SerializeField] [Range(0f, 10f)] private float stickyStrength;
    [SerializeField] [Range(0f, 180)] private int stickyAllowance;
    [Space] 
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private Transform speedTransform;
    [SerializeField] private Transform rpmTransform;
    
    private int pauseCount;
    private bool selfPaused;
    public bool IsPaused => pauseCount > 0;

    private float wheelRotation;

    private Quaternion targetLook;

    private void Start()
    {
        Global.Instance.Maps.Avatar = this;
        targetLook = camera.transform.rotation;
    }

    private void Update()
    {
        if (!IsPaused)
        {
            HandleFPC();
        }

        HandleStickyCam();
    }

    public void OnEnable()
    {
        InputManager.Instance.PushListener(this);
    }

    public void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.RemoveListener(this);
        }
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

    public bool OnCommand(InputManager.Command command, InputManager.Event eventType)
    {
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
        Debug.Log(inX + ", " + inY);

        var trans = camera.transform;

        var sense = mouseRotateSensitivity;
        if (Application.platform == RuntimePlatform.WebGLPlayer) sense *= 10;
        trans.rotation = trans.rotation * Quaternion.AngleAxis(inX * mouseRotateSensitivity, Vector3.up)
                                        * Quaternion.AngleAxis(inY * mouseRotateSensitivity, Vector3.left);

        var angX = trans.eulerAngles.x;
        while (angX > 180) angX -= 360;
        while (angX < -180) angX += 360;
        
        var angY = trans.eulerAngles.y;
        while (angY > 180) angY -= 360;
        while (angY < -180) angY += 360;

        trans.rotation = Quaternion.Euler(
            Mathf.Clamp(angX, rotationYBounds.x, rotationYBounds.y),
            Mathf.Clamp(angY, rotationXBounds.x, rotationXBounds.y),
            0
        );
    }

    private void HandleStickyCam()
    {
        if (stickyStrength == 0)
        {
            return;
        }

        var strength = Quaternion.Angle(camera.transform.rotation, targetLook);
        strength = (strength - stickyAllowance) / 180f;
        if (strength <= 0)
        {
            return;
        }
        strength *= stickyStrength;
        camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, targetLook, strength);
    }
}
