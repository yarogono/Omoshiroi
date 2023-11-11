using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncModule : MonoBehaviour
{
    CharacterStats stats;

    public int Id { get; set; }

    public string Name { get; set; }

    int _state;
    public int State
    {
        get { return _state; }
        set
        {
            if (_state.Equals(value))
                return;

            _state = value;
        }
    }

    float _animTime;
    public float AnimTime
    {
        get { return _animTime; }
        set
        {
            if (_animTime.Equals(value))
                return;

            _animTime = value;
        }
    }

    int _comboIndex;
    public int ComboIndex
    {
        get { return _comboIndex; }
        set
        {
            if (_comboIndex.Equals(value))
                return;

            _comboIndex = value;
        }
    }

    public StatInfo StatInfo
    {
        set
        {
            stats.Level = value.Level;
            stats.Hp = value.Hp;
            stats.MaxHp = value.MaxHp;
            stats.AtkPower = value.Attack;
            stats.MoveSpeed = value.Speed;
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

            _posInfo.PosX = value.PosX;
            _posInfo.PosY = value.PosY;
            _posInfo.PosZ = value.PosZ;
        }
    }

    public Vector3 PosInfoToVec3
    {
        get { return new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ); }
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

    public Vector3 ToVector3(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }

    public TextMeshPro TestText1;
    public TextMeshPro TestText2;
    public TextMeshPro TestText3;
    public TextMeshPro TestText4;
    public TextMeshPro TestText5;
    public GameObject healthPointBar;

    private void Start()
    {
        stats = gameObject.GetComponent<DataContainer>().Stats;
    }

    protected virtual void Update()
    {
        DrawInfo();
    }

    public void DrawInfo()
    {
        TestText1.text = $"PlayerName : {Name}";
        TestText2.text = $"Level : {stats.Level}";
        TestText3.text = $"Hp / MaxHP: {stats.Hp} / {stats.MaxHp} | {stats.Hp / stats.MaxHp}";
        TestText4.text = $"AtkPower : {stats.AtkPower}";
        TestText5.text = $"State : {State}";

        healthPointBar.GetComponent<RectTransform>().localScale = new Vector3(
            50.0f / stats.MaxHp,
            0,
            0
        );
    }
}
