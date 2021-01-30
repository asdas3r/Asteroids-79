using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreInputPanel : BaseMenuPanel
{
    public GameObject inputButton1;
    public GameObject inputButton2;
    public GameObject inputButton3;

    private ScoreManager _scoreManager;
    private int[] _keyCodeValues;
    private string _inputLetters;
    private int _activeLetter;

    private bool _isHorizontalAxisInUse;

    protected override void Awake()
    {
        base.Awake();

        _inputLetters = "_abcdefghijklmnopqrstuvwxyz";
        _keyCodeValues = (int[])Enum.GetValues(typeof(KeyCode));
    }

    protected override void Start()
    {
        base.Start();

        _scoreManager = ScoreManager.singleton;

        ResetFields();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        var verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput != 0)
        {
            if (_isHorizontalAxisInUse)
                return;

            _isHorizontalAxisInUse = true;

            var currentObject = EventSystem.current.currentSelectedGameObject;
            if (currentObject != null)
            {
                var letterFromObject = currentObject.GetComponentInChildren<TMP_Text>().text;
                _activeLetter = _inputLetters.IndexOf(letterFromObject);

                var inputAmount = _inputLetters.Length;

                if (verticalInput > 0)
                {
                    _activeLetter++;
                    if (_activeLetter > inputAmount - 1)
                        _activeLetter = 0;
                }
                else
                {
                    _activeLetter--;
                    if (_activeLetter < 0)
                        _activeLetter = inputAmount - 1;
                }

                InputLetterIntoField(_inputLetters[_activeLetter]);
            }
        }
        else
        {
            _isHorizontalAxisInUse = false;
        }
        /*
        else
        {
            foreach (KeyCode keyCode in _keyCodeValues)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if ((int)keyCode >= 97 && (int)keyCode <= 122)
                    {
                        InputLetterIntoField(_inputLetters[(int)keyCode - 96]);
                    }
                }
            }
        }*/
    }

    public override void NavigateTo()
    {
        base.NavigateTo();

        if (gameObject.activeSelf)
        {
            ResetFields();
        }
    }

    private void ResetFields()
    {
        inputButton1.GetComponentInChildren<TMP_Text>().text = "_";
        inputButton2.GetComponentInChildren<TMP_Text>().text = "_";
        inputButton3.GetComponentInChildren<TMP_Text>().text = "_";

        EventSystem.current.SetSelectedGameObject(inputButton1);
    }

    private void InputLetterIntoField(char letter)
    {
        var currentObject = EventSystem.current.currentSelectedGameObject;
        if (currentObject != null)
        {
            var tmpText = currentObject.GetComponentInChildren<TMP_Text>();
            tmpText.text = letter.ToString();
        }
    }

    public void ConfirmAction()
    {
        var name = string.Concat(
            inputButton1.GetComponentInChildren<TMP_Text>().text,
            inputButton2.GetComponentInChildren<TMP_Text>().text,
            inputButton3.GetComponentInChildren<TMP_Text>().text);

        _scoreManager.TryAddCurrentScore(name);

        navigationManager.NavigatePanel("MainMenu");
    }
}
