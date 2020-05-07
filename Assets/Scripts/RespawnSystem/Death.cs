using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public RespawnSystem respawnSystem;
    public GameObject player;

    void OnTriggerEnter(Collider other){
            if(other.tag == "Player"){
                respawnSystem.RunRespawnPlayer();
            }
            // Debug.Log("Other Collider:" + other.name);
        }    

    void Update(){
    }


}
