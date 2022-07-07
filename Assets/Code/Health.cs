using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hitpoints;

    public void hit(int damage) {
        hitpoints -= damage;
        if (hitpoints <= 0) {
            hitpoints = 0;
            Destroy(gameObject);
        }
    }
}
