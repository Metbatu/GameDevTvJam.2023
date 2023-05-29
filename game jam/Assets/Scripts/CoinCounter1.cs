using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinCounter1 : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    private void Start()
    {
        score = 16;
        scoreText.SetText(score.ToString());
    }

    private void OnTriggerEnter2D(Collider2D coin)
    {
        if (coin.CompareTag("Collectible"))
        {
            score += 1;
            scoreText.SetText(score.ToString());
            CheckSceneChange();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallTrigger"))
        {
            score -= 5;
            scoreText.SetText(score.ToString());
            CheckSceneChange();
        }
    }

    private void CheckSceneChange()
    {
        if (score >= 15 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2); // Load Scene 2 (Rich World)
        }
        else if (score < 15 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(1); // Load Scene 1 (Poor World)
        }
        else if (score >= 30)
        {
            SceneManager.LoadScene(3); // Load Scene 3 (Game Over)
        }
    }
}