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
            _player.Position = value.Position;
            _player.StatInfo = value.StatInfo;
            _player.State = value.State;
            _player.AnimTime = value.AnimTime;
            _player.Velocity = value.Velocity;
        }
    }

    P_Vector3 _position = new P_Vector3();
    public P_Vector3 Position
    {
        get { return _position; }
        set
        {
            if (_position.Equals(value))
                return;

            _player.Position.X = value.X;
            _player.Position.Y = value.Y;
            _player.Position.Z = value.Z;
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
        checkPositionX.text = $"Ser_X : {Player.Position.X} | Cli_X : {transform.position.x}";
        checkPositionY.text = $"Ser_Y : {Player.Position.Y} | Cli_Y : {transform.position.y}";
        checkPositionZ.text = $"Ser_Z : {Player.Position.Z} | Cli_Z : {transform.position.z}";
        // checkStateTest.text = $"State : {ObjectInfo.State}";
    }

    protected virtual void Update()
    {
        DrawTestInfo();
    }
}
