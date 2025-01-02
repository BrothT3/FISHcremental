using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.FSM
{
    public abstract class State
    {
        protected GameObject owner;
        public State(GameObject owner)
        {
            this.owner = owner;
        }

        public abstract void Enter(); // Called when entering the state
        public abstract void Execute(); // Called on Update
        public abstract void Exit(); // Called when leaving the state
        public abstract void SendCommand(ICommand command);
        public abstract T SendResultCommand<T>(IResultCommand<T> command);
    }
}
