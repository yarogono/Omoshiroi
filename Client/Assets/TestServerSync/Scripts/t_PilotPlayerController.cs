using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class t_PilotPlayerController : t_PlayerController
{
    bool _moveKeyPressed = false;
    public Vector3 inputVec;
    public float speed;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        transform.position = Vector3.zero;
    }

    protected override void Update()
    {
        base.Update();

        position = transform.position;

        if (State == CreatureState.Moving)
        {
            C_Move movePacket = new C_Move { PosInfo = PosInfo };
            NetworkManager.Instance.Send(movePacket);
        }
    }

    private void FixedUpdate()
    {
        Vector3 nextVec = speed * Time.deltaTime * inputVec.normalized;

        if (nextVec == Vector3.zero)
        {
            State = CreatureState.Idle;
            return;
        }

        Vector3 destination = rigid.position + nextVec;
        State = CreatureState.Moving;

        rigid.MovePosition(destination);
    }

    private void OnMove(InputValue value)
    {
        inputVec = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }
}
