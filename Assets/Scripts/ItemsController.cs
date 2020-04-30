using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{

    public Animator animator;
    public Rigidbody player;
    public Rigidbody item;

    public bool IsPlayerNear; 
    
    void OnTriggerStay(Collider other) {
        if(other.gameObject.name == player.name ){  // Items layer
            IsPlayerNear = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if( transform.position.x - player.transform.position.x > 1.6f || player.transform.position.x - transform.position.x < -1.6f ){
            IsPlayerNear = false;
            print("Hi");
        }
        
        animator.SetBool("IsNearItem", IsPlayerNear);
    }
}
