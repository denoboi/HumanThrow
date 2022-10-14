using System;
using System.Collections;
using System.Collections.Generic;
using HCB.PoolingSystem;
using TMPro;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private bool _isCollided;
    
    private void OnTriggerEnter(Collider other)
    {
        ObstacleDestruction breakable = other.GetComponentInParent<ObstacleDestruction>();
        
        
        
        if (breakable != null && !_isCollided)
        {
            _isCollided = true;
          
            breakable.ObstacleLevel--;
            breakable.OnHit.Invoke();
            
        
            
            
            Debug.Log("Carptii");

            if (breakable.ObstacleLevel <= 0)
            {
                breakable.ObstacleLevel = 0;
                breakable.DestructObsacle();
                
                //Destroy(other.gameObject);
            }
        }

    }
}
