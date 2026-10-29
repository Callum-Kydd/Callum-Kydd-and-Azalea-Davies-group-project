using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float sensX;
    public float sensY;

    public Transform orientation;

    float Xrotation;
    float Yrotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        Yrotation += mouseX;
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
        orientation.rotation = Quaternion.Euler(0, Yrotation, 0);
    }
}
