using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private GameObject slipperPoof;

    public float playerSpeed;
    public float startSpeed = 8;
    public float dodgeTime;
    public float dodgeSpeed = 15;
    public float dodgeLength = 3;
    public float angle = 7;
    private int health = 6;
    private int score;

    [SerializeField] ScoreManager scoreManager;
    [SerializeField] HealthManager healthManager;

    private bool isDodging;
    private bool justTeleported;
    private bool wobbleRight;
    private float counter;
    private bool isInvincible = false;

    private Vector3 bob;

    private Rigidbody2D rb2d;
    public Animator animator;
    private Vector2 movementInput;
    //[SerializeField] private GameObject dodgeText;
    


    void Start()
    {
        //dodgeText.SetActive(false);

        rb2d = GetComponent<Rigidbody2D>();
        playerSpeed = startSpeed;
    }

    void Update()
    {
        Dodge();
        Move();
    }

    private void Move()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            MovementAnimation();
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        movementInput.Normalize();
        rb2d.velocity = movementInput * playerSpeed;
    }

    private void MovementAnimation()
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

        if (counter % 120 == 0)
        {
            wobbleRight = !wobbleRight;
        }
    }

    private void Dodge()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                animator.Play("CartwheelingLeft");
            }
            else
            {
                animator.Play("CartwheelingRight");
            }

            isDodging = true;
            playerSpeed += dodgeSpeed;
        }

        if (isDodging)
        {
            dodgeTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump") || dodgeTime > 0.2f)
        {
            isDodging = false;
            dodgeTime = 0f;
            playerSpeed = startSpeed;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullseye"))
        {
            AddScore(10);
        }

        if (other.CompareTag("Bullseye") && isDodging)
        {
            AddScore(100);

            enemyScript.GrannyRageCounter();

            //dodgeText.SetActive(true);
            Invoke(nameof(TurnOffText), 3);
            Debug.Log("hej");
        }
    }
    private void TurnOffText()
    {
        //dodgeText.SetActive(false);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !isDodging && !isInvincible)
        {
            isInvincible = true;
            animator.SetTrigger("isInvincible");
            Invoke(nameof(SetInvincibleFalse), 1);

            var poof = Instantiate(slipperPoof, collision.transform.position, collision.transform.rotation);
            Destroy(poof, 0.5f);
            Destroy(collision.gameObject);

            health--;
            healthManager.UpdateHealth(health);

            if(health == 1)
            {
                animator.SetBool("lastLife", true);
            }
            if (health <= 0)
            {
                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    gameOverScript.GameOver();
                    sceneHandler.GameOver();
                }
            }
        }
    }

    private void SetInvincibleFalse()
    {
        isInvincible = false;
    }

    void AddScore(int points)
    {
        score += points;
        scoreManager.UpdateScore(score);
    }
}
