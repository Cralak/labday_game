using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{
    [SerializeField, Range(0, 0.1f)] float amplitudeX;
    [SerializeField, Range(0, 0.1f)] float amplitudeY;
    [SerializeField, Range(0, 30)] float frequency;
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform cameraHolder;

    float toggleSpeed = 3.0f;
    Vector3 startPos;
    CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        startPos = playerCamera.localPosition;
    }

    void Update()
    {
        CheckMotion();
        ResetPosition();
        playerCamera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitudeY;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitudeX * 2;
        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        if (speed > toggleSpeed && controller.isGrounded)
        {
            playerCamera.localPosition += FootStepMotion();
        }
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }

    private void ResetPosition()
    {
        if (playerCamera.localPosition != startPos)
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, startPos, 1 * Time.deltaTime);
        }
    }
}
