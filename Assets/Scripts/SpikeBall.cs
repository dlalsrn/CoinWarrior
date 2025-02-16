using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float bounceForce = 10f;
    private float soundVolume = 0.5f;
    [SerializeField] private AudioClip parrySound;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Weapon")) {
            SoundManager.instance.PlaySoundEffect(parrySound, soundVolume);
            rigidbody2d.velocity = Vector2.zero; // 기존 속도 초기화
            Vector3 bounceDir = transform.position - collider.transform.position; // 상대 벡터
            bounceDir.Normalize();
            rigidbody2d.AddForce(bounceDir * bounceForce, ForceMode2D.Impulse);
        } else if (collider.CompareTag("Ground")) {
            Destroy(gameObject);
        }
    }
}
