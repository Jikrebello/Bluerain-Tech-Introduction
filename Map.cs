using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bluerain_Tech_Introduction
{
    public class Map
    {
        public string[,] World = new string[5, 5];
        public List<Rover> Rovers = new();
        int roverNumber;
        string instructions;

        public Map(int mapSizeX, int mapSizeY)
        {
            GenerateWorld(mapSizeX, mapSizeY);
        }

        public void GenerateWorld(int mapSizeX, int mapSizeY)
        {
            World = new string[mapSizeX, mapSizeY];
            for (int row = 0; row < World.GetLength(0); row++)
            {
                for (int column = 0; column < World.GetLength(1); column++)
                {
                    World[row, column] = ".";
                }
            }
        }

        public void AddRoverToMap(int startingX, int startingY, int destinationX, int destinationY)
        {
            // Safety Guards for startingX coordinate.
            if (startingX < 0 || startingX > World.Length)
            {
                Console.WriteLine("Invalid starting X position");
                return;
            }

            // Safety Guards for startingY coordinate.
            if (startingY < 0 || startingY > World.Length)
            {
                Console.WriteLine("Invalid starting Y position");
                return;
            }

            // Safety Guards for destinationX coordinate.
            if (startingY < 0 || startingY > World.Length)
            {
                Console.WriteLine("Invalid starting Y position");
                return;
            }

            // Safety Guards for destinationY coordinate.
            if (startingY < 0 || startingY > World.Length)
            {
                Console.WriteLine("Invalid starting Y position");
                return;
            }

            int x = 0;
            int y = 0;

            for (int row = 0; row < World.GetLength(0); row++)
            {
                for (int column = 0; column < World.GetLength(1); column++)
                {
                    if (row == startingX && column == startingY)
                    {
                        World[row, column] = "S";
                        x = row;
                        y = column;
                    }
                }
            }

            World[destinationX, destinationY] = "D";

            Rovers.Add(new Rover
            {
                CurrentPosition = new Vector2(x, y)
            });

            Console.WriteLine("Rover " + Rovers.Count + ": starting position: (" + x.ToString() + ", " + y.ToString() + ")");
            Console.WriteLine();
        }

        public void MoveRover(int roverNumber, string instructions)
        {
            var selectedRover = Rovers[roverNumber];

            foreach (var coordinate in instructions)
            {
                bool openSpace = World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y] == "." ||
                                    World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y] == "@";

                if (coordinate == 'N')
                {
                    // Update the Map.
                    if (openSpace)
                    {
                        // Free open space.
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y] = "↑";
                    }

                    if (World[(int)selectedRover.CurrentPosition.X + 1, (int)selectedRover.CurrentPosition.Y] is "↑" or "↓" or ">" or "<")
                    {
                        // Paths intersect.
                        World[(int)selectedRover.CurrentPosition.X + 1, (int)selectedRover.CurrentPosition.Y] = "#";
                    }
                    else
                    {
                        World[(int)selectedRover.CurrentPosition.X + 1, (int)selectedRover.CurrentPosition.Y] = "@";
                    }

                    // Update the Rover.
                    var newPostion = new Vector2(selectedRover.CurrentPosition.X + 1, selectedRover.CurrentPosition.Y);
                    selectedRover.CurrentPosition = newPostion;
                    Rovers.Remove(selectedRover);
                    Rovers.Insert(roverNumber, selectedRover);
                }
                else if (coordinate == 'S')
                {
                    // Update the Map.
                    if (openSpace)
                    {
                        // Free open space.
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y] = "↓";
                    }

                    if (World[(int)selectedRover.CurrentPosition.X - 1, (int)selectedRover.CurrentPosition.Y] is "↑" or "↓" or ">" or "<")
                    {
                        // Paths intersect.
                        World[(int)selectedRover.CurrentPosition.X - 1, (int)selectedRover.CurrentPosition.Y] = "#";
                    }
                    else
                    {
                        World[(int)selectedRover.CurrentPosition.X - 1, (int)selectedRover.CurrentPosition.Y] = "@";
                    }


                    // Update the Rover.
                    var newPostion = new Vector2(selectedRover.CurrentPosition.X - 1, selectedRover.CurrentPosition.Y);
                    selectedRover.CurrentPosition = newPostion;
                    Rovers.Remove(selectedRover);
                    Rovers.Insert(roverNumber, selectedRover);
                }
                else if (coordinate == 'E')
                {
                    // Update the Map.
                    if (openSpace)
                    {
                        // Free open space.
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y] = ">";
                    }

                    if (World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y + 1] is "↑" or "↓" or ">" or "<")
                    {
                        // Paths intersect.
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y + 1] = "#";
                    }
                    else
                    {
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y + 1] = "@";
                    }

                    // Update the Rover.
                    var newPostion = new Vector2(selectedRover.CurrentPosition.X, selectedRover.CurrentPosition.Y + 1);
                    selectedRover.CurrentPosition = newPostion;
                    Rovers.Remove(selectedRover);
                    Rovers.Insert(roverNumber, selectedRover);
                }
                else if (coordinate == 'W')
                {
                    // Update the Map.
                    if (openSpace)
                    {
                        // Free open space.
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y] = "<";
                    }

                    if (World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y - 1] is "↑" or "↓" or ">" or "<")
                    {
                        // Paths intersect.
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y - 1] = "#";
                    }
                    else
                    {
                        World[(int)selectedRover.CurrentPosition.X, (int)selectedRover.CurrentPosition.Y - 1] = "@";
                    }

                    // Update the Rover.
                    var newPostion = new Vector2(selectedRover.CurrentPosition.X, selectedRover.CurrentPosition.Y - 1);
                    selectedRover.CurrentPosition = newPostion;
                    Rovers.Remove(selectedRover);
                    Rovers.Insert(roverNumber, selectedRover);
                }
            }
        }

        void ChangeMap(List<Rover> rovers, string instructions)
        {
            foreach (var rover in rovers)
            {
                bool openSpace = World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] == "." ||
                    World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] == "@";

                foreach (var coordinate in instructions)
                {
                    if (coordinate == 'N')
                    {
                        // Update the Map.
                        if (openSpace)
                        {
                            // Free open space.
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = "↑";
                        }

                        if (World[(int)rover.CurrentPosition.X + 1, (int)rover.CurrentPosition.Y] is "↑" or "↓" or ">" or "<")
                        {
                            // Paths intersect.
                            World[(int)rover.CurrentPosition.X + 1, (int)rover.CurrentPosition.Y] = "#";
                        }
                        else
                        {
                            World[(int)rover.CurrentPosition.X + 1, (int)rover.CurrentPosition.Y] = "@";
                        }
                    }
                    else if (coordinate == 'S')
                    {
                        // Update the Map.
                        if (openSpace)
                        {
                            // Free open space.
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = "↓";
                        }

                        if (World[(int)rover.CurrentPosition.X - 1, (int)rover.CurrentPosition.Y] is "↑" or "↓" or ">" or "<")
                        {
                            // Paths intersect.
                            World[(int)rover.CurrentPosition.X - 1, (int)rover.CurrentPosition.Y] = "#";
                        }
                        else
                        {
                            World[(int)rover.CurrentPosition.X - 1, (int)rover.CurrentPosition.Y] = "@";
                        }
                    }
                    else if (coordinate == 'E')
                    {
                        // Update the Map.
                        if (openSpace)
                        {
                            // Free open space.
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = ">";
                        }

                        if (World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y + 1] is "↑" or "↓" or ">" or "<")
                        {
                            // Paths intersect.
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y + 1] = "#";
                        }
                        else
                        {
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y + 1] = "@";
                        }
                    }
                    else if (coordinate == 'W')
                    {
                        // Update the Map.
                        if (openSpace)
                        {
                            // Free open space.
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = "<";
                        }

                        if (World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y - 1] is "↑" or "↓" or ">" or "<")
                        {
                            // Paths intersect.
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y - 1] = "#";
                        }
                        else
                        {
                            World[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y - 1] = "@";
                        }
                    }
                }
            }
        }

        public void RenderWorld()
        {
            for (int row = World.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < World.GetLength(1); column++)
                {
                    Console.Write(World[row, column]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Legend:");
            Console.WriteLine(". = Empty");
            Console.WriteLine("S = Starting Position");
            Console.WriteLine("D = Destination Position");
            Console.WriteLine("@ = Rover/s");
            Console.WriteLine("↑ = Northwards Movement");
            Console.WriteLine("↓ = Southwards Movement");
            Console.WriteLine("> = Eastwards Movement");
            Console.WriteLine("< = Westwards Movement");
            Console.WriteLine("# = Rover path intersection");
        }

    }
}
