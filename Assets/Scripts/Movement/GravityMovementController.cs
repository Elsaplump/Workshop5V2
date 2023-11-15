using UnityEngine;
using UnityEngine.InputSystem;

// This component can be used to move a character that should be affected by gravity
// Use with the components CharacterController and PlayerInput.
// PlayerInput should be set to Behavior: Invoke Unity Events
public class GravityMovementController : MonoBehaviour
{ 
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private CharacterController controller;

    private Vector2 moveInput;
    private bool jumpInput;
    private Vector3 velocity;
    private bool wasGrounded;
    public Animator animator;

    void Update()
    {
        ApplyGravity();
        
        velocity = transform.TransformDirection(TranslateInputToVelocity(moveInput));
        
        animator.SetFloat("Speed", Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z));
        //animator.SetFloat("Speed", 2.0f);
        
        // Apply jump-input:
        if (jumpInput)
        {
            velocity.y = jumpForce;
            jumpInput = false;
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        controller.Move(velocity * Time.deltaTime);

        bool isGrounded = controller.isGrounded;

        // Check if character lost contact with ground this frame
        if (wasGrounded == true && isGrounded == false)
        {
            animator.SetBool("Grounded", false);
            animator.SetBool("FreeFall", true);
            // Has fallen. Play fall sound and/or trigger fall animation etc
        }
        // Check if character gained contact with ground this frame
        else if (wasGrounded == false && isGrounded == true)
        {
            // Has landed. Play landing sound and/or trigger landing animation etc
            animator.SetBool("Grounded", true);
            animator.SetBool("FreeFall", false);
        }

        wasGrounded = isGrounded;
    }
    
    private void ApplyGravity()
    {
        // Applies a set gravity for when player is grounded
        if (controller.isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -1.0f;
        }
        // Updates fall speed with gravity if object isn't grounded
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    Vector3 TranslateInputToVelocity(Vector2 input)
    {
        // Make the character move on the ground (XZ-plane)
        return new Vector3(input.x * speed, velocity.y, input.y * speed);
    }

    // Handle Move-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().normalized;
    }

    // Handle Fire-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("FIRE!");
            // Play fire-animation and/or trigger sound etc
        }
    }
    
    // Handle Jump-input
    // This method can be triggered through the UnityEvent in PlayerInput
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && wasGrounded)
        {
            //Debug.Log("Jump!");
            jumpInput = true;
            // Jumps: Set animation parameters etc here
        }
    }
}
