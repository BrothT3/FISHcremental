using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.FSM
{
    public class AIController : MonoBehaviour
    {
        public FSM FSM;
        private State currentState;

        private void Start()
        {
            FSM = new FSM();

            // Initialize with a starting state
            FSM.ChangeState(new WanderState(gameObject));
        }

        private void Update()
        {
            FSM.Update();
            if (gameObject.GetComponent<FISH>().Hunger > 40)
            {
                FSM.ChangeState(new SearchForFood(gameObject));
            }
        }
    }
}
