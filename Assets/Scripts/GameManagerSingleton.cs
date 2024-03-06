using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Represents the game manager singleton responsible for managing various game functionalities.
/// </summary>
public class GameManagerSingleton : MonoBehaviour
{
    public static GameManagerSingleton instance { get; private set; }
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private FloatingObjectLabel objectLabelPrefab;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerController playerController;

    private List<FloatingObjectLabel> objectLabels = new List<FloatingObjectLabel>();
    private CheckpointSave checkpoint = new CheckpointSave();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Canvas GetUICanvas()
    {
        return uiCanvas;
    }
    public Camera GetCamera()
    {
        return mainCamera;
    }

    public void CreateCheckpoint()
    {
        checkpoint.playerPosition = playerController.transform.position;
        checkpoint.playerCameraRotation = mainCamera.transform.rotation;

        checkpoint.isMenuOpen = uiManager.IsMenuOpen();
    }

    public void LoadLastCheckpoint()
    {
        playerController.LoadCheckpointData(checkpoint);
        uiManager.ClearInterationsText();
        if (checkpoint.isMenuOpen)
            uiManager.OpenMenu();

    }

    public void PickupInstrument(string instrument_name)
    {
        uiManager.AddInstrument(instrument_name);
        uiManager.OpenMenu();

        CreateCheckpoint();
    }

    public void DropInBox(string box_name)
    {
        uiManager.DropItemInBox(box_name);
    }

    public void CreateFloatingLabel(Transform target_object, Vector3 offset, Vector2 view_offset, string text)
    {
        FloatingObjectLabel label = Instantiate(objectLabelPrefab, uiCanvas.transform);

        label.SetTarget(target_object, offset, view_offset, text);

        objectLabels.Add(label);
    }

    /// <summary>
    /// Represents a checkpoint save data.
    /// </summary>
    public class CheckpointSave
    {
        public Vector3 playerPosition;
        public Quaternion playerCameraRotation;
        public bool isMenuOpen;
    }
}
