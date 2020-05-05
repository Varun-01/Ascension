/*
 Player_Movement: Script enabling character control.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityChan
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]

	public class Player_Movement : MonoBehaviour
	{

		public float animSpeed = 1.5f;				 
		public float lookSmoother = 3.0f;			      
		public bool useCurves = true;				 
		public float useCurvesHeight = 0.5f;


        public float walkingSpeed = 3.0f;
        public float runningSpeed = 5.0f;
        public float playerDirection = 0f;
        public bool movement = false;
        public float jumpPower = 5.0f; 
		private CapsuleCollider col;
		private Rigidbody rb;
		private float orgColHight;
		private Vector3 orgVectColCenter;
		private Animator anim;							 
		private AnimatorStateInfo currentBaseState;

        public float facingLeft = 95f;
        public float facingRight = 265f;

        //double tap to run variables.
        public float tapDelay = 0.2f; //how much delay you can have to activate double tap to run
        public int buttonPresses = 0; //how many times the player pressed the button within the allowed timeframe
        public float Run = 0f; //acts as boolean - 0 is walk and 1 is run. Used for blending tree in animator.
        public float movementDelay = 0f; //used for smoothing forward and backward animation so that idle animation doesn't activate during "uninterrupted" movement.
        
        public Move moveRequest;

        private GameObject cameraObject;	 
		
		static int idleState = Animator.StringToHash ("Base Layer.Idle");
		static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash ("Base Layer.Jump");
		static int restState = Animator.StringToHash ("Base Layer.Rest");

		void Start ()
		{
			anim = GetComponent<Animator> ();
			col = GetComponent<CapsuleCollider> ();
			rb = GetComponent<Rigidbody> ();
			cameraObject = GameObject.FindWithTag ("MainCamera");
			orgColHight = col.height;
			orgVectColCenter = col.center;
            Run = 0f;
            //Network
            moveRequest = gameObject.GetComponent<Move>();
		}

        /*
         TODO: Change Update to apply for different facing directions.
               SO FAR ONLY P1 CAN DO DOUBLE TAP.
             */

        void Update() {

            /*
             double tap to run:
                if button is pressed take note of it, and if it is pressed twice then activate run.

            KEEP IN UPDATE - IT DOESN'T WORK IN FIXED UPDATE BECAUSE UPDATE AND FIXEDUPDATE RUN AT DIFFERENT INTERVALS
             */

            float playerDirection = rb.transform.localEulerAngles.y;

            if (playerDirection == facingRight)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    buttonPresses += 1;
                    if (buttonPresses > 1)
                    {
                        Run = 1.0f;
                        Debug.Log("Double tap");
                    }
                }
            }
            else {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    buttonPresses += 1;
                    if (buttonPresses > 1)
                    {
                        Run = 1.0f;
                        Debug.Log("Double tap");
                    }
                }

            }
        }
	
	
		void FixedUpdate ()
		{
            /*     direction == 0 : no buttons pressed. 
                   direction > 0.1 : pressed button to go right. 
                   direction < -0.1 : pressed button to go left
            */

            float movementDirection = Input.GetAxisRaw("Vertical");
            float playerDirection = rb.transform.localEulerAngles.y;
 
            //Debug.Log(playerDirection);

            anim.SetFloat ("Speed", movementDirection);							 
            anim.SetBool("Movement", movement);
            anim.SetFloat("Run", Run);
            anim.SetFloat("Direction", playerDirection);

            anim.speed = animSpeed;								  
			currentBaseState = anim.GetCurrentAnimatorStateInfo (0);	   
			rb.useGravity = true;

            //double tap to run: if key up for D it'll set the button presses to 0 after the preset delay.
            if (playerDirection == facingRight)
            {
                if (Input.GetKeyUp(KeyCode.D))
                {   
                    Invoke("SetButtonPressesToZero", tapDelay);
                    Run = 0f;
                    //Network
                    moveRequest.sendMoveRequest("D");
                }
            }
            else {
                if (Input.GetKeyUp(KeyCode.A))
                {
                    Invoke("SetButtonPressesToZero", tapDelay);
                    Run = 0f;
                    //Network
                    moveRequest.sendMoveRequest("A");
                }
            }
      
            //depending on movement direction and player direction, different force applied.
            if (movementDirection > 0.1) {
                if (playerDirection == facingRight){
                        moveRight();
                } else {
                        moveLeft();
                }
            } else if (movementDirection < -0.1){
                if (playerDirection == facingRight)
                {
                    moveLeft();
                }
                else {
                    moveRight();
                }
            }
            
            //for animations: if a movement key isnt pressed in .02 seconds then the player is idle and movement animations are stopped
            //character would return to idle state animation if a key wasn't pressed which made the animation from forward to backward clunky.

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                movementDelay = 0f;
            }
            else
            {
                movementDelay += .01f;
                if (movementDelay > .02f)
                {
                    movement = false;
                }
            }

            if (Input.GetButtonDown ("Jump")) {	 
				if (currentBaseState.nameHash != jumpState) {
					if (!anim.IsInTransition (0)) {
						anim.SetBool ("Jump", true);
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                    }
                }
			}

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
            {
                anim.SetBool("Movement", false);
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                anim.SetBool("Movement", false);
                rb.velocity = new Vector3(0, 0, 0);
            }

            if (currentBaseState.nameHash == locoState) {
				if (useCurves) {
					resetCollider ();
				}
			}
		else if (currentBaseState.nameHash == jumpState) {
                anim.SetBool("Movement", false);
                
				if (!anim.IsInTransition (0)) {		
					if (useCurves) {
						float jumpHeight = anim.GetFloat ("JumpHeight");
						float gravityControl = anim.GetFloat ("GravityControl"); 
						//if (gravityControl > 0)
							//rb.useGravity = false;	
										
						Ray ray = new Ray (transform.position + Vector3.up, -Vector3.up);
						RaycastHit hitInfo = new RaycastHit ();
						if (Physics.Raycast (ray, out hitInfo)) {
							if (hitInfo.distance > useCurvesHeight) {
								col.height = orgColHight - jumpHeight;			 
								float adjCenterY = orgVectColCenter.y + jumpHeight;
								col.center = new Vector3 (0, adjCenterY, 0);	 
							} else {
								resetCollider ();
							}
						}
					}
					anim.SetBool ("Jump", false);
				}
			}
		else if (currentBaseState.nameHash == idleState) {
				if (useCurves) {
					resetCollider ();
				}
				if (Input.GetButtonDown ("Fire1")) {
					anim.SetBool ("Rest", true);
				}
			}
		else if (currentBaseState.nameHash == restState) {
				if (!anim.IsInTransition (0)) {
					anim.SetBool ("Rest", false);
				}
			}
		}

		void resetCollider ()
		{
			col.height = orgColHight;
			col.center = orgVectColCenter;
		}

        void SetButtonPressesToZero ()
        {
            buttonPresses = 0;
        }

        void moveRight()
        {
            if (Run > 0)
            {
                rb.velocity = transform.forward * runningSpeed;
            }
            else
            {
                rb.velocity = transform.forward * walkingSpeed;
            }

            movement = true;
        }

        void moveLeft() {
            rb.velocity = -(transform.forward * walkingSpeed);
            movement = true;
        }

    }
}