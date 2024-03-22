using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;


public class DataManager : MonoBehaviour
{
    private readonly string apiUrl = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    private List<Client> clientsList = new List<Client>();
    private Dictionary<string, ClientDetail> clientsDetails = new Dictionary<string, ClientDetail>();
    public static  DataManager instance;
    public event Action<List<Client>, Dictionary<string, ClientDetail>> onDataReady;
    // Start is called before the first frame update

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
        }
    }




    void Start()
    {
        StartCoroutine(GetClientCoroutine());
    }

    IEnumerator GetClientCoroutine() {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
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
                clientsList = new List<Client>(clientsDataWrapper.clients);
                clientsDetails.Clear();
                foreach (var client in clientsDataWrapper.clients)
                {
                    if (clientsDataWrapper.data.TryGetValue(client.id.ToString(), out ClientDetail detail))
                    {
                        clientsDetails[client.id.ToString()] = detail;
                        Debug.Log($"Client: {client.label}, Name: {detail.name}, Address: {detail.address}, Points: {detail.points}");
                    }
                }
                onDataReady?.Invoke(clientsList, clientsDetails);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error deserializing JSON: {ex.Message}");
        }
    }

    public ClientDetail GetClientDetail(int clientId)
    {
        if (clientsDetails.TryGetValue(clientId.ToString(), out var detail))
        {
            return detail;
        }
        return null;
    }

    public List<Client> GetAllClients()
    {
        return clientsList;
    }

    public List<Client> GetManagers()
    {
        return clientsList.FindAll(client => client.isManager);
    }


    public List<Client> GetNonManagers()
    {
        return clientsList.FindAll(client => !client.isManager);
    }

    public DataManager.Client GetClientByName(string name)
    {
        return clientsList.Find(client => client.label == name);
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
        public Client[] clients; 
        public Dictionary<string, ClientDetail> data;
        public string label;
    }
}
