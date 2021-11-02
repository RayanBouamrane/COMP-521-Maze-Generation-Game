using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class MazeGenerator : MonoBehaviour {

    public GameObject platform;
    public GameObject wall;

    void Start() {
	Cell[,]theOneGrid = new Cell[5, 5];
	theOneGrid = MazeGen(5);
	
	float x = -25.6f;
	float y = 1.6f;
	float z = -12.6f;

	float wallZ = -14.3f;

	Quaternion q = Quaternion.identity;
	q.eulerAngles = new Vector3(0,90,0);
	for (int i = 0; i < 5; i++)
	    for (int j = 0; j < 5; j++) {
		
		//instatiated all 25 ground platforms
		Instantiate(platform, new Vector3(x - (i * 5.3f), y, z + (j * 5.3f)), Quaternion.identity);

		//if cell has an up, down, left, or right wall, instantiate a wall object at 
		//appropriate location to seperate platforms
		if(theOneGrid[i, j].uWall) 
		    Instantiate(wall, new Vector3(x - (i * 5.3f), 4f, wallZ+ (j * 5.3f)), Quaternion.identity);
		
		if(theOneGrid[i, j].dWall)
		    Instantiate(wall, new Vector3(x - (i * 5.3f), 4f, wallZ + 5.3f + (j * 5.3f)), Quaternion.identity);
		
		if(theOneGrid[i, j].lWall) 
		    Instantiate(wall, new Vector3(x - (i * 5.3f) + 2.65f, 4f, z + (j * 5.3f)), q);

		if(theOneGrid[i, j].rWall) 
		    Instantiate(wall, new Vector3(x - (i * 5.3f) - 2.65f, 4f, z + (j * 5.3f)), q);
	}
    }
	
    class Cell {
        
	//Cell of maze consists of bools indicating if walls in each direction are present
        public bool uWall = true;
        public bool dWall = true;
        public bool lWall = true;
        public bool rWall = true;

	//the previous cell in algorithm that led to this one, allows backtracking
        public int iPrev;
        public int jPrev;

        public int x;
        public int y;

        public Cell(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }


    //Recursive backtracking algorithm is implemented to create perfect maze structure
    //Maze is stored as 2D array of Cells
    static Cell[,] MazeGen(int s) {
        
        Cell[,]theOneGrid = new Cell[s, s];

        for (int i = 0; (i < s); i++) {
            for (int j = 0; (j < s); j++) {
                theOneGrid[i, j] =new Cell(i, j);
            }
        }
        
        Random rand = new Random();

        bool[,]nodeHasBeenVisited = new bool[s, s];

        Cell current = theOneGrid[rand.Next(0, s), rand.Next(0, s)];
	//Initial cell's previous values set to flag -1, when the pointer returns here, algorithm terminates
        current.iPrev = -1;
        current.jPrev = -1;

        do {
            int x = current.x;
            int y = current.y;
            Cell next;
            List<Cell> currentNeighbors = new List<Cell>();


	    //checks if a cell's up, down, left and right cells respectively are candidates to be next
            if ((x > 0) && !nodeHasBeenVisited[(x - 1),y])
                currentNeighbors.Add(theOneGrid[(x - 1), y]);

            if ((x < s-1) && !nodeHasBeenVisited[(x + 1),y])
                currentNeighbors.Add(theOneGrid[(x + 1), y]);

            if ((y > 0) && !nodeHasBeenVisited[x,(y - 1)])
                currentNeighbors.Add(theOneGrid[x, (y - 1)]);

            if ((y < s-1) && !nodeHasBeenVisited[x,(y + 1)])
                currentNeighbors.Add(theOneGrid[x, (y + 1)]);

            int r = rand.Next(0, currentNeighbors.Count);

	    //if no cells are valid candidates, we backtrack
            if (currentNeighbors.Count == 0) {
                nodeHasBeenVisited[x, y] =true;
                current = theOneGrid[current.iPrev, current.jPrev];
		    continue;
            }
	

            next = currentNeighbors[r];


	    //both if blocks check the relative position of two cells, and destroy corresponding wall
            if (y == next.y) {
                if (x > next.x) {
                    current.lWall = false;
                    next.rWall = false;
                } else {
                    current.rWall = false;
                    next.lWall = false;
                }
            }

            if (x == next.x) {
                if (y < next.y) {
                    current.dWall = false;
                    next.uWall = false;
                } else {
                    current.uWall = false;
                    next.dWall = false;
                }
            }

            nodeHasBeenVisited[x, y] =true;
            next.iPrev = x;
            next.jPrev = y;
            current = next;

        } while ((current.iPrev != -1));

	//create entrance and exit
	theOneGrid[0, rand.Next(0,s)].lWall = false;
	theOneGrid[4, rand.Next(0,s)].rWall = false;

	return theOneGrid;
        
    }
}