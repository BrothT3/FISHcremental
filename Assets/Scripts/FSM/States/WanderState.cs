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
        private Vector2 nextPos;
        public Vector2 GetNextPos()
        {

            Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

            float randomX = UnityEngine.Random.Range(bottomLeft.x, topRight.x);
            float randomY = UnityEngine.Random.Range(bottomLeft.y, topRight.y);

            return new Vector2(randomX, randomY);

        }
        public void MoveToNextPos()
        {
            Vector2 pos = owner.transform.position;
            Vector2 movedir = nextPos - pos;
            movedir.Normalize();
            pos += movedir * owner.GetComponent<FISH>().MoveSpeed * Time.deltaTime;
            owner.transform.position = pos;
        }
        public override void Enter()
        {
            Debug.Log("Entering Wander State", owner);

        }

        public override void Execute()
        {
            Debug.Log("Wandering...", owner);
            if (nextPos == null)
            {
                nextPos = GetNextPos();
            }
            MoveToNextPos();
            if (Vector2.Distance(owner.transform.position, nextPos) < 3)
                nextPos = GetNextPos();
         
        }

        public override void Exit()
        {
            Debug.Log("Exiting Wander State", owner);

        }
    }
}
