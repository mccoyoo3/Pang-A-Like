using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsOfSteel : MonoBehaviour
{
    private float ballSpeed = 1.9f;
    private Vector2 currentVelocity;
    private float startY;
    [SerializeField]
    private GameObject originalBall;
    private GameObject ball1;
    private GameObject ball2;
    private Rigidbody2D RB;
    [SerializeField]
    private float startDirection;

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.AddForce(Vector2.left * startDirection);
    }

    void FixedUpdate()
    {
        currentVelocity = RB.velocity;
    }
    // if balls are not equal to the tag, spawn some balls yeah!
    private void ProduceBalls()
    {
        if (!gameObject.CompareTag("Smallest Ball"))
        {
            ball1 = Instantiate(originalBall);
            ball2 = Instantiate(originalBall);
            ball1.name = originalBall.name;
            ball2.name = originalBall.name;
        }
    }
    //if current ball is hit, spawn more balls!
    private void InitializeBallsAndDeactivateCurrentBall()
    {
        ProduceBalls();

        Vector2 temp = transform.position;
        ball1.transform.position = temp;
        ball2.transform.position = temp;
        ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0f);
        ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0f);
        gameObject.SetActive(false);
        CharacterAudio.Instance.Explode();
    }
    //make the balls bounce in an unlimited manner.
    void OnCollisionEnter2D(Collision2D coll)
    {
        ContactPoint2D hit = coll.contacts[0];
        Vector3 Vel_out = Vector3.Reflect(currentVelocity, hit.normal);
        if (hit.normal.x < 0)
            RB.velocity = new Vector2(-ballSpeed, currentVelocity.y);
        else if (hit.normal.x > 0)
            RB.velocity = new Vector2(ballSpeed, currentVelocity.y);
        else
            RB.velocity = new Vector2(ballSpeed * currentVelocity.x, RB.velocity.y);
        if (hit.normal.y < 0)
        {
            if (Mathf.Abs(currentVelocity.y) < 1)
                RB.velocity = new Vector2(RB.velocity.x, -1);
            else
                RB.velocity = new Vector2(RB.velocity.x, -Mathf.Abs(currentVelocity.y));
        }
        else if (hit.normal.y > 0)
        {
            float relPos = transform.position.y - startY;
            if (relPos > 0f)
                relPos = 0f;
            float newYVel = Mathf.Sqrt(2 * relPos * Physics2D.gravity.y * RB.gravityScale);
            if (newYVel == 0) newYVel = 1f;
            RB.velocity = new Vector2(RB.velocity.x, newYVel);
        }

        currentVelocity = RB.velocity;

        if (coll.collider.CompareTag("Bullet"))
        {
            if (!gameObject.CompareTag("Smallest Ball"))
            {
                InitializeBallsAndDeactivateCurrentBall();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
