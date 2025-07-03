using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Camera Settings
    public Camera playerCam;
    public float lookSpeed = 2f;
    public float lookXLimit = 85f;
    public float cameraRotationSmooth = 10f;

    // Movement Settings
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float gravity = 10f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private bool canMove = true;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        float moveX = canMove ? speed * Input.GetAxis("Vertical") : 0f;
        float moveZ = canMove ? speed * Input.GetAxis("Horizontal") : 0f;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * moveX) + (right * moveZ);

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y = -gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Camera Rotation
        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            rotationY += Input.GetAxis("Mouse X") * lookSpeed;

            Quaternion targetRotationX = Quaternion.Euler(rotationX, 0, 0);
            Quaternion targetRotationY = Quaternion.Euler(0, rotationY, 0);

            playerCam.transform.localRotation = Quaternion.Slerp(playerCam.transform.localRotation, targetRotationX, Time.deltaTime * cameraRotationSmooth);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationY, Time.deltaTime * cameraRotationSmooth);
        }
    }
}
