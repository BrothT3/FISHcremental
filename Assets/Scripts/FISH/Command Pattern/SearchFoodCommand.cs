using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class SearchFoodCommand : IResultCommand<Transform>
{
    Transform owner;

    public SearchFoodCommand(Transform Object)
    {
        owner = Object;
    }

    public Transform Execute()
    {
        return SearchForTarget();
    }

    Transform SearchForTarget()
    {
        // Find all colliders within the search range
        Collider2D[] hits = Physics2D.OverlapCircleAll(owner.transform.position, 500f);

        if (!owner.GetComponent<FISH>().carnivore)
        {
            foreach (Collider2D hit in hits)
            {
                float closesttarget = -1;
                if (hit.CompareTag("Food")) // Check if the collider has the correct tag
                {
                    float dist = Vector2.Distance(owner.transform.position, hit.transform.position);
                    if (closesttarget == -1 || dist > closesttarget)
                    {
                        closesttarget = dist;
                        return hit.transform;
                    }
                }
            }
            return null;
        }
        else
        {
            foreach (Collider2D hit in hits)
            {
                float smallesttarget = 100;
                if (hit.CompareTag("ClownFish")) // Check if the collider has the correct tag
                {
                    float size = hit.GetComponent<FISH>().currentScale;
                    if (smallesttarget == 100 || size < smallesttarget)
                    {
                        smallesttarget = size;
                        return hit.transform;
                    }
                }
            }
            return null;
        }


        //Debug.Log("Target found: " + target.name);
    }
}

