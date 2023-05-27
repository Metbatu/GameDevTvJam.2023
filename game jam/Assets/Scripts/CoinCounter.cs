using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    void Start()
    {
        score = 0;
        scoreText.SetText(score.ToString());
    }

    private void OnTriggerEnter2D(Collider2D Coin)
    {
        if (Coin.tag == "Collectible")
        {
            score += 1;
            scoreText.SetText(score.ToString());
        }
    }
}
