using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Class representing the User Interface Manager.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject masterMenu;

    [SerializeField] private GameObject weaponsMenu;

    [SerializeField] private GameObject pointsMenu;

    [SerializeField] private GameObject instrumentsMenu;

    [SerializeField] private TMP_Text instrumentsListText;

    [SerializeField] private PlayerController playerController;

    [SerializeField] private TMP_Text interactionsText;

    private List<string> instruments = new List<string>();

    private void Start()
    {
        CloseMenu();
        ChangeMenuState((int)MenuState.Master);
    }

    public bool IsMenuOpen()
    {
        return masterMenu.activeInHierarchy;
    }

    public void DropItemInBox(string boxName)
    {
        if (string.IsNullOrEmpty(boxName))
        {
            interactionsText.text = "";
            return;
        }

        interactionsText.text = "You have dropped in box " + boxName;
    }

    public void AddInstrument(string instrument_name)
    {
        instruments.Add(instrument_name);

        string _newText = "Instruments:\n";

        foreach (string item in instruments)
        {
            _newText += item + "\n";
        }

        instrumentsListText.text = _newText;

        ChangeMenuState((int)MenuState.Instruments);

    }

    public void OpenMenu()
    {
        masterMenu.SetActive(true);
        playerController.Freeze();
    }

    public void CloseMenu()
    {
        masterMenu.SetActive(false);
        playerController.UnFreeze();

    }

    public void ClearInterationsText()
    {
        interactionsText.text = "";
    }

    /// <summary>
    /// Changes the menu state.
    /// </summary>
    /// <param name="new_state">
    /// The new state of the menu.
    /// 0 - Master Menu
    /// 1 - Weapons Menu
    /// 2 - Points Menu
    /// 3 - Instruments Menu
    /// </param>
    public void ChangeMenuState(int new_state)
    {
        weaponsMenu.SetActive(false);
        pointsMenu.SetActive(false);
        instrumentsMenu.SetActive(false);

        if (new_state == 1) weaponsMenu.SetActive(true);
        if (new_state == 2) pointsMenu.SetActive(true);
        if (new_state == 3) instrumentsMenu.SetActive(true);
    }
}
// Turns out UnityEvents doesn't support enums but still used for readabilit
// even though it requires a conversion to int
public enum MenuState { Master, Weapons, Points, Instruments }