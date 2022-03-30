using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerGroundedState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        this.player = player;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.Respawn();
        CameraShake.Instance.ShakeCamera(5f, .1f);
        Scoring.Instance.DecreaseLives(1);
        CharacterAudio.Instance.Dead();
        GameObject.Instantiate(playerData.deadGundam, player.transform.position, playerData.deadGundam.transform.rotation);
        GameObject.Destroy(player.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
