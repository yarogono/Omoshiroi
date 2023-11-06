using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;
using System;

public class PilotSync : SyncModule
{
    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();

        UpdateState();
        SendPacket();
    }

    private void UpdateState()
    {
        if (position != transform.position)
        {
            State = CreatureState.Moving;
        }
        else
        {
            State = CreatureState.Idle;
        }
    }

    private void SendPacket()
    {
        position = transform.position;

        if (State == CreatureState.Moving)
        {
            C_Move movePacket = new C_Move { PosInfo = PosInfo };
            NetworkManager.Instance.Send(movePacket);
        }
    }
}
