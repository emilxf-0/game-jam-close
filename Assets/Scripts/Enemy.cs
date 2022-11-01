using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 5f;
    private int direction = 1;
    Vector2 movement;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = new Vector2(speed * Time.deltaTime, rb2d.velocity.y);

        switch (direction)
        {
            case 1:
                rb2d.velocity += movement;
                WobbleRight();
                break;

            case 0:
                rb2d.velocity -= movement;
                WobbleLeft();
                break;
        }
    }
    private void WobbleRight()
    {

    }
    private void WobbleLeft()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (direction == 0)
            {
                rb2d.velocity = movement;
                direction = 1;
            }
            else
            {
                rb2d.velocity = movement;
                direction = 0;
            }
        }
    }
}
