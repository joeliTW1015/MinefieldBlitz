using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class MinesweeperManager : MonoBehaviour
{
    public int width, height, mineNum;

    public static bool gameover;
    public GameObject background, player;
    public GameManager gameManager;
    public Cell[ , ] state;
    Board board;
    private void Awake() 
    {
        board = GetComponentInChildren<Board>();
        switch (ModeManager.mode)
        {
            case 1:
                width = 10;
                height = 10;
                mineNum = 12;
                break;
            case 2:
                width = 16;
                height = 16;
                mineNum = 31;
                break;
            case 3: 
                width = 32;
                height = 16;
                mineNum = 62;
                break;
            default: return;
        }
    }

    void Start() 
    {
        NewGame();
    }

    void NewGame()
    {
        gameover = false;
        state = new Cell[width, height];
        GenerateCells();
        GenerateMines();
        GeneratNum();
        board.Draw(state);
        background.transform.position = new Vector3(width / 2f, height / 2f, 0f);
        player.transform.position = new Vector3(width/2f -20f, height/2f + 10f, 0f);
    }

    void GenerateCells()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                state[x, y] = cell;
            }
        }
    }

    void GenerateMines()
    {
        for(int i = 0; i < mineNum; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while(state[x,y].type == Cell.Type.Mine)
            {
                x++;
                if(x >= width)
                {
                    y++;
                    x = 0;
                }
                if(y >= height)
                {
                    y = 0;
                }
            }
            state[x,y].type = Cell.Type.Mine;
        }
    }

    void GeneratNum()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Cell cell = state[x,y];
                
                if(cell.type == Cell.Type.Mine)
                {
                    continue;
                }
                cell.number = CountMine(x, y);
                if(cell.number > 0)
                {
                    cell.type = Cell.Type.Number;
                }
                //決定是否有怪物
                cell.monster = (Random.Range(1, 100) > 92);
                state[x,y] = cell;
            }
        }
    }

    int CountMine(int x, int y)
    {
        int count = 0;

        for(int dx = -1; dx <= 1; dx++)
        {
            for(int dy = -1; dy <= 1; dy++)
            {
                if(dx == 0 && dy == 0)
                {
                    continue;
                }
                int cheX = x + dx;
                int cheY = y + dy;
                if(cheX  < 0 || cheX >= width || cheY  < 0 || cheY >= height)
                {
                    continue;
                }

                if(state[cheX,cheY].type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }

        return count;
    }

    void Update() 
    {
        if(!gameover)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Flag();
            }
        }
    }

    void Flag()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);
        if(cell.type == Cell.Type.Invalid || cell.revealed)
        {
            return;
        }
        else if(cell.flagged)
        {
            GameManager.mineCount += 1;
        }
        else
        {
            GameManager.mineCount -= 1;
        }
        cell.flagged = !cell.flagged;
        state[cellPosition.x, cellPosition.y] = cell;
        board.Draw(state);
    }

    public void Reveal(int x, int y)
    { 
        Cell cell = GetCell(x, y);
        if(cell.type == Cell.Type.Invalid || cell.revealed)
        {
            return;
        }

        switch (cell.type)
        {
            case Cell.Type.Mine:
                Explode(cell);
                break;
            case Cell.Type.Empty:
                Flood(cell);
                CheckWin();
                break;
            default:
                cell.revealed = true;
                state[x, y] = cell;
                CheckWin();
                break;
        }

        board.Draw(state);
    }

    void Flood(Cell cell)
    {
        if(cell.revealed) return;
        if(cell.type == Cell.Type.Invalid || cell.type == Cell.Type.Mine) return;

        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        if(cell.type == Cell.Type.Empty)
        {
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
        }
        if(cell.monster)
            board.SpawnMonster(cell.position.x, cell.position.y);
    }

    void Explode(Cell cell)
    {
        Debug.Log("gameOver");
        gameover = true;
        cell.exploded = true;
        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;
        
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                cell = state[x,y];
                
                if(cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x,y] = cell;
                }
               
            }
        }
        gameManager.LoseGame();
    }

    void CheckWin()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Cell cell = state[x,y];
                
                if(cell.type != Cell.Type.Mine && !cell.revealed)
                {
                    return;
                }
               
            }
        }

        Debug.Log("Win");
        gameover = true;

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Cell cell = state[x,y];
                
                if(cell.type == Cell.Type.Mine && !cell.flagged)
                {
                    cell.flagged = true;
                }
                state[x,y] = cell;
               
            }
        }
        gameManager.WinGame();
    }

    Cell GetCell(int x, int y)
    {
        if(IsValid(x, y))
        {
            return state[x, y];
        }
        else
        {
            return new Cell();
        }
    }

    public bool IsValid(int x, int y)
    {
        if(x  < 0 || x >= width || y  < 0 || y >= height)
        {
            return false; 
        }
        else
        {
            return true;
        }
    }

}
