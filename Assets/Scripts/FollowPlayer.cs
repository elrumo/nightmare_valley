
using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset;
    public float smoothTime = 0.07f;

    Vector3 velocity = Vector3.zero;

    public float boundX = 2.0f;
    public float maxBoundY = 1.5f;
    public float minBoundY = 1.5f;

    private Vector3 desiredPosition;

    private void Start() {
        
    }

    private void FixedUpdate()
    {
        Vector3 delta = Vector3.zero;

        // X Axis
        float dx = player.position.x - transform.position.x;
        if (dx > boundX || dx < -boundX){
            if(transform.position.x < player.position.x){
                delta.x = dx - boundX ;
            } else{
                delta.x = dx + boundX;
            }
        }

        // y Axis
        float dy = (player.position.y + cameraOffset.y) - transform.position.y;
        if (dy > maxBoundY || dy < minBoundY){
            if(transform.position.y < player.position.y){
                delta.y = dy - maxBoundY;
            } else{
                delta.y = dy + maxBoundY;
            }
        }

        // Move camera
        desiredPosition = transform.position + (Vector3.zero +  delta);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
    }
   

}