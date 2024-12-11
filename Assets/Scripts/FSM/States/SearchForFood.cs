using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.FSM
{
    public class SearchForFood : State
    {

        public float moveSpeed = 2f;  // Movement speed
        public string targetTag = "Fish"; // Tag to identify target objects
        private FISH fish;
        private UnityEngine.Transform target; // Current target
        public SearchForFood(GameObject owner) : base(owner)
        {
        }

        public override void Enter()
        {
            fish = owner.GetComponent<FISH>();
        }

        public override void Execute()
        {
            if (target == null)
                SearchForTarget();
            else
            { }
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        void SearchForTarget()
        {
            // Find all colliders within the search range
            Collider2D[] hits = Physics2D.OverlapCircleAll(owner.transform.position, 500f);

            foreach (Collider2D hit in hits)
            {
                float closesttarget = -1;
                if (hit.CompareTag(targetTag)) // Check if the collider has the correct tag
                {
                    float dist = Vector2.Distance(owner.transform.position, hit.transform.position);
                    if (closesttarget == -1 || dist > closesttarget)
                    {
                        closesttarget = dist;
                        target = hit.transform;
                    }




                }
            }
            //Debug.Log("Target found: " + target.name);
        }
    }
}