using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DropDownManager : MonoBehaviour
{
    public GameObject[] Clients;

    private DataManager dataManager;
    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>(); // Find the DataManager instance
        if (dataManager == null)
        {
            Debug.LogError("DataManager not found in the scene.");
        }
    }
    public void HandleInputData(int val)
    {
        List<DataManager.Client> clientsToShow = new List<DataManager.Client>();

        if (val == 0) // All clients
        {
            clientsToShow = dataManager.GetAllClients();
            
        }
        else if (val == 1) // Managers
        {
            clientsToShow = dataManager.GetManagers();
            
        }
        else if (val == 2) // Non-Managers
        {
            clientsToShow = dataManager.GetNonManagers();
        }

    }
    
}
