using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class t_PlayerController : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        C_EnterGame enterGamePacket = new C_EnterGame { Player = new ObjectInfo() };
        enterGamePacket.Player.Name = "test";
        enterGamePacket.Player.PosInfo = new PositionInfo() { PosX = 0, PosY = 0 };
        enterGamePacket.Player.StatInfo = null;

        NetworkManager.Instance.Send(enterGamePacket);
    }

    private void FixedUpdate()
    {
        Vector3 nextVec = speed * Time.deltaTime * inputVec.normalized;
        rigid.MovePosition(rigid.position + nextVec);

        // TODO
        // 서버로 목적지 Pos 패킷 보내기
    }

    private void OnMove(InputValue value)
    {
        inputVec = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }
}
