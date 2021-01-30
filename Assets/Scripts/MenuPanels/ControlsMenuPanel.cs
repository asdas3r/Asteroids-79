using UnityEngine;

public class ControlsMenuPanel : BaseMenuPanel
{
    public void GoBack()
    {
        navigationManager.NavigatePanel("MainMenu");
    }
}
