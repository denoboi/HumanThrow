using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class IncomeManager : IdleStatObjectBase
{
    private float IncomeRate;
    public override void UpdateStat(string id)
    {
        IncomeRate = (float)IdleStat.CurrentValue;
    }
}
