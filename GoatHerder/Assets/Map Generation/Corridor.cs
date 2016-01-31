using UnityEngine;

public enum Direction { North, East, South, West }

public class Corridor
{
    public int startXPos;        
    public int startYPos;         
    public int corridorLength;     
    public Direction direction;


    // Get the end position of the corridor based on it's start position and which direction it's heading.
    public int EndPositionX
    {
        get
        {
            if (direction == Direction.North || direction == Direction.South)
                return startXPos;
            if (direction == Direction.East)
                return startXPos + corridorLength - 1;
            return startXPos - corridorLength + 1;
        }
    }


    public int EndPositionY
    {
        get
        {
            if (direction == Direction.East || direction == Direction.West)
                return startYPos;
            if (direction == Direction.North)
                return startYPos + corridorLength - 1;
            return startYPos - corridorLength + 1;
        }
    }


    public void SetupCorridor(Room room, IntRange length, IntRange roomWidth, IntRange roomHeight, int columns, int rows, bool firstCorridor)
    {
        // Set a random direction (a random index from 0 to 3, cast to Direction).
        direction = (Direction)Random.Range(0, 4);

        //Makes sure there's no overlapping corridors on then next generation step
        Direction oppositeDirection = (Direction)(((int)room.enteringCorridor + 2) % 4);
        if (!firstCorridor && direction == oppositeDirection)
        {
            int directionInt = (int)direction;
            directionInt++;
            directionInt = directionInt % 4;
            direction = (Direction)directionInt;

        }
        corridorLength = length.Random;

        // Create a cap for how long the length can be (this will be changed based on the direction and position).
        int maxLength = length.m_Max;

        switch (direction)
        {
            // If the choosen direction is North (up)...
            case Direction.North:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth - 1);
                startYPos = room.yPos + room.roomHeight;
                maxLength = rows - startYPos - roomHeight.m_Min;
                break;
            case Direction.East:
                startXPos = room.xPos + room.roomWidth;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight - 1);
                maxLength = columns - startXPos - roomWidth.m_Min;
                break;
            case Direction.South:
                startXPos = Random.Range(room.xPos, room.xPos + room.roomWidth);
                startYPos = room.yPos;
                maxLength = startYPos - roomHeight.m_Min;
                break;
            case Direction.West:
                startXPos = room.xPos;
                startYPos = Random.Range(room.yPos, room.yPos + room.roomHeight);
                maxLength = startXPos - roomWidth.m_Min;
                break;
        }

        // We clamp the length of the corridor to make sure it doesn't go off the board.
        corridorLength = Mathf.Clamp(corridorLength, 1, maxLength);
    }
}