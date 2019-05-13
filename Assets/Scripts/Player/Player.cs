using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour {

    public float energy = 100;
    private bool canJet = true;
    private Transform mainCameraTran;
    public Transform cameraDir;
    private float forwardSpeed = 4.0f;
    private float vSpeed = 0;
    public float SpeedSlow = 2.0f;
    public float SpeedFast = 6.0f;
    public float gravity = 20.0f;
    public float JumpHeight = 5;
    private float rotate = 0.0F;
    public float rotateSpeed = 0.05f;
    private bool Canwalk = true;
    private bool CanTurn = true;
    private bool Aim = false;
    private float dodgeCounter = 0;
    public GameObject Body;
    private Vector3 moveDirection = Vector3.zero;
    public Animator Player1;
    public Image mpBar;
    private CharacterController controller;
    // Use this for initialization
    void Start() {
        gameObject.GetComponent<CharacterController>().enabled = true;
        mainCameraTran = Camera.main.transform;
        GameObject cameraDir_obj = new GameObject();
        cameraDir_obj.transform.parent = transform;
        cameraDir_obj.transform.localPosition = Vector3.zero;
        cameraDir_obj.name = "CameraDir";
        cameraDir = cameraDir_obj.transform;
        forwardSpeed = SpeedSlow;
        Player1 = Body.GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
    }
    void Rotation(float iTarget) {
        Body.transform.eulerAngles = new Vector3(0, Mathf.SmoothDampAngle(Body.transform.eulerAngles.y, iTarget + 90, ref rotate, rotateSpeed), 0);
    }
    // Update is called once per frame
    void Update() {
        controller.Move(moveDirection * Time.deltaTime);
        //jeta();
        Animator();
        SetRotation();
        dodge();
       /* if (Aim == false&& (Input.GetAxis("Horizontal")!=0|| Input.GetAxis("Vertical")!=0))
        {
            SetRotation(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        }
        else if (Aim&&(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            SetRotation(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }*/
        
        move(new Vector2(hInput.GetAxis("Horizontal"), hInput.GetAxis("Vertical")));
    }
    void LateUpdate() {
        cameraDir.eulerAngles = new Vector3(0, mainCameraTran.eulerAngles.y, 0);
    }
    public void setWalk(bool walk)
    {
        Canwalk = walk;
    }
    void dodge()
    {
        if (hInput.GetButton("Run"))
        {
            dodgeCounter += 1 * Time.deltaTime;
            Debug.Log("dodgeCounter=" + dodgeCounter);
        }
        if (hInput.GetButtonUp("Run"))
        {
            if (dodgeCounter <= 0.5f)
            {

                moveDirection+=new Vector3(moveDirection.x,0,moveDirection.z)*10*Time.deltaTime;
            }
            dodgeCounter = 0;
        }
    }
    public void SetRotation()
    {
        Vector2 input = new Vector2(hInput.GetAxis("Horizontal"), hInput.GetAxis("Vertical"));
        if ((Aim == false && (hInput.GetAxis("Horizontal") != 0 || hInput.GetAxis("Vertical") != 0))&&CanTurn)
        {
            Rotation(mainCameraTran.eulerAngles.y + Mathf.Atan2(0 - input.y, input.x) * 180 / Mathf.PI);
        }
        else if (Aim && CanTurn)
        {
            Rotation(mainCameraTran.eulerAngles.y-90);

        }
    }
    public void rootMove(Vector3 moveVector)
    {
        moveDirection += Body.transform.TransformDirection(moveVector);
        //controller.Move(moveDirection * Time.deltaTime);
    }
    public void move(Vector2 input ) {
        if (controller.isGrounded&&Canwalk)
        {

            vSpeed = -0.01f;
            moveDirection = cameraDir.TransformVector(input.x, 0, input.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= forwardSpeed;

            if (Input.GetButton("Jump"))
            {
                vSpeed = JumpHeight;
            }

        }
        if (hInput.GetButtonDown("Run")&&controller.isGrounded)
        {
            forwardSpeed = SpeedFast;
        }
        else if (hInput.GetButtonUp("Run"))
        {
            forwardSpeed = SpeedSlow;
        }
        vSpeed -= gravity * Time.deltaTime;
            moveDirection.y = vSpeed;
        if (Canwalk == false)
        {
            moveDirection.x *= 0;
            moveDirection.z *= 0;

        }
        //Debug.Log("Vspeed="+vSpeed);
        vSpeed = Mathf.Clamp(vSpeed,-30,30);
        

    }
    public void jeta() {
        energy = Mathf.Clamp(energy, 0, 100);
        mpBar.fillAmount = energy / 100;
        //Debug.Log("canget="+canJet);

        if (controller.isGrounded) {
            canJet = true;
            energy += 15 * Time.deltaTime;
        }
            if (controller.isGrounded == false&&Canwalk)
        {
            moveDirection = cameraDir.TransformVector(hInput.GetAxis("Horizontal"), 0, hInput.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= forwardSpeed;
            if (hInput.GetButtonDown("Jump")&&canJet)
            {
                vSpeed = 0;

            }
            if (hInput.GetButton("Jump")&&canJet)
            {
                gravity = 0;
                vSpeed +=0.5f;
                energy -= 2;
                if (energy <= 0)
                {
                    canJet = false;
                    gravity = 20;
                }
                
            }
        }
            else if (Canwalk == false)
        {
            moveDirection.x *= 0;
            moveDirection.z *= 0;

        }
        if (hInput.GetButtonUp("Jump"))
        {
            gravity = 20;
        }
        if (energy <= 0)
        {
            gravity = 20;
            canJet = false;
        }
    }
    public void SetJet(bool Jswitch) {
        canJet = Jswitch;
        Debug.Log("Jet"+canJet);
    }
    public void SetTurn(bool turn)
    {
        CanTurn = turn;
    }
    public void SetAim(bool aiim)
    {
        Aim = aiim;
    }
    public void Animator() {
        Player1.SetFloat("WalkArc",(controller.velocity.magnitude)/SpeedFast);
        if ((hInput.GetAxis("Horizontal") != 0 || hInput.GetAxis("Vertical") != 0)&&Canwalk)
        {
            Player1.SetBool("walkforward", true);
        }
        else {
            Player1.SetBool("walkforward", false);
        }
        if (controller.isGrounded) {
            Player1.SetBool("isGround", true);
            Player1.SetBool("onAir", false);
        }
        else if (controller.isGrounded==false)
        {
            Player1.SetBool("isGround", false);
            Player1.SetBool("onAir",true);
        }
        if (hInput.GetButtonDown("Jump")) {
            Player1.SetTrigger("Jump");
        }
        if (hInput.GetButtonDown("Run")&&Canwalk&&controller.isGrounded)
        {
            Player1.SetBool("Running", true);
        }
        else if (hInput.GetButtonUp("Run")) {
            Player1.SetBool("Running",false);
        }

    }
}

