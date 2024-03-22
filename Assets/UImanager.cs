using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UImanager : MonoBehaviour
{
    public TMP_Text detailsText; 
    private DataManager dataManager;
    public GameObject panel;
    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        if (dataManager == null)
        {
            Debug.LogError("DataManager not found in the scene.");
        }
        if (panel != null)
        {
            panel.SetActive(false); 
        }

        DOTween.Init();
    }

    public void DisplayClientDetails(string clientName)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            panel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).From(0);
        }

        DataManager.Client client = dataManager.GetClientByName(clientName);
        DataManager.ClientDetail detail = dataManager.GetClientDetail(client.id);

        if (detail != null)
        {
            
            detailsText.text = $"Name: {detail.name}\nPoints: {detail.points}\nAddress: {detail.address}";
            detailsText.rectTransform.DOLocalMoveY(0, 0.5f).From(new Vector2(detailsText.rectTransform.localPosition.x, -50));
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
