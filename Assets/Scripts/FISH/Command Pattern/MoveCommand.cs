using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


internal class MoveCommand : ICommand
{
    private Transform objectToMove;
    private float speed;
    private Vector2 target;
    public MoveCommand(Transform Object, Vector2 Target, float Speed)
    {
        objectToMove = Object;
        speed = Speed;
        target = Target;
    }
    public void SetupCommand(Transform Object, Vector2 Target, float Speed)
    {
        objectToMove = Object;
        speed = Speed;
        target = Target;
    }
    public void Execute()
    {
        MoveToNextPos();
    }
    public void MoveToNextPos()
    {
        Vector2 pos = objectToMove.transform.position;
        Vector2 movedir = target - pos;
        movedir.Normalize();
        objectToMove.GetComponent<FISH>().ChangeFacing(movedir.x);
        pos += movedir * speed * Time.deltaTime;
        objectToMove.position = pos;
    }
}

