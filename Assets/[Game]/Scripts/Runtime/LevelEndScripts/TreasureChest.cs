using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      Interactor interactor = other.GetComponentInParent<Interactor>();

      if (interactor != null)
      {
         Player.Instance.IsWin = true;
         GameManager.Instance.OnStageSuccess.Invoke();
         
      }
   }
}
