using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Values")]
    [SerializeField] private float fallSpeed = 0.981f;          //How fast the player falls

    [Header("Camera Controls")]
    [SerializeField] private Camera playerCam;                  //Player camera

    [Header("Player Rotation Properties")]
	[SerializeField] private bool blockRotationPlayer = false;  //Incase we want to prevent the player from rotating
	[SerializeField] private float desiredRotationSpeed = 0.1f; //Speed of the rotating player
    [SerializeField] private float allowPlayerRotation = 0.1f;  //

    private float inputX;
    private float inputZ;
    private float gravity;
    private Vector3 desiredMoveDirection;                       //The intended movement of the player
    private CharacterController controller;                     //Reference to character controller
    private Player player;
    //private Animator anim;                                      //Reference to Animator component

    // Start is called before the first frame update
    void Start()
    {
        //Get the player component
        player = GetComponent<Player>();

        GetCameraRef();

        //Get the character controller component
        controller = GetComponent<CharacterController>();
        //Get the animator component
        //anim = GetComponent<Animator>();
    }

    public void GetCameraRef()
    {
        //Set the main camera as the main camera
        playerCam = Camera.main;
    }

    public void SetPosition(Transform newPosition)
    {
        //Disable the character controller
        controller.enabled = false;

        //Change the character position
        this.gameObject.transform.position = newPosition.position;

        //Enable the character controller
        controller.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the direction the character is moving to
        InputMagnitude();

        if (!controller.isGrounded)
        {
            gravity -= fallSpeed * Time.deltaTime;

            controller.Move(new Vector3(0, gravity, 0));
        }
        else
        {
            gravity = 0;
        }
    }

    void InputMagnitude(){
        //Check if the player is not in the active state
        if(player.playerState != PlayerState.Active)
        {
            return;
        }

        inputX = Input.GetAxis("MoveHorizontal");
        inputZ = Input.GetAxis("MoveVertical");

        //Calculate the Input Magnitude based on the moveDirection Vector2
		float Speed = new Vector2(inputX, inputZ).sqrMagnitude;

        //Check if the Speed is greater than the allowed movement
        if(Speed > allowPlayerRotation){
            //Move the player in that case
            Move(inputX,inputZ);
            //Play the running animation
            player.anim.SetBool("IsMoving", true);
        }else{
            //Play the idle animation
            player.anim.SetBool("IsMoving", false);
        }
    }

    void Move(float horizontal, float vertical){
        //Get the forward and right values of the camera
		var forward = playerCam.transform.forward;
		var right = playerCam.transform.right;

        //Set the y values to 0
		forward.y = 0f;
		right.y = 0f;

        /*Normalize the 2 values, this makes sure that the character won't move
		Faster when moving Diagonally*/
        forward.Normalize ();
		right.Normalize ();

        //Move the player based on the player input relative to the camera direction
		desiredMoveDirection = forward * vertical + right * horizontal;

        //If the player's rotation isn't false
		if (blockRotationPlayer == false) {
            //Turn the player in the desiredMoveDirection
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
		}

        //Move the controller in the desiredMoveDirection taking the moveSpeed and Time into account
        controller.Move(desiredMoveDirection * Time.deltaTime * player.speed.Value);
    }
}
