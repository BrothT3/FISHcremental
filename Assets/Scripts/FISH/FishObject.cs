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
    public float MoveSpeed;
    public float Scale;
    public float MaxScale;
    public bool Carnivore;
    public string Tag;
    [Header("Hunger")]
    public float Hunger;
    public float MaxHunger;
    public float HungerRate;
    public float HungryRange;
    [Header("Resources")]
    public GameObject CoinType;
    public float coinDropInterval;
    public float coinValue;
    [Header("Purchase")]
    public string ResourceName;
    public int cost;

}

