using System;
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
        //List<DataManager.Client> clientsToShow = new List<DataManager.Client>();

        //if (val == 0) // All clients
        //{
        //    clientsToShow = dataManager.GetAllClients();

        //}
        //else if (val == 1) // Managers
        //{
        //    clientsToShow = dataManager.GetManagers();

        //}
        //else if (val == 2) // Non-Managers
        //{
        //    clientsToShow = dataManager.GetNonManagers();
        //}
        List<DataManager.Client> clientsToShow = new List<DataManager.Client>();

        if (val == 0) // All clients
        {
            clientsToShow = dataManager.GetAllClients();
            ShowAllClients();
        }
        else if (val == 1) // Managers
        {
            clientsToShow = dataManager.GetManagers();
            ShowRelevantClientButtons(clientsToShow, true);
        }
        else if (val == 2) // Non-Managers
        {
            clientsToShow = dataManager.GetNonManagers();
            ShowRelevantClientButtons(clientsToShow, false);
        }

    }


    private void ShowAllClients()
    {
        foreach (GameObject clientButton in Clients)
        {
            // Enable all client buttons
            clientButton.SetActive(true);
        }
    }

    private void ShowRelevantClientButtons(List<DataManager.Client> clientsToShow, bool isManager)
    {
        foreach (GameObject clientButton in Clients)
        {
            // Initially disable all client buttons
            clientButton.SetActive(false);
        }

        foreach (var client in clientsToShow)
        {
            if (client.isManager == isManager)
            {
                // Find the client button GameObject by name
                GameObject clientButton = Array.Find(Clients, item => item.name == client.label);
                if (clientButton != null)
                {
                    // Show the relevant client button
                    clientButton.SetActive(true);
                }
            }
        }
    }

    

}
