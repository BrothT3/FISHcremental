using Assets.Scripts.FSM.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.FSM
{
    public class WanderState : State
    {
        public WanderState(GameObject owner) : base(owner) { }
        private Vector2 nextPos = Vector2.zero;
        private ObjectPool<FindNextPosCommand> findNextPosPool;
        private ObjectPool<MoveCommand> moveCommandPool;
        private FISH fish;
        public override void Enter()
        {
            if (GameManager.Instance.LogStateChanges)
                Debug.Log("Entering Wander State", owner);
            nextPos = Vector2.zero;
            fish = owner.GetComponent<FISH>();
            fish.currentState = CURRENTSTATE.WAMDER;
            findNextPosPool = new ObjectPool<FindNextPosCommand>(() => new FindNextPosCommand());
            moveCommandPool = new ObjectPool<MoveCommand>(() => new MoveCommand(owner.transform, Vector2.zero, fish.MoveSpeed));
            nextPos = SendResultCommand(findNextPosPool.Get());
        }

        public override void Execute()
        {
            if (GameManager.Instance.LogStateChanges)
                Debug.Log("Wandering...", owner);
            if (nextPos == Vector2.zero)
            {
                nextPos = SendResultCommand(findNextPosPool.Get());
                
            }
            SendCommand(moveCommandPool.Get());
            if (Vector2.Distance(owner.transform.position, nextPos) < 1)
            {
                owner.GetComponent<AIController>().FSM.ChangeState(new IdleState(owner));
                return;
                //nextPos = GetNextPos();
            }
            if (owner.GetComponent<FISH>().Hungry)
            {
                owner.GetComponent<AIController>().FSM.ChangeState(new SearchForFoodState(owner));
                return;
            }
        }

        public override void Exit()
        {
            if (GameManager.Instance.LogStateChanges)
                Debug.Log("Exiting Wander State", owner);

        }

        public override void SendCommand(ICommand command)
        {
            command.Execute();
        }

        public override T SendResultCommand<T>(IResultCommand<T> command)
        {
            return command.Execute();
        }
    }
}
