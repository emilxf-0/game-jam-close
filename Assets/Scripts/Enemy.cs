using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator animator;

    private float throwTimer;
    private float throwRate = 4f;
    private float minThrowRate = 2f;
    private float maxThrowRate = 4.5f;
    private float gameTimer;
    private float speedUpThrowRate = 4f;

    public int slipperAmount;

    private bool wobbleRight;
    private float counter;

    private Rigidbody2D rb2d;
    public float speed = 5f;
    private int direction = 1;
    Vector2 movement;
    private bool moving;

    public bool grannyRage = false;
    public int rageCounter = 0;

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
                slipperAmount = Random.Range(10, 20); //;
                ThrowSlipper(slipperAmount);

            }
            if (gameTimer >= speedUpThrowRate && throwRate >= 0.3f)
            {
                minThrowRate -= 0.05f;
                maxThrowRate -= 0.05f;
                gameTimer = 0;
            }
            Wobble();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GrannyRageCounter();
        }
    }
    private void ThrowSlipper(int slipperAmount)
    {
        moving = false;
        rb2d.velocity = Vector2.zero;
        Invoke(nameof(StartMoving), 1);
        
        for (int i = 0; i < slipperAmount; i++)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
        }

        animator.SetTrigger("isThrowing");
        throwTimer = 0;
        throwRate = Random.Range(minThrowRate, maxThrowRate);
    }

    public void GrannyRageCounter()
    {
        rageCounter++;
        //todo termometer update
        Debug.Log(rageCounter);
        if(rageCounter >= 5)
        {
            rageCounter = 0;
            GrannyRage();
        }
    }
    private void GrannyRage()
    {
        moving = false;
        rb2d.velocity = Vector2.zero;
        Invoke(nameof(StartMoving), 15);

        Invoke(nameof(RageThrowSlipper), 2.5f);
        Invoke(nameof(RageThrowSlipper), 4f);
        Invoke(nameof(RageThrowSlipper), 5.5f);

        animator.SetTrigger("GrannyRageTrigger");
    }

    private void RageThrowSlipper()
    {
        slipperAmount = 40;
        projectile.GetComponent<Projectile>().maxSpread = 15f;

        ThrowSlipper(slipperAmount);

        projectile.GetComponent<Projectile>().maxSpread = 6f;

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
