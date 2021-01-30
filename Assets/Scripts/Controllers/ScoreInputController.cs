using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreInputController : MonoBehaviour
{
    public GameObject inputButton1;
    public GameObject inputButton2;
    public GameObject inputButton3;

    private int[] _keyCodeValues;
    private string _inputLetters;
    private int _activeLetter;

    private bool _isHorizontalAxisInUse;

    private void Awake()
    {
        _inputLetters = "_abcdefghijklmnopqrstuvwxyz";
        _keyCodeValues = (int[])Enum.GetValues(typeof(KeyCode));
    }

    private void OnEnable()
    {
        SetDefaults();
    }

    void Update()
    {
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

    private void SetDefaults()
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

        //ScoreManager.singleton.TryAddScore(name, )
    }
}
