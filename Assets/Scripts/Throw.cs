using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator animator;

    private float throwTimer;
    private float throwRate = 2f;

    private float gameTimer;
    private float speedUpThrowRate = 4f;


    void Update()
    {
        throwTimer += Time.deltaTime;
        gameTimer += Time.deltaTime;

        if (throwTimer >= throwRate)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            animator.SetTrigger("isThrowing");
            throwTimer = 0;
        }
        if (gameTimer >= speedUpThrowRate && throwRate >= 0.3f)
        {
            throwRate -= 0.05f;
            gameTimer = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }
}
