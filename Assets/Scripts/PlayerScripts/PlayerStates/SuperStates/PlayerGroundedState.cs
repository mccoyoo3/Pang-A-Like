using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;
    protected bool isTouchingWall;
    private bool shootInput;
    private bool isGrounded;
    private bool isDead;


    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = core.CollisionSenses.Ground;
        isTouchingWall = core.CollisionSenses.WallFront;
        isDead = core.CollisionSenses.Trap;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        shootInput = player.InputHandler.shootInput;
        xInput = player.InputHandler.NormInputX;

        if (isGrounded && isTouchingWall && !isDead)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (xInput == 0 && shootInput && !isDead)
        {
            if (Time.time >= startTime + playerData.shootAgainTime)
            {
                stateMachine.ChangeState(player.ShootState);
            }
        }
        else if (isDead)
        {
            stateMachine.ChangeState(player.DeadState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
