using System;

namespace TextBasedRPG
{
    static class World
    {
        static public int SizeX { get; private set; }
        static public int SizeY { get; private set; }

        static public Room[,] rooms;

        static public void Generate(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            Room[,] generatedRooms = new Room[sizeX, sizeY];
            Random random = new Random();

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    generatedRooms[i, j] = new Room("Room", i, j, random);
                }
            }

            HuntAndKill mazeBuilder = new HuntAndKill();
            mazeBuilder.BuildMaze(generatedRooms, random);

            rooms = generatedRooms;
        }
    }
}
