using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SyncModule : MonoBehaviour
{
    private CharacterStats stats;

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

    private readonly VelocityInfo _velInfo = new();
    public VelocityInfo VelInfo
    {
        get { return _velInfo; }
        set
        {
            if (_velInfo.Equals(value))
                return;

            _velInfo.VelX = value.VelX;
            _velInfo.VelY = value.VelY;
            _velInfo.VelZ = value.VelZ;
        }
    }

    public Vector3 ToVector3(float x, float y, float z)
    {
        return new Vector3(x, y, z);
    }

    public DirectionInfo _dirInfo = new();
    public DirectionInfo DirInfo
    {
        get { return _dirInfo; }
        set
        {
            if (_dirInfo.Equals(value))
                return;

            _dirInfo.DirX = value.DirX;
            _dirInfo.DirY = value.DirY;
            _dirInfo.DirZ = value.DirZ;
        }
    }

    public RectTransform healthBar;

    public TextMeshPro TestText1;
    public TextMeshPro TestText2;
    public TextMeshPro TestText3;
    public TextMeshPro TestText4;
    public TextMeshPro TestText5;

    protected virtual void Awake()
    {
        stats = gameObject.GetComponent<DataContainer>().Stats;
    }

    protected virtual void Update()
    {
        DrawInfo();
    }

    public void InitCharacterStats()
    {
        // TODO
        // 추후에 ObjectManager.cs TODO 작업이 끝나면
        // PilotSync.cs에서 해당 메서드를 오버라이드 하는 식으로 작업할 예정
        stats.Level = StatInfo.Level;
        stats.MaxHp = StatInfo.MaxHp;
        stats.Hp = StatInfo.Hp;
        stats.Def = 1;
        stats.AtkSpeed = 1;
        stats.AtkPower = StatInfo.Attack;
        stats.CritRate = 1;
        stats.CritPower = 1;
        stats.MoveSpeed = StatInfo.Speed;
        stats.RunMultipiler = 2;
    }

    private void DrawInfo()
    {
        if (stats.Hp != 0 && stats.MaxHp != 0)
        {
            TestText1.text = $"PlayerName : {Name}";
            TestText2.text = $"Level : {stats.Level}";
            TestText3.text = $"Hp / MaxHP : {stats.Hp} / {stats.MaxHp} | {stats.Hp / stats.MaxHp}";
            TestText4.text = $"AtkPower : {stats.AtkPower}";
            TestText5.text = $"State : {State}";

            healthBar.localScale = new Vector3(stats.Hp / stats.MaxHp, 1, 1);
        }
    }
}
