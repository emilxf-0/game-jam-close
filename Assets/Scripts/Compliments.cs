using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Compliments : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] complimentArray;

    private void Start()
    {
        spriteRenderer.sprite = complimentArray[Random.Range(0, 3)];
    }
}
