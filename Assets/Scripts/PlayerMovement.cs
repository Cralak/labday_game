using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float Speed = 6.0f;
    [SerializeField] float jumpHeight = 6f;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask jumpBlock;

    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    AudioSource footsteps;
    float velocityY;
    bool isGrounded;
    bool isOnPad;
    bool isCrouched = false;
    float cameraCap;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("SFX")) PlayerPrefs.SetFloat("SFX", 1);
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
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;

        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;

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
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouched = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouched = false;
        }

        if (isCrouched && controller.height > 1f)
        {
            controller.height -= 0.05f;
        }

        if (!isCrouched && controller.height < 2f)
        {
            controller.height += 0.05f;
        }
    }
}
