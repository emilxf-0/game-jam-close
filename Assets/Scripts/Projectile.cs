using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Sprite[] spriteArray;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject slipperPoof;

    private Rigidbody2D rb2d;
    private Transform target;

    private Vector2 aimDirection;
    public float projectileSpeed = 9f;
    public float maxSpread = 6f;
    private float rotateSpeed;


    void Start()
    {
        spriteRenderer.sprite = spriteArray[Random.Range(0, 3)];
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;

        rotateSpeed = Random.Range(300, 500);

        transform.up = target.transform.position - transform.position + new Vector3(Random.Range(-maxSpread, maxSpread), 0, 0);
        aimDirection = transform.up;
        rb2d.velocity = aimDirection * projectileSpeed; //move towards player

        Destroy(gameObject, 5);
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var poof = Instantiate(slipperPoof, transform.position, transform.rotation);
            Destroy(poof, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
