using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveElement : MonoBehaviour
{
    
    public Rigidbody player;
    public Rigidbody triggerElement;
    public Rigidbody targetElement;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnCollisionEnter(Collision other){
        
        //Set targetElement to fall after triggerElement is activated
        if(other.gameObject.name == triggerElement.name){
            targetElement.constraints = ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezeRotationZ;
            // Play success sound
            FindObjectOfType<AudioManager>().Play("ImpactSound");
        }

        // Play hit soundFX when interactive element collides with ground layer
        if(other.gameObject.layer == 9){
            FindObjectOfType<AudioManager>().Play("ImpactSound");
        }
    }

    // Update is called once per frame
    void Update()
    {   
    }
}