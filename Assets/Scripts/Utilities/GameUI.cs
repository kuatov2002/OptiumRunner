using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    public void UpdateScoreText(float score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {Mathf.Floor(score)}";
        }
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}