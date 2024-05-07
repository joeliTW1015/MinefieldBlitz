using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GhostGFX : MonoBehaviour
{
    // Start is called before the first frame update
    public AIPath aIPath;
    public GhostDie ghostDie;

    // Update is called once per frame
    void Update()
    {
        if(aIPath.desiredVelocity.x >= 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void AnimatorTrigger()
    {
        ghostDie.DeadCheck();
    }

}
