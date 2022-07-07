using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private Vector2 distance;
    [SerializeField]
    private float speed;

    private Vector3 startingPos;
    private Vector3 endingPos;
    private bool goingForeward = true;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        endingPos = new Vector3(startingPos.x + distance.x, startingPos.y + distance.y, startingPos.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        if (goingForeward) {
            transform.position = Vector3.MoveTowards(transform.position, endingPos, step);
            if (transform.position == endingPos) {
                goingForeward = false;
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, step);
            if (transform.position == startingPos) {
                goingForeward = true;
            }
        }
    }

    // Make player move with platform
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") {
            col.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player") {
            col.transform.parent = null;
        }
    }
}
