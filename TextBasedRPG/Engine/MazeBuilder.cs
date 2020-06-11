using System;
using System.Collections.Generic;

namespace TextBasedRPG
{
    class HuntAndKill
    {
        int rows;
        int columns;

        public void BuildMaze(Room[,] rooms, Random random)
        {
            rows = rooms.GetLength(0);
            columns = rooms.GetLength(1);

            MazeCell[,] mazeCells = new MazeCell[rows, columns];
            List<MazeCell> unvisitedCells = new List<MazeCell>();

            int id = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    mazeCells[i, j].room = rooms[i, j];
                    mazeCells[i, j].isVisited = false;
                    mazeCells[i, j].id = id;

                    unvisitedCells.Add(mazeCells[i, j]);
                    id++;
                }
            }

            STAGE stage = STAGE.KILL;

            //choose a starting location
            MazeCell current = mazeCells[0, 0];

            while(stage != STAGE.COMPLETED)
            {
                //perform a random walk, carving passages to unvisited neighbours until the current cell has no unvisited neighbours
                while (stage == STAGE.KILL)
                {
                    //remove current from unvisitedCells
                    for (int i = unvisitedCells.Count - 1; i >= 0; i--)
                    {
                        if (current.id == unvisitedCells[i].id)
                        {
                            mazeCells[current.room.X, current.room.Y].isVisited = true;
                            unvisitedCells.RemoveAt(i);
                            break;
                        }
                    }

                    //get unvisited neighbours
                    List<MazeCell> unvisitedNeighbours = FindNeighbours(current, mazeCells);
                    for (int i = unvisitedNeighbours.Count - 1; i >= 0; i--)
                    {
                        if (unvisitedNeighbours[i].isVisited)
                        {
                            unvisitedNeighbours.RemoveAt(i);
                        }
                    }

                    if (unvisitedNeighbours.Count == 0)
                    {
                        stage = STAGE.HUNT;
                        break;
                    }

                    MazeCell next = unvisitedNeighbours[random.Next(0, unvisitedNeighbours.Count)];
                    Carve(random, current, next);
                    current = next;
                }

                //enter hunt mode -- scan the grid looking for an unvisited cell which is adjacent to a visited cell. 
                //carve a passage between the two and let the formely unvisited cell be the new starting location.
                while (stage == STAGE.HUNT)
                {
                    if (unvisitedCells.Count == 0)
                    {
                        stage = STAGE.COMPLETED;
                        break;
                    }

                    for (int i = 0; i < unvisitedCells.Count; i++)
                    {
                        MazeCell hunted = unvisitedCells[i];

                        //get visited neighbours
                        List<MazeCell> visitedNeighbours = FindNeighbours(hunted, mazeCells);
                        for (int k = visitedNeighbours.Count - 1; k >= 0; k--)
                        {
                            if (!visitedNeighbours[k].isVisited)
                            {
                                visitedNeighbours.RemoveAt(k);
                            }
                        }

                        if (visitedNeighbours.Count == 0) continue;

                        MazeCell next = visitedNeighbours[random.Next(0, visitedNeighbours.Count)];
                        Carve(random, hunted, next);
                        current = hunted;

                        stage = STAGE.KILL;
                        break;
                    }
                }
            }
        }

        private void Carve(Random random, MazeCell current, MazeCell next)
        {
            //create doors between the rooms
            current.room.CreateDoor(random, next.room.X, next.room.Y);
            next.room.CreateDoor(random, current.room.X, current.room.Y);
        }

        private List<MazeCell> FindNeighbours(MazeCell cell, MazeCell[,] mazeCells)
        {
            int x = cell.room.X;
            int y = cell.room.Y;

            List<MazeCell> neighbours = new List<MazeCell>();
            if (x < rows - 1)
            {
                neighbours.Add(mazeCells[x + 1, y]);//east
            }
            if (x > 0)
            {
                neighbours.Add(mazeCells[x - 1, y]);//west
            }
            if (y < columns - 1)
            {
                neighbours.Add(mazeCells[x, y + 1]);//north
            }
            if (y > 0)
            {
                neighbours.Add(mazeCells[x, y - 1]);//south
            }
            return neighbours;
        }
    }

    struct MazeCell
    {
        public int id;
        public Room room;
        public bool isVisited;
    }

    enum STAGE
    {
        KILL, HUNT, COMPLETED
    }
}
