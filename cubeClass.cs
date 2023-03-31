using System;
    



public class rubiksCube
{
    protected string[,,] sides = new string[6, 3, 3];

    public record struct colours
    {
        public string colour;
        public int used;
    }

    colours[] sideColours = new colours[6];

    public rubiksCube()
    {

        Random rnd = new Random();
        int randomNumber = 0;
        bool valid = false;

        
        //Initalising sideColour structure
        this.sideColours[0].colour = "yellow";
        this.sideColours[1].colour = "white";
        this.sideColours[2].colour = "blue";
        this.sideColours[3].colour = "green";
        this.sideColours[4].colour = "red";
        this.sideColours[5].colour = "magenta";
        
        
        for (int i = 0; i < 5; i++)
        {
            this.sideColours[i].used = 0;
        }

        //Populating cube sides with the correct colours
        for (int sides = 0; sides < 6; sides++)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    valid = false;
                    
                    while (valid == false)
                    {
                    randomNumber = rnd.Next(0, 6);  
                      if (this.sideColours[randomNumber].used < 9)
                      {
                        this.sides[sides, x, y] = this.sideColours[randomNumber].colour;
                        this.sideColours[randomNumber].used++;
                        valid = true;
                      }
                    }
                }
            }
        }

    }



    public void displayCube()
    {

      string rowString = "";

        for (int sides = 0; sides < 6; sides++)
        {
            Console.WriteLine("Side " + sides);
            rowString = "";

            for (int x = 0; x < 3; x++)
            {
                Console.WriteLine("\n");
                for (int y = 0; y < 3; y++)
                {
                    consoleColor(this.sides[sides, x, y]);
                    Console.WriteLine(" ");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

    static void consoleColor(string colourString)
    {
        if (colourString == "white")
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (colourString == "blue")
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        else if (colourString == "red")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (colourString == "yellow")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (colourString == "magenta")
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        else if (colourString == "green")
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

    }
}
