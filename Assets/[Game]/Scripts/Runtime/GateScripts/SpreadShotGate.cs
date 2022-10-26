using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class SpreadShotGate : GateBase
{
    private void OnTriggerEnter(Collider other)
    {
        Interactor splineCharacter = other.GetComponentInParent<Interactor>();

        if (splineCharacter != null)
        {
            HapticManager.Haptic(HapticTypes.RigidImpact);

            HCB.Core.EventManager.OnSpreadShotGateInteracted.Invoke();
            OnInteracted.Invoke();
            Debug.Log("SpreadShot");
        }
    }
}
