using UnityEngine;
using UnityEngine.UI;

public class PurchaseFishButton : MonoBehaviour, IClickable
{
    public FishObject FO;
    private Image Image;

    public void Start()
    {
        Image = GetComponent<Image>();
        GameManager.Instance.RM.OnCoinsChanged += CheckAvailability;
    }
    public void OnLeftClick()
    {
        GameManager.Instance.RM.PurchaseFish(FO.ResourceName, FO.cost, FO);
    }

    public void CheckAvailability(int newAmount)
    {
        if (newAmount >= FO.cost)
        {
            Image.color = Color.white;
        }
        else
        {
            Image.color = Color.gray;
        }
    }

}
