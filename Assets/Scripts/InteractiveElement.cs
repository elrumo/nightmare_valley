using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElement : MonoBehaviour
{
    
    public Rigidbody triggerElement;
    public Rigidbody player;
    public Rigidbody targetElement;

    // Start is called before the first frame update
    void Start()
    {
        targetElement.constraints = RigidbodyConstraints.FreezePositionY;
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.name == triggerElement.name){
            targetElement.constraints = ~RigidbodyConstraints.FreezePositionY;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown("a")){
            targetElement.constraints = ~RigidbodyConstraints.FreezePositionY;
        }
    }
}