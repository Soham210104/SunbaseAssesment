using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UImanager : MonoBehaviour
{
    public TMP_Text detailsText; // Assign this in the inspector
    private DataManager dataManager;
    public GameObject panel;
    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        if (dataManager == null)
        {
            Debug.LogError("DataManager not found in the scene.");
        }
    }

    public void DisplayClientDetails(string clientName)
    {
        if (panel != null)
        {
            panel.SetActive(true); // Enable the panel when a client button is clicked
        }
        DataManager.Client client = dataManager.GetClientByName(clientName);
        DataManager.ClientDetail detail = dataManager.GetClientDetail(client.id);

        if (detail != null)
        {
            // Update the TextMeshPro text with the client's details
            detailsText.text = $"Name: {detail.name}\nPoints: {detail.points}\nAddress: {detail.address}";
        }
        else
        {
            detailsText.text = "Client details not found.";
        }
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
