using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject player;
    // public Transform player;
    public Rigidbody playerRig;

    public Transform respawnPoint;

    void OnTriggerEnter(Collider other){
        if(other.name == player.name){
            player.transform.position = respawnPoint.transform.position;
            Physics.gravity = new Vector3(0, -30, 0);
            playerRig.velocity = Vector3.up * -20;
            player.transform.rotation = Quaternion.Euler(0,0,0);
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
