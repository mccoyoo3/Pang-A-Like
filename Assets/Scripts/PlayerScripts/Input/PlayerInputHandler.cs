using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    //private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool shootInput { get; private set; }
    public bool shootInputStop { get; private set; }


    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float shootInputStartTime;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        OnMoveInput(RawMovementInput);
        CheckShootInputHoldTime();
        MovementControl();
    }

    private void FixedUpdate()
    {

    }

    private void MovementControl()
    {
#if UNITY_EDITOR
        RawMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Fire2"))
        {
            OnShootInput();
        }
#endif
#if UNITY_ANDROID
        RawMovementInput = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        if (SimpleInput.GetButtonDown("Fire2"))
        {
            OnShootInput();
        }
#endif
#if UNITY_IOS
        RawMovementInput = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        if (SimpleInput.GetButtonDown("Fire2"))
        {
            OnShootInput();
        }
#endif
    }

    public void OnMoveInput(Vector2 _axis)
    {
        _axis = RawMovementInput;
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);

    }

    public void OnShootInput()
    {
        shootInput = true;
    }

    private void CheckShootInputHoldTime()
    {
        if (Time.time >= shootInputStartTime + inputHoldTime)
        {
            shootInput = false;
        }
    }
}
