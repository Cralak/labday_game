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
    float jumpHeight = 6.0f;

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
        Vector2 targetDir = new((IsPressed("right") ? 1 : 0) - (IsPressed("left") ? 1 : 0), (IsPressed("forward") ? 1 : 0) - (IsPressed("backward") ? 1 : 0));
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
        if (controller.velocity.y == 0.0f && velocityY < -1.0f) velocityY = 0.0f;
        velocityY += gravity * 2.0f * Time.deltaTime;
    }

    void PerformJump()
    {
        // Perform jumping logic
        if (isGrounded && IsPressed("jump")) velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        if (Physics.CheckSphere(groundCheck.position, 0.2f, jumpBlock)) velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        if (!isGrounded && controller.velocity.y < -1.0f) velocityY = -8.0f;
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
        if (IsPressed("crouch"))
        {
            isCrouched = true;
            speed = 3.0f;
        }

        if (IsUnpressed("crouch"))
        {
            isCrouched = false;
            speed = 6.0f;
        }

        if (isCrouched && controller.height > 1.0f) controller.height -= 0.05f;

        if (!isCrouched && controller.height < 2.0f) controller.height += 0.05f;
    }

    bool IsPressed(string key)
    {
        // Detect if key is pressed 
        switch (PlayerPrefs.GetString(key))
        {
            case "escape":
                if (Input.GetKey(KeyCode.Escape))
                {
                    return true;
                }
                break;
            case "tab":
                if (Input.GetKey(KeyCode.Tab))
                {
                    return true;
                }
                break;
            case "lock":
                if (Input.GetKey(KeyCode.CapsLock))
                {
                    return true;
                }
                break;
            case "backspace":
                if (Input.GetKey(KeyCode.Backspace))
                {
                    return true;
                }
                break;
            case "return":
                if (Input.GetKey(KeyCode.Return))
                {
                    return true;
                }
                break;
            case "space":
                if (Input.GetKey(KeyCode.Space))
                {
                    return true;
                }
                break;
            case "shift":
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    return true;
                }
                break;
            case "alt":
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    return true;
                }
                break;
            case "control":
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    return true;
                }
                break;
            case "meta":
                if (Input.GetKey(KeyCode.LeftMeta) || Input.GetKey(KeyCode.RightMeta))
                {
                    return true;
                }
                break;
            case "upArrow":
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    return true;
                }
                break;
            case "downArrow":
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    return true;
                }
                break;
            case "leftArrow":
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    return true;
                }
                break;
            case "rightArrow":
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    return true;
                }
                break;
            case "leftClick":
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    return true;
                }
                break;
            case "rightClick":
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    return true;
                }
                break;
            case "wheelClick":
                if (Input.GetKey(KeyCode.Mouse2))
                {
                    return true;
                }
                break;
            default:
                if (Input.GetKey(PlayerPrefs.GetString(key)))
                {
                    return true;
                }
                break;
        }
        return false;
    }

    bool IsUnpressed(string key)
    {
        // Toggle the inventory on/off with the inventory key
        switch (PlayerPrefs.GetString(key))
        {
            case "escape":
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    return true;
                }
                break;
            case "tab":
                if (Input.GetKeyUp(KeyCode.Tab))
                {
                    return true;
                }
                break;
            case "lock":
                if (Input.GetKeyUp(KeyCode.CapsLock))
                {
                    return true;
                }
                break;
            case "backspace":
                if (Input.GetKeyUp(KeyCode.Backspace))
                {
                    return true;
                }
                break;
            case "return":
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    return true;
                }
                break;
            case "space":
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    return true;
                }
                break;
            case "shift":
                if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
                {
                    return true;
                }
                break;
            case "alt":
                if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.RightAlt))
                {
                    return true;
                }
                break;
            case "control":
                if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
                {
                    return true;
                }
                break;
            case "meta":
                if (Input.GetKeyUp(KeyCode.LeftMeta) || Input.GetKeyUp(KeyCode.RightMeta))
                {
                    return true;
                }
                break;
            case "upArrow":
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    return true;
                }
                break;
            case "UpArrow":
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    return true;
                }
                break;
            case "leftArrow":
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    return true;
                }
                break;
            case "rightArrow":
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    return true;
                }
                break;
            case "leftClick":
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    return true;
                }
                break;
            case "rightClick":
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    return true;
                }
                break;
            case "wheelClick":
                if (Input.GetKeyUp(KeyCode.Mouse2))
                {
                    return true;
                }
                break;
            default:
                if (Input.GetKeyUp(PlayerPrefs.GetString(key)))
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
