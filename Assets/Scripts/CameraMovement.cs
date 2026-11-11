using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float sensX;
    public float sensY;

    public Transform orientation;

    float Xrotation;
    float Yrotation;

    private void Start()
    {
        // lock the cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        orientation.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
    }

    private void Update()
    {
        // set camera rotation based on mouse position
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        Yrotation += mouseX;
        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
        orientation.rotation = Quaternion.Euler(Xrotation, Yrotation, 0);
    }
}
