using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;

public class ProjectileCreator : MonoBehaviour
{
    private Player _player;

    public Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;
    public const string PROJECTILE_POOL_ID = "Human";
    [SerializeField] private Projectile _projectile;

    [SerializeField] private Transform _projectileSpawnPoint;
    

    public Projectile CreateProjectile()
    {
        if (Player.IsFailed || Player.IsWin)
            return null;

        Projectile projectile = Instantiate(_projectile, _projectileSpawnPoint.position, _projectile.transform.rotation)
            .GetComponentInChildren<Projectile>();
        
        projectile.Initialize(Vector3.forward);
        
        return projectile;
    }
}