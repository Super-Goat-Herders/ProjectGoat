using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

//Game Engine
public class GameEngine : MonoBehaviour {
    public enum TileType { Wall, Floor }
    //Room Size
    public int columns = 100;
    public int rows = 100;
    //Range of rooms available per level
    public IntRange numRooms = new IntRange(15, 20);
    
    //Room Size settings
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    
    //Corridor length
    public IntRange corridorLength = new IntRange(6, 10);
    
    //Prefab Objects
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;
    //public GameObject[] NPCs;
    //....
    
    public GameObject player;
    
    //Level floor tiles
    private TileType[][] tiles;
    //Rooms in the level
    private Room[] rooms;
    //Cooridors in the level
    private Corridor[] corridors;
    //Container
    private GameObject levelHolder;
    
    //Level Initialization 
    private void Start() 
    {
        levelHolder = new GameObject("Level_1_Holder");
        setupTiles();
        generateRoomsAndCorridors();
        InstantiateTiles ();
        InstantiateOuterWalls ();   
    }
    
    void setupTiles()
    {
        //needed for level tile grid width
        tiles = new TileType[columns][];
        for (int i = 0; i < tiles.Length; ++i)
        {
           //needed for level tile grid height
           tiles[i] = new TileType[rows];
        }
    }
    void generateRoomsAndCorridors()
    {
        //Random number of rooms
        rooms = new Room[numRooms.Random];
        //And now we apply some pigeon hole principle for the number of corridors
        corridors = new Corridor[rooms.Length - 1];
        
        //First room of the level
        rooms[0] = new Room();
        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
        corridors[0] = new Corridor();
        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);
        
        //generating the rest of the rooms and corridors on the level
        for(int i = 1; i < rooms.Length; ++i)
        {
            rooms[i] = new Room();
            //Connect room i to corridor i - 1
            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);
            
            //And add a corridor to the new room
            if (i < corridors.Length)
            {
                corridors[i] = new Corridor();
                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);    
            }
            if(i == rooms.Length * .5f)
            {
                Vector3 playerPos = new Vector3 (rooms[i].xPos, rooms[i].yPos, 0);
                Instantiate(player, playerPos, Quaternion.identity); //??? No idea what this does
            }
        }
    }
    //tripple nested for loops ftw!!!
    void SetTileValuesForRooms()
    {
        for (int i = 0; i < rooms.Length; ++i)
        {
            Room currentRoom = rooms[i];
            //go through the currentRooms width
            for (int j = 0; j < currentRoom.roomWidth; ++j)
            {
                int xCoord = currentRoom.xPos + j;
                //for each horizontal tile, go up vertically through the room's height
                for (int k = 0; k < currentRoom.roomHeight; ++k)
                {
                    int yCoord = currentRoom.yPos + k;
                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }
    void SetTileValuesForCorridors()
    {
        for (int i = 0; i < corridors.Length; ++i)
        {
            Corridor currentCorridor = corridors[i];
            //go through the currentCorridors length
            for(int j = 0; j < currentCorridor.corridorLength; ++j)
            {
                int xCoord = currentCorridor.xStartPosition;
                int yCoord = currentCorridor.yStartPosition;
              
                //more direction stuff
                switch (currentCorridor.direction)
                {
                    case Direction.North: yCoord += j; break;
                    case Direction.East: xCoord += j; break;
                    case Direction.South: yCoord -= j; break;
                    case Direction.West: xCoord -= j; break;
                }
                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }
    
    void InstantiateTiles()
    {
        //Level floor tiles
        for (int i = 0; i < tiles.Length; ++i)
        {
            for (int j = 0; j < tiles[i].Length; ++j)
            {
                InstantiateFromArray(floorTiles, i, j);
                
                //Tile Types
                
                //Wall
                if (tiles[i][j] == TileType.Wall) InstantiateFromArray(wallTiles, i, j);
            }
        }
    }
    void InstantiateOuterWalls()
    {
        //Place 1 unit outside the Level Grid
        float leftEdgeX = -1f;
        float rightEdgeX = columns + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = rows + 0f;
        
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);
        
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }
    
    void InstantiateVerticalOuterWall(float xCoord, float yStart, float yEnd)
    {
        float currentY = yStart;
        while (currentY <= yEnd)
        {
            InstantiateFromArray(outerWallTiles, xCoord, currentY);
            currentY++;
        }
    }
    void InstantiateHorizontalOuterWall(float xStart, float xEnd, float yCoord)
    {
        float currentX = xStart;
        while (currentX <= xEnd)
        {
            InstantiateFromArray(outerWallTiles, currentX, yCoord);
            currentX++;
        }
    }
    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        //psuedo-random index for the array
        int randomIndex = Random.Range(0, prefabs.Length);
        Vector3 position = new Vector3(xCoord, yCoord, 0f);
        
        //create an instance of the randomly selected prefab
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;
        
        tileInstance.transform.parent = levelHolder.transform;
    }
}
//Serializable for the inspector???
[Serializable]
public class IntRange
{
    public int m_Min, m_Max;
    public IntRange(int min, int max) { m_Min = min; m_Max = max; }
    public int Random { get{ return UnityEngine.Random.Range(m_Min, m_Max); }}
}


//Corridor stuff
public enum Direction { North, East, South, West }
public class Corridor
{
    public int xStartPosition, yStartPosition, corridorLength;
    public Direction direction;
    
    //get the end position of the corridor using its start position and direction
    public int xEndPosition
    {
        get
        {
            if(direction == Direction.North || direction == Direction.South) return xStartPosition;
            if(direction == Direction.East) return xStartPosition + corridorLength - 1;
            return xStartPosition - corridorLength + 1;
        }
    }
    public int yEndPosition
    {
        get
        {
            if(direction == Direction.East || direction == Direction.West) return yStartPosition;
            if(direction == Direction.North) return yStartPosition + corridorLength - 1;
            return yStartPosition - corridorLength + 1;
        }
    }
    
    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        //Give the corridor a psuedo-random direction
        direction = (Direction)Random.Range(0,4);
        
        //Give the opposite end the correct direction
        Direction oppositeDirection = (Direction)(((int)room.corridorIn + 2) % 4);
        
        //If north and random direction is opposite previous corridors direction, rotate 90 degrees clockwise 
        if(!firstCorridor && direction == oppositeDirection)
        {
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;
        }
        //psuedo-random length
        corridorLength = length.Random;
        //Set a max length
        int maxLength = length.m_Max;
        switch (direction)
        {
            case Direction.North:
                xStartPosition = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);
                yStartPosition = room.yPos + room.roomHeight;
                maxLength = rows - yStartPosition - roomHeight.m_Min;
                break;
            case Direction.East:
                xStartPosition = room.xPos + room.roomWidth;
                yStartPosition = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - xStartPosition - roomWidth.m_Min;
                break;
            case Direction.South:
                xStartPosition = Random.Range(room.xPos, room.xPos + room.roomWidth);
                yStartPosition = room.yPos;
                maxLength = yStartPosition - roomHeight.m_Min;
                break;
            case Direction.West:
                xStartPosition = room.xPos;
                yStartPosition = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = xStartPosition - roomWidth.m_Min;
                break;
        }
        //make sure the corridor doesn't go off the board
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }    
}


