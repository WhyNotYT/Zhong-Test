using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The `SphereController` class is responsible for controlling a sphere object in the game.
/// </summary>
public class SphereController : MonoBehaviour, IInteractable
{
    [SerializeField] private string labelText;
    [SerializeField] private Vector3 floatingLabelWorldOffset;
    [SerializeField] private Vector2 floatingLabelViewOffset;
    [SerializeField] private UnityEvent eventOnInteraction;

    private void Start()
    {
        GameManagerSingleton.instance.CreateFloatingLabel(this.transform, floatingLabelWorldOffset, floatingLabelViewOffset, labelText);
    }

    public void Interact()
    {
        eventOnInteraction.Invoke();
        GameManagerSingleton.instance.PickupInstrument(gameObject.name);
    }
}
