using System;
using System.IO;

namespace RPG_Project
{

    enum MapTiles
    {
        Empty = 0,
        Forest = 70,
        GrassLand = 71,
        Mountian = 77,
        Road =82,
        Water = 87,
        Shop = 83,
        Dungeon = 68,
        Player = 100
    }
    class Map
    {
        const int MAP_HEIGHT = 63;
        const int MAP_WIDTH = 108;

        int height;
        int width;
        int characterNmb;

        int yPosition = 0;
        int xPosition = 0;

        char character;

        MapTile[,] mapData;

        MapTile playerTile;
        MapTile defaultTile;


        StreamReader reader;

        public Map()
        {
           
        }

        /*Getters and Setters*/

        public int Height{
            get { return height; }
            private set
            {
                height = value;
            }
        }

        public int Width
        {
            get { return width; }
            private set
            {
                width = value;
            }
        }

        public void GenerateMap()
        {

            //Generate special tiles
            playerTile = new MapTile(0, 0, MapTiles.Player);
            defaultTile = new MapTile(0, 0, MapTiles.Mountian);
            //TODO Read document and generate the map
            reader = File.OpenText("D:/0_School Work/GitHub/Intro_To_Html5/New folder/map/test.txt");
            //Finds maximum height and width of map
            Console.WriteLine("Generating map...");
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (line.Length > width)
                {
                    width = line.Length;
                }
                height++;
            }
            reader.Close();
            reader = File.OpenText("D:/0_School Work/GitHub/Intro_To_Html5/New folder/map/test.txt");
            Console.WriteLine("Width: {0} Height {1}",Width,Height);
            mapData = new MapTile[height,width+2];
            Console.WriteLine("Propgating data");
            //propgate array with data
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width+2; x++)
                {
                    character = (char)reader.Read();
                    
                    try
                    {
                        //characterNmb = (int)Char.GetNumericValue(character);
                        characterNmb = Convert.ToInt32(Char.ToUpper(character));
                        //characterNmb = Convert.ToInt32(Char.GetNumericValue(char.ToLower(character)));
                        //characterNmb = Convert.ToInt32(new string (string.ToUpper(character)));
                    }
                    catch
                    {
                        Console.WriteLine("Error Converting map at {0}, {1}", y, x);
                    }
                    switch (characterNmb)
                    {
                        //Dungeon
                        case 68:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Dungeon);
                            break;
                        //Forest
                        case 70:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Forest);
                            break;
                        //GrassLand
                        case 71:
                            mapData[y, x] = new MapTile(y, x, MapTiles.GrassLand);
                            break;
                        //Mountain
                        case 77:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Mountian);
                            break;
                        //Road
                        case 82:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Road);
                            break;
                        //Shop
                        case 83:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Shop);
                            break;
                        //Water
                        case 87:
                            mapData[y, x] = new MapTile(y, x, MapTiles.Water);
                            break;
                            //Catch for no char default to mountain
                        //case 65535:
                        //    mapData[x, y] = new MapTile(y, x, MapTiles.Mountian);
                        //    break;
                    }
                    //GenerateVisualMap();
                }
            }

            //reader = new StreamReader("~/map/test.txt");

            //do
            //{
            //    string character = (string)reader.ReadLine();
            //    Console.Write(character);

            //    

            //} while (!reader.EndOfStream);


            //FInd maximum width

            //Read character
            //Create object after that character
            //Set object to map position
            GenerateVisualMap();
            //UpdatePlayerMap();
        }

        void GenerateVisualMap()
        {
            //Go through map, printing each item.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width-2 + 2; x++)
                {
                    mapData[y, x].PrintSelf();
                }
                Console.Write("\n");
            }
        }

        void UpdatePlayerMap()
        {
            Console.Clear();

            for (int y = 0; y < MAP_HEIGHT; y++)
            {
                if (y + yPosition >= mapData.GetLength(0) || y + yPosition < 0)
                {
                   // MapTile tile = new MapTile(0, 0, MapTiles.Mountian);
                    //tile.PrintSelf();
                    for(int x = 0; x < MAP_WIDTH; x++)
                    {
                        defaultTile.PrintSelf();
                    }
                }
                else
                {
                    for (int x = 0; x < MAP_WIDTH; x++)
                    {
                        if (x + xPosition >= width || x + xPosition < 0)
                        {
                            //MapTile tile = new MapTile(0, 0, MapTiles.Mountian);
                            defaultTile.PrintSelf();
                        }
                        else
                        {
                            //Find Center of Map
                            if(y == MAP_HEIGHT/2 && x == MAP_WIDTH / 2)
                            {
                                //MapTile player = new MapTile(0, 0, MapTiles.Player);
                                playerTile.YCord = y + yPosition;
                                playerTile.XCord = x + xPosition;
                                playerTile.PrintSelf();
                            }
                            else
                            {
                                mapData[y + yPosition, x + xPosition].PrintSelf();
                            }
                        }

                    }
                }
                Console.Write("\n");
            }
            MovePlayer();
        }

        void MovePlayer()
        {

            //TODO REPROGRAM ALL OF THIS. THIS IS TEMP MOVEMENMT CODE FOR TESTING ONLY
            int moveDirection;
            while (true)
            {
                try
                {
                    moveDirection = int.Parse(Console.ReadLine());
                }
                catch
                {
                    moveDirection = 0;
                }

                CheckSurroundings(moveDirection);
                if (moveDirection == 3)
                {
                    xPosition++;
                    UpdatePlayerMap();
                    return;
                }
                if (moveDirection == 4)
                {
                    xPosition--;
                    UpdatePlayerMap();
                    return;
                }
            }
        }

        void CheckSurroundings(int direction)
        {
            switch (direction)
            {
                case 1:
                    //While moving up
                    if (playerTile.YCord != 0)
                    {
                        if (mapData[playerTile.YCord -1, playerTile.XCord].mapType == MapTiles.Mountian)
                        {
                            UpdatePlayerMap();
                            break;
                        }
                        yPosition--;
                        UpdatePlayerMap();
                    }
                    break;

                case 2:
                    //While moving down
                    if (playerTile.YCord != mapData.GetLength(0))
                    {
                        if (mapData[playerTile.YCord + 1, playerTile.XCord].mapType == MapTiles.Mountian)
                        {
                            UpdatePlayerMap();
                            break;
                        }
                        yPosition++;
                        UpdatePlayerMap();
                    }
                    break;

                case 3:
                    //While moving left
                    if (playerTile.XCord != 0)
                    {
                        if (mapData[playerTile.YCord, playerTile.XCord - 1].mapType == MapTiles.Mountian)
                        {
                            UpdatePlayerMap();
                            break;
                        }
                        xPosition--;
                        UpdatePlayerMap();
                    }
                    break;
                case 4:
                    //While moving right
                    if (playerTile.XCord != mapData.GetLength(1))
                    {
                        if (mapData[playerTile.YCord, playerTile.XCord + 1].mapType == MapTiles.Mountian)
                        {
                            UpdatePlayerMap();
                            break;
                        }
                        xPosition++;
                        UpdatePlayerMap();
                    }
                    break;
            }
        }
    }
}
