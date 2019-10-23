using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    private Vector3 moveDir;
    public CharacterController charC;
    public float jumpSpeed = 8f;
    public float speed = 10f, shiftSpeed = 15f, gravity = 20f;

    [Header("Health")]
    public Slider hpSlider;
    public Image hpFill;
    public float maxHP = 100f;
    public float curHP = 0f;

    [Header("Mouse Look")]
    public bool canLook = false;
    public RotationalAxis axis = RotationalAxis.MouseX;
    public float sensitivity = 15f;
    public float minY = -60f;
    public float maxY = 60f;
    float rotationY = 0f;

    [Header("Interact")]
    public GameObject mainCamera;
    public bool isHolding = false;
    public float playerReach = 1f;
    public Text totalItem, holdingItem;
    public int voteTotal = 0;

    [Header("Script References")]
    public PlayerShoot pShoot;
    public Menus menus;
    public EnemyManager enemy;

    #endregion

    #region Start
    void Start()
    {
        //Movement
        charC = this.GetComponent<CharacterController>();

        //Health
        curHP = maxHP;

        //Mouse Look
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        //Interact
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        holdingItem.text = "Collect votes to beat the Liberals!";
    }
    #endregion

    #region Update
    void Update()
    {
        //Movement
        if (charC.isGrounded)
        {
            moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * shiftSpeed);
            }

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        charC.Move(moveDir * Time.deltaTime);

        //Health
        hpSlider.value = Mathf.Clamp01(curHP / maxHP);

        //Mouse Look
        if (canLook)
        {
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
        }

        //Shoot
        if (Input.GetButton("Fire1"))
        {
            pShoot.Shoot();
        }

        //Interact
        if (voteTotal == 1)
        {
            totalItem.text = voteTotal.ToString() + " vote!";
        }
        else
        {
            totalItem.text = voteTotal.ToString() + " votes!";
        }

        if (voteTotal >= 4)
        {
            holdingItem.text = "You won the battle!";
        }
        else
        {
            if (Input.GetButtonDown("Interact"))
            {
                Ray interact;
                interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hitInfo;
                if (Physics.Raycast(interact, out hitInfo, playerReach))
                {
                    if (hitInfo.collider.CompareTag("Collectable"))
                    {
                        if (isHolding == false)
                        {
                            isHolding = true;
                            Destroy(hitInfo.collider.gameObject);
                            holdingItem.text = "Holding a Vote!";
                        }
                        else
                        {
                            holdingItem.text = "Hurry! Go submit the vote!";
                        }
                    }

                    if (hitInfo.collider.CompareTag("Depositor"))
                    {
                        if (isHolding == true)
                        {
                            isHolding = false;
                            holdingItem.text = "Vote submitted! Go collect another vote!";
                            voteTotal++;
                        }
                        else
                        {
                            holdingItem.text = "Quick! Go collect a vote!";
                        }
                    }
                }
            }
        }       

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menus.OpenPauseMenu();
        }
    }
    #endregion

    #region TakeDamage
    public void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            curHP -= col.GetComponent<EnemyManager>().damage;
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