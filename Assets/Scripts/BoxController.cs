using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Represents a controller for a box object that can be interacted with.
/// </summary>
public class BoxController : MonoBehaviour, IInteractable
{
    [SerializeField] private string boxName;
    [SerializeField] private UnityEvent eventOnInteraction;

    public void Interact()
    {
        eventOnInteraction.Invoke();

        GameManagerSingleton.instance.DropInBox(boxName);

    }
}
