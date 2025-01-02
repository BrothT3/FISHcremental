using Assets.Scripts;
using Assets.Scripts.FSM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FISH : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float Hunger = 0;
    public float HungerRate = 0.5f;
    public float MaxHunger = 100;
    private Color startColor = Color.white; // original color (not gray)
    private Color endColor = Color.gray; // fully gray
    public GameObject coinPrefab;
    public float coinDropInterval = 5f;
    public float coinValue = 10f;
    public bool FacingRight;
    [SerializeField]
    private Transform CoinContainer;
    [SerializeField]
    float maxScale;
    public float defaultScale;
    public float currentScale;
    public float GrowthModifier = 0;
    public bool carnivore;
    public Animator anim;
    public bool notDying = true;

    private void Start()
    {
        // Start dropping coins at regular intervals
        InvokeRepeating(nameof(DropCoin), coinDropInterval, coinDropInterval);
        anim = GetComponentInChildren<Animator>();
    }
    public void Update()
    {
        Hunger += HungerRate * Time.deltaTime;
        if (Hunger >= 60)
        {
            // Calculate the lerp factor based on the hunger value (normalized between 0 and 1)
            float lerpFactor = Mathf.InverseLerp(60, 100, Hunger);

            // Lerp between the start color (white) and the end color (gray)
            GetComponentInChildren<Image>().color = Color.Lerp(startColor, endColor, lerpFactor);
        }
        if (Hunger >= 100 && notDying)
        {
            notDying = false;
            anim.SetTrigger("Death");
            GetComponent<AIController>().FSM.ChangeState(new DyingState(gameObject));
        }
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
        defaultScale = currentScale;
        maxScale = FO.MaxScale;
        carnivore = FO.Carnivore;
        gameObject.tag = FO.Tag;
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
            coinScript.SetupCoin(coinValue, GrowthModifier);
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
        float newGrowthModifier = GrowthModifier + ff.FoodQuality / 10;
        if (defaultScale * (1 + newGrowthModifier) >= maxScale)
            return;

        Image img = GetComponentInChildren<Image>();
        GrowthModifier += newGrowthModifier;
        currentScale = defaultScale * (1 + GrowthModifier);
        img.gameObject.GetComponent<RectTransform>().localScale = new Vector3(currentScale, currentScale, currentScale);
    }
    public void GrowFish(FISH ff)
    {
        float newGrowthModifier = GrowthModifier + 0.1f;
        if (defaultScale * (1 + newGrowthModifier) >= maxScale)
            return;
        Image img = GetComponentInChildren<Image>();
        GrowthModifier += 0.1f;
        currentScale = defaultScale * (1 + (GrowthModifier));
        img.gameObject.GetComponent<RectTransform>().localScale = new Vector3(currentScale, currentScale, currentScale);
    }


}

