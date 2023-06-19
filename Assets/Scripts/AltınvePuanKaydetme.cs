using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AltınvePuanKaydetme : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coin;

    private int highScore;
    private int yourscore;
    private void Start()
    {

        LoadHighScore();
        UpdateHighScoreText();
    }

    public void UpdateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
            yourscore = newScore;
            SaveHighScore();
            UpdateHighScoreText();
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("YourScore", yourscore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        yourscore = PlayerPrefs.GetInt("YourScore");
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
        scoreText.text = "Score: " + yourscore.ToString();
    }
}

