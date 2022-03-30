using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerMoveState MoveState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerShootState ShootState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D MovementCollider { get; private set; }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

          IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
          MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
          ShootState = new PlayerShootState(this, StateMachine, playerData, "shoot");
          DeadState = new PlayerDeadState(this, StateMachine, playerData, "dead");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        MovementCollider = GetComponent<BoxCollider2D>();
        StateMachine.Initialize(MoveState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

}
