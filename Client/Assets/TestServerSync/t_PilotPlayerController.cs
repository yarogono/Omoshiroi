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
    }

    private void Update()
    {
        checkIdTest.text = "P_ID : " + Id;
        Debug.Log($"Pilot ID : {Id}");

        if (State == CreatureState.Moving)
        {
            C_Move movePacket = new C_Move();
            movePacket.PosInfo = PosInfo;
            NetworkManager.Instance.Send(movePacket);

            Debug.Log($"C / S / D -> {position}, {State}");
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
        else
        {
            Vector3 destination = rigid.position + nextVec;
            State = CreatureState.Moving;

            position = Vector3Int.FloorToInt(destination);
            rigid.MovePosition(destination);
        }
    }

    private void OnMove(InputValue value)
    {
        inputVec = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }
}
