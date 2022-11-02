using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator animator;

    private float throwTimer;
    private float throwRate = 4f;
    private float gameTimer;
    private float speedUpThrowRate = 4f;

    public float slipperSpread;
    public int slipperAmount;

    private bool wobbleRight;
    private float counter;

    private Rigidbody2D rb2d;
    public float speed = 5f;
    private int direction = 1;
    Vector2 movement;
    private bool moving;

    void Start()
    {
        moving = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (moving)
        {
            movement = new Vector2(speed * Time.deltaTime, rb2d.velocity.y);

            switch (direction)
            {
                case 1:
                    rb2d.velocity += movement;
                    break;

                case 0:
                    rb2d.velocity -= movement;
                    break;
            }

            throwTimer += Time.deltaTime;
            gameTimer += Time.deltaTime;

            if (throwTimer >= throwRate)
            {
                moving = false;
                rb2d.velocity = Vector2.zero;
                Invoke(nameof(StartMoving), 1);

                ThrowSlipper();
                throwRate = Random.Range(2.5f, 4.5f);
            }
            if (gameTimer >= speedUpThrowRate && throwRate >= 0.3f)
            {
                throwRate -= 0.05f;
                gameTimer = 0;
            }
            Wobble();
        }
        

    }
    private void ThrowSlipper()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        animator.SetTrigger("isThrowing");
        throwTimer = 0;
    }
    private void StartMoving()
    {
        moving = true;
    }
    private void Wobble()
    {
        if (wobbleRight)
        {
            transform.eulerAngles = new Vector3(0, 0, -7 * 0.5f);
            counter++;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 7 * 0.5f);
            counter++;
        }

        if (counter % 200 == 0)
        {
            wobbleRight = !wobbleRight;
        }
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
