using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class PlayerFireRate : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;   
    public float FireRate { get; private set; }
    
    //private float InitialFireRate => (float)GameManager.Instance.UpgradeData.FireRateStat.CurrentValue;

    private const float MAX_FIRE_RATE = 5f;
    private const float MIN_FIRE_RATE = 0.35f;
    
    
}
