using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


public class FishCoin : MonoBehaviour, IClickable
{
    private float CoinReward;
    [SerializeField]
    private float CoinFallSpeed = 3f;
    private Animator anim;
    private bool destroyTriggerSet;
    private float currentFallSpeed = 10;
    public static event Action<float> OnCoinClicked;

    private void Update()
    {
        CoinFallDown();
    }
    public void OnLeftClick()
    {
        // Notify listeners (e.g., ResourceManager) when clicked
        OnCoinClicked?.Invoke(CoinReward);
        Destroy(gameObject); // Remove the coin after it's clicked
    }
    public void SetupCoin(float CoinRew)
    {
        CoinReward = CoinRew;
        anim = GetComponent<Animator>();
    }

    private void CoinFallDown()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 pos = rectTransform.anchoredPosition;

        float adjustedBottomY = transform.parent.GetComponent<RectTransform>().rect.min.y + GetComponent<RectTransform>().rect.height;
        if (currentFallSpeed < CoinFallSpeed * 2 - 10)
        {

            currentFallSpeed = Mathf.Lerp(currentFallSpeed, CoinFallSpeed * 2, 0.1f * Time.deltaTime);
        }

        if (pos.y > adjustedBottomY)
            rectTransform.anchoredPosition = new Vector2(pos.x, pos.y - currentFallSpeed * Time.deltaTime);
        else
        {
            rectTransform.anchoredPosition = new Vector2(pos.x, adjustedBottomY);
            if (!destroyTriggerSet)
            {
                anim.SetTrigger("DestroyCoin");
                destroyTriggerSet = true;
            }
                
        }
    }

    public void DestroyCoin()
    {
        Destroy(this.gameObject);
    }
}

