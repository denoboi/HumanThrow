using System;
using System.Collections;
using System.Collections.Generic;
using HCB.PoolingSystem;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInChildren<Rigidbody>() : _rigidbody;

    private RagdollController _ragdollController;

    public RagdollController RagdollController => _ragdollController == null
        ? _ragdollController = GetComponentInChildren<RagdollController>()
        : _ragdollController;
    
    [HideInInspector]
    public UnityEvent OnInitialized = new UnityEvent();

    
    public Vector3 Direction { get; private set; }
    
    
    public void Initialize(Vector3 direction)
    {
        Direction = direction;
        Rigidbody.isKinematic = true;
        RagdollController.EnableRagdollWithForce(Vector3.forward, 2000);
        OnInitialized.Invoke();
    }

   
}
