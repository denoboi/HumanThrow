using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    private Projectile _projectile;
    
    public Projectile Projectile
    {
        get { return _projectile == null ? _projectile = GetComponentInChildren<Projectile>() : _projectile; }
    }
    private void OnEnable()
    {
       

    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
     
        
    }

   
    
   
}
