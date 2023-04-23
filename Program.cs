using System;

namespace cubetime 
{

    class cubeRunner
    {
        static void Main(string[] args)
        {
            rubiksCube cube = new rubiksCube();
            cube.displayCube();

            

            string userInput = " ";

            while (userInput.ToUpper() != "STOP")
            {
                
                //Displays entire cube initally
                

                Console.WriteLine("Would you like to view the cube or change it? (Enter change or view)");
                userInput = Console.ReadLine().ToUpper();



                if (userInput == "VIEW")
                {
                Console.WriteLine("Enter a side to view: from front, top, bottom, left, right or back");
                userInput = Console.ReadLine();
                cube.displaySide(userInput);
                }
                else if (userInput == "CHANGE")
                {
                    Console.WriteLine("Enter Direction (horizontal or vertical)");
                    string? direction = Console.ReadLine();

                    if (direction.ToUpper() == "VERTICAL")
                    {
                        Console.WriteLine("Enter row that you want to rotate. (RIGHT, MIDDLE OR LEFT)");
                    }
                    else 
                    {
                        Console.WriteLine("Enter row that you want to rotate. (TOP, MIDDLE or BOTTOM)");
                    }
                    
                    
                    string? row = Console.ReadLine();

                    cube.changeCube2(direction, row);
                }
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
    public string[,,] sides = new string[6, 3, 3];
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
        string xStr = "";
        string yStr = "";

        for (int sides = 0; sides < 6; sides++)
        {
            Console.WriteLine("Side " + sides);

            for (int x = 0; x < 3; x++)
            {
                Console.Write("\n");
                for (int y = 0; y < 3; y++)
                {
                    xStr = Convert.ToString(x);
                    yStr = Convert.ToString(y);

                    Console.Write(x + "," + y + "," + sides);
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
        
        int  faceIndex = -1;

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

        if (faceIndex != -1)
        {
            for (int x = 0; x < 3; x++)
            {
                Console.WriteLine("");
                for (int y = 0; y < 3; y++)
                {
                    consoleColor(this.sides[faceIndex, x, y]);
                    Console.Write("■");
                }
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
                    
                    

                    while (finalDirectionValid)
                    {
                        Console.WriteLine("Would you like to rotate this section UP or DOWN (Enter up or down)");
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

                makeChange(rowIndex, rotationalDirection, direction.ToUpper());
            
        }

    }

    
   static string getDirection(string direction)
   {
    bool directionValid = false;
    string? actualDirection = "";
    string option1 = "";
    string option2 = "";

    if (direction.ToUpper() == "VERTICAL")
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
            if (actualDirection.ToUpper() == option1 || actualDirection.ToUpper() == option2)
            {
                return actualDirection.ToUpper();
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

    //When the user flips the cube vertically
   private void makeChange(int row, string rotationDirection, string flipDirection)
   {
        string[] curTemp = {"", "", ""};
        string[] nextTemp = {"", "", ""};
        int sideLoopCount = 0;
        int[] faceIndicies = {0, 0, 0, 0};

        if (rotationDirection == "LEFT")
        {
            int[] referenceArray = {0, 1, 2, 3};
            referenceArray.CopyTo(faceIndicies, 0);
        }
        else if (rotationDirection == "RIGHT")
        {
            int[] referenceArray = {0, 3, 2, 1};
            referenceArray.CopyTo(faceIndicies, 0);
        }
        else if (rotationDirection == "UP")
        {
            int[] referenceArray = {0, 4, 2, 5};
            referenceArray.CopyTo(faceIndicies, 0);
        }
        else if (rotationDirection == "DOWN")
        {
            int[] referenceArray = {0, 5, 2, 4};
            referenceArray.CopyTo(faceIndicies, 0);
        }

        //Stores each of the sides sqaures of the front face (face 0) in a temp string before they get moved
        curTemp[0] = this.sides[0, row, 0];
        curTemp[1] = this.sides[0, row, 1];
        curTemp[2] = this.sides[0, row, 2];

        Console.WriteLine("temp 0 " + curTemp[0] + " temp 1 " + curTemp[1] + " temp 2 " + curTemp[2]);

        if (flipDirection == "HORIZONTAL")
        {
             curTemp[0] = this.sides[0, row, 0];
             curTemp[1] = this.sides[0, row, 1];
             curTemp[2] = this.sides[0, row, 2];

            foreach (int side in faceIndicies)
            {
                //Console.WriteLine("Side " + side + "has colours " + this.sides[side + 2, row, 0] + ", " + this.sides[side + 2, row, 1] + ", " + this.sides[side + 2, row, 2]);
                 for (int rowNo = 0; rowNo < 3; rowNo++)
                 {
                    if (sideLoopCount < 3)
                    {
                        nextTemp[rowNo] = sides[side + 1, row, rowNo];
                        Console.WriteLine("Replacing " + sides[side + 1, row, rowNo] + " with " + curTemp[rowNo]);
                        sides[side + 1, row, rowNo] = curTemp[rowNo];
                        curTemp[rowNo] = nextTemp[rowNo];
                    }
                    else 
                    {
                         this.sides[0, row, rowNo] = curTemp[rowNo]; 
                         Console.WriteLine("Hi");
                        
                    }
                 }
                 //Console.WriteLine("Side " + side + "has colours " + this.sides[side + 2, row, 0] + ", " + this.sides[side + 2, row, 1] + ", " + this.sides[side + 2, row, 2]);
                sideLoopCount++;
            }
        }
        else
        {
             curTemp[0] = this.sides[0, 0, row];
             curTemp[1] = this.sides[0, 1, row];
             curTemp[2] = this.sides[0, 2, row];
             
          foreach (int side in faceIndicies)
            {
                //Console.WriteLine("Side " + side + "has colours " + this.sides[side + 2, row, 0] + ", " + this.sides[side + 2, row, 1] + ", " + this.sides[side + 2, row, 2]);
                 for (int rowNo = 0; rowNo < 3; rowNo++)
                 {
                    if (sideLoopCount < 3)
                    {
                        nextTemp[rowNo] = sides[side + 1, rowNo, row];
                        Console.WriteLine("Replacing " + sides[side + 1, row, rowNo] + " with " + curTemp[rowNo]);
                        sides[side + 1, rowNo, row] = curTemp[rowNo];
                        curTemp[rowNo] = nextTemp[rowNo];
                    }
                    else 
                    {
                         this.sides[0, rowNo, row] = curTemp[rowNo]; 
                         Console.WriteLine("Hi");
                        
                    }
                 }
                 //Console.WriteLine("Side " + side + "has colours " + this.sides[side + 2, row, 0] + ", " + this.sides[side + 2, row, 1] + ", " + this.sides[side + 2, row, 2]);
                sideLoopCount++;
            }  
        }
   }

    private int findRowIndex(string direction, string row)
    {
        direction = direction.ToUpper();
        row = row.ToUpper();

        
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


    private void consoleColor(string colourString)
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