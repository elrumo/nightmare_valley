using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMessage : MonoBehaviour
{   
    public DeathCount deathCount;
    public Text text;

    // Update is called once per frame
    void Update(){
        text.text = "Well done, you only died " + deathCount.deathCount.ToString() + " times.";
    }
}
