using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_CreatureController : t_BaseController
{
    StatInfo _stat = new StatInfo();

    public override StatInfo Stat
    {
        get { return base.Stat; }
        set { base.Stat = value; }
    }

    public override int Hp
    {
        get { return base.Stat.Hp; }
        set { base.Stat.Hp = value; }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        UpdateController();
    }

    protected override void Init()
    {
        base.Init();
    }
}
