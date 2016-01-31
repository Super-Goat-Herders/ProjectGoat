using System.Collections;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Types of items to be put down. NPC's could go here as well
    public enum TileType { Wall, Floor }


    public int columns = 100;                                 
    public int rows = 100;                                    
    public IntRange numRooms = new IntRange(15, 20);         
    public IntRange roomWidth = new IntRange(3, 10);         
    public IntRange roomHeight = new IntRange(3, 10);        
    public IntRange corridorLength = new IntRange(6, 10);    
    public GameObject[] floorTiles;                          
    public GameObject[] wallTiles;                           
    public GameObject[] outerWallTiles;                      
    public GameObject player;

    private TileType[][] tiles;                               
    private Room[] rooms;                                     
    private Corridor[] corridors;                             
    private GameObject boardHolder;                           


    private void Start()
    {
        // Create the board holder. This can be changed for levels
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();

        InstantiateTiles();
        InstantiateOuterWalls();
    }


    void SetupTilesArray()
    {
        // Set the tiles jagged array to the correct width.
        tiles = new TileType[columns][];
        for (int i = 0; i < tiles.Length; i++) tiles[i] = new TileType[rows];
    }


    void CreateRoomsAndCorridors()
    {
        // Create the rooms array with a random size.
        rooms = new Room[numRooms.Random];
        // There should be one less corridor than there is rooms.
        corridors = new Corridor[rooms.Length - 1];
        // Create the first room and corridor.
        rooms[0] = new Room();
        corridors[0] = new Corridor();
        // Setup the first room
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

        // Setup the first corridor using the first room.
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);
        //This puts the player in the lower left corner of the first room
        Vector3 playerPos = new Vector3(rooms[0].xPos, rooms[0].yPos, 0);
        Instantiate(player, playerPos, Quaternion.identity);    
        for (int i = 1; i < rooms.Length; i++)
        {
            rooms[i] = new Room();
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }
        }
    }


    void SetTilesValuesForRooms()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            Room currentRoom = rooms[i];
            for (int j = 0; j < currentRoom.roomWidth; j++)
            {
                int xCoord = currentRoom.xPos + j;
                for (int k = 0; k < currentRoom.roomHeight; k++)
                {
                    int yCoord = currentRoom.yPos + k;
                    // The coordinates in the jagged array are based on the room's position and it's width and height.
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }


    void SetTilesValuesForCorridors()
    {
        for (int i = 0; i < corridors.Length; i++)
        {
            Corridor currentCorridor = corridors[i];
            for (int j = 0; j < currentCorridor.corridorLength; j++)
            {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;
                
                switch (currentCorridor.direction)
                {
                    case Direction.North: yCoord += j; break;
                    case Direction.East: xCoord += j; break;
                    case Direction.South: yCoord -= j; break;
                    case Direction.West: xCoord -= j; break;
                }

                // Set the tile at these coordinates to Floor.
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }


    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                // ... and instantiate a floor tile for it.
                InstantiateFromArray(floorTiles, i, j);

                // If the tile type is Wall...
                if (tiles[i][j] == TileType.Wall)
                {
                    // ... instantiate a wall over the top.
                    InstantiateFromArray(wallTiles, i, j);
                }
            }
        }
    }


    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }


    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        float currentY = startingY;
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(outerWallTiles, xCoord, currentY);

            currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        float currentX = startingX;
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWallTiles, currentX, yCoord);
            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Weird scaling trick that fixed the black lines/rendering issue.
        tileInstance.transform.localScale = new Vector3(1.01f, 1.01f, 1.0f);

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = boardHolder.transform;
    }
}