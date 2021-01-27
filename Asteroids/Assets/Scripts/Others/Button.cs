using UnityEngine;

public class Button : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.singleton;
    }

    public void OnPointerEnter()
    {
        _audioManager.Play("button_hover");
    }

    public void OnClick()
    {
        _audioManager.Play("button_click");
    }
}
