using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateBase : MonoBehaviour, IInteractable
{
    public bool IsInteracted { get; }

    [HideInInspector]
    public UnityEvent OnInteracted = new UnityEvent();


}

