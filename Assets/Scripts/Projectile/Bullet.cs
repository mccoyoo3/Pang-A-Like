using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D RB;
    public bool isGrapple;

    //bullets go boom! tick if it is grapple
    private void OnEnable()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.velocity = Vector2.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.CompareTag("Ceiling"))
        {
            if (!isGrapple)
            { 
                Pooling.Instance.AddToPool(gameObject);
            }
            else
            {
                RB.velocity = Vector2.zero;
            }

        }

        string[] name = target.gameObject.name.Split();

        if(name.Length > 1)
        {
            if (name [1] == "Ball")
            {
                Pooling.Instance.AddToPool(gameObject);
            }
        }
    } 
}
