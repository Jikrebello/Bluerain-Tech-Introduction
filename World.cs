using System.Numerics;

namespace Bluerain_Tech_Introduction
{
    public class World
    {
        string[,] Map = new string[5, 5];
        readonly List<Rover> Rovers = new();

        public World(int mapSizeX, int mapSizeY)
        {
            GenerateWorld(mapSizeX: mapSizeX, mapSizeY: mapSizeY);
        }

        void GenerateWorld(int mapSizeX, int mapSizeY)
        {
            Map = new string[mapSizeX, mapSizeY];
            for (int row = 0; row < Map.GetLength(dimension: 0); row++)
            {
                for (int column = 0; column < Map.GetLength(dimension: 1); column++)
                {
                    Map[row, column] = ".";
                }
            }
        }

        public void AddRoverToMap(int startingX, int startingY, int destinationX, int destinationY)
        {
            // Safety Guards for startingX coordinate.
            if (startingX < 0 || startingX > Map.Length)
            {
                Console.WriteLine("Invalid starting X position");
                return;
            }

            // Safety Guards for startingY coordinate.
            if (startingY < 0 || startingY > Map.Length)
            {
                Console.WriteLine("Invalid starting Y position");
                return;
            }

            // Safety Guards for destinationX coordinate.
            if (startingY < 0 || startingY > Map.Length)
            {
                Console.WriteLine("Invalid starting Y position");
                return;
            }

            // Safety Guards for destinationY coordinate.
            if (startingY < 0 || startingY > Map.Length)
            {
                Console.WriteLine("Invalid starting Y position");
                return;
            }

            int x = 0;
            int y = 0;

            for (int row = 0; row < Map.GetLength(dimension: 0); row++)
            {
                for (int column = 0; column < Map.GetLength(dimension: 1); column++)
                {
                    if (row == startingX && column == startingY)
                    {
                        Map[row, column] = "S";
                        x = row;
                        y = column;
                    }
                }
            }

            Map[destinationX, destinationY] = "D";

            Rovers.Add(new Rover { CurrentPosition = new Vector2(x: x, y: y) });

            Console.WriteLine(
                "Rover "
                    + Rovers.Count
                    + ": starting position: ("
                    + x.ToString()
                    + ", "
                    + y.ToString()
                    + ")"
            );
            Console.WriteLine();
        }

        public void MoveRover(int roverNumber, string instructions)
        {
            var selectedRover = Rovers[roverNumber];
            Vector2 newPosition;

            foreach (var coordinate in instructions)
            {
                switch (coordinate)
                {
                    case 'N':
                    {
                        UpdateMap(rover: selectedRover, coordinate: coordinate);

                        // Update the Rover.
                        newPosition = new Vector2(
                            selectedRover.CurrentPosition.X + 1,
                            selectedRover.CurrentPosition.Y
                        );
                        selectedRover.CurrentPosition = newPosition;
                        Rovers.Remove(selectedRover);
                        Rovers.Insert(roverNumber, selectedRover);
                        break;
                    }

                    case 'S':
                    {
                        UpdateMap(rover: selectedRover, coordinate: coordinate);

                        // Update the Rover.
                        newPosition = new Vector2(
                            selectedRover.CurrentPosition.X - 1,
                            selectedRover.CurrentPosition.Y
                        );
                        selectedRover.CurrentPosition = newPosition;
                        Rovers.Remove(selectedRover);
                        Rovers.Insert(roverNumber, selectedRover);
                        break;
                    }

                    case 'E':
                    {
                        UpdateMap(rover: selectedRover, coordinate: coordinate);

                        // Update the Rover.
                        newPosition = new Vector2(
                            selectedRover.CurrentPosition.X,
                            selectedRover.CurrentPosition.Y + 1
                        );
                        selectedRover.CurrentPosition = newPosition;
                        Rovers.Remove(selectedRover);
                        Rovers.Insert(roverNumber, selectedRover);
                        break;
                    }

                    case 'W':
                    {
                        UpdateMap(rover: selectedRover, coordinate: coordinate);

                        // Update the Rover.
                        newPosition = new Vector2(
                            selectedRover.CurrentPosition.X,
                            selectedRover.CurrentPosition.Y - 1
                        );
                        selectedRover.CurrentPosition = newPosition;
                        Rovers.Remove(selectedRover);
                        Rovers.Insert(roverNumber, selectedRover);
                        break;
                    }
                }
            }
        }

        void UpdateMap(Rover rover, char coordinate)
        {
            bool openSpace =
                Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] == "."
                || Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] == "@";

            switch (coordinate)
            {
                case 'N':
                    if (openSpace)
                    {
                        // Add Direction.
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = "↑";
                    }

                    if (
                        Map[(int)rover.CurrentPosition.X + 1, (int)rover.CurrentPosition.Y]
                        is "↑"
                            or "↓"
                            or ">"
                            or "<"
                    )
                    {
                        // Paths intersect.
                        Map[(int)rover.CurrentPosition.X + 1, (int)rover.CurrentPosition.Y] = "#";
                    }
                    else
                    {
                        // Free open space.
                        Map[(int)rover.CurrentPosition.X + 1, (int)rover.CurrentPosition.Y] = "@";
                    }
                    break;
                case 'S':
                    if (openSpace)
                    {
                        // Free open space.
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = "↓";
                    }

                    if (
                        Map[(int)rover.CurrentPosition.X - 1, (int)rover.CurrentPosition.Y]
                        is "↑"
                            or "↓"
                            or ">"
                            or "<"
                    )
                    {
                        // Paths intersect.
                        Map[(int)rover.CurrentPosition.X - 1, (int)rover.CurrentPosition.Y] = "#";
                    }
                    else
                    {
                        Map[(int)rover.CurrentPosition.X - 1, (int)rover.CurrentPosition.Y] = "@";
                    }
                    break;
                case 'E':
                    if (openSpace)
                    {
                        // Free open space.
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = ">";
                    }

                    if (
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y + 1]
                        is "↑"
                            or "↓"
                            or ">"
                            or "<"
                    )
                    {
                        // Paths intersect.
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y + 1] = "#";
                    }
                    else
                    {
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y + 1] = "@";
                    }
                    break;
                case 'W':
                    if (openSpace)
                    {
                        // Free open space.
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y] = "<";
                    }

                    if (
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y - 1]
                        is "↑"
                            or "↓"
                            or ">"
                            or "<"
                    )
                    {
                        // Paths intersect.
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y - 1] = "#";
                    }
                    else
                    {
                        Map[(int)rover.CurrentPosition.X, (int)rover.CurrentPosition.Y - 1] = "@";
                    }
                    break;
            }
        }

        public void RenderWorld()
        {
            for (int row = Map.GetLength(dimension: 0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < Map.GetLength(dimension: 1); column++)
                {
                    Console.Write(Map[row, column]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            for (int row = Map.GetLength(dimension: 0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < Map.GetLength(dimension: 1); column++)
                {
                    if (Map[row, column] == "#")
                    {
                        Console.WriteLine(
                            "Rover Intersection point at: (" + column + "," + row + ")"
                        );
                    }
                }
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
