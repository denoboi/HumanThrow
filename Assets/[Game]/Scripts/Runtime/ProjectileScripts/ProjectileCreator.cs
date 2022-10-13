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


    [SerializeField] private Transform _projectileSpawnPoint;

    public Projectile CurrentProjectile { get; private set; }

   

    public Projectile CreateProjectile()
    {
        if (Player.IsFailed)
            return null;
        
        Projectile projectile = PoolingSystem.Instance.InstantiateAPS(PROJECTILE_POOL_ID, _projectileSpawnPoint.position, Quaternion.identity).GetComponentInChildren<Projectile>();

        projectile.Initialize(Vector3.forward);
        CurrentProjectile = projectile;

        return projectile;
    }
}