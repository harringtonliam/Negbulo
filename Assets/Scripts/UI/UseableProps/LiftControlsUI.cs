using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.UseablePropControl;

public class LiftControlsUI : MonoBehaviour
{
    [SerializeField] GameObject uiCanvas = null;
    [SerializeField] Button closeButton;

    int nextIndicatorIndex = 0;

    UseablePropLink useablePropLink;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        useablePropLink = FindObjectOfType<UseablePropLink>();
        useablePropLink.onDisplayLiftControlsUI += ShowDisplay;
        closeButton.onClick.AddListener(Close);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Close()
    {
        player.GetComponent<UseProp>().Cancel();
        HideDisplay();
    }

    private void ShowDisplay()
    {
        if (useablePropLink == null) return;

        player.GetComponent<UseProp>().onUsePropCancel += HideDisplay;

        uiCanvas.SetActive(true);
    }

    private void HideDisplay()
    {
        uiCanvas.SetActive(false);
    }



}
