using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            SceneManager.LoadScene(sceneName);
        }
    }
}
