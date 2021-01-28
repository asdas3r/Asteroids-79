using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButton : Button
{
    private AudioManager _audioManager;

    protected override void Start()
    {
        base.Start();

        _audioManager = AudioManager.singleton;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (gameObject.activeSelf)
            _audioManager.Play("button_hover");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (gameObject.activeSelf)
            _audioManager.Play("button_click");
    }

    /*public void OnPointerEnter()
    {
        if (gameObject.activeSelf)
            _audioManager.Play("button_hover");
    }

    public void OnClick()
    {
        if (gameObject.activeSelf)
            _audioManager.Play("button_click");
    }*/
    
    protected override void OnDisable()
    {
        InstantClearState();
        base.OnDisable();
        InstantClearState();
    }
}
