using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.FSM
{
    public class FSM
    {
        private State currentState;

        public void ChangeState(State newState)
        {
            currentState?.Exit(); // Call Exit on the old state
            currentState = newState; // Set the new state
            currentState?.Enter(); // Call Enter on the new state
        }

        public void Update()
        {
            currentState?.Execute(); // Call Execute on the current state
        }
    }
}
