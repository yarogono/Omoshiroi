using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncModule : MonoBehaviour
{
    public int Id { get; set; }

    public ObjectInfo _player = new ObjectInfo();
    public ObjectInfo Player
    {
        get { return _player; }
        set
        {
            if (_player.Equals(value))
                return;

            _player.ObjectId = value.ObjectId;
            _player.Name = value.Name;
            _player.PosInfo = value.PosInfo;
            _player.StatInfo = value.StatInfo;
            _player.State = value.State;
            _player.AnimTime = value.AnimTime;
            _player.VelInfo = value.VelInfo;
        }
    }

    PositionInfo _posInfo = new PositionInfo();
    public PositionInfo PosInfo
    {
        get { return _posInfo; }
        set
        {
            if (_posInfo.Equals(value))
                return;

            _player.PosInfo.PosX = value.PosX;
            _player.PosInfo.PosY = value.PosY;
            _player.PosInfo.PosZ = value.PosZ;
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
        checkPositionX.text = $"Ser_X : {Player.PosInfo.PosX} | Cli_X : {transform.position.x}";
        checkPositionY.text = $"Ser_Y : {Player.PosInfo.PosY} | Cli_Y : {transform.position.y}";
        checkPositionZ.text = $"Ser_Z : {Player.PosInfo.PosZ} | Cli_Z : {transform.position.z}";
        // checkStateTest.text = $"State : {ObjectInfo.State}";
    }

    protected virtual void Update()
    {
        DrawTestInfo();
    }
}
