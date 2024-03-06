using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a player controller that handles player movement, camera rotation, and interaction with objects in the environment.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactDistance = 4f;
    [SerializeField] private float lookSpeed = 2f;
    [SerializeField] private float lookXLimit = 45f;
    [SerializeField] private UIManager mainUIManager;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool canMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!canMove)
            return;

        HandleMovement();
        HandleInteraction();
        HandleUIControls();
    }
    private void HandleUIControls()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            mainUIManager.OpenMenu();
        }
    }

    private void HandleInteraction()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (!Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance, interactableLayer))
            return;

        if (!hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            return;

        interactable.Interact();
    }
    private void HandleMovement()
    {
        // Mouse Control
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Movement
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            return;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * walkSpeed;
        Vector3 right = transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * walkSpeed;

        moveDirection = forward + right + (Vector3.down * gravity);
        characterController.Move(moveDirection * Time.deltaTime);
    }
    public void Freeze()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canMove = false;
    }

    public void UnFreeze()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
    }

    public void LoadCheckpointData(GameManagerSingleton.CheckpointSave checkpoint)
    {
        Quaternion playerRotation = Quaternion.Euler(0, checkpoint.playerCameraRotation.eulerAngles.y, 0);
        Quaternion cameraRotation = checkpoint.playerCameraRotation;

        characterController.Move(Vector3.zero);
        transform.position = checkpoint.playerPosition;
        transform.rotation = playerRotation;
        playerCamera.transform.rotation = cameraRotation;
    }
}