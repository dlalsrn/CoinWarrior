using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PolygonCollider2D collider2d;

    void Start() {
        collider2d = GetComponent<PolygonCollider2D>();
    }

    public void EnableWeaponCollider() {
        collider2d.enabled = true;
    }

    public void DisableWeaponCollider() {
        collider2d.enabled = false;
    }
}
