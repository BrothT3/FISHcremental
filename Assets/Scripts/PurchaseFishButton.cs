using UnityEngine;

public class PurchaseFishButton : MonoBehaviour, IClickable
{
    [SerializeField]
    private int FishIndex;
    [SerializeField]
    private string ResourceName;
    [SerializeField]
    private int cost;
    public void OnLeftClick()
    {
        GameManager.Instance.RM.PurchaseFish(ResourceName, cost, FishIndex);
    }
}
