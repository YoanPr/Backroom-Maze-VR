using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

// Generate a random maze at the start of the game
public class MazeGenerator : MonoBehaviour
{

    [SerializeField] int gridSize = 11;

    [SerializeField] float cellSize = 20;

    [SerializeField] int height = 20;

    // Prefabs
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject[] roofsPrefab;

    public static event Action<float, float> onMazeGenerated;

    private bool[][] maze;

    private List<Vector2Int> wallList;

    void Start()
    {
        InitialiazeGrid();

        // Take a random cell to start 
        // we multiply the result by 2 in order to have a cell and not a wall
        int xChoosen = Random.Range(0, gridSize) * 2;
        int yChoosen = Random.Range(0, gridSize) * 2;
        // Cell is visited
        maze[xChoosen][yChoosen] = true;
        // add the walls to the list
        addWallToList(xChoosen, yChoosen);
        while(wallList.Count > 0)
        {
            // Take a random wall from the list
            Vector2Int wall = wallList[Random.Range(0, wallList.Count)];
            // find the cells that are next to the wall
            Vector2Int cell1 = new Vector2Int(wall.x - wall.x % 2, wall.y - wall.y % 2);
            Vector2Int cell2 = new Vector2Int(wall.x + wall.x % 2, wall.y + wall.y % 2);
            // If one of the cell is not visited yet, we remove the wall and set this cell to visited
            // We then add walls from this cells to our walls list to visit
            if (!maze[cell1.x][cell1.y] || !maze[cell2.x][cell2.y])
            {
                maze[wall.x][wall.y] = false;
                if (!maze[cell1.x][cell1.y])
                {
                    maze[cell1.x][cell1.y] = true;
                    addWallToList(cell1.x, cell1.y);
                }
                else
                {
                    maze[cell2.x][cell2.y] = true;
                    addWallToList(cell2.x, cell2.y);
                }
            }
            // Remove the wall from the list
            wallList.Remove(wall);
        }

        PaintTheMaze();
    }

    // Initialiaze the grid array
    // Every cell are set to false (not visited yet)
    // Every wall are set to true
    private void InitialiazeGrid()
    {
        maze = new bool[2 * gridSize - 1][];
        wallList = new List<Vector2Int>();
        for (int i = 0; i < maze.Length; i++)
        {
            maze[i] = new bool[2 * gridSize - 1];
            for (int j = 0; j < maze[i].Length; j++)
            {
                // cells
                if (i % 2 == 0 && j % 2 == 0)
                {
                    maze[i][j] = false;
                }
                // walls
                else
                {
                    maze[i][j] = true;
                }

            }
        }
    }

    // Add wall next to cell into the list
    private void addWallToList(int x, int y)
    {
        if (x > 0) wallList.Add(new Vector2Int(x - 1, y));
        if (y > 0) wallList.Add(new Vector2Int(x, y - 1));
        if (x < 2 * gridSize - 2) wallList.Add(new Vector2Int(x + 1, y));
        if (y < 2 * gridSize - 2) wallList.Add(new Vector2Int(x, y + 1));
    }

    // Paint the maze into the game, with wall and roofs
    private void PaintTheMaze()
    {
        // set the coord of the exit
        Vector2 exit = new Vector2(maze.Length, maze[0].Length - 1);
        for (int i = -1; i < maze.Length + 1; i++)
        {
            for (int j = -1; j < maze[0].Length + 1; j++)
            {
                bool isABorder = j < 0 || i < 0 || i == maze.Length || j == maze[i].Length;
                bool isNotExit = j != exit.y || i != exit.x;
                // If it's a border or a wall that is set to true we add a wall in the game
                if (isNotExit && (isABorder || ((i % 2 != 0 || j % 2 != 0) && maze[i][j])))
                {
                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i * cellSize, height / 2, j * cellSize), wallPrefab.transform.rotation);
                    newWall.transform.localScale = new Vector3(cellSize * newWall.transform.localScale.x, height, cellSize * newWall.transform.localScale.z);
                    newWall.GetComponent<BoxCollider>().layerOverridePriority = Mathf.Abs(i + j);
                }
                // If it's a cell, the exit or the a wall set to false we add a roof
                else
                {
                    GameObject roof = roofsPrefab[Random.Range(0, roofsPrefab.Length)];
                    GameObject newRoof = Instantiate(roof,
                        new Vector3(i * cellSize, height - 0.8f, j * cellSize),
                        roof.transform.rotation);
                    newRoof.transform.localScale = new Vector3(cellSize * newRoof.transform.localScale.x, newRoof.transform.localScale.y, cellSize * newRoof.transform.localScale.z);
                }
            }
        }
        onMazeGenerated?.Invoke(exit.x * cellSize + cellSize / 2, (exit.y + 1) * cellSize + cellSize / 2);
    }
}
