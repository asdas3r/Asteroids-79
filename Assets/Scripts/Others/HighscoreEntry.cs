using UnityEngine;
using TMPro;

public class HighscoreEntry : MonoBehaviour
{
    public TMP_Text orderText;
    public TMP_Text nameText;
    public TMP_Text scoreText;

    public void SetupEntry(int order, string name, long score)
    {
        orderText.text = order.ToString();
        nameText.text = name;
        scoreText.text = score.ToString();
    }
}
