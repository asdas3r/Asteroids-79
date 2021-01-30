using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigationManager : MonoBehaviour
{
    public static MenuNavigationManager singleton;

    public BaseMenuPanel[] menuPanels;

    private BaseMenuPanel _currentPanel;
    private List<BaseMenuPanel> _menuPanelsList;

    void Awake()
    {
        if (singleton != null)
        {
            Debug.Log(gameObject.name + " already exists.");
            return;
        }

        singleton = this;
    }

    private void Start()
    {
        _menuPanelsList = new List<BaseMenuPanel>();

        foreach (var panel in menuPanels)
        {
            _menuPanelsList.Add(panel);
        }

        _currentPanel = null;

        NavigatePanel("MainMenu");
    }

    public void NavigatePanel(string panelName)
    {
        BaseMenuPanel targetPanel = _menuPanelsList.Find(e => e.panelName == panelName);
        if (targetPanel == null)
        {
            Debug.Log("Target panel \"" + panelName + "\" wasn't found. Navigation cancelled.");
            return;
        }

        if (_currentPanel != null)
        {
            _currentPanel.NavigateFrom();
        }

        _currentPanel = targetPanel;
        _currentPanel.NavigateTo();
    }

    public void HideCurrentPanel()
    {
        if (_currentPanel != null)
        {
            _currentPanel.NavigateFrom();
            _currentPanel = null;
        }
    }
}
