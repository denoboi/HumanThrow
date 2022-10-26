using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using HCB.Utilities;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private IdleStat _fireRate;
    [SerializeField] private IdleStat _fireRange;
    [SerializeField] private IdleStat _income;

    public IdleStat FireRate => _fireRate;
    public IdleStat FireRange => _fireRange;
    public IdleStat Income => _income;
    





}
