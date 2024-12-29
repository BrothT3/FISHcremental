using UnityEngine;
using UnityEngine.UI;

public class PurchaseFishButton : MonoBehaviour, IClickable
{
    [SerializeField]
    private int FishIndex;
    [SerializeField]
    private string ResourceName;
    [SerializeField]
    private int cost;
    private Image Image;

    public void Start()
    {
        Image = GetComponent<Image>();
        GameManager.Instance.RM.OnCoinsChanged += CheckAvailability;
    }
    public void OnLeftClick()
    {
        GameManager.Instance.RM.PurchaseFish(ResourceName, cost, FishIndex);
    }

    public void CheckAvailability(int newAmount)
    {
        if (newAmount >= cost)
        {
            Image.color = Color.white;
        }
        else
        {
            Image.color = Color.gray;
        }
    }

}
