using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class FireRateUpgrade : IdleStatObjectBase
{
    private PlayerFireRate _playerFireRate;

    public PlayerFireRate PlayerFireRate => _playerFireRate == null
        ? _playerFireRate = GetComponentInParent<PlayerFireRate>()
        : _playerFireRate;
    
    void Start()
    {
        PlayerFireRate.FireRate = (float)IdleStat.CurrentValue;
    }

    public override void UpdateStat(string id)
    {
        PlayerFireRate.FireRate = (float)IdleStat.CurrentValue;
    }

   
}
