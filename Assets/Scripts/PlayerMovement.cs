using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody player;
    public Rigidbody boots;
    public GameObject playerObject;
    
    public Animator animator;
    
    private IEnumerator coroutine;

    public bool IsInAir = true;
    public bool CanInvert = false;
    public bool IsGravityInverted = false;
    public bool IsJumping = false;

    public string RespawnPoint = "respawnPoint_1";

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
    
    public bool IsWalking;


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

    void playFootsetps(bool muteSound){
        FindObjectOfType<AudioManager>().Play("FootSteps");
    }

    // Player Controller
    void movePlayer(float force, float x, float y, float z ) {
        transform.rotation = Quaternion.Euler(x, y, z);
        player.AddForce(force * Time.deltaTime, 0, 0);
    }

    void Jump(float force) {
        IsJumping = true;
        player.AddForce(0, force, 0);
        player.drag = midAirDrag;
    }

    void MakeJump(){
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

    void InvertGravity(){
        if(!IsInAir || IsJumping & IsInAir){
            if( !IsGravityInverted ){
                Physics.gravity = InvertedGravity;
                transform.rotation=Quaternion.Euler(0,180,180);
            } 
            if( IsGravityInverted ){
                Physics.gravity = -InvertedGravity;
                transform.rotation=Quaternion.Euler(0,0,0);
            }
            IsGravityInverted = !IsGravityInverted;
            IsJumping = false;
        }
    }

    void Start(){
        Physics.gravity = RegularGravity;
    }

    void FixedUpdate(){
        if ( Input.GetKeyDown("c") && !IsJumping) {
            MakeJump();
        }
    }

    void Update(){

        float horizontalMove = player.velocity.x;

        // Trigger animations
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("IsJumping", IsJumping);

        // Right
        if ( Input.GetKey(KeyCode.RightArrow)){
            if (IsGravityInverted){
                if(!IsInAir){
                    IsWalking = true;
                    movePlayer(movingForce, 0, 180, 180);
                } else{
                    IsWalking = false;
                    movePlayer((movingForce/30), 0, 180, 180);
                }
            }else{
                if(!IsInAir){
                    IsWalking = true;
                    movePlayer(movingForce, 0, 0, 0);
                } else {
                    IsWalking = false;
                    movePlayer((movingForce/30), 0, 0, 0);
                }
            }
        }else{
            IsWalking = false;
        }
        
        // Left
        if ( Input.GetKey(KeyCode.LeftArrow)) {
            if (IsGravityInverted){
                if(!IsInAir){
                    IsWalking = true;
                    movePlayer(-movingForce, 0, 0, 180);
                } else {
                    IsWalking = false;
                    movePlayer(-(movingForce/30), 0, 0, 180);
                }
            }
            else{
                if(!IsInAir){
                    IsWalking = true;
                    movePlayer(-movingForce, 0, 180, 0);
                }
                else{
                    IsWalking = false;
                    movePlayer(-(movingForce/30), 0, 180, 0);
                }
            }
        }

         // Better jump
        if(player.velocity.y < -3 && IsJumping) 
        {
            player.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
        }

        // Invert gravity
        if (Input.GetKeyDown("x") && CanInvert){   
            InvertGravity();
        }

    }
}
