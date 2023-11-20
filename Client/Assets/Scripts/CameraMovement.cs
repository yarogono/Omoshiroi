using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : CustomSingleton<CameraMovement>
{
    public Vector3 Offset;
    public Vector3 Rotate;
    [Range(0.5f, 20f)] public float Speed;
    private Vector3 _dampingVelocity;

    private Transform _target;

    public void AttachToPlayer(Transform player)
    {
        _target = player;
    }
    private void Awake()
    {

    }
    private void Start()
    {
        transform.Rotate(Rotate);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _target.position + Offset, ref _dampingVelocity, 1 / Speed);
    }
}
