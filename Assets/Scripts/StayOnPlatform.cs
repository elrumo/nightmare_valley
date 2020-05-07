using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlatform : MonoBehaviour{


    private void OnTriggerEnter(Collider other) {
        other.gameObject.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other) {
        other.gameObject.transform.parent = null;
    }

}
