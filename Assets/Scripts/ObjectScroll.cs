using UnityEngine;

public class ObjectScroll : MonoBehaviour
{
    [SerializeField] private MoveSpeedType type;
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
}
