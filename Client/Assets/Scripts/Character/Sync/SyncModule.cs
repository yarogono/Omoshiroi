using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncModule : MonoBehaviour
{
    CharacterStats stats;

    public int Id { get; set; }

    private string _name = string.Empty;
    public string Name
    {
        get { return _name; }
        set
        {
            if (!_name.Equals(string.Empty))
                return;

            _name = value;
        }
    }

    private int _state;
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

    private float _animTime;
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

    private int _comboIndex;
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

    private readonly StatInfo _statInfo = new();
    public StatInfo StatInfo
    {
        get { return _statInfo; }
        set
        {
            _statInfo.Level = value.Level;
            _statInfo.Hp = value.Hp;
            _statInfo.MaxHp = value.MaxHp;
            _statInfo.Attack = value.Attack;
            _statInfo.Speed = value.Speed;
        }
    }

    private readonly PositionInfo _posInfo = new();
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

    private readonly VelocityInfo _velInfo = new();
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

    public RectTransform healthBar;

    public TextMeshPro TestText1;
    public TextMeshPro TestText2;
    public TextMeshPro TestText3;
    public TextMeshPro TestText4;
    public TextMeshPro TestText5;

    private void Start()
    {
        stats = gameObject.GetComponent<DataContainer>().Stats;

        UpdateStats();
    }

    protected virtual void Update()
    {
        DrawInfo();
    }

    private void UpdateStats()
    {
        stats.Level = StatInfo.Level;
        stats.MaxHp = StatInfo.MaxHp;
        stats.Hp = StatInfo.Hp;
        stats.AtkPower = StatInfo.Attack;
        stats.MoveSpeed = StatInfo.Speed;
    }

    private void DrawInfo()
    {
        TestText1.text = $"PlayerName : {Name}";
        TestText2.text = $"Level : {stats.Level}";
        TestText3.text = $"Hp / MaxHP : {stats.Hp} / {stats.MaxHp}";
        TestText4.text = $"AtkPower : {stats.AtkPower}";
        TestText5.text = $"State : {State}";

        // healthBar.sizeDelta = new Vector2(stats.Hp / stats.MaxHp, 0);

        healthBar.localScale = new Vector3(stats.Hp / stats.MaxHp, 1, 1);
    }
}
