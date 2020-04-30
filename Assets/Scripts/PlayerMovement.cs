using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody player;
    public Rigidbody boots;
    public GameObject playerObject;
    public FollowPlayer cameraObj;
    
    public Animator animator;
    
    private IEnumerator coroutine;

    public bool IsInAir = true;
    public bool CanInvert = false;
    public bool IsGravityInverted = false;
    public bool IsJumping = false;

    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 50f;

    public Vector3 RegularGravity;
    public Vector3 InvertedGravity;

    public dynamic collidingElement;

    public float movingForce = 200f;
    public float jumpingForce = 5000f;
    public float midAirDrag = 1f;
    public float groundDrag = 12f;
    public float jumpingVel = 12f;

    public float smoothTime = 1.5f;

    void Jump(float force) {
        IsJumping = true;
        player.AddForce(0, force, 0);
        player.drag = midAirDrag;
    }


    // Collision
    void OnCollisionStay(Collision other) {
        if(other.gameObject.layer == 9) { //Ground layer
            if(other.gameObject.tag  == "GroundInverted" && IsGravityInverted) {
                Physics.gravity = -RegularGravity;
            } else if (other.gameObject.tag  == "GroundRegular" && !IsGravityInverted) {
                Physics.gravity = RegularGravity;
            }

            IsInAir = false;
            player.drag = groundDrag;
        }
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == 9) { //Ground layer
            IsJumping = false;
        }
    }

    void OnCollisionExit(Collision other) {
        if(other.gameObject.layer == 9) { //Ground layer
            IsInAir = true;
            player.drag = midAirDrag;
        }
    }

    // Tiggers
    void OnTriggerStay(Collider other) {
        if(Input.GetKeyDown("z") && other.gameObject.layer == 11 ){  // Items layer
            coroutine = PikcupItem(other.gameObject);
            StartCoroutine(coroutine);
            if(other.gameObject.name == "Boots"){
                animator.SetBool("IsBootsOn", true);
            }
        }
    }

    private IEnumerator PikcupItem(GameObject item){
        animator.SetTrigger("Pickup");
        yield return new WaitForSeconds(0.5f);
        Destroy(item);
        CanInvert = true;

    }


    // Player Controller
    void movePlayer(float force, float x, float y, float z ) {
        transform.rotation = Quaternion.Euler(x, y, z);
        player.AddForce(force * Time.deltaTime, 0, 0);
    }

    void Start(){
        Physics.gravity = RegularGravity;
    }

    void FixedUpdate(){
        // Right
        if ( Input.GetKey(KeyCode.RightArrow)){
            if (IsGravityInverted){
                if(!IsInAir){
                    movePlayer(movingForce, 0, 180, 180);
                } else{
                    movePlayer(movingForce/30, 0, 180, 180);
                }
            }else{
                if(!IsInAir){
                    movePlayer(movingForce, 0, 0, 0);
                } else {
                    movePlayer((movingForce/30), 0, 0, 0);
                }
            }
        }
        
        // Left
        if ( Input.GetKey(KeyCode.LeftArrow)) {
            if (IsGravityInverted){
                if(!IsInAir){
                    movePlayer(-movingForce, 0, 0, 180);
                } else {
                    movePlayer(-(movingForce/30), 0, 0, 180);
                }
            }
            else
            {
                if(!IsInAir)
                {
                    movePlayer(-movingForce, 0, 180, 0);
                }
                else
                {
                    movePlayer(-(movingForce/30), 0, 180, 0);
                }
            }
        }
        
        if(IsGravityInverted){
            // cameraObj.cameraOffset.y = Mathf.SmoothStep(cameraObj.cameraOffset.y, -5, 0f);
            // print("Hi");
        } else{
            // cameraObj.cameraOffset.y = Mathf.SmoothStep(cameraObj.cameraOffset.y, 8, 0f);
        }

    }
    void Update()
    {
        float horizontalMove = player.velocity.x;

        // Trigger animations
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("IsJumping", IsJumping);

        if(CanInvert){

        }


         // Better jump
        if(player.velocity.y < -3 && IsJumping) 
        {
            player.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
        }

        // Jump
        if ( Input.GetKeyDown("c") && !IsJumping) {
            if (!IsGravityInverted){
                
                Jump(jumpingForce);

                // Add some vertical motion to player when jumping
                player.velocity += Vector3.up * 2;

                // Add some horizontal motion to player when jumping
                if(player.velocity.x > 5){
                    player.velocity += Vector3.right * jumpingVel;
                }
                if(player.velocity.x < -5){
                    player.velocity += Vector3.left * jumpingVel;
                }
            } else {
                Jump(-jumpingForce);
            }
        }

        // Invert gravity
        if (Input.GetKeyDown("x") && CanInvert)
        {   
            if(!IsInAir || IsJumping & IsInAir){
                player.drag = groundDrag - 6;

                if( !IsGravityInverted )
                {
                    Physics.gravity = InvertedGravity;
                    transform.rotation=Quaternion.Euler(0,180,180);
                    // IsGravityInverted = true;
                } 
                if( IsGravityInverted )
                {
                    Physics.gravity = -InvertedGravity;
                    transform.rotation=Quaternion.Euler(0,0,0);
                    // IsGravityInverted = false;
                }
                IsGravityInverted = !IsGravityInverted;
                IsJumping = false;
            }
            
        }

        // Pikcup items
        if (Input.GetKeyDown("z") && CanInvert)
        {   
            
        }

    }
}
