using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class FishFood : MonoBehaviour
    {
        [SerializeField]
        private float FoodFallSpeed = 60f;
        private Animator anim;
        private bool destroyTriggerSet;
        private float currentFallSpeed = 20;
        public float FoodQuality = 1;

        public int Nutrition = 50;
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            FoodFallDown();
        }
        private void FoodFallDown()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector2 pos = rectTransform.anchoredPosition;

            float adjustedBottomY = transform.parent.GetComponent<RectTransform>().rect.min.y + GetComponent<RectTransform>().rect.height;
            if (currentFallSpeed < FoodFallSpeed * 2 - 10)
            {

                currentFallSpeed = Mathf.Lerp(currentFallSpeed, FoodFallSpeed * 2, 0.1f * Time.deltaTime);
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
    }
}
