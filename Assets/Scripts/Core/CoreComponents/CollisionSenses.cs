using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms


    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform DeathCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(deathCheck, core.transform.parent.name);
        private set => deathCheck = value;
    }
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    public Transform ShootingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(shootingCheck, core.transform.parent.name);
        private set => shootingCheck = value;
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float DeathCheckRadius { get => deathCheckRadius; set => deathCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform deathCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform shootingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float shootingCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float deathCheckRadius;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsTrap;

    #endregion

    public bool Shoot
    {
        get => Physics2D.OverlapCircle(ShootingCheck.position, shootingCheckRadius, whatIsGround);
    }

    public bool Ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool Trap
    {
        get => Physics2D.OverlapCircle(DeathCheck.position, deathCheckRadius, whatIsTrap);
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 direction = Vector2.right * WallCheckDistance;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(deathCheck.position, deathCheckRadius);
        Gizmos.DrawWireSphere(shootingCheck.position, shootingCheckRadius);
        Gizmos.DrawRay(wallCheck.position, direction);
    }
}
