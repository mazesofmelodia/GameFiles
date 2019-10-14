using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;               //Movement speed
    [SerializeField] private Camera playerCam;                  //Player camera
	[SerializeField] private bool blockRotationPlayer = false;  //Incase we want to prevent the player from rotating
	[SerializeField] private float desiredRotationSpeed = 0.1f; //Speed of the rotating player
    [SerializeField] private float allowPlayerRotation = 0.1f;  //

    private Vector3 desiredMoveDirection;                       //The intended movement of the player
    private CharacterController controller;                     //Reference to character controller
    private Vector2 moveDirection;                              //Player move direction

    // Start is called before the first frame update
    void Start()
    {
        //Set the main camera as the main camera, I'll need to change this later
        playerCam = Camera.main;
        //Get the character controller component
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();
    }

    void InputMagnitude(){
        //Calculate the Input Magnitude based on the moveDirection Vector2
		float Speed = new Vector2(moveDirection.x, moveDirection.y).sqrMagnitude;

        //Check if the Speed is greater than the allowed movement
        if(Speed > allowPlayerRotation){
            //Move the player in that case
            Move();
        }
    }

    void Move(){
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
		desiredMoveDirection = forward * moveDirection.y + right * moveDirection.x;

        //If the player's rotation isn't false
		if (blockRotationPlayer == false) {
            //Turn the player in the desiredMoveDirection
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
		}

        //Move the controller in the desiredMoveDirection taking the moveSpeed and Time into account
        controller.Move(desiredMoveDirection * Time.deltaTime * moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context){
        //Get the vector2 of move value
        moveDirection = context.ReadValue<Vector2>();
    }
}
