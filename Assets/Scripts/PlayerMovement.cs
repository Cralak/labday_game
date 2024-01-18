using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30.0f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask jumpBlock;

    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    AudioSource footsteps;
    float sensitivity;
    float speed = 6.0f;
    float jumpHeight = 6.0f;
    float velocityY;
    bool isGrounded;
    bool isCrouched = false;
    float cameraCap;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("SFX")) PlayerPrefs.SetFloat("SFX", 1);
        if (!PlayerPrefs.HasKey("sensitivity")) PlayerPrefs.SetFloat("sensitivity", 5.0f);
    }

    void Start()
    {
        footsteps = GetComponent<AudioSource>();
        footsteps.Play();

        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        footsteps.volume = PlayerPrefs.GetFloat("SFX");
        sensitivity = PlayerPrefs.GetFloat("sensitivity");
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * sensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2.0f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (Vector2.Distance(new Vector2(velocity.x, velocity.z), Vector2.zero) > 0.3f && isGrounded)
        {
            footsteps.UnPause();
        }
        else
        {
            footsteps.Pause();
        }

        if (Physics.CheckSphere(groundCheck.position, 0.2f, jumpBlock))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        if (isGrounded! && controller.velocity.y < -1.0f)
        {
            velocityY = -8.0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = true;
            speed = 0.3f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouched = false;
            speed = 0.6f;
        }

        if (isCrouched && controller.height > 1.0f)
        {
            controller.height -= 0.05f;
        }

        if (!isCrouched && controller.height < 2.0f)
        {
            controller.height += 0.05f;
        }
    }
}
