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
    private int minSlipperAmount = 10;
    private int maxSlipperAmount = 20;
    private int rageThrowcounter = 0;

    public float maxSpread, rageMaxSpread, oldMaxSpread;

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
        maxSpread = projectile.GetComponent<Projectile>().maxSpread;
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

            if (throwTimer >= throwRate)
            {
                slipperAmount = Random.Range(minSlipperAmount, maxSlipperAmount);
                ThrowSlipper(slipperAmount);
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
            var slipper = Instantiate(projectile, transform.position, Quaternion.identity);
            slipper.GetComponent<Projectile>().maxSpread = maxSpread;
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
        if (rageCounter >= 5)
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

        animator.SetTrigger("GrannyRageTrigger");

        oldMaxSpread = maxSpread;
        Debug.Log("oldmax " + oldMaxSpread);
        Debug.Log("max " + maxSpread);

        Invoke(nameof(RageThrowSlipper), 2f);
        Invoke(nameof(RageThrowSlipper), 3.5f);
        Invoke(nameof(RageThrowSlipper), 5f);

        Invoke(nameof(DifficultyUp), 10);


    }
    private void RageThrowSlipper()
    {

        slipperAmount = 40;
        maxSpread = 30f;
        Debug.Log("max2 " + maxSpread);

        ThrowSlipper(slipperAmount);

        rageThrowcounter++;
        Debug.Log("counter " + rageThrowcounter);
        if (rageThrowcounter == 3)
        {
            rageThrowcounter = 0;
            maxSpread = oldMaxSpread;
            Debug.Log("max3 " + maxSpread);
        }
    }
    private void DifficultyUp()
    {
        if (minThrowRate >= 1f)
        {
            minThrowRate -= 0.5f;
        }
        if (maxThrowRate >= 2f)
        {
            maxThrowRate -= 0.5f;
        }

        minSlipperAmount += 5;
        maxSlipperAmount += 5;

        if (maxSpread <= 40)
        {
            maxSpread += 6;
        }
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
