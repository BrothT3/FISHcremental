using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.FSM
{
    public class DyingState : State
    {
        public DyingState(GameObject owner) : base(owner) { }

        public override void Enter()
        {

        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }

        public override T SendResultCommand<T>(IResultCommand<T> command)
        {
            throw new NotImplementedException();
        }

        public override void SendCommand(ICommand c)
        {

        }
    }
}
