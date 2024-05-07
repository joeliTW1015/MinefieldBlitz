
using UnityEngine;

public struct Cell 
{
    public enum Type
    {
        Invalid,
        Empty,
        Number,
        Mine,
    }

    public Vector3Int position;
    public Type type;
    public bool flagged, revealed, exploded, monster;
    public int number;
}
