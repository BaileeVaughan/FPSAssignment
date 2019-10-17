using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    //Movement
    public Vector3 moveDir;
    public CharacterController charC;
    public float jumpSpeed = 8f;
    public float speed = 5f, gravity = 20f;
    public static bool canMove;
    //Shooting
    public PlayerShoot pShoot;
    //Mouse Look
    public RotationalAxis axis = RotationalAxis.MouseX;
    public float sensitivity = 15f;
    public float minY = -60f;
    public float maxY = 60f;
    float rotationY = 0f;
    //Interact
    public GameObject player;
    public GameObject mainCamera;
    public bool isHolding = false;
    public int voteTotal = 0;
    #endregion
    #region Start  
    void Start()
    {
        //Movement
        charC = this.GetComponent<CharacterController>();
        //Mouse Look
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        //Interact
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    #endregion
    #region Update
    void Update()
    {
        //Movement
        if (charC.isGrounded)
        {
            moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed);
            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        charC.Move(moveDir * Time.deltaTime);
        //Mouse Look
        if (axis == RotationalAxis.MouseXandY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        else if (axis == RotationalAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = Mathf.Clamp(rotationY, minY, maxY);
            transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }
        //Shoot
        if (Input.GetButtonDown("Fire1"))
        {
            pShoot.Shoot();
        }
        //Interact
        if (Input.GetButtonDown("Interact"))
        {
            Ray interact;
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                if (hitInfo.collider.CompareTag("Collectable"))
                {
                    if (isHolding == false)
                    {
                        Debug.Log("Vote Collected!");
                        isHolding = true;
                    }
                    else
                    {
                        Debug.Log("Already holding vote!");
                    }
                }
                if (hitInfo.collider.CompareTag("Depositor"))
                {
                    if (isHolding == true)
                    {
                        isHolding = false;
                        Debug.Log("Vote Deposited!");
                        voteTotal++;
                    }
                    else
                    {
                        Debug.Log("Go collect a vote!");
                    }
                }
            }
        }
    }
    #endregion
}

public enum RotationalAxis
{
    MouseXandY,
    MouseX,
    MouseY
}