using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ScanPath : MonoBehaviour
{
    AstarPath astarPath;
    void Start()
    {
        astarPath = GetComponent<AstarPath>();
    }


    public void PathUpdate()
    {
        astarPath.graphs[0].Scan();
    }
}