//Room stuff
public class Room
{
    //Rooms created with (x,y) = (0,0) being the lower left corner. The rooms width and height are determined by how many base level tiles across it is. 
    public int xPos, yPos, roomWidth, roomHeight;
    
    //Direction of the entering corridor
    public Direction corridorIn;
    //Default corridor
    
    //Setup the first room
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows)
    {
        //psuedo-random width and height for the room
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;
        
        //We might want to change this later, but this puts the first room in the middle of the level grid
        xPos = Mathf.RoundToInt(columns / 2f - roomWidth / 2f);
        yPos = Mathf.RoundToInt(rows / 2f - roomWidth / 2f);
    }
    //overload function for setting up the rest of the rooms
    public void SetupRoom(IntRange widthRange, IntRange heightRange, int columns, int rows, Corridor corridor)
    {
        //set direction of entrance corridor
        corridorIn = corridor.direction;
        //psuedo-random width and height for the room
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;
        
        //Make sure rooms are staying within the bounds of the level grid
        switch (corridor.direction)
        {
            case Direction.North:
                roomHeight = Mathf.Clamp(roomHeight, 1, rows - corridor.yEndPosition);
                yPos = corridor.yEndPosition;
                //constrained psuedo-random x initial position
                xPos = Random.Range(corridor.xEndPosition - roomWidth + 1, corridor.xEndPosition);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.East:
                roomWidth = Mathf.Clamp(roomWidth, 1, columns - corridor.xEndPosition);
                xPos = corridor.xEndPosition;
                //constrained psuedo-random y initial position
                yPos = Random.Range(corridor.yEndPosition - roomHeight + 1, corridor.yEndPosition);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
            case Direction.South:
                roomHeight = Mathf.Clamp(roomHeight, 1, corridor.yEndPosition);
                yPos = corridor.yEndPosition - roomHeight + 1;
                //constrained psuedo-random x initial position
                xPos = Random.Range(corridor.xEndPosition - roomWidth + 1, corridor.xEndPosition);
                xPos = Mathf.Clamp(xPos, 0, columns - roomWidth);
                break;
            case Direction.West:
                roomWidth = Mathf.Clamp(roomWidth, 1, corridor.xEndPosition);
                xPos = corridor.xEndPosition - roomWidth + 1;
                //constrained psuedo-random y initial position
                yPos = Random.Range(corridor.yEndPosition - roomHeight + 1, corridor.yEndPosition);
                yPos = Mathf.Clamp(yPos, 0, rows - roomHeight);
                break;
        }
    } 
}


