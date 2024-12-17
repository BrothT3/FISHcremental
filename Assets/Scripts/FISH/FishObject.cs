using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "FishObject", menuName = "Objects/New FishObject")]
public class FishObject : ScriptableObject
{
    public Sprite Sprite;
    public GameObject CoinType;
    public float MoveSpeed;
    public float Hunger;
    public float HungerRate;
    public float coinDropInterval;
    public float coinValue;
}

