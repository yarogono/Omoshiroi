using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncModule : MonoBehaviour
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int State
    {
        get { return State; }
        set
        {
            if (State.Equals(value))
                return;

            State = value;
        }
    }

    StatInfo _statInfo = new StatInfo();
    public StatInfo StatInfo
    {
        get { return _statInfo; }
        set
        {
            if (_statInfo.Equals(value))
                return;

            _statInfo.Level = value.Level;
            _statInfo.Hp = value.Hp;
            _statInfo.MaxHp = value.MaxHp;
            _statInfo.Attack = value.Attack;
            _statInfo.Speed = value.Speed;
        }
    }

    public int Level
    {
        get { return StatInfo.Level; }
        set { StatInfo.Level = value; }
    }

    public float Speed
    {
        get { return StatInfo.Speed; }
        set { StatInfo.Speed = value; }
    }

    public int Attack
    {
        get { return StatInfo.Attack; }
        set { StatInfo.Attack = value; }
    }

    public int Hp
    {
        get { return StatInfo.Hp; }
        set { StatInfo.Hp = value; }
    }

    PositionInfo _posInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _posInfo; }
        set
        {
            if (_posInfo.Equals(value))
                return;

            _posInfo.PosX = value.PosX;
            _posInfo.PosY = value.PosY;
            _posInfo.PosZ = value.PosZ;
        }
    }

    VelocityInfo _velInfo = new VelocityInfo();
    public VelocityInfo VelInfo
    {
        get { return _velInfo; }
        set
        {
            if (_velInfo.Equals(value))
                return;

            VelInfo.VelX = value.VelX;
            VelInfo.VelY = value.VelY;
            VelInfo.VelZ = value.VelZ;
        }
    }

    public TextMeshPro checkIdTest;
    public TextMeshPro checkPositionX;
    public TextMeshPro checkPositionY;
    public TextMeshPro checkPositionZ;
    public TextMeshPro checkStateTest;

    public void DrawTestInfo()
    {
        checkIdTest.text = $"ID : {Id}";
        checkPositionX.text = $"Ser_X : {PosInfo.PosX} | Cli_X : {transform.position.x}";
        checkPositionY.text = $"Ser_Y : {PosInfo.PosY} | Cli_Y : {transform.position.y}";
        checkPositionZ.text = $"Ser_Z : {PosInfo.PosZ} | Cli_Z : {transform.position.z}";
        // checkStateTest.text = $"State : {ObjectInfo.State}";
    }

    protected virtual void Update()
    {
        DrawTestInfo();
    }
}
