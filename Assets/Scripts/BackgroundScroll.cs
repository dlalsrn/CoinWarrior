using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private MoveSpeedType type;
    private float width; // sprite의 가로 길이
    private float moveSpeed;
    private float offset; // 움직인 거리
    private float newPosition; // 현재 위치
    private Vector3 startPosition; // 초기 위치

    void Start() {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        width = renderer.sprite.bounds.size.x; // sprite render의 가로 사이즈
        startPosition = transform.position;
    }

    void Update() {
        if (GameManager.instance.GetIsGameOver()) {
            return;
        }
        
        moveSpeed = GameManager.instance.GetMoveSpeed(type); // 자신의 type에 맞는 MoveSpeed return
        offset += moveSpeed * Time.deltaTime; // 프레임마다 움직여야하는 값을 누적
        newPosition = Mathf.Repeat(offset, width); // 0 ~ width 사이의 값을 반복
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
