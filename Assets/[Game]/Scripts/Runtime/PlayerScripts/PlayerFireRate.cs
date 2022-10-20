using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class PlayerFireRate : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;   
    public float FireRate { get; set; }
    
    //private float InitialFireRate => (float)GameManager.Instance.UpgradeData.FireRateStat.CurrentValue;

    private const float MAX_FIRE_RATE = 100f;
    private const float INCREASE_AMOUNT = 1f;


    private void OnEnable()
    {
        HCB.Core.EventManager.OnFireRateGateInteracted.AddListener(IncreaseFireRate);

    }

    private void OnDisable()
    {
        HCB.Core.EventManager.OnFireRateGateInteracted.RemoveListener(IncreaseFireRate);

    }

    private void IncreaseFireRate()
    {
        FireRate += INCREASE_AMOUNT;
        FireRate = Mathf.Min(FireRate, MAX_FIRE_RATE);
    }
}
