using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncModule : MonoBehaviour
{
    public int Id { get; set; }

    protected bool _updated = false;

    public ObjectInfo _objectInfo = new ObjectInfo();
    public ObjectInfo ObjectInfo
    {
        get { return _objectInfo; }
        set
        {
            if (_objectInfo.Equals(value))
                return;

            _objectInfo.Position = P_Vector3;
        }
    }

    P_Vector3 _pVector3 = new P_Vector3();
    public P_Vector3 P_Vector3
    {
        get { return _pVector3; }
        set
        {
            if (_pVector3.Equals(value))
                return;

            position = new Vector3(value.X, value.Y, value.Z);
        }
    }

    public Vector3 position
    {
        get { return new Vector3(_pVector3.X, _pVector3.Y, _pVector3.Z); }
        set
        {
            if (_pVector3.X == value.x && _pVector3.Y == value.y && _pVector3.Z == value.z)
                return;

            _pVector3.X = value.x;
            _pVector3.Y = value.y;
            _pVector3.Z = value.z;
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
        checkPositionX.text = $"Ser_X : {_pVector3.X} | Cli_X : {transform.position.x}";
        checkPositionY.text = $"Ser_Y : {_pVector3.Y} | Cli_Y : {transform.position.y}";
        checkPositionZ.text = $"Ser_Z : {_pVector3.Z} | Cli_Z : {transform.position.z}";
        // checkStateTest.text = $"State : {ObjectInfo.State}";
    }

    protected virtual void Update()
    {
        DrawTestInfo();
    }
}
