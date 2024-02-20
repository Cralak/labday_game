using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Camera Transform for mouse look
    [SerializeField] Transform playerCamera;

    // Parameters for mouse input smoothing
    [SerializeField, Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    // Parameters for movement smoothing
    [SerializeField, Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

    // Parameters for head bobbing motion
    [SerializeField, Range(0, 0.1f)] float bobbingX = 0.01f;
    [SerializeField, Range(0, 0.1f)] float bobbingY = 0.005f;
    [SerializeField, Range(0, 30)] float frequency = 10.0f;

    // Gravity force
    [SerializeField] float gravity = -30.0f;

    // Ground check parameters
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask jumpBlock;

    // Character Controller component
    CharacterController controller;

    // Variables for mouse input smoothing
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    // Variables for movement smoothing
    Vector2 currentDir;
    Vector2 currentDirVelocity;

    // Starting position of the camera
    Vector3 startPos;

    // Footsteps sound
    AudioSource footsteps;

    // Sensitivity for mouse input
    float sensitivity;

    // Movement speed
    float speed = 6.0f;

    // Jump height
    readonly float jumpHeight = 6.0f;

    // Vertical velocity
    float velocityY;

    // Grounded state flag
    bool isGrounded;

    // Crouch state flag
    bool isCrouched = false;

    // Camera pitch cap
    float cameraCap;

    void Start()
    {
        // Make the GameObject persistent between scenes
        DontDestroyOnLoad(gameObject);

        // Initialize components and variables
        controller = GetComponent<CharacterController>();
        footsteps = GetComponent<AudioSource>();
        footsteps.Play();
        startPos = playerCamera.localPosition;

        // Set cursor state
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        // Update volume of footsteps sound based on player preferences
        footsteps.volume = PlayerPrefs.GetFloat("SFX");

        // Update mouse input and movement
        sensitivity = PlayerPrefs.GetFloat("sensitivity");
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        // Update mouse look based on input
        Vector2 targetMouseDelta = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        // Adjust camera rotation based on mouse input
        cameraCap -= currentMouseDelta.y * sensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(currentMouseDelta.x * sensitivity * Vector3.up);
    }

    void UpdateMove()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        // Get movement input and normalize it
        Vector2 targetDir = new((ToggleActions.IsHeld("right") ? 1 : 0) - (ToggleActions.IsHeld("left") ? 1 : 0), (ToggleActions.IsHeld("forward") ? 1 : 0) - (ToggleActions.IsHeld("backward") ? 1 : 0));
        targetDir.Normalize();

        // Smoothly interpolate the current direction towards the target direction
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Update vertical velocity based on gravity
        UpdateVelocityY();

        // Calculate the total velocity vector
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;

        // Perform jumping logic
        PerformJump();

        // Move the character controller
        controller.Move(velocity * Time.deltaTime);

        // Update head bobbing motion and play/pause footsteps sound
        UpdateHeadBobbing(velocity);

        // Perform crouching logic
        PerformCrouch();
    }

    void UpdateVelocityY()
    {
        // Update vertical velocity based on gravity
        if (controller.velocity.y == 0.0f && velocityY > 1.0f) velocityY = -8.0f; 
        if (controller.velocity.y == 0.0f && velocityY < -1.0f) velocityY = 0.0f;
        velocityY += gravity * 2.0f * Time.deltaTime;
    }

    void PerformJump()
    {
        // Perform jumping logic
        if (isGrounded && ToggleActions.IsHeld("jump")) velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        if (Physics.CheckSphere(groundCheck.position, 0.2f, jumpBlock)) velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
    }

    void UpdateHeadBobbing(Vector3 velocity)
    {
        // Update head bobbing motion and play/pause footsteps sound
        if (Vector2.Distance(new Vector2(controller.velocity.x, controller.velocity.z), Vector2.zero) > 0.3f && isGrounded)
        {
            playerCamera.localPosition += new Vector3(Mathf.Cos(Time.time * frequency / 2.0f) * bobbingX * 2.0f, Mathf.Sin(Time.time * frequency) * bobbingY, 0.0f);
            footsteps.UnPause();
        }
        else
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, startPos, Time.deltaTime);
            footsteps.Pause();
        }
    }

    void PerformCrouch()
    {
        // Perform crouching logic
        if (ToggleActions.IsHeld("crouch"))
        {
            isCrouched = true;
            speed = 2.0f;
        }

        if (ToggleActions.IsUnpressed("crouch"))
        {
            isCrouched = false;
            speed = 6.0f;
        }

        if (isCrouched && controller.height > 1.0f) controller.height -= 0.05f;

        if (!isCrouched && controller.height < 2.0f) controller.height += 0.05f;
    }
}
