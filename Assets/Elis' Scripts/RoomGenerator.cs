using UnityEngine;
using UnityEngine . Tilemaps;
using System . Collections . Generic;
using System . Linq;
using UnityEditor;

public class RoomGenerator : MonoBehaviour
{
    public Tilemap tilemap; // Tile map that will the dungeon will be generated on
    public TileBase floorTile; // the floor tile 
    public TileBase wallTile; // basic tile for all walls outer edges
    public TileBase tallWallTile; // wall tile for the top side walls that add height

    public int dungeonWidth = 20; //total area width of area where dungeon can generate
    public int dungeonHeight = 20; // total height of area where dungeon can generate

    public int minRoomSize = 3; //min size that rooms can  be
    public int maxRoomSize = 6; // max size rooms can be
    public int numRooms = 5; // int for the random amount of rooms that can generate
  


    public GameObject [] spawnLoot;  // loot that will spawn throughout the dungeon
    public GameObject [] spawnEnemies;  // enemies that will spawn through the dungeon
    public GameObject [ ] destroyableObjects; // objects and furnature to spawn



   public int numLootToSpawn = 3; // number of loot to spawn
    public int numEnemiesToSpawn = 3;  // number of enemies to spawn
   public int objectToSpawn = 6; // number of objects to spawn

    public GameObject startPointPrefab; // start point prefab
    public GameObject endPointPrefab;   // end point prefab

    private void Start ( )
    {
        // get random amounts to determine how many rooms will spawn and how many enemies and loot objects will spawn based on level
        numRooms = Random.Range(3, 8); 
        objectToSpawn = Random.Range(2 * numRooms, objectToSpawn * numRooms ) * numRooms;
        numEnemiesToSpawn = Mathf . RoundToInt ( 2.3f  * numRooms) + GameManager.Instance.level /5;
        numLootToSpawn = Mathf . RoundToInt (1 * numRooms + 1 * 5 % GameManager . Instance . level) + Random . Range (-2, 3); 
       

        GenerateDungeon ();
    }


    public void GenerateDungeon ( )
    {
        // sets the dungeon with empty tiles
        for ( int x = 0 ; x < dungeonWidth ; x++ )
        {
            for ( int y = 0 ; y < dungeonHeight ; y++ )
            {
                tilemap . SetTile (new Vector3Int (x , y , 0) , null);
            }
        }

        // generates rooms with random sizes within the min and max size ranges
        List<Rect> rooms = new List<Rect>();

        for ( int i = 0 ; i < numRooms ; i++ )
        { 
            int roomWidth = Random.Range(minRoomSize, maxRoomSize + 1);
            int roomHeight = Random.Range(minRoomSize, maxRoomSize + 1);

            int xPos = Random.Range(1, dungeonWidth - roomWidth - 1);
            int yPos = Random.Range(1, dungeonHeight - roomHeight - 1);

            Rect newRoom = new Rect(xPos, yPos, roomWidth, roomHeight);

            // detect if the room would overlap with another, if it doesnt then generate the room     
             bool overlaps = rooms.Any(existingRoom => newRoom.Overlaps(existingRoom));
            if ( !overlaps )
            {
           
                rooms . Add (newRoom);

                
                GenerateIrregularRoom (newRoom);
            }
            else
            {
           
                i--;
            }
        }

        // connects each room to the next in the index
        for ( int i = 1 ; i < rooms . Count ; i++ )
        {
       
            ConnectRooms (rooms [ i - 1 ] , rooms [ i ]);
        }

        // generate all secondary items like walls and objects
        AddWallsAroundRoomsAndCorridors ();

        
        SpawnEnemiesTiles (rooms);
        SpawnLootTiles (rooms);
        SpawnObjectsTiles(rooms);


        SpawnStartAndEndPoints (rooms); 
    }


