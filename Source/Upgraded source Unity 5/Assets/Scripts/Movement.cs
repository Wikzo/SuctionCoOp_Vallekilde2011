using UnityEngine;
using System.Collections;


//[RequireComponent(typeof(HingeJoint))]

public enum State
{
    Suge,
    CanMove,
    Falling
}

public class Movement : MonoBehaviour {

    public int targetVelocity = 200;
    public int motorForce = 100;
    public HingeJoint[] hinges;
    public GameObject empty1;
    public GameObject empty2;

    bool player1Touch;
    bool player2Touch;

    bool justPressedSuge1;
    bool justPressedSuge2;
	bool suckingNow1;
	bool suckingNow2;
	
	public static int suckTimes;

    // private bool playerOneActivated = false;
    // private bool playerTwoActivated = false;

    public static State Player1State;
    public static State Player2State;

    public GameObject sugekop1;
    public GameObject sugekop2;

    public AudioClip lyd1Sug;
    public AudioClip lyd2Sug;
    public AudioClip lyd1Pop;
    public AudioClip lyd2Pop;

    //private JointMotor tempMotor;

	// Use this for initialization
	void Start ()
    {
        Player1State = State.Falling;
        Player2State = State.Falling;

        hinges = new HingeJoint[2];
        hinges = GetComponents<HingeJoint>();

        hinges[0].connectedBody = empty1.GetComponent<Rigidbody>();
        hinges[1].connectedBody = empty2.GetComponent<Rigidbody>();

        player1Touch = sugekop1.GetComponent<SugeFeedback>().playerTouching;
        player2Touch = sugekop2.GetComponent<SugeFeedback>().playerTouching;

        //tempMotor = new JointMotor();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
		//print("SuckingNow: " + suckingNow1);
		//print("SuckingNow: " + suckingNow2);
		print("Suck points" + suckTimes);
		
        //bool bothSuge = Player1State == State.Suge && Player2State == State.Suge;

        player1Touch = sugekop1.GetComponent<SugeFeedback>().playerTouching;
        player2Touch = sugekop2.GetComponent<SugeFeedback>().playerTouching;

        #region Play sounds
        if (Input.GetKeyDown(KeyCode.S) && player1Touch)
            GetComponent<AudioSource>().PlayOneShot(lyd1Sug);
        else if (Input.GetKeyUp(KeyCode.S) && player1Touch)
            GetComponent<AudioSource>().PlayOneShot(lyd1Pop);

        if (Input.GetKeyDown(KeyCode.DownArrow) && player2Touch)
            GetComponent<AudioSource>().PlayOneShot(lyd2Sug);
        else if (Input.GetKeyUp(KeyCode.DownArrow) && player2Touch)
            GetComponent<AudioSource>().PlayOneShot(lyd2Pop);
        #endregion

        // SET STATES
        #region Set Player 1 states
        if (Input.GetKey(KeyCode.S) && player1Touch)
        {
            if (Player1State != State.Suge)
                justPressedSuge1 = true;
            else
                justPressedSuge1 = false;
            
            if (justPressedSuge1)
            {
                // Reset cylinder rigidbody
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

            Player1State = State.Suge;
            transform.FindChild("Lys1").GetComponent<Light>().enabled = true;
            
        }
        else if (!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.DownArrow))
        {
            Player1State = State.CanMove;
            transform.FindChild("Lys1").GetComponent<Light>().enabled = false;
        }
        else
        {
            Player1State = State.Falling;
            transform.FindChild("Lys1").GetComponent<Light>().enabled = false;
        }
        #endregion

        #region Set Player 2 states
        if (Input.GetKey(KeyCode.DownArrow) && player2Touch)
        {
            if (Player2State != State.Suge)
                justPressedSuge2 = true;
            else
                justPressedSuge2 = false;

            if (justPressedSuge2)
            {
                // Reset cylinder rigidbody
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

            Player2State = State.Suge;
            transform.FindChild("Lys2").GetComponent<Light>().enabled = true;
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.S))
        {
            Player2State = State.CanMove;
            transform.FindChild("Lys2").GetComponent<Light>().enabled = false;
        }
        else
        {
            Player2State = State.Falling;
            transform.FindChild("Lys2").GetComponent<Light>().enabled = false;
        }
        #endregion

        /*print("P1: " + Player1State);
        print("P2: " + Player2State);
        print(player1Touch);
        print(player2Touch);*/

        // SWITCH ON STATES

        #region Switch on Player 1 states
        switch (Player1State)
        {
            case State.Suge:
				
				if (suckingNow1 == false)
				{
					suckTimes++;
					suckingNow1 = true;
				}
                hinges[0].connectedBody = null; // suger fast til verden

                // Motor force = 0
                //hinges[0].motor.force = 0; // DON'T WORK
                JointMotor myForce_suge = hinges[0].motor;
                myForce_suge.force = 0;

                // Velocity = 0
                myForce_suge.targetVelocity = 0f;
                
                /*// Reset cylinder rigidbody
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;*/
                
                // Set all
                hinges[0].motor = myForce_suge;


                break;

            case State.CanMove:
				
				suckingNow1 = false;
			
                hinges[0].connectedBody = empty1.GetComponent<Rigidbody>();
                if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Player2State == State.Suge)
                {
                    // LEFT

                    

                    // Motor target velocity
                    //hinges[0].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
                    JointMotor myMotor = hinges[0].motor; // make new motor
                    myMotor.targetVelocity = targetVelocity; // assign value
                    hinges[0].motor = myMotor; // put value back in property

                    // Motor force 
                    //hinges[0].motor.force = 0; // DON'T WORK
                    JointMotor myForce = hinges[0].motor;
                    myForce.force = motorForce;
                    hinges[0].motor = myForce;
                }
                else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && Player2State == State.Suge)
                {
                    // RIGHT

                    hinges[0].connectedBody = empty1.GetComponent<Rigidbody>();

                    // Motor target velocity
                    //hinges[0].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
                    JointMotor myMotor = hinges[0].motor; // make new motor
                    myMotor.targetVelocity = -targetVelocity; // assign value
                    hinges[0].motor = myMotor; // put value back in property

                    // Motor force 
                    //hinges[0].motor.force = 0; // DON'T WORK
                    JointMotor myForce = hinges[0].motor;
                    myForce.force = motorForce;
                    hinges[0].motor = myForce;
                }
                else //if (Input.GetAxis("Horizontal") == 0)
                {
                    // NOT MOVING

                    // Motor target velocity
                    //hinges[0].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
                    /*JointMotor myMotor = hinges[0].motor; // make new motor
                    myMotor.targetVelocity = 0; // TODO: maybe not neccesary assign value
                    hinges[0].motor = myMotor; // put value back in property*/

                    // Motor force = 0
                    //hinges[0].motor.force = 0; // DON'T WORK
                    JointMotor myForce = hinges[0].motor;
                    myForce.force = 0;

                    // Velocity = 0
                    myForce.targetVelocity = 0f;

                    // Set all
                    hinges[0].motor = myForce;

                    


                }
                break;

