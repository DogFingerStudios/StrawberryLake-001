using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float sensitivity = 2f; // Mouse sensitivity
    public float rotationSpeed = 100f; // Rotation speed for Q and E

    private CharacterController characterController;
    private Transform cameraTransform;
    private float verticalRotation = 0f; // Tracks vertical camera rotation
    private Vector3 movement; // Tracks movement for FixedUpdate
    private Vector3 gravityForce = Vector3.zero; // Tracks gravity force for FixedUpdate

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

        // Capture movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        movement = transform.right * moveX + transform.forward * moveZ;

        // Gravity calculation
        gravityForce += Vector3.down * 9.81f * Time.deltaTime;
    }

    void FixedUpdate()
    {
        // Gravity and ground detection
        if (characterController.isGrounded)
        {
            gravityForce = Vector3.zero; // Reset gravity if grounded
        }
        else
        {
            gravityForce += Vector3.down * 9.81f * Time.fixedDeltaTime; // Apply gravity
        }

        // Apply movement and gravity
        characterController.Move(movement * speed * Time.fixedDeltaTime);
        characterController.Move(gravityForce * Time.fixedDeltaTime);
    }

}
