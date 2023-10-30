using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float DampingTime = 0.3f;

    private Vector3 currentVelocity;
    private float verticalVelocity;
    private bool _isKnockout = false;

    public Vector3 Movement { get; private set; }
    public Vector3 Impact { get; private set; }
    public Vector3 ControlDirection { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Impact = Vector3.SmoothDamp(Impact, Vector3.zero, ref currentVelocity, DampingTime);
        if (!_controller.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        else
        {

        }

    }
    public void Reset()
    {
        Impact = Vector3.zero;
        verticalVelocity = 0f;
        ControlDirection = Vector3.zero;
    }

    public void AddImpact(Vector3 impact)
    {
        Impact += impact;
    }

    public void ControlMovement(Vector3 control)
    {
        ControlDirection = control;
    }
}
