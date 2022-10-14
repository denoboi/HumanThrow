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
    
    [HideInInspector]
    public UnityEvent OnHit = new UnityEvent();

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
        foreach (var obstacle in _obstaclePieces)
        {
            obstacle.GetComponent<Rigidbody>().isKinematic = false;
            obstacle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)) * 200);
        }
        
        HapticManager.Haptic(HapticTypes.RigidImpact);
    }
    
    
}
