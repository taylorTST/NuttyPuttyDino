using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        // Get the Character Controller component attached to this GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is touching the ground
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            // Reset velocity but maintain a small downward force to keep grounded
            velocity.y = -2f; 
        }

        // Get Input (WASD or Arrow Keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Move relative to the direction the player is facing
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Handle Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Physics formula for calculating exact jump velocity based on desired height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the controller vertically (handling jumping/falling)
        controller.Move(velocity * Time.deltaTime);
    }
}
