using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Sprite[] spriteArray;
    private Rigidbody2D rb2d;
    private Transform target;

    private Vector2 aimDirection;
    public float projectileSpeed = 9f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;

        transform.up = target.transform.position - transform.position;
        aimDirection = transform.up;
        rb2d.velocity = aimDirection * projectileSpeed; //move towards player

        Destroy(gameObject, 5);
    }
    private void Update()
    {
        //rb2d.velocity += new Vector2(target.transform.position.x, rb2d.velocity.y) * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
