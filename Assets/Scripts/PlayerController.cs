using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSenstivity = 4.0f;
    [SerializeField] bool cursorLock=true;
    [SerializeField]  float cameraHorizontalMovement = 0.0f; 
    [SerializeField] float walkSpeed = 8.0f;
    [SerializeField] float movementSpeed = 8.0f;
    [SerializeField] float runSpeed = 8.0f;
    [SerializeField] float runBuildUpSpeed = 4.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f,0.5f)]float movementSmoothTime = 0.35f;
    [SerializeField] [Range(0.0f,0.5f)]float cameraSmoothTime = 0.35f;

    Vector2 currDir = Vector2.zero;
    Vector2 currDirVelocity = Vector2.zero;
    Vector2 currMouseDelta = Vector2.zero;
    Vector2 currMouseDeltaVelocity = Vector2.zero;
    float velocityY=0.0f;

    CharacterController controller = null;
    void Start()
    {  controller = GetComponent<CharacterController>();
        if(cursorLock)
        {
            Cursor.visible=false;
            Cursor.lockState= CursorLockMode.Locked;
        }
       
    }

   
    void Update()
    {
         MouseLook();
         Movement();
    }

    void MouseLook()
    {   //Vector2 mouseDelta =new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector2 targetMouseDelta =new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currMouseDelta= Vector2.SmoothDamp(currMouseDelta,targetMouseDelta,ref currMouseDeltaVelocity,cameraSmoothTime);
        //transform.Rotate(Vector3.up*mouseDelta.x*mouseSenstivity);
        //cameraHorizontalMovement-=mouseDelta.y;
        transform.Rotate(Vector3.up*currMouseDelta.x*mouseSenstivity);
        cameraHorizontalMovement-=currMouseDelta.y;
        cameraHorizontalMovement= Mathf.Clamp(cameraHorizontalMovement,-22.5f,22.5f);
       // transform.Rotate(Vector3.right*mouseDelta.y*mouseSenstivity);
        playerCamera.localEulerAngles= (Vector3.right*cameraHorizontalMovement*mouseSenstivity);
    }

    void Movement()
    {   //imputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //inputDirection.Normalize();
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        Run();
        Gravity();
       
        currDir = Vector2.SmoothDamp(currDir , targetDir ,ref currDirVelocity , movementSmoothTime);
        //Vector3 velocity = (transform.forward*inputDirection.y+transform.right*inputDirection.x)*walkSpeed;
        //controller.Move(velocity*Time.deltaTime);  
        Vector3 velocity = (transform.forward*currDir.y+transform.right*currDir.x)*movementSpeed+Vector3.up*velocityY;
        controller.Move(velocity*Time.deltaTime);  
    }
    void Run()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed=Mathf.Lerp(movementSpeed,runSpeed,Time.deltaTime*runBuildUpSpeed);
        }
        else
        {
            movementSpeed=Mathf.Lerp(movementSpeed,walkSpeed,Time.deltaTime*runBuildUpSpeed);
        }

    }
    void Gravity()
    {
         if(controller.isGrounded)
        {
            velocityY=-1.0f;
        }
        else
        {
            velocityY+=gravity*Time.deltaTime;
        }
    }
}

    