using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        // Recupera o score salvo
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        scoreText.text = " Final Score: " + finalScore.ToString();
    }
}