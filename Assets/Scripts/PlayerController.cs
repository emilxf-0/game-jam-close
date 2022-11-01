using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float startSpeed = 2;
    public float dodgeTime;
    public float dodgeSpeed = 10;

    private bool isDodging;
    private bool wobbleRight;
    
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
            //transform.Translate(0, Mathf.Sin(-5) * 0.5f, 0);
            transform.eulerAngles = new Vector3(0, 0, -7 * 0.5f);
        }
        else
        {
            //transform.Translate(0, Mathf.Sin(5) * 0.5f, 0);
            transform.eulerAngles = new Vector3(0, 0, 7 * 0.5f);
        }

        wobbleRight = !wobbleRight;

    }

    private void Dodge()
    {
        
        // if (Input.GetButtonDown("Jump") && movementInput.x > 0)
        // {
        //     transform.Translate(dodgeSpeed, 0, 0);
        // }
        //
        // if (Input.GetButtonDown("Jump") && movementInput.x < 0)
        // {
        //     transform.Translate(-dodgeSpeed, 0, 0);
        // }
        //
        // if (Input.GetButtonDown("Jump") && movementInput.y > 0)
        // {
        //     transform.Translate(0, dodgeSpeed, 0);
        // }
        //
        // if (Input.GetButtonDown("Jump") && movementInput.y < 0)
        // {
        //     transform.Translate(0, -dodgeSpeed, 0);
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
        }
        
        if (Input.GetButtonUp("Jump") || dodgeTime > 0.2f)
        {
            isDodging = false;
            dodgeTime = 0;
            playerSpeed = startSpeed;
        }
    }
    
    
}
