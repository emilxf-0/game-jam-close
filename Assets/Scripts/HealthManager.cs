using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;
    public void UpdateHealth(int health)
    {
        switch (health)
        {
            case 5:
                heart3.GetComponent<Image>().sprite = halfHeart;
                break;
            case 4:
                heart3.GetComponent<Image>().sprite = emptyHeart;
                break;
            case 3:
                heart2.GetComponent<Image>().sprite = halfHeart;
                break;
            case 2:
                heart2.GetComponent<Image>().sprite = emptyHeart;
                break;
            case 1:
                heart1.GetComponent<Image>().sprite = halfHeart;
                break;
            case 0:
                heart1.GetComponent<Image>().sprite = emptyHeart;
                break;
        }
    }
}
