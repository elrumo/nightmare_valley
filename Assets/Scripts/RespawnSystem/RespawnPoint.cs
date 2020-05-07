using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public GameObject player;
    public RespawnSystem respawnSystem;

    public bool isActive;
    
    void OnTriggerEnter(Collider other){
        // if(other.name == player.name){
        if(other.name == player.name){
            if(!isActive){
                isActive = true;
                respawnSystem.respawnPointActive = int.Parse(name);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
