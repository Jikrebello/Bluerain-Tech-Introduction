using Bluerain_Tech_Introduction;

var world = new World(10, 10);

world.AddRoverToMap(startingX: 0, startingY: 0, destinationX: 2, destinationY: 7);
world.AddRoverToMap(startingX: 8, startingY: 8, destinationX: 7, destinationY: 2);
world.MoveRover(0, "NNNEESSEENNNNNN");
world.MoveRover(1, "SSWWWWWW");

world.RenderWorld();

Console.ReadLine();
