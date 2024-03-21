using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;


public class DataManager : MonoBehaviour
{
    private readonly string apiUrl = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetClientCoroutine());
    }

    // Update is called once per frame
    IEnumerator GetClientCoroutine() {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                DeserializeJson(jsonResponse);
            }
        }
    }

    void DeserializeJson(string jsonString)
    {
        try
        {
            ClientsDataWrapper clientsDataWrapper = JsonConvert.DeserializeObject<ClientsDataWrapper>(jsonString);

            if (clientsDataWrapper != null)
            {
                Debug.Log($"Label for all clients: {clientsDataWrapper.label}");

                foreach (var client in clientsDataWrapper.clients)
                {
                    if (clientsDataWrapper.data.TryGetValue(client.id.ToString(), out ClientDetail detail))
                    {
                        Debug.Log($"Client: {client.label}, Name: {detail.name}, Address: {detail.address}, Points: {detail.points}");
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error deserializing JSON: {ex.Message}");
        }
    }



[Serializable]
    public class Client
    {
        public bool isManager;
        public int id;
        public string label;
    }

    [Serializable]
    public class ClientDetail
    {
        public string address;
        public string name;
        public int points;
    }

    [Serializable]
    public class ClientsDataWrapper
    {
        public Client[] clients; // Adjusted to array based on provided JSON structure
        public Dictionary<string, ClientDetail> data;
        public string label;
    }
}
