using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    
    private float _timer = 0;

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody>() : _rigidbody;
    private void Start()
    {
        Rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }

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

    private void Update()
    {
        DestroyProjectile();
    }


    private void DestroyProjectile()
    {
        

        _timer += Time.deltaTime;

        if (_timer >= PlayerFireRange.Instance.DestroyTime)
        {
            Rigidbody.constraints = RigidbodyConstraints.None;
            
            _timer = 0;
        }
    }
    
    
   
}
