using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseMenuPanel : MonoBehaviour
{
    public string panelName;

    private CanvasGroup _canvasGroup;

    protected MenuNavigationManager navigationManager;
    protected bool isPanelActive;

    protected virtual void Awake()
    {
        InitializeCanvasGroup();
    }

    protected virtual void Start()
    {
        navigationManager = MenuNavigationManager.singleton;
    }

    private void Update()
    {
        if (!isPanelActive)
            return;

        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }

    public virtual void NavigateFrom()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;

        isPanelActive = false;
    }

    public virtual void NavigateTo()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        InitializeCanvasGroup();
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        isPanelActive = true;
    }

    private void InitializeCanvasGroup()
    {
        if (_canvasGroup == null)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