    void GenerateIrregularRoom ( Rect room )
    {
        // ramdomly picks if the room generated is going to be square or circular 

        //generate a square room within the chosen min and max height and width
        if ( Random . value > 0.5f )
        {

            for ( int x = Mathf . FloorToInt (room . x) ; x < Mathf . FloorToInt (room . x + room . width) ; x++ )
            {
                for ( int y = Mathf . FloorToInt (room . y) ; y < Mathf . FloorToInt (room . y + room . height) ; y++ )
                {
                    tilemap . SetTile (new Vector3Int (x , y , 0) , floorTile);
                }
            }
        }
        else
        {
            // generate circular room within the randomly chosen height and width 
            int centerX = Mathf.FloorToInt(room.x + room.width / 2);
            int centerY = Mathf.FloorToInt(room.y + room.height / 2);
            int radius = Mathf.FloorToInt(Mathf.Min(room.width, room.height) / 2);

            for ( int x = centerX - radius ; x <= centerX + radius ; x++ )
            {
                for ( int y = centerY - radius ; y <= centerY + radius ; y++ )
                {
                    if ( Vector2 . Distance (new Vector2 (x , y) , new Vector2 (centerX , centerY)) <= radius )
                    {
                        tilemap . SetTile (new Vector3Int (x , y , 0) , floorTile);
                    }
                }
            }
        }
    }


    void ConnectRooms ( Rect roomA , Rect roomB )
    {
        // find the center of room A
        Vector3Int centerA = new Vector3Int(
        Mathf.FloorToInt(roomA.x + roomA.width / 2),
        Mathf.FloorToInt(roomA.y + roomA.height / 2),
        0
    );
        // find the center of room B
        Vector3Int centerB = new Vector3Int(
        Mathf.FloorToInt(roomB.x + roomB.width / 2),
        Mathf.FloorToInt(roomB.y + roomB.height / 2),
        0
    );

    
        int corridorWidth = Random.Range(2, 5); 

        // randomly generates a corridor between two rooms that are either vertically connect or horizontally
        if ( Random . value < 0.5f )
        {
       
            CreateHorizontalCorridor (centerA . x , centerB . x , centerA . y , corridorWidth);
            CreateVerticalCorridor (centerA . y , centerB . y , centerB . x , 3); 
        }
        else
        {
    
            CreateVerticalCorridor (centerA . y , centerB . y , centerA . x , corridorWidth);
            CreateHorizontalCorridor (centerA . x , centerB . x , centerB . y , 3); 
        }
    }

    void CreateHorizontalCorridor ( int startX , int endX , int y , int width )
    {
        // finds the direction between the two rooms and generates a corridor to connect the two 
        int direction = (startX < endX) ? 1 : -1;

        for ( int x = startX ; x != endX + direction ; x += direction )
        {
            for ( int w = -width / 2 ; w <= width / 2 + 1 ; w++ ) 
            {
                tilemap . SetTile (new Vector3Int (x , y + w , 0) , floorTile);
            }
        }
    }

