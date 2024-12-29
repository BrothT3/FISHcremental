using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FISH : MonoBehaviour, IClickable
{
    public float MoveSpeed = 2f;
    public float Hunger = 0;
    public float HungerRate = 0.5f;
    public GameObject coinPrefab;
    public float coinDropInterval = 5f;
    public float coinValue = 10f;
    public bool FacingRight;
    [SerializeField]
    private Transform CoinContainer;
    [SerializeField]
    float maxScale;
    [SerializeField]
    float currentScale;
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
        coinPrefab = FO.CoinType;
        CoinContainer = GameManager.Instance.CoinContainer.transform;
        Image img = GetComponentInChildren<Image>();
        img.sprite = FO.Sprite;
        img.gameObject.GetComponent<RectTransform>().localScale = new Vector3(FO.Scale, FO.Scale, FO.Scale);
        currentScale = FO.Scale;
        maxScale = FO.MaxScale;
    }
    private void DropCoin()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

        coin.transform.SetParent(CoinContainer, false);
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            CoinContainer.GetComponent<RectTransform>(),
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

    public void GrowFish(FishFood ff)
    {
        Image img = GetComponentInChildren<Image>();
        currentScale = currentScale * (1 + (ff.FoodQuality / 10));
        img.gameObject.GetComponent<RectTransform>().localScale = new Vector3(currentScale, currentScale, currentScale);
    }

    public void OnLeftClick()
    {
        if (GameManager.Instance.IP.ClickType != CLICKTYPE.SELL)
            return;

        Destroy(gameObject);
    }
}

