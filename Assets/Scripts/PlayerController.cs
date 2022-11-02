using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float startSpeed = 2;
    public float dodgeTime;
    public float dodgeSpeed = 10;
    public float dodgeLength = 3;
    public float angle = 7;

    private bool isDodging;
    private bool justTeleported;
    private bool wobbleRight;
    private float counter;

    private Vector3 bob;
    
    private Rigidbody2D rb2d;
    private Vector2 movementInput;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSpeed = startSpeed;
    }

    // Update is called once per frame
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
            // Vector3 mov = new Vector3 (transform.position.x, Mathf.Sin(1 * Time.time) * 1, transform.position.z);
            // transform.position = mov;
            // bob = new Vector3(transform.position.x, Mathf.Sin(-25) * 1f, transform.position.z);
            // transform.position = bob;
            //transform.Translate(0, Mathf.Sin(Time.deltaTime), -7);
            transform.eulerAngles = new Vector3(0, 0, -7 * 0.5f);
            counter++;
        }
        else
        {
            // bob = new Vector3(transform.position.x, Mathf.Sin(25) * 1f, transform.position.z);
            // transform.position = bob;

            //transform.Translate(0, Mathf.Sin(Time.deltaTime), 7);
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
        
        // if (Input.GetButtonDown("Jump") && movementInput.x > 0)
        // {
        //     transform.Translate(dodgeLength, 1, 0);
        //     justTeleported = true;
        // }
        //
        // if (Input.GetButtonDown("Jump") && movementInput.x < 0)
        // {
        //     transform.Translate(-dodgeLength, 1, 0);
        //     justTeleported = true;
        // }
        //
        // if (Input.GetButtonDown("Jump") && movementInput.y > 0)
        // {
        //     transform.Translate(0, dodgeLength, 0);
        //     justTeleported = true;
        // }
        //
        // if (Input.GetButtonDown("Jump") && movementInput.y < 0)
        // {
        //     transform.Translate(0, -dodgeLength, 0);
        //     justTeleported = true;
        // }
        //
        // if (justTeleported)
        // {
        //     rb2d.gravityScale = 30;
        //     dodgeTime += Time.deltaTime;
        // }
        //
        // if (dodgeTime > 0.2f)
        // {
        //     dodgeTime = 0;
        //     justTeleported = false;
        //     rb2d.gravityScale = 0;
        // }

        // TODO check out lerp or movetowards for this instead of messing with speed

        if (Input.GetButtonDown("Jump"))
        {
            isDodging = true;
            playerSpeed += dodgeSpeed;
        }
        
        if (isDodging)
        {
            dodgeTime += Time.deltaTime;
            //rb2d.position = Vector2.Lerp(rb2d.position, new Vector2(Input.GetAxis("Horizontal") * dodgeLength, 0), dodgeSpeed);
        }
        
        if (Input.GetButtonUp("Jump") || dodgeTime > 0.2f)
        {
            isDodging = false;
            dodgeTime = 0f;
            playerSpeed = startSpeed;
        }
    }
    
    
}
