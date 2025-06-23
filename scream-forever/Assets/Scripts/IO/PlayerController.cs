using DG.Tweening;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IInputListener
{
    [SerializeField] public new Camera camera;
    [SerializeField] public Rigidbody body;
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
    [SerializeField] private float maxWheelRotate;
    [SerializeField] private float maxSpeedRotate;
    [SerializeField] private float maxRPMRotate;
    [SerializeField] private GameObject brakelightsArea;
    [Space]
    [SerializeField] private Transform frontSampler;
    [SerializeField] private Transform backSampler;
    [Space]
    [SerializeField] private float turnRate = 90;
    [SerializeField] private float returnRate = 30;
    [SerializeField] private float accRate = 1;
    [SerializeField] private float decRate = 2;
    [SerializeField] private float maxSpeed = 24;
    [SerializeField] private float maxTurn = 30;
    [SerializeField] private float friction = .5f;
    [SerializeField] private float suspension = .05f;
    
    private int pauseCount;
    private bool selfPaused;
    public bool IsPaused => pauseCount > 0;

    private bool handledWheelThisFrame;
    private float speed;
    private float wheelRotation;
    private float timeSinceBrakes = 100f;

    private Quaternion targetLook;

    private void Start()
    {
        Global.Instance.Maps.Avatar = this;
        targetLook = camera.transform.localRotation;
    }

    private void Update()
    {
        if (!IsPaused)
        {
            HandleFPC();
        }

        HandleStickyCam();
        HandlePhysics();
        HandleRoadLock();
        
        brakelightsArea.gameObject.SetActive(timeSinceBrakes <= .1f);
        timeSinceBrakes += Time.deltaTime;
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
            case InputManager.Event.Hold:
                switch (command)
                {
                    case InputManager.Command.Left:
                        wheelRotation -= turnRate * Time.deltaTime;
                        handledWheelThisFrame = true;
                        break;
                    case InputManager.Command.Right:
                        wheelRotation += turnRate * Time.deltaTime;
                        handledWheelThisFrame = true;
                        break;
                    case InputManager.Command.Up:
                        speed += accRate * Time.deltaTime;
                        break;
                    case InputManager.Command.Down:
                        timeSinceBrakes = 0f;
                        speed -= decRate * Time.deltaTime;
                        break;
                }
                break;
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
        trans.localRotation = trans.localRotation * Quaternion.AngleAxis(inX * mouseRotateSensitivity, Vector3.up)
                                                  * Quaternion.AngleAxis(inY * mouseRotateSensitivity, Vector3.left);

        var angX = trans.localEulerAngles.x;
        while (angX > 180) angX -= 360;
        while (angX < -180) angX += 360;
        
        var angY = trans.localEulerAngles.y;
        while (angY > 180) angY -= 360;
        while (angY < -180) angY += 360;

        trans.localRotation = Quaternion.Euler(
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

        var strength = Quaternion.Angle(camera.transform.localRotation, targetLook);
        strength = (strength - stickyAllowance) / 180f;
        if (strength <= 0)
        {
            return;
        }
        strength *= stickyStrength;
        camera.transform.localRotation = Quaternion.Slerp(camera.transform.localRotation, targetLook, strength);
    }

    private void HandlePhysics()
    {
        if (handledWheelThisFrame)
        {
            handledWheelThisFrame = false;
        }
        else
        {
            if (wheelRotation > 0)
            {
                wheelRotation -= returnRate * Time.deltaTime;
                if (wheelRotation < 0) wheelRotation = 0;
            }
            if (wheelRotation < 0)
            {
                wheelRotation += returnRate * Time.deltaTime;
                if (wheelRotation > 0) wheelRotation = 0;
            }
        }

        speed -= friction * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        body.velocity = transform.forward * speed;

        wheelRotation = Mathf.Clamp(wheelRotation, -maxTurn, maxTurn);
        var angles = transform.localRotation.eulerAngles;
        angles.y += wheelRotation * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(angles);

        wheelTransform.localRotation = Quaternion.Euler(0f, wheelRotation / maxTurn * maxWheelRotate, 0f);
    }

    private void HandleRoadLock()
    {
        var frontHeight = MapManager.Instance.ActiveMap.terrain.GetHeightAt(frontSampler.position);
        var backHeight = MapManager.Instance.ActiveMap.terrain.GetHeightAt(backSampler.position);
        var adjFront = new Vector3(frontSampler.position.x, frontHeight, frontSampler.position.z);
        var adjBack = new Vector3(backSampler.position.x, backHeight, backSampler.position.z);
        body.transform.rotation = Quaternion.RotateTowards(
            body.transform.rotation,
            Quaternion.LookRotation(adjFront - adjBack), 
            1f / suspension * Time.deltaTime);
    }
}
