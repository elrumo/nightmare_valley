using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCount : MonoBehaviour
{
    public int deathCount;
    public Text text;

    void Update() {
        text.text = deathCount.ToString();
    }
}
