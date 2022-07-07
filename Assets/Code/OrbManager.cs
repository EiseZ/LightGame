using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tuple <T1, T2> {
    public readonly T1 Item1;
    public readonly T2 Item2;

    public Tuple(T1 item1, T2 item2) {
        Item1 = item1;
        Item2 = item2;
    }
}

public class OrbManager : MonoBehaviour
{
    [SerializeField]
    private OrbSlot orbSlot1;
    [SerializeField]
    private OrbSlot orbSlot2;

    [SerializeField]
    private string color1 = "none";
    [SerializeField]
    private string color2 = "none";

    Dictionary<Tuple<string, string>, string> abilityDictionary = new Dictionary<Tuple<string,string>, string>()
        {
            { new Tuple<string, string>("red", "none"), "ability" },
            { new Tuple<string, string>("none", "red"), "ability" },
            { new Tuple<string, string>("green", "none"), "ability" },
            { new Tuple<string, string>("none", "green"), "ability" },
            { new Tuple<string, string>("blue", "none"), "ability" },
            { new Tuple<string, string>("none", "blue"), "ability" },
            { new Tuple<string, string>("magenta", "none"), "ability" },
            { new Tuple<string, string>("none", "magenta"), "ability" },
            { new Tuple<string, string>("yellow", "none"), "ability" },
            { new Tuple<string, string>("none", "yellow"), "ability" },
            { new Tuple<string, string>("cyan", "none"), "ability" },
            { new Tuple<string, string>("none", "cyan"), "ability" },

            { new Tuple<string, string>("red", "green"), "ability1" },
            { new Tuple<string, string>("green", "red"), "ability1" },
            { new Tuple<string, string>("green", "blue"), "ability2" },
            { new Tuple<string, string>("blue", "green"), "ability2" },
            { new Tuple<string, string>("blue", "red"), "ability3" },
            { new Tuple<string, string>("red", "blue"), "ability3" },
        };

    public void UpdateColors() {
        color1 = orbSlot1.getColor();
        color2 = orbSlot2.getColor();

        Debug.Log(abilityDictionary[new Tuple<string, string>(color1, color2)]);
    }

    private bool redEnabled = false;
    private bool greenEnabled = false;
    private bool blueEnabled = false;
    private bool magentaEnabled = false;
    private bool yellowEnabled = false;
    private bool cyanEnabled = false;

    [SerializeField]
    private GameObject redOrb;
    [SerializeField]
    private GameObject greenOrb;
    [SerializeField]
    private GameObject blueOrb;
    [SerializeField]
    private GameObject magentaOrb;
    [SerializeField]
    private GameObject yellowOrb;
    [SerializeField]
    private GameObject cyanOrb;

    private OrbUI redOrbScript;
    private OrbUI greenOrbScript;
    private OrbUI blueOrbScript;
    private OrbUI magentaOrbScript;
    private OrbUI yellowOrbScript;
    private OrbUI cyanOrbScript;

    private void UpdateOrbs() {
        redOrbScript.orbEnabled = redEnabled;
        greenOrbScript.orbEnabled = greenEnabled;
        blueOrbScript.orbEnabled = blueEnabled;
        magentaOrbScript.orbEnabled = magentaEnabled;
        yellowOrbScript.orbEnabled = yellowEnabled;
        cyanOrbScript.orbEnabled = cyanEnabled;
    }

    private void Start() {
        redOrbScript = redOrb.GetComponent<OrbUI>();
        greenOrbScript = greenOrb.GetComponent<OrbUI>();
        blueOrbScript = blueOrb.GetComponent<OrbUI>();
        magentaOrbScript = magentaOrb.GetComponent<OrbUI>();
        yellowOrbScript = yellowOrb.GetComponent<OrbUI>();
        cyanOrbScript = cyanOrb.GetComponent<OrbUI>();

        redOrbScript.color = "red";
        greenOrbScript.color = "green";
        blueOrbScript.color = "blue";
        magentaOrbScript.color = "magenta";
        yellowOrbScript.color = "yellow";
        cyanOrbScript.color = "cyan";
    }
}
