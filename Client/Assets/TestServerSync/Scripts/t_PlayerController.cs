using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class t_PlayerController : MonoBehaviour
{
    public int Id { get; set; }

    StatInfo _stat = new StatInfo();
    public virtual StatInfo Stat
    {
        get { return _stat; }
        set
        {
            if (_stat.Equals(value))
                return;

            _stat.Speed = value.Speed;
        }
    }

    public float Speed
    {
        get { return Stat.Speed; }
        set { Stat.Speed = value; }
    }

    protected bool _updated = false;

    PositionInfo _positionInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _positionInfo; }
        set
        {
            if (_positionInfo.Equals(value))
                return;

            position = new Vector3(value.PosX, value.PosY, value.PosZ);
            State = value.State;
        }
    }

    public Vector3 position
    {
        get { return new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ); }
        set
        {
            if (PosInfo.PosX == value.x && PosInfo.PosY == value.y && PosInfo.PosZ == value.z)
                return;

            PosInfo.PosX = value.x;
            PosInfo.PosY = value.y;
            PosInfo.PosZ = value.z;
            _updated = true;
        }
    }

    public virtual CreatureState State
    {
        get { return PosInfo.State; }
        set
        {
            if (PosInfo.State == value)
                return;

            PosInfo.State = value;
            _updated = true;
        }
    }

    public TextMeshPro checkIdTest;
    public TextMeshPro checkPosXTest;
    public TextMeshPro checkPosYTest;
    public TextMeshPro checkPosZTest;
    public TextMeshPro checkStateTest;

    public void DrawTestInfo()
    {
        checkIdTest.text = $"ID : {Id}";
        checkPosXTest.text = $"Ser_X : {PosInfo.PosX} | Cli_X : {transform.position.x}";
        checkPosYTest.text = $"Ser_Y : {PosInfo.PosY} | Cli_Y : {transform.position.y}";
        checkPosZTest.text = $"Ser_Z : {PosInfo.PosZ} | Cli_Z : {transform.position.z}";
        checkStateTest.text = $"State : {PosInfo.State}";
    }

    protected virtual void Update()
    {
        DrawTestInfo();
    }
}
