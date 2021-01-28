using UnityEngine;

public class SoundButtonsBehaviour : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;

    private int _soundValue;

    public int SoundValue
    {
        get { return _soundValue; }
        set { _soundValue = value; UpdateButtons(value); }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            SoundValue = PlayerPrefs.GetInt("Sound");
        }
        else
        {
            SoundValue = 1;
        }
    }

    private void UpdateButtons(int value)
    {
        if (value == 0)
        {
            soundOn.gameObject.SetActive(false);
            soundOff.gameObject.SetActive(true);
            AudioListener.volume = 0f;
        }
        else
        {
            soundOn.gameObject.SetActive(true);
            soundOff.gameObject.SetActive(false);
            AudioListener.volume = 1f;
        }

        PlayerPrefs.SetInt("Sound", value);
        PlayerPrefs.Save();
    }
}
