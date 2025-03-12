using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; //singleton instance
    public int score = 0;
    public TMP_Text scoreText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void AddScore()
    {
        score += 1;
        UpdateScoreText();
    }
    void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