    void CreateVerticalCorridor ( int startY , int endY , int x , int height )
    {
        // finds the direction between the two rooms and generates a corridor to connect the two 
        int direction = (startY < endY) ? 1 : -1;

        for ( int y = startY ; y != endY + direction ; y += direction )
        {
            for ( int h = -height / 2 ; h <= height / 2 ; h++ )
            {
                tilemap . SetTile (new Vector3Int (x + h , y , 0) , floorTile);
            }
        }
    }
    void AddWallsAroundRoomsAndCorridors ( )
    {
        // detects the outer edges of all rooms and corridors and draws wall tiles around them
        BoundsInt bounds = tilemap.cellBounds;

        for ( int x = bounds . x ; x < bounds . x + bounds . size . x ; x++ )
        {
            for ( int y = bounds . y ; y < bounds . y + bounds . size . y ; y++ )
            {
                if ( tilemap . GetTile (new Vector3Int (x , y , 0)) == floorTile )
                {
                    for ( int w = -1 ; w <= 1 ; w++ )
                    {
                        for ( int h = -1 ; h <= 1 ; h++ )
                        {
                            if ( w == 0 && h == 0 )
                            {
                                continue; // skip the floor tile itself
                            }

                            int wallHeight = (h == -1) ? 1 : 3;

                            // check if it's a taller wall (top wall of a room or corridor)
                            bool isTallerWall = (h == 1 && tilemap.GetTile(new Vector3Int(x + w, y + h - 1, 0)) == floorTile);

                            if ( tilemap . GetTile (new Vector3Int (x + w , y + h , 0)) == null )
                            {
                                for ( int i = 0 ; i < wallHeight ; i++ )
                                {
                                    // if the wall tile is a tall wall place it,and if not set a normal wall
                                    if ( isTallerWall )
                                    {
                                        tilemap . SetTile (new Vector3Int (x + w , y + h + i , 0) , tallWallTile);
                                    }
                                    else
                                    {
                                        tilemap . SetTile (new Vector3Int (x + w , y + h + i , 0) , wallTile);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    void SpawnLootTiles ( List<Rect> rooms )
    {
        // get all floor tile positions
        List<Vector3Int> floorTilePositions = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;
        foreach ( var position in bounds . allPositionsWithin )
        {
            if ( tilemap . HasTile (position) && tilemap . GetTile (position) == floorTile )
            {
                floorTilePositions . Add (position);
            }
        }

        // shuffle the floor tile positions
        floorTilePositions = floorTilePositions . OrderBy (x => Random . value) . ToList ();

        // spawn objects on a few random floor tiles in both rooms and corridors
        for ( int i = 0 ; i < Mathf . Min (numLootToSpawn , floorTilePositions . Count) ; i++ )
        {
            Vector3Int spawnPosition = floorTilePositions[i];
          
           Instantiate (spawnLoot [Random.Range(0,spawnLoot .Length)] , tilemap . GetCellCenterWorld (spawnPosition) , Quaternion . identity);
        }
    }

    void SpawnEnemiesTiles ( List<Rect> rooms )
    {
        // get all floor tile positions
        List<Vector3Int> floorTilePositions = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;
        foreach ( var position in bounds . allPositionsWithin )
        {
            if ( tilemap . HasTile (position) && tilemap . GetTile (position) == floorTile )
            {
                floorTilePositions . Add (position);
            }
        }


        floorTilePositions = floorTilePositions . OrderBy (x => Random . value) . ToList ();

        // spawn enemies around the floor tiles of the rooms and corridors
        for ( int i = 0 ; i < Mathf . Min (numEnemiesToSpawn , floorTilePositions . Count) ; i++ )
        {
            Vector3Int spawnPosition = floorTilePositions[i];

            Instantiate (spawnEnemies [ Random . Range (0 , spawnEnemies. Length) ] , tilemap . GetCellCenterWorld (spawnPosition) , Quaternion . identity);
        }
    }

    void SpawnObjectsTiles ( List<Rect> rooms )
    {
        // get all floor tile positions
        List<Vector3Int> floorTilePositions = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;
        foreach ( var position in bounds . allPositionsWithin )
        {
            if ( tilemap . HasTile (position) && tilemap . GetTile (position) == floorTile )
            {
                floorTilePositions . Add (position);
            }
        }


        floorTilePositions = floorTilePositions . OrderBy (x => Random . value) . ToList ();

        // spawn objects in the rooms and corridors on th efloor tiles
        for ( int i = 0 ; i < Mathf . Min (objectToSpawn , floorTilePositions . Count) ; i++ )
        {
            Vector3Int spawnPosition = floorTilePositions[i];

            Instantiate (destroyableObjects[ Random . Range (0 , destroyableObjects . Length) ] , tilemap . GetCellCenterWorld (spawnPosition) , Quaternion . identity);
        }
    }


    void SpawnStartAndEndPoints ( List<Rect> rooms )
    {
        // shuffle the rooms
        rooms = rooms . OrderBy (x => Random . value) . ToList ();

        // spawn start point at the center of the first room
        Rect firstRoom = rooms[0];
        Vector3Int startSpawnPosition = new Vector3Int(
            Mathf.FloorToInt(firstRoom.x + firstRoom.width / 2),
            Mathf.FloorToInt(firstRoom.y + firstRoom.height / 2),
            0
        );
        Instantiate (startPointPrefab , tilemap . GetCellCenterWorld (startSpawnPosition) , Quaternion . identity);

       
        // spawn end point at the center of the last room
        Rect lastRoom = rooms[rooms.Count - 1];
        Vector3Int endSpawnPosition = new Vector3Int(
            Mathf.FloorToInt(lastRoom.x + lastRoom.width / 2),
            Mathf.FloorToInt(lastRoom.y + lastRoom.height / 2),
            0
        );
        Instantiate (endPointPrefab , tilemap . GetCellCenterWorld (endSpawnPosition) , Quaternion . identity);


    }
}