using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void UpdateScoreText(int score) {
        scoreText.text = $"{score}";
    }

    public void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }
}
