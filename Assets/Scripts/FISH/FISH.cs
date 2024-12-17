using Unity.VisualScripting;
using UnityEngine;

public class FISH : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float Hunger = 0;
    public float HungerRate = 0.5f;
    public GameObject coinPrefab;
    public float coinDropInterval = 5f;
    public float coinValue = 10f;
    public bool FacingRight;
    public Animator anim;
    private void Start()
    {
        // Start dropping coins at regular intervals
        InvokeRepeating(nameof(DropCoin), coinDropInterval, coinDropInterval);
        anim = GetComponentInChildren<Animator>();
    }
    public void Update()
    {
        Hunger += HungerRate * Time.deltaTime;
    }
    public void SetupFish(FishObject FO)
    {
        Hunger = FO.Hunger;
        MoveSpeed = FO.MoveSpeed;
        HungerRate = FO.HungerRate;
        coinDropInterval = FO.coinDropInterval;
        coinValue = FO.coinValue;
        GetComponent<SpriteRenderer>().sprite = FO.Sprite;
        coinPrefab = FO.CoinType;
    }
    private void DropCoin()
    {
        Canvas c = FindAnyObjectByType<Canvas>();
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

        coin.transform.SetParent(c.transform, false);
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            c.GetComponent<RectTransform>(),
            RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position),
            Camera.main,
            out canvasPosition
        );

        coin.GetComponent<RectTransform>().anchoredPosition = canvasPosition;
        FishCoin coinScript = coin.GetComponent<FishCoin>();
        if (coinScript != null)
        {
            coinScript.SetupCoin(coinValue);
        }
    }
    public void ChangeFacing(float x)
    {
        if (!FacingRight && x > 0)
        {
            FacingRight = true;
            anim.SetTrigger("FaceRight");
        }
        if (FacingRight && x < 0)
        {
            FacingRight = false;
            anim.SetTrigger("FaceLeft");
        }
    }
}
