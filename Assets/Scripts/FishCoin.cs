using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class FishCoin : MonoBehaviour
{
    private float CoinReward;
    private float CoinFallSpeed = 3f;

    public static event Action<float> OnCoinClicked;

    private void Update()
    {
        CoinFallDown();
    }
    private void OnMouseDown()
    {
        // Notify listeners (e.g., ResourceManager) when clicked
        OnCoinClicked?.Invoke(CoinReward);
        Destroy(gameObject); // Remove the coin after it's clicked
    }
    public void SetupCoin(float CoinRew)
    {
        CoinReward = CoinRew;
    }

    private void CoinFallDown()
    {
        Vector2 pos = gameObject.transform.position;

        float adjustedBottomY = ScreenBottomY() + HalfSpriteHeight();

        if (pos.y > adjustedBottomY)
            gameObject.transform.position = new Vector2(pos.x, pos.y - CoinFallSpeed * Time.deltaTime);
        else     
            gameObject.transform.position = new Vector2(pos.x, adjustedBottomY);

    }

    private float ScreenBottomY()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
            return mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        return -Mathf.Infinity;
    }

    private float HalfSpriteHeight()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            return spriteRenderer.bounds.extents.y; 

        return 0f; 
    }

}

