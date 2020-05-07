using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void ChangeScene(int lvlNum){
        SceneManager.LoadScene(lvlNum);
    }

    void OnTriggerEnter(Collider other) {
     if (other.gameObject.tag == "Player"){
         ChangeScene(1);
     }      
    }
}
