using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MoveSpeedType {
    Background1 = 0,
    Background2 = 1,
    Background3 = 2,
    Background4 = 3,
    Object = 3, // Coin, SpikePlate, Arrow
    Background5 = 4,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private float[] moveSpeed = {2f, 4f, 6f, 8f, 10f};
    private int score; // 먹은 coin 개수
    private bool isGameOver = false;

    private Coroutine coroutine;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void AddScore() {
        score++;
        HUDManager.instance.UpdateScoreText(score);
        if (score % 20 == 0) {
            SpeedUp();
        }
    }

    public void GameOver() {
        isGameOver = true;
        ObjectSpawner objectSpawner = FindObjectOfType<ObjectSpawner>();
        if (objectSpawner != null) {
            objectSpawner.StopObjectSpawn();
        }
        StartCoroutine(ShowGameOverPanel());
    }

    public float GetMoveSpeed(MoveSpeedType type) { // 자기 type에 맞게 moveSpeed 설정
        return moveSpeed[(int)type];
    }

    private void SpeedUp() {
        for (int i = 0; i < moveSpeed.Length; i++) {
            moveSpeed[i] += 1f;
        }
        ObjectSpawner objectSpawner = FindObjectOfType<ObjectSpawner>();
        if (objectSpawner != null) {
            objectSpawner.DecreaseInterval();
        }
    }

    public bool GetIsGameOver() {
        return isGameOver;
    }

    IEnumerator ShowGameOverPanel() {
        yield return new WaitForSeconds(1.5f);
        HUDManager.instance.ShowGameOverPanel();
    }

    public void LoadToGameScene() {
        SceneManager.LoadScene("Scenes/GameScene");
    }

    public void LoadToMenuScene() {
        SceneManager.LoadScene("Scenes/MenuScene");
    }
}
