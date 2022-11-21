
using Bluerain_Tech_Introduction;

var map = new Map(10, 10);

map.AddRoverToMap(startingX: 0, startingY: 0, destinationX: 2, destinationY: 7);
map.AddRoverToMap(startingX: 8, startingY: 8, destinationX: 7, destinationY: 2);
map.MoveRover(0, "NNNEESSEENNNNNN");
map.MoveRover(1, "SSWWWWWW");

map.RenderWorld();

Console.ReadLine();
