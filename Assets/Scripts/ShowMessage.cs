using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{

    public GameObject messages;
    
    // Start is called before the first frame update
    void Start()
    {
        messages.SetActive(false);
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            messages.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            messages.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
