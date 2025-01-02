using Assets.Scripts.FSM.States;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.FSM
{
    public class SearchForFoodState : State
    {

        private FISH fish;
        private UnityEngine.Transform target; // Current target
        private Vector2 nextPos = Vector2.zero;
        private ObjectPool<SearchFoodCommand> searchFoodPool;
        private ObjectPool<FindNextPosCommand> findNextPosPool;
        private ObjectPool<MoveCommand> moveCommandPool;

        public SearchForFoodState(GameObject owner) : base(owner)
        {
        }

        public override void Enter()
        {
            fish = owner.GetComponent<FISH>();
            nextPos = Vector2.zero;
            searchFoodPool = new ObjectPool<SearchFoodCommand>(() => new SearchFoodCommand(owner.transform));
            findNextPosPool = new ObjectPool<FindNextPosCommand>(() => new FindNextPosCommand());
            moveCommandPool = new ObjectPool<MoveCommand>(() => new MoveCommand(owner.transform, Vector2.zero, fish.MoveSpeed));
        }

        public override void Execute()
        {
            if (GameManager.Instance.LogStateChanges)
                Debug.Log("SearchingForFood", owner);

            target = SendResultCommand(searchFoodPool.Get());
            if (target != null)
            {
                nextPos = target.position;
            }
            else
            {
                if (nextPos == Vector2.zero)
                {
                    nextPos = SendResultCommand(findNextPosPool.Get());
                }
            }
            var moveCommand = moveCommandPool.Get();
            moveCommand.SetupCommand(owner.transform,nextPos, fish.MoveSpeed);
            SendCommand(moveCommand);

            TargetReachedCheck();
        }
        public void TargetReachedCheck()
        {
            if (Vector2.Distance(owner.transform.position, nextPos) < 1)
            {
                if (target != null)
                {
                    if (target.GetComponent<FishFood>())
                    {
                        FishFood ff = target.GetComponent<FishFood>();
                        FISH f = owner.GetComponent<FISH>();
                        f.Hunger -= ff.Nutrition;
                        f.GrowFish(ff);
                        GameObject.Destroy(ff.gameObject);
                        if (owner.GetComponent<FISH>().Hunger < 30)
                        {
                            owner.GetComponent<AIController>().FSM.ChangeState(new IdleState(owner));
                        }
                    }
                    else if (target.GetComponent<FISH>())
                    {
                        FISH ff = target.GetComponent<FISH>();
                        FISH f = owner.GetComponent<FISH>();
                        f.Hunger = 0;
                        f.GrowFish(ff);
                        GameObject.Destroy(ff.gameObject);
                        owner.GetComponent<AIController>().FSM.ChangeState(new IdleState(owner));
                    }

                }
                else
                    nextPos = Vector2.zero;
            }
        }
        public override void Exit()
        {
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