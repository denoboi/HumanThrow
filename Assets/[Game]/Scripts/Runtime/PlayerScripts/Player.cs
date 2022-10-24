using System;
using System.Collections;
using System.Collections.Generic;
using HCB.SplineMovementSystem;
using UnityEngine;

public class Player : SplineCharacter
{
    
    public bool IsFailed { get; set; }
    public bool IsWin { get; set; }
    
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }
}
