using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private string dealDamageToTag;

    private List<Collider2D> damageCollisions = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == dealDamageToTag) {
            damageCollisions.Add(col);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == dealDamageToTag) {
            damageCollisions.Remove(col);
        }
    }

    public void hitCollisions() {
        for (int i = 0; i < damageCollisions.Count; i++) {
                damageCollisions[i].GetComponent<Health>().hit(damage);
        }
    }
}
