using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCount : MonoBehaviour
{
    static int deathCountStatic;
    public int deathCount;

    public Text text;

    public void AddDeath(){
        deathCountStatic ++;
    }

    public void ResetDeath(){
        deathCountStatic = 0;
    }
    
    void Update() {
        deathCount = deathCountStatic;
        text.text = deathCountStatic.ToString();
    }
}
