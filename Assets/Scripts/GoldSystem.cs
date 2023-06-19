using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldSystem : MonoBehaviour
{
    public int PlayerCoin;
    public TextMeshProUGUI Metin;

    public float detectionRadius = 1f; // Algýlama yarýçapý

    private void Update()
    {
        CheckCollision();
    }

    private void CheckCollision()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(transform.position, coin.transform.position);

            if (distance <= detectionRadius)
            {
                PlayerCoin++;
                Destroy(coin);
                Metin.text = PlayerCoin.ToString();
            }
        }
    }
}