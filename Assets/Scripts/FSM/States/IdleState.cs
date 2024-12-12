using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.FSM.States
{
    public class IdleState : State
    {
        public IdleState(GameObject owner) : base(owner) { }
        public float timer;
        public override void Enter()
        {
            timer = Random.Range(0.2f, 0.6f);
            owner.GetComponent<FISH>().anim.SetBool("FishIdle", true);
        }

        public override void Execute()
        {
            timer -= 0.1f * Time.deltaTime;
            if (timer < 0)
            {
                owner.GetComponent<AIController>().FSM.ChangeState(new WanderState(owner));
            }
            if (owner.GetComponent<FISH>().Hunger > 10)
            {
                owner.GetComponent<AIController>().FSM.ChangeState(new SearchForFood(owner));

            }
        }

        public override void Exit()
        {
            owner.GetComponent<FISH>().anim.SetBool("FishIdle", false);
        }
    }
}
