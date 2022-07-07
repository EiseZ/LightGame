using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSlot : MonoBehaviour
{
    [SerializeField]
    private OrbManager orbManager;

    private string orbColor = "none";

    public void changeColor(string color) {
        orbColor = color;
        orbManager.UpdateColors();
    }

    public string getColor() {
        return orbColor;
    }
}
