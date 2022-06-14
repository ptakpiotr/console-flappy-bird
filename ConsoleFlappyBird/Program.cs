using ConsoleFlappyBird;

GameAreaCreator gac = new();

Console.WriteLine("Hello and welcome to Console Flappy Bird by Piotr Ptak!\nPlease place your cursor at the middle of the screen.\nTo steer the bird-asterisk move your mouse up.");
Thread.Sleep(5000);
Console.Clear();

gac.GenerateArea();
