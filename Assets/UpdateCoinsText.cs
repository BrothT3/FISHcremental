using TMPro;
using UnityEngine;

public class UpdateCoinsText : MonoBehaviour
{
    TextMeshProUGUI text;
    public void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.RM.OnCoinsChanged += ChangeText;
    }
    private void ChangeText(int amount)
    {
        text.text = amount.ToString();
    }
}
