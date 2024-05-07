using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class Board : MonoBehaviour
{
    public List<GameObject> monsterList;
    int spawnIndex;
    Vector3 spawnOffset;
    public Tilemap tilemap{get; private set;}
    public ScanPath scanPath;
    MinesweeperManager mineManager;
    public Tile num1, num2, num3, num4, num5, num6, num7, num8, mineT, explodedT, flagT, emptyT, unknowT;
    private void Awake()
    {
        spawnOffset =new Vector3(0.5f, 0.5f, 0);
        tilemap = GetComponent<Tilemap>();
        mineManager = GetComponentInParent<MinesweeperManager>();   
    }
 
    public void Draw(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Cell cell = state[x,y];
                tilemap.SetTile(cell.position, GetTile(cell));
            }
        }
        Invoke("PathUpdateOnBoard" , 0.05f);
    }
    void PathUpdateOnBoard()
    {
        scanPath.PathUpdate();
    }

    Tile GetTile(Cell cell)
    {
        if(cell.revealed)
        {
            if(cell.type == Cell.Type.Empty)
            {
                return emptyT;
            }
            else if(cell.type == Cell.Type.Number)
            {
                return GetTileNumber(cell);
            }
            else if(cell.type == Cell.Type.Mine)
            {
                if(cell.exploded)
                {
                    return explodedT;
                }
                else
                {
                    return mineT;
                }
            }
            else
            {
                return null;
            }
        }
        else if(cell.flagged)
        {
            return flagT;
        }
        else
        {
            return unknowT;
        }

    }

    Tile GetTileNumber(Cell cell)
    {
        switch (cell.number)
        {
            case 1: return num1;
            case 2: return num2;
            case 3: return num3;
            case 4: return num4;
            case 5: return num5;
            case 6: return num6;
            case 7: return num7;
            case 8: return num8;
            default:return null;
        }
    }

    //tricky!
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            Vector3 hitPos = Vector3.zero;
            Vector3Int hitCellPos = Vector3Int.zero;
            foreach (ContactPoint2D hit in other.contacts)
            {
                hitPos.x = hit.point.x - 0.01f * hit.normal.x;
                hitPos.y = hit.point.y - 0.01f * hit.normal.y;
                hitCellPos = tilemap.WorldToCell(hitPos);
                mineManager.Reveal(hitCellPos.x, hitCellPos.y);
                if(mineManager.IsValid(hitCellPos.x, hitCellPos.y))
                    if(mineManager.state[hitCellPos.x, hitCellPos.y].monster)
                    {
                        SpawnMonster(hitCellPos.x, hitCellPos.y);
                    }
            }
        }

    }

    public void SpawnMonster(int tempX, int tempY)
    {
        mineManager.state[tempX, tempY].monster = false;
        Vector3Int spawnPos = new Vector3Int(tempX, tempY, 0);
        spawnIndex = Random.Range(0, monsterList.Count);
        Instantiate(monsterList[spawnIndex], tilemap.CellToWorld(spawnPos) + spawnOffset, transform.rotation);
    }
}
