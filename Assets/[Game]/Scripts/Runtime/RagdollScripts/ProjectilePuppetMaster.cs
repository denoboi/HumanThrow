using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class ProjectilePuppetMaster : MonoBehaviour
{
    private bool _isRagdollActivated;
    private RootMotion.Dynamics.PuppetMaster _puppetMaster;

    public PuppetMaster PuppetMaster =>
        _puppetMaster == null ? _puppetMaster = GetComponent<PuppetMaster>() : _puppetMaster;

    private Projectile _projectile;

    private Projectile Projectile =>
        _projectile == null ? _projectile = GetComponentInParent<Projectile>() : _projectile;
    
    private void OnEnable()
    {
        Projectile.OnInitialized.AddListener(ActivateRagdoll);
    }

    private void OnDisable()
    {
        Projectile.OnInitialized.RemoveListener(ActivateRagdoll);
    }

    private void ActivateRagdoll()
    {
        if (PuppetMaster.mode == PuppetMaster.Mode.Disabled)
            PuppetMaster.mode = RootMotion.Dynamics.PuppetMaster.Mode.Active;

        _isRagdollActivated = true;
        
        PuppetMaster.state = PuppetMaster.State.Dead;
    }
}
