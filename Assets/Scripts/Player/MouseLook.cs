using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public bool canLook = false;
    public RotationalAxis axis = RotationalAxis.MouseX;
    public float sensitivity = 15f;
    public float minY = -60f;
    public float maxY = 60f;
    float rotationY = 0f;

    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {
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
    }
}