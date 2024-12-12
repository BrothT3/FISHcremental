using Assets.Scripts.FSM.States;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.FSM
{
    public class SearchForFood : State
    {


        public string targetTag = "Food"; // Tag to identify target objects
        private FISH fish;
        private UnityEngine.Transform target; // Current target
        private Vector2 nextPos = Vector2.zero;
        public SearchForFood(GameObject owner) : base(owner)
        {
        }

        public override void Enter()
        {
            fish = owner.GetComponent<FISH>();
            nextPos = Vector2.zero;
        }

        public override void Execute()
        {
            Debug.Log("SearchingForFood", owner);
            target = SearchForTarget();
            if (target != null)
            {
                nextPos = GetTargetPos();
            }
            else
            {
                if (nextPos ==  Vector2.zero)
                {
                    nextPos = GetNextPos();
                }    
            }
            MoveToNextPos();
            if (Vector2.Distance(owner.transform.position, nextPos) < 1)
            {
                if (target != null)
                {
                    FishFood ff = target.GetComponent<FishFood>();
                    FISH f = owner.GetComponent<FISH>();
                    f.Hunger -= ff.Nutrition;
                    GameObject.Destroy(ff.gameObject);
                    if (owner.GetComponent<FISH>().Hunger < 30)
                    {
                        owner.GetComponent<AIController>().FSM.ChangeState(new IdleState(owner));
                    }
                }
                else 
                    nextPos = Vector2.zero;  
            }

        }
        public Vector2 GetNextPos()
        {
            Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

            float randomX = UnityEngine.Random.Range(bottomLeft.x, topRight.x);
            float randomY = UnityEngine.Random.Range(bottomLeft.y, topRight.y);

            return new Vector2(randomX, randomY);

        }
        public Vector2 GetTargetPos()
        {
            return target.position;
        }
        public void MoveToNextPos()
        {
            Vector2 pos = owner.transform.position;
            Vector2 movedir = nextPos - pos;
            movedir.Normalize();
            owner.GetComponent<FISH>().ChangeFacing(movedir.x);
            pos += movedir * owner.GetComponent<FISH>().MoveSpeed * Time.deltaTime;
            owner.transform.position = pos;
        }
        public override void Exit()
        {
        }

        UnityEngine.Transform SearchForTarget()
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
                        return hit.transform;
                    }
                }
            }
            return null;
            //Debug.Log("Target found: " + target.name);
        }
    }
}