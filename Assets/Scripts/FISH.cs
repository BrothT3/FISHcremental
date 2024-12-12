using Unity.VisualScripting;
using UnityEngine;

public class FISH : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float Hunger = 0;
    public GameObject coinPrefab;
    public float coinDropInterval = 5f;
    public float coinValue = 10f;
    public bool FacingRight;
    private Animator anim;
    private void Start()
    {
        // Start dropping coins at regular intervals
        InvokeRepeating(nameof(DropCoin), coinDropInterval, coinDropInterval);
        anim = GetComponentInChildren<Animator>();
    }
    private void DropCoin()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
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

