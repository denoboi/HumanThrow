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

    public float ForceAmount { get; set; }
    
    public void Initialize()
    {
        //Direction = direction;
        //Rigidbody.isKinematic = true;
        //transform.localScale = new Vector3(0.01f, .01f, .01f);
        RagdollController.EnableRagdollWithForce(Vector3.forward, 1200);
        OnInitialized.Invoke();
    }

   
}
