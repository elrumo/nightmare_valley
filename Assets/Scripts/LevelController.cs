using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public DeathCount deathCount;
    public int levelNumber = 0;

    public void ChangeScene(int lvlNum){
        SceneManager.LoadScene(lvlNum);
        deathCount.ResetDeath();
    }

    void OnTriggerEnter(Collider other) {
     if (other.gameObject.tag == "Player"){
         ChangeScene(levelNumber);
     }      
    }
}
