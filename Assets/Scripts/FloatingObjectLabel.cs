using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Represents a floating object label that displays text above a target object in the game world.
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class FloatingObjectLabel : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 worldOffset;
    [SerializeField] private Vector3 viewOffset;

    private Camera mainCamera;
    private Vector3 labelScreenPosition;
    private void Start()
    {
        mainCamera = GameManagerSingleton.instance.GetCamera();


        viewOffset.x *= Screen.width;
        viewOffset.y *= Screen.height;
    }

    private void Update()
    {
        if (targetTransform == null)
        {
            SetOffScreenPosition();
            return;
        }

        labelScreenPosition = mainCamera.WorldToScreenPoint(targetTransform.position + worldOffset);

        if (labelScreenPosition.z < 0)
        {
            SetOffScreenPosition();
            return;
        }

        this.transform.position = labelScreenPosition + viewOffset;
    }

    private void SetOffScreenPosition()
    {
        this.transform.position = new Vector3(-1000, 0);
    }

    public void SetTarget(Transform target, Vector3 world_offset, Vector3 view_offset, string text)
    {
        targetTransform = target;
        worldOffset = world_offset;
        viewOffset = view_offset;
        this.GetComponent<TMP_Text>().text = text;
    }
}
