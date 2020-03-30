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
        public float direction = 0f;
        public bool movement = false;
        public float jumpPower = 3.0f; 
		private CapsuleCollider col;
		private Rigidbody rb;
		private float orgColHight;
		private Vector3 orgVectColCenter;
		private Animator anim;							 
		private AnimatorStateInfo currentBaseState;

        //double tap to run
        public float tapDelay = 0.2f; 
        public int dPressed = 0;
        public float Run = 0f; //acts as boolean - 0 is walk and 1 is run. Used for blending tree in animator.

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
		}

        /*
         TODO: Change Update to apply for different facing directions.
               SO FAR ONLY P1 CAN DO DOUBLE TAP.
             */

        void Update() {

            //double tap to run
            if (Input.GetKeyDown(KeyCode.D))
            {
                dPressed += 1;
                if (dPressed > 1)
                {
                    Run = 1.0f;
                    Debug.Log("Double tap");
                }
            }
        }
	
	
		void FixedUpdate ()
		{
          
			float v = Input.GetAxisRaw ("Vertical");
            float direction = rb.transform.localEulerAngles.y;
            //Debug.Log(direction);

            anim.SetFloat ("Speed", v);							 
            anim.SetBool("Movement", movement);
            anim.SetFloat("Run", Run);
            anim.SetFloat("Direction", direction);

            anim.speed = animSpeed;								  
			currentBaseState = anim.GetCurrentAnimatorStateInfo (0);	   
			rb.useGravity = true;

            if (Input.GetKeyUp(KeyCode.D))
            {
                Invoke("SetDPressedToZero", tapDelay);
                Run = 0f;
            }
      
            
            if (v > 0.1) {
                if (direction == 265){
                        moveRight();
                } else {
                        moveLeft();
                }
            } else if (v < -0.1){
                if (direction == 265)
                {
                    moveLeft();
                }
                else {
                    moveRight();
                }
            }
            else if (v == 0)
            {
                movement = false;
            }
         
            if (Input.GetButtonDown ("Jump")) {	 
				if (currentBaseState.nameHash != jumpState) {
					if (!anim.IsInTransition (0)) {
						anim.SetBool ("Jump", true);
                        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                    }
                }
			}

			if (currentBaseState.nameHash == locoState) {
				if (useCurves) {
					resetCollider ();
				}
			}
		else if (currentBaseState.nameHash == jumpState) {
				cameraObject.SendMessage ("setCameraPositionJumpView");	 
				if (!anim.IsInTransition (0)) {		
					if (useCurves) {
						float jumpHeight = anim.GetFloat ("JumpHeight");
						float gravityControl = anim.GetFloat ("GravityControl"); 
						if (gravityControl > 0)
							rb.useGravity = false;	
										
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

        void SetDPressedToZero ()
        {
            dPressed = 0;
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