            case State.Falling:
					
				suckingNow1 = false;
			
                hinges[0].connectedBody = empty1.GetComponent<Rigidbody>(); // connected body = sig selv --> falder ned


                // Motor force = 0
                //hinges[0].motor.force = 0; // DON'T WORK
                JointMotor myForce_again = hinges[0].motor;
                myForce_again.force = 0;
                
                // Velocity = 0
                myForce_again.targetVelocity = 0f; // put value back in property

                // Set
                hinges[0].motor = myForce_again;



                break;
        }

        #endregion

        #region Switch on Player 2 states
        switch (Player2State)
        {
            case State.Suge:
			
			if (suckingNow2 == false)
				{
					suckTimes++;
					suckingNow2 = true;
				}	
			
                hinges[1].connectedBody = null; // suger fast til verden

                // Motor force = 0
                //hinges[1].motor.force = 0; // DON'T WORK
                JointMotor myForce_suge = hinges[1].motor;
                myForce_suge.force = 0;

                // Velocity = 0
                myForce_suge.targetVelocity = 0f;

                // Set all
                hinges[1].motor = myForce_suge;



                break;

            case State.CanMove:
			
				suckingNow2 = false;
			
                hinges[1].connectedBody = empty2.GetComponent<Rigidbody>();
                if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && Player1State == State.Suge)
                {
                    // LEFT

                    // Motor target velocity
                    //hinges[1].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
                    JointMotor myMotor = hinges[1].motor; // make new motor
                    myMotor.targetVelocity = targetVelocity; // assign value
                    hinges[1].motor = myMotor; // put value back in property

                    // Motor force 
                    //hinges[1].motor.force = 0; // DON'T WORK
                    JointMotor myForce = hinges[1].motor;
                    myForce.force = motorForce;
                    hinges[1].motor = myForce;
                }
                else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && Player1State == State.Suge)
                {
                    // RIGHT

                    hinges[1].connectedBody = empty2.GetComponent<Rigidbody>();

                    // Motor target velocity
                    //hinges[1].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
                    JointMotor myMotor = hinges[1].motor; // make new motor
                    myMotor.targetVelocity = -targetVelocity; // assign value
                    hinges[1].motor = myMotor; // put value back in property

                    // Motor force 
                    //hinges[1].motor.force = 0; // DON'T WORK
                    JointMotor myForce = hinges[1].motor;
                    myForce.force = motorForce;
                    hinges[1].motor = myForce;
                }
                else //if (Input.GetAxis("Horizontal2") == 0)
                {
                    // NOT MOVING

                    /*// Motor target velocity
                    //hinges[1].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
                    JointMotor myMotor = hinges[1].motor; // make new motor
                    myMotor.targetVelocity = 0; // TODO: maybe not neccesary assign value
                    hinges[1].motor = myMotor; // put value back in property*/

                    // Motor force 
                    //hinges[1].motor.force = 0; // DON'T WORK
                    JointMotor myForce = hinges[1].motor;
                    myForce.force = 0;
                    

                    // Velocity = 0
                    myForce.targetVelocity = 0f;

                    // Set all
                    hinges[1].motor = myForce;
                }
                break;

            case State.Falling:
				
				suckingNow2 = false;

				hinges[1].connectedBody = empty2.GetComponent<Rigidbody>(); // connected body = sig selv --> falder ned

                // Motor force = 0
                //hinges[1].motor.force = 0; // DON'T WORK
                JointMotor myForce_again = hinges[1].motor;
                myForce_again.force = 0;

                // Velocity = 0
                hinges[1].motor = myForce_again; // put value back in property

                // Set all
                hinges[1].motor = myForce_again;

                break;
        }

        #endregion

        // OLD shit

        #region Player1
        /* OLD
        // Player 1 sugekop
        if (Input.GetKey(KeyCode.S))
        {
            hinges[0].connectedBody = null;
            playerOneActivated = true;
            hinges[0].useMotor = false;
        }
        else
        {
            hinges[0].connectedBody = gameObject.rigidbody;
            playerOneActivated = false;
            hinges[0].useMotor = false;
        }

        // Player 1 movement
        if (!playerOneActivated && Input.GetAxis("Horizontal") < 0)
        {
            hinges[0].useMotor = true;
            //hinges[0].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
            JointMotor myMotor = hinges[0].motor; // make new motor
            myMotor.targetVelocity = -300; // assign value
            hinges[0].motor = myMotor; // put value back in property
        }
        else if (!playerOneActivated && Input.GetAxis("Horizontal") > 0)
        {
            hinges[0].useMotor = true;
            //hinges[0].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
            JointMotor myMotor = hinges[0].motor; // make new motor
            myMotor.targetVelocity = 300; // assign value
            hinges[0].motor = myMotor; // put value back in property
        }
        else if (!playerOneActivated && Input.GetAxis("Horizontal") == 0)
        {
            hinges[0].useMotor = false;
            //hinges[0].motor.force = 0; // DON'T WORK
            JointMotor myForce = hinges[0].motor;
            myForce.force = 0;
            hinges[0].motor = myForce;
        }

        // Player 1 velocity default
        //hinges[0].motor.force = 0; // DON'T WORK
        JointMotor defaultForce = hinges[0].motor;
        defaultForce.force = 1000;
        hinges[0].motor = defaultForce;
        */
        #endregion


        #region Player2
        /* OLD
        // Player 2 sugekop
        if (Input.GetKey(KeyCode.DownArrow))
        {
            hinges[1].connectedBody = null;
            playerTwoActivated = true;
        }
        else
        {
            hinges[1].connectedBody = gameObject.rigidbody;
            playerTwoActivated = false;
        }

        //// ---------------------------------------sadasd
        
        // Player 2 movement
        if (!playerTwoActivated && Input.GetAxis("Horizontal2") < 0)
        {
            hinges[1].useMotor = true;
            //hinges[1].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
            JointMotor myMotor2 = hinges[1].motor; // make new motor
            myMotor2.targetVelocity = -300; // assign value
            hinges[1].motor = myMotor2; // put value back in property
        }
        else if (!playerTwoActivated && Input.GetAxis("Horizontal2") > 0)
        {
            hinges[1].useMotor = true;
            //hinges[1].motor.targetVelocity = 10; // DON'T WORK - can't directly change the property
            JointMotor myMotor2 = hinges[1].motor; // make new motor
            myMotor2.targetVelocity = 300; // assign value
            hinges[1].motor = myMotor2; // put value back in property
        }
        else if (!playerTwoActivated && Input.GetAxis("Horizontal2") == 0)
        {
            //hinges[1].motor.force = 0; // DON'T WORK
            JointMotor myForce2 = hinges[1].motor;
            myForce2.force = 0;
            hinges[1].motor = myForce2;
        }

        // Player 1 velocity default
        //hinges[1].motor.force = 0; // DON'T WORK
        JointMotor defaultForce2 = hinges[1].motor;
        defaultForce.force = 1000;
        hinges[1].motor = defaultForce2;
        */
        #endregion



        // Restart
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
		
		// Skip level
		if (Input.GetKey(KeyCode.N))
		{
			Application.LoadLevel(Application.loadedLevel +1);
		}

        /*// Player 1, motor enable/disable
        if (Input.GetKeyDown(KeyCode.E))
        {
            hinges[0].useMotor = !hinges[0].useMotor;
        }

        // Player 2, motor enable/disable
        if (Input.GetKey(KeyCode.L))
        {
            hinges[1].useMotor = !hinges[1].useMotor;
        }

        // Tims glade debug kode
        if (bothSuge && Player2State == State.CanMove)
            Debug.Break();*/
	}


}
