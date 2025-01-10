using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float sensitivity = 2f; // Mouse sensitivity
    public float rotationSpeed = 100f; // Rotation speed for Q and E

    private CharacterController characterController;
    private Transform cameraTransform;
    private float verticalRotation = 0f; // Tracks vertical camera rotation

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera (vertical rotation)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limit up/down rotation

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Handle Q and E for rotation
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F))
        {
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * speed * Time.deltaTime);

        // Gravity
        Vector3 gravity = Vector3.down * 9.81f * Time.deltaTime;
        characterController.Move(gravity);
    }
}
