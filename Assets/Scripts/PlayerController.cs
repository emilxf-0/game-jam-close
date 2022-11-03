using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] private SceneHandler sceneHandler;

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

    private Vector3 bob;

    private Rigidbody2D rb2d;
    private CapsuleCollider2D capsuleCollider;
    public Animator animator;
    private Vector2 movementInput;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream
        //  animator = GetComponent<Animator>();
=======
        capsuleCollider = GetComponent<CapsuleCollider2D>();
      //  animator = GetComponent<Animator>();
>>>>>>> Stashed changes
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
                animator.SetTrigger("isDodgingLeft");
            }
            else
            {
                animator.SetTrigger("isDodgingRight");
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
            capsuleCollider.enabled = true;
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
            capsuleCollider.enabled = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !isDodging)
        {
            Destroy(collision.gameObject);
            
            health--;
            healthManager.UpdateHealth(health);

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

    void AddScore(int points)
    {
        score += points;
        scoreManager.UpdateScore(score);
    }
}
