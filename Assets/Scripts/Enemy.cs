using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator animator;

    private float throwTimer;
    private float throwRate = 2f;
    private float gameTimer;
    private float speedUpThrowRate = 4f;

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

        throwTimer += Time.deltaTime;
        gameTimer += Time.deltaTime;

        if (throwTimer >= throwRate)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            animator.SetTrigger("isThrowing");
            throwTimer = 0;
        }
        if (gameTimer >= speedUpThrowRate && throwRate >= 0.3f)
        {
            throwRate -= 0.05f;
            gameTimer = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
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
