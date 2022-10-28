using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ObstacleDestruction : MonoBehaviour, IBreakable
{
    [SerializeField] private GameObject[] _obstaclePieces;
    [SerializeField] private ParticleSystem _destructionParticle;

    public int ObstacleLevel = 3;

    private bool _isDestructed;

    private BoxCollider _collider;
    public BoxCollider Collider => _collider == null ? _collider = GetComponentInChildren<BoxCollider>() : _collider;
    
    [HideInInspector]
    public UnityEvent OnHit = new UnityEvent();
    
    [HideInInspector]
    public UnityEvent OnObstacleDestroyed = new UnityEvent();

    private bool _isCollided { get; set; }
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        foreach (var obstacle in _obstaclePieces)
        {
            obstacle.AddComponent<Rigidbody>().isKinematic = true;
            obstacle.AddComponent<MeshCollider>().convex = true;
        }
    }

    [Button]
    public void DestructObsacle()
    {
        if (_isDestructed)
            return;
        
        _isDestructed = true;
        Collider.enabled = false;
        foreach (var obstacle in _obstaclePieces)
        {
            obstacle.GetComponent<Rigidbody>().isKinematic = false;
            obstacle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)) * 200);
           

        }

        OnObstacleDestroyed.Invoke();
        HapticManager.Haptic(HapticTypes.RigidImpact);
    }
    
    
}
