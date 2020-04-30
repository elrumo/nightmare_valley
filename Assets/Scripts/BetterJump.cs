using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 2.45f;
    public float lowJumpMultiplier = 2f;

    public Rigidbody rb;
    public PlayerMovement PlayerMovement;

    void Awake()
    {
        
    }

    // Update is called once per frame
    public void Jumping()
    {
        if( rb.velocity.y < 0 && PlayerMovement.IsJumping)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            
        }
        
    }
}
