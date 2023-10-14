using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Rigidbody2D rb;
    Mob mob;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mob = GetComponent<Mob>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        mob.Vulner_buf(1);
    }
}
