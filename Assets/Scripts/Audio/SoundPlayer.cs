using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    
    public CollidingSound[] otherCollider;

    private void Start() {
        foreach (CollidingSound collider in otherCollider){
            collider.name = collider.otherCollider.name;
        }
    }

    private void OnCollisionEnter(Collision other) {

        foreach (CollidingSound collider in otherCollider){
            if (other.gameObject.name == collider.name){
                FindObjectOfType<AudioManager>().Play(collider.soundToPlay);
            }
        }

    }
}
