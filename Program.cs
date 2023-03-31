using System;

namespace cubetime 
{

    class cubeRunner
    {
        static void Main(string[] args)
        {
            rubiksCube cube = new rubiksCube();

            string userInput = " ";

            while (userInput.ToUpper() != "STOP")
            {
                Console.WriteLine("Enter a side to view: from front, top, bottom, left, right or back");
                userInput = Console.ReadLine();
                cube.displaySide(userInput);
            }
        }
    }


    public struct colours
    {
        public string colour;
        public int used;
    }

    
public class rubiksCube
{
    protected string[,,] sides = new string[6, 3, 3];
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

        for (int sides = 0; sides < 6; sides++)
        {
            Console.WriteLine("Side " + sides);

            for (int x = 0; x < 3; x++)
            {
                Console.Write("\n");
                for (int y = 0; y < 3; y++)
                {
                    consoleColor(this.sides[sides, x, y]);
                    Console.Write("■");
                }
            }
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }


    public void displaySide(string face)
    {
        
        int  faceIndex = 0;

        if (face.ToUpper() == "FRONT")
        {
            faceIndex = 0;
        }
        else if (face.ToUpper() == "LEFT")
        {
            faceIndex = 1;
        }
        else if (face.ToUpper() == "BACK")
        {
            faceIndex = 2;
        }
        else if (face.ToUpper() == "RIGHT")
        {
            faceIndex = 3;
        }
        else if (face.ToUpper() == "BOTTOM")
        {
            faceIndex = 4;
        }
        else if (face.ToUpper() == "TOP")
        {
            faceIndex = 5;
        }

        for (int x = 0; x < 3; x++)
        {
            Console.WriteLine("");
            for (int y = 0; y < 3; y++)
            {
                consoleColor(this.sides[faceIndex, x, y]);
                Console.Write("■");
            }
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(" ");

    }

    public void changeCube(string direction)
    {
        string row = "";
        bool rowValid = false;
        string finalDirection = "";
        bool finalDirectionValid = false;
        int rowIndex = 0;

        Console.WriteLine("");

        if (direction.ToUpper() == "HORIZONTAL")
        {
            while (row.ToUpper() != "TOP" || row.ToUpper() != "MIDDLE" || row.ToUpper() == "BOTTOM")
            {
                Console.WriteLine("Would you like to move the top, middle or bottom row?");
                row = Console.ReadLine();

                if (row.ToUpper() != "TOP" || row.ToUpper() != "MIDDLE" || row.ToUpper() == "BOTTOM")
                {
                    rowValid = true;
                }
            }
        }
        else if (direction.ToUpper() == "VERTICAL") 
        {
            while (rowValid == false)
            {
                Console.WriteLine("Would you like to move the right, middle or left row?");
                row = Console.ReadLine();

                if (row.ToUpper() == "RIGHT" || row.ToUpper() == "MIDDLE" || row.ToUpper() == "LEFT")
                {
                    rowValid = true;
                    rowIndex = findRowIndex(row, direction);
                    
                    

                    while (finalDirection != "UP" || finalDirection != "DOWN")
                    {
                        Console.WriteLine("Would you like to rotate this section up or down (Enter up or down)");
                        finalDirection = Console.ReadLine();

                        if (finalDirection.ToUpper() == "UP")
                        {
                            finalDirectionValid = true;
                        }
                        else if (finalDirection.ToUpper() == "DOWN")
                        {
                            finalDirectionValid = true;

                        }
                    }
                }
            }
        }

    }


    public void changeCube2(string direction, string row)
    {
        direction = direction.ToUpper();
        
        int rowIndex = findRowIndex(direction, row);

        if (rowIndex != -1)
        {
            string rotationalDirection = getDirection(direction);

            if (direction == "VERTICAL")
            {
              verticalChange(rowIndex, rotationalDirection);
            }
            else
            {
                horizontalChange(rowIndex, rotationalDirection);
            }
        }

    }

    
   static string getDirection(string direction)
   {
    bool directionValid = false;
    string actualDirection = "";
    string option1 = "";
    string option2 = "";

    if (direction == "VERTICAL")
    {
        option1 = "UP";
        option2 = "DOWN";
    }
    else
    {
        option1 = "LEFT";
        option2 = "RIGHT";
    }


        while (!directionValid)
        {
            Console.WriteLine("Would you like to roate this section " + option1 +  " or " + option2);
            actualDirection = Console.ReadLine();
            if (direction == option1 || direction == option2)
            {
                return actualDirection;
            }
        }
    
    
    return "";
   }

   static void verticalChange(int row, string rotationDirection)
   {
    if (rotationDirection == "UP")
    {
        
    }
    else
    {

    }
   }

   static void horizontalChange(int row, string rotationDirection)
   {

   }

    static int findRowIndex(string direction, string row)
    {
        direction = direction.ToUpper();

        
        if (direction == "STOP")
        {
            return -1;
        }
        else if (direction == "VERTICAL")
        {
            if (row == "RIGHT")
            {
                return 0;
            }
            else if (row == "MIDDLE")
            {
                return 1;
            }
            else if (row == "LEFT")
            {
                return 3;
            }
        }
        else if (direction == "HORIZONTAL")
        {
            if (row == "TOP")
            {
                return 0;
            }
            else if (row == "MIDDLE")
            {
                return 1;
            }
            else if (row == "BOTTOM")
            {
                return 3;
            }
        }
        
        return -1;
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
            Console.ForegroundColor = ConsoleColor.Yellow;
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


}