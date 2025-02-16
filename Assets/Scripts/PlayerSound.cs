using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip parrySound;

    public void PlayAttackSound() {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayParrySound() {
        audioSource.PlayOneShot(parrySound);
    }

    public void PlayCoinSound() {
        audioSource.PlayOneShot(coinSound);
    }
}
