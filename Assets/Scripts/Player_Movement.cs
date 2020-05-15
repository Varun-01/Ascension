/*
 Player_Movement: Script enabling character control.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
        public CapsuleCollider col;
        public Rigidbody rb;
        public float orgColHight;
        public Vector3 orgVectColCenter;
        public Animator anim;
        public AnimatorStateInfo currentBaseState;
        public float movementDirection;

        public float facingLeft = 95f;
        public float facingRight = 265f;

        //double tap to run variables.
        public float tapDelay = 0.2f; //how much delay you can have to activate double tap to run
        public int buttonPresses = 0; //how many times the player pressed the button within the allowed timeframe
        public float Run = 0f; //acts as boolean - 0 is walk and 1 is run. Used for blending tree in animator.
        public float movementDelay = 0f; //used for smoothing forward and backward animation so that idle animation doesn't activate during "uninterrupted" movement.
        
        public Move moveRequest;
        public Player_Manager playerManager;

        private GameObject cameraObject;	 
		
		static int idleState = Animator.StringToHash ("Base Layer.Idle");
		static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash ("Base Layer.Jump");
		static int restState = Animator.StringToHash ("Base Layer.Rest");

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

        }

		void Start ()
		{
            playerManager = GetComponent<Player_Manager>();
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

        void Update()
        {

            /*
             double tap to run:
                if button is pressed take note of it, and if it is pressed twice then activate run.

            KEEP IN UPDATE - IT DOESN'T WORK IN FIXED UPDATE BECAUSE UPDATE AND FIXEDUPDATE RUN AT DIFFERENT INTERVALS
             */
            checkRun();
        }


        void FixedUpdate()
        {
        /*     direction == 0 : no buttons pressed. 
               direction > 0.1 : pressed button to go right. 
               direction < -0.1 : pressed button to go left
        */

        if (playerManager.getControllable())
        {
            movementDirection = Input.GetAxisRaw("Vertical");
            playerDirection = rb.transform.localEulerAngles.y;

            //Debug.Log(playerDirection);

            anim.SetFloat("Speed", movementDirection);
            anim.SetBool("Movement", movement);
            anim.SetFloat("Run", Run);
            anim.SetFloat("Direction", playerDirection);

            anim.speed = animSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
            rb.useGravity = true;

            //double tap to run: if key up for D it'll set the button presses to 0 after the preset delay.
            checkDoubleTap(playerDirection);

            //depending on movement direction and player direction, different force applied.
            checkMovement(movementDirection);

            //for animations: if a movement key isnt pressed in .02 seconds then the player is idle and movement animations are stopped
            //character would return to idle state animation if a key wasn't pressed which made the animation from forward to backward clunky.
            checkJump();
        }

            checkJumpComplete();
            
            checkMovementDelay();

            checkStun();

            checkAttacking();

            checkIdle();

            checkRest();
    }

        void resetCollider()
        {
            col.height = orgColHight;
            col.center = orgVectColCenter;
        }

        void SetButtonPressesToZero()
        {
            buttonPresses = 0;
        }

        void checkRun()
        {
            float playerDirection = rb.transform.localEulerAngles.y;

            if (playerDirection == facingRight)
            {
                if (Input.GetKeyDown(KeyCode.D)) // "D"
                {
                    buttonPresses += 1;
                    if (buttonPresses > 1)
                    {
                        Run = 1.0f;
                        Debug.Log("Double tap");
                    }
                }
            }
            else
            {
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

        void checkDoubleTap(float playerDirection)
        {
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
            else
            {
                if (Input.GetKeyUp(KeyCode.A))
                {
                    Invoke("SetButtonPressesToZero", tapDelay);
                    Run = 0f;
                    //Network
                    moveRequest.sendMoveRequest("A");
                }
            }

        }

        public void checkMovement(float movementDirection)
        {

            playerDirection = rb.transform.localEulerAngles.y;

            if (movementDirection > 0.1)
            {
                if (playerDirection == facingRight)
                {
                    moveRight();
                }
                else
                {
                    moveLeft();
                }
            }
            else if (movementDirection < -0.1)
            {
                if (playerDirection == facingRight)
                {
                    moveLeft();
                }
                else
                {
                    moveRight();
                }
            }
        }

        void checkMovementDelay()
        {
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
        }

        public void moveRight()
        { 
            if (Run > 0)
            {
                rb.MovePosition(rb.position + transform.forward * runningSpeed / 20);
            }
            else
            {
                rb.MovePosition(rb.position + transform.forward * walkingSpeed / 20);
            }

            movement = true;
        }

        public void moveLeft()
        {
            rb.MovePosition(rb.position -(transform.forward * walkingSpeed / 20));
            movement = true;
        }

        void checkJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (currentBaseState.nameHash != jumpState)
                {
                    if (!anim.IsInTransition(0))
                    {
                        jump();
                    }
                }
            }
        }

        public void jump()
        {
            anim.SetBool("Jump", true);
            rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        }

        void checkJumpComplete()
        {
            if (currentBaseState.nameHash == jumpState)
            {
                anim.SetBool("Movement", false);

                if (!anim.IsInTransition(0))
                {
                    if (useCurves)
                    {
                        float jumpHeight = anim.GetFloat("JumpHeight");
                        float gravityControl = anim.GetFloat("GravityControl");
                        //if (gravityControl > 0)
                        //rb.useGravity = false;	

                        Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                        RaycastHit hitInfo = new RaycastHit();
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            if (hitInfo.distance > useCurvesHeight)
                            {
                                col.height = orgColHight - jumpHeight;
                                float adjCenterY = orgVectColCenter.y + jumpHeight;
                                col.center = new Vector3(0, adjCenterY, 0);
                            }
                            else
                            {
                                resetCollider();
                            }
                        }
                    }
                    completeJump();
                }
            }
        }

        void completeJump()
        {
            anim.SetBool("Jump", false);
        }

        void checkStun()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stun"))
            {
                stun();
            }
        }

        void stun()
        {
            anim.SetBool("Movement", false);
            rb.velocity = new Vector3(0, 0, 0);
        }

        void checkAttacking()
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                attack();
            }
        }

        void attack()
        {
            anim.SetBool("Movement", false);
            rb.velocity = new Vector3(0, 0, 0);
        }

        void checkIdle()
        {
            if (currentBaseState.nameHash == idleState)
            {
                if (useCurves)
                {
                    resetCollider();
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetBool("Rest", true);
                }
            }

        }
        void checkRest()
        {
            if (currentBaseState.nameHash == restState)
            {
                if (!anim.IsInTransition(0))
                {
                    anim.SetBool("Rest", false);
                }
            }
        }
    }
