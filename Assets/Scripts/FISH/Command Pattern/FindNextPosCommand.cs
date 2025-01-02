using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class FindNextPosCommand : IResultCommand<Vector2>
{

    public FindNextPosCommand()
    {
    }

    Vector2 IResultCommand<Vector2>.Execute()
    {
        return GetNextPos();
    }

    public Vector2 GetNextPos()
    {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 870, Camera.main.nearClipPlane));

        float randomX = UnityEngine.Random.Range(bottomLeft.x, topRight.x);
        float randomY = UnityEngine.Random.Range(bottomLeft.y, topRight.y);

        return new Vector2(randomX, randomY);

    }
}

