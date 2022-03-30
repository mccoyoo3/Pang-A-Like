using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerGroundedState
{
    private Vector2 lastAIPos;
    private bool isShooting;
    public PlayerShootState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocityX(0f);
        CharacterAudio.Instance.Shoot();
        GameObject obj = Pooling.Instance.GetFromPool();
        obj.transform.position = core.CollisionSenses.ShootingCheck.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
