using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator animator;
    [SerializeField] private Ragemeter ragemeter;
    [SerializeField] private CharacterSounds characterSounds;

    private float throwTimer;
    private float throwRate = 4f;
    private float minThrowRate = 2f;
    private float maxThrowRate = 4.5f;
    private float gameTimer;
    private float speedUpThrowRate = 4f;

    public int slipperAmount;
    private int minSlipperAmount = 10;
    private int maxSlipperAmount = 20;
    private int rageWaveAmount = 3;
    private float maxSpread;
    private float rageWaveIntervall = 1.5f;

    private bool wobbleRight;
    private float counter;

    private Rigidbody2D rb2d;
    public float speed = 5f;
    private int direction = 1;
    Vector2 movement;
    private bool moving;

    public bool grannyRage = false;

    public int startRage = 0;
    public int maxRage = 5;
    public int rageCounter = 0;

    void Start()
    {
        moving = true;
        rb2d = GetComponent<Rigidbody2D>();
        ragemeter.SetStartRage(startRage);
        ragemeter.SetMaxRage(maxRage);

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
            gameTimer += Time.deltaTime;

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
        characterSounds.audioSource.PlayOneShot(characterSounds.audioLibrary[1]);
        rageCounter++;
        //todo termometer update
        ragemeter.SetRage(rageCounter);
        Debug.Log(rageCounter);

        if (rageCounter > maxRage)
        {
            GrannyRage();
            rageCounter = 0;
            maxRage *= 2;
            ragemeter.SetRage(rageCounter);
            ragemeter.SetMaxRage(maxRage);
        }
    }
    private void GrannyRage()
    {
        moving = false;
        rb2d.velocity = Vector2.zero;

        for (int i = 0; i < rageWaveAmount; i++)
        {
            RageWave();

            if (i == rageWaveAmount - 1)
            {
                StartMoving();
            }
        }

        Invoke(nameof(DifficultyUp), 10);

        animator.SetTrigger("GrannyRageTrigger");
    }
    private void RageWave()
    {
        Invoke(nameof(RageThrowSlipper), rageWaveIntervall);
        rageWaveIntervall += 1.5f;
    }
    private void RageThrowSlipper()
    {
        slipperAmount = 40;

        ThrowSlipper(slipperAmount);
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

        rageWaveAmount++;
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
