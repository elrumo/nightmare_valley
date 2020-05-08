using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public GameObject player;
    public DeathCount deathCount;
    public GameObject[] respawnPoints;

    public int respawnPointActive = 0;

    public void RunRespawnPlayer(){
        RespawnPlayer(respawnPoints[respawnPointActive].transform);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            print(other.tag);
            RunRespawnPlayer();
        }
    }
    
    public void RespawnPlayer(Transform respawnPoint){
            var playerObj = player.gameObject.GetComponent<PlayerMovement>();
            var playerRig = player.gameObject.GetComponent<Rigidbody>();

            playerObj.IsGravityInverted = false;
            
            player.transform.position = respawnPoint.transform.position;
            Physics.gravity = new Vector3(0, -30, 0);
            playerRig.velocity = Vector3.up * -20;
            player.transform.rotation = Quaternion.Euler(0,0,0);
            deathCount.AddDeath();
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
