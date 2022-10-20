using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class FireRateGate : GateBase
{
   private void OnTriggerEnter(Collider other)
   {
      Interactor splineCharacter = other.GetComponentInParent<Interactor>();

      if (splineCharacter != null)
      {
         HapticManager.Haptic(HapticTypes.Selection);

         HCB.Core.EventManager.OnFireRateGateInteracted.Invoke();
         Debug.Log("FireRate");
      }
   }
}
