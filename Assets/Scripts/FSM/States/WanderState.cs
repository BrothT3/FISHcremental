using Assets.Scripts.FSM.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.FSM
{
    public class WanderState : State
    {
        public WanderState(GameObject owner) : base(owner) { }
        private Vector2 nextPos = Vector2.zero;
        public Vector2 GetNextPos()
        {

            Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 870, Camera.main.nearClipPlane));

            float randomX = UnityEngine.Random.Range(bottomLeft.x, topRight.x);
            float randomY = UnityEngine.Random.Range(bottomLeft.y, topRight.y);

            return new Vector2(randomX, randomY);
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
        public override void Enter()
        {
            if (GameManager.Instance.LogStateChanges)
                Debug.Log("Entering Wander State", owner);
            nextPos = Vector2.zero;
        }

        public override void Execute()
        {
            if (GameManager.Instance.LogStateChanges)
                Debug.Log("Wandering...", owner);
            if (nextPos == Vector2.zero)
            {
                nextPos = GetNextPos();

            }
            MoveToNextPos();
            if (Vector2.Distance(owner.transform.position, nextPos) < 1)
            {
                owner.GetComponent<AIController>().FSM.ChangeState(new IdleState(owner));
                //nextPos = GetNextPos();
            }
            if (owner.GetComponent<FISH>().Hunger > 10)
            {
                owner.GetComponent<AIController>().FSM.ChangeState(new SearchForFood(owner));

            }
        }

        public override void Exit()
        {
            if (GameManager.Instance.LogStateChanges)
                Debug.Log("Exiting Wander State", owner);

        }
    }
}
