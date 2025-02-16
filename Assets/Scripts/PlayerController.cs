using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Component
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private BoxCollider2D boxCollider2d;
    private Weapon weapon;

    // Player Sound Effect
    [SerializeField] private AudioClip attackSound;
    private float soundVolume = 0.5f;

    // 초기 Player의 BoxCollider 정보
    private Vector2 initBoxColliderOffset; 
    private Vector2 initBoxColliderSize;

    // Jump 관련 값
    private Vector2 jumpBoxColliderOffset = new Vector2(-0.3637605f, -0.1619951f); 
    private Vector2 jumpBoxColliderSize = new Vector2(1.358502f, 2.217382f);
    private bool isGround;
    [SerializeField] private float jumpForce = 12f;

    // Slide 관련 값
    private bool isSliding;
    private float slideDuration = 0.8f; // 슬라이드 시간
    private float startSlidingTime; // 슬라이드를 시간한 시간
    private Vector2 slideBoxColliderOffset = new Vector2(-0.3614092f, -0.8012236f); 
    private Vector2 slideBoxColliderSize  = new Vector2(2.023346f, 1.233866f);
    
    // Game Over 
    [SerializeField] private Sprite gameOverSprite;

    private bool isReady = true;

    private void Start() {
        isGround = true;
        isSliding = false;
        boxCollider2d = GetComponent<BoxCollider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>(); // 하위 Object에서 Weapon을 찾기
        initBoxColliderOffset = boxCollider2d.offset;
        initBoxColliderSize = boxCollider2d.size;
    }

    void Update() {
        if (GameManager.instance.GetIsGameOver()) {
            return;
        }

        if (isReady && Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        } else if (isReady && Input.GetKeyDown(KeyCode.LeftShift)) {
            StartSlide();
        } else if (isReady && Input.GetMouseButtonDown(0)) { // 왼 클릭
            PlayAttackSound();
            StartAttack();
        }

        if (isSliding && Time.time - startSlidingTime >= slideDuration) { // 슬라이드 시간이 지났으면
            EndSlide();
        }

        animator.SetFloat("JumptoFall", rigidbody2d.velocity.y);
    }

    private void Jump() {
        isReady = false;
        isGround = false;
        animator.SetBool("IsGround", isGround);
        boxCollider2d.offset = jumpBoxColliderOffset;
        boxCollider2d.size = jumpBoxColliderSize;
        rigidbody2d.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void StartSlide() {
        isReady = false;
        isGround = false;
        isSliding = true;
        animator.SetBool("IsSliding", isSliding);
        boxCollider2d.offset = slideBoxColliderOffset;
        boxCollider2d.size = slideBoxColliderSize;
        startSlidingTime = Time.time;
    }

    private void EndSlide() {
        isGround = true;
        isSliding = false;
        animator.SetBool("IsSliding", isSliding);
        boxCollider2d.offset = initBoxColliderOffset;
        boxCollider2d.size = initBoxColliderSize;
        isReady = true;
    }

    private void StartAttack() {
        isReady = false;
        animator.SetTrigger("IsAttack");
    }

    public void EndAttack() {
        isReady = true;
    }

    private void GameOver() {
        animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = gameOverSprite;
        rigidbody2d.velocity = new Vector2(0, 0);
        rigidbody2d.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    public void EnableWeaponCollider() {
        if (weapon != null) {
            weapon.EnableWeaponCollider();
        }
    }

    public void DisableWeaponCollider() {
        if (weapon != null) {
            weapon.DisableWeaponCollider();
        }
    }

    private void PlayAttackSound() {
        SoundManager.instance.PlaySoundEffect(attackSound, soundVolume);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!isSliding && collision.collider.CompareTag("Ground")) {
            isGround = true;
            animator.SetBool("IsGround", isGround);
            boxCollider2d.offset = initBoxColliderOffset;
            boxCollider2d.size = initBoxColliderSize;
            isReady = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (GameManager.instance.GetIsGameOver()) {
            return;
        }

        if (collider.CompareTag("Obstacle")) {
            GameManager.instance.GameOver();
            GameOver();
        }
    }
}
