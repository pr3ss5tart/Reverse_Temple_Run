using UnityEngine;
using System.Collections;

public class SimpleCharacter : MonoBehaviour {

    public float forwardSpeed = 2f; // just for forward vector
    public float groundSpeed = 3; // for all vectors
	public float aerialSpeed = 2f; // movement speed when in air
    public float backDecrease = 0.5f; // amount speed will increase or decrease
    public Transform cameraTransform; // camera's transform because character moves relative to that
    public CharacterController myController; // the character controller on the player character
    public float jumpSpeed = 10f; // jump speed
    public float gravityStrength = 9.8f; // default gravity strength
    public float currentGravity = 9.8f; // current gravity strength
    float verticalVelocity; // used to make character jump
	bool canJump = false; // checks if player can jump
	Vector3 velocity; // no idea what this does but probably is used to project the speed on the player so that camera direction is the only thing that matters
	Vector3 groundedVelocity; // stores ground velocity, is useful when player jumps
	//Vector3 normal; keep it just in case
	//bool onWall = false; keep it just in case
    private float backSpeed; // variable for later


    // Update is called once per frame
    void Update()
    {
        Vector3 myVector = Vector3.zero;

        Vector3 input = Vector3.zero;
        backSpeed = Input.GetAxis("Vertical") * -backDecrease;
        input.x = Input.GetAxis("Horizontal");
        input.z = forwardSpeed - backSpeed;

        //get input from player
        if (myController.isGrounded)
        {
            myVector = input;
            Quaternion inputRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up), Vector3.up);
            myVector = inputRotation * myVector;
            myVector *= groundSpeed;
        }
        else
        {
            myVector = groundedVelocity;
            myVector += input * aerialSpeed;
        }

        myVector = myVector * Time.deltaTime;

        verticalVelocity = verticalVelocity - currentGravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            if (canJump)
                verticalVelocity += jumpSpeed;
        }

        myVector.y = verticalVelocity * Time.deltaTime;
        velocity = myVector / Time.deltaTime;

        //use input
        CollisionFlags flag = myController.Move(myVector); //flag contains whether or not the player is colliding with a surface

        if ((flag & CollisionFlags.Below) != 0)
        {
            groundedVelocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
            canJump = true;
            verticalVelocity = -2f;
            //onWall = false;
        }
        else
        {
            canJump = false;
            //onWall = false;
        }
    }
}