using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncModule : MonoBehaviour
{
    public int Id { get; set; }

    protected bool _updated = false;

    ObjectInfo _objectInfo = new ObjectInfo();
    public ObjectInfo ObjectInfo
    {
        get { return _objectInfo; }
        set
        {
            if (_objectInfo.Equals(value))
                return;

            position = new Vector3(value.Position.X, value.Position.Y, value.Position.Z);
        }
    }

    public Vector3 position
    {
        get
        {
            return new Vector3(ObjectInfo.Position.X, ObjectInfo.Position.Y, ObjectInfo.Position.Z);
        }
        set
        {
            if (
                ObjectInfo.Position.X == value.x
                && ObjectInfo.Position.Y == value.y
                && ObjectInfo.Position.Z == value.z
            )
                return;

            ObjectInfo.Position.X = value.x;
            ObjectInfo.Position.Y = value.y;
            ObjectInfo.Position.Z = value.z;
            _updated = true;
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
        checkPositionX.text = $"Ser_X : {ObjectInfo.Position.X} | Cli_X : {transform.position.x}";
        checkPositionY.text = $"Ser_Y : {ObjectInfo.Position.Y} | Cli_Y : {transform.position.y}";
        checkPositionZ.text = $"Ser_Z : {ObjectInfo.Position.Z} | Cli_Z : {transform.position.z}";
        checkStateTest.text = $"State : {ObjectInfo.State}";
    }

    protected virtual void Update()
    {
        DrawTestInfo();
    }
}
