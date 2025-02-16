using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private MoveSpeedType type;
    [SerializeField] private AudioClip coinSound;
    private float soundVolume = 0.3f;
    private float destroyPosX = -10f; // 특정 X 값을 넘어가면 Object 삭제

    void Update() {
        if (GameManager.instance.GetIsGameOver()) {
            return;
        }
        
        transform.position += Vector3.left * GameManager.instance.GetMoveSpeed(type) * Time.deltaTime;

        if (transform.position.x <= destroyPosX) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (GameManager.instance.GetIsGameOver()) {
            return;
        }

        if (collider.CompareTag("Player")) {
            SoundManager.instance.PlaySoundEffect(coinSound, soundVolume);
            GameManager.instance.AddScore();
            Destroy(gameObject);
        }
    }
}
