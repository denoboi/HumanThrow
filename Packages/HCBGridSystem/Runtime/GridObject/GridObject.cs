using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class GridObject : PlaceableBase
    {
        protected override void Awake()
        {
            base.Awake();
            InitializeGridObject();
        }

        protected virtual void InitializeGridObject() 
        {
            IsActive = true;
            CanSelectable = true;         
        }
    }
}
