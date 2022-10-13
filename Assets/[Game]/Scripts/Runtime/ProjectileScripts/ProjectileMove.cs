using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    private Projectile _projectile;
    public Projectile Projectile
    {
        get { return _projectile == null ? _projectile = GetComponentInChildren<Projectile>() : _projectile; }
    }

    private bool _canShoot = true;
    [SerializeField] private float _speed = 50f;

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        if (!_canShoot) return;
        
        transform.Translate( Projectile.Direction * _speed * Time.deltaTime);
       
        
    }
    
    
    
}
