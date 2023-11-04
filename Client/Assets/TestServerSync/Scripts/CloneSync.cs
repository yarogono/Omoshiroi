using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSync : SyncModule
{
    protected override void Update()
    {
        base.Update();

        SyncPosition();
    }

    public void SyncPosition()
    {
        gameObject.transform.position = new Vector3(PosInfo.PosX, PosInfo.PosY, PosInfo.PosZ);
    }
}
