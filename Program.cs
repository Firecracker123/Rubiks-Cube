﻿using System;
using Microsoft.VisualBasic;

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
        var console_colours = new Dictionary<string, System.ConsoleColor>
        {
            {"white", ConsoleColor.White},
            {"blue", ConsoleColor.Blue},
            {"red", ConsoleColor.Red},
            {"yellow", ConsoleColor.Yellow},
            {"magenta", ConsoleColor.Magenta},
            {"green", ConsoleColor.Green},
            
        };
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

                    Console.ForegroundColor = console_colours[this.sides[sides, x, y]];
                    Console.Write("■");
                }
            }
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }


    public void displaySide(string face)
    {
        var console_colours = new Dictionary<string, System.ConsoleColor>
        {
            {"white", ConsoleColor.White},
            {"blue", ConsoleColor.Blue},
            {"red", ConsoleColor.Red},
            {"yellow", ConsoleColor.Yellow},
            {"magenta", ConsoleColor.Magenta},
            {"green", ConsoleColor.Green},
            
        };

        var face_index_values = new Dictionary<string, int>
        {
            {"FRONT", 0},
            {"LEFT", 1},
            {"BACK", 2},
            {"RIGHT", 3},
            {"BOTTOM", 4},
            {"TOP", 5}
        };
        
        
        int  faceIndex = -1;
        faceIndex = face_index_values[face.ToUpper()];
        

        if (faceIndex != -1)
        {
            for (int x = 0; x < 3; x++)
            {
                Console.WriteLine("");
                for (int y = 0; y < 3; y++)
                {
                    Console.ForegroundColor = console_colours[this.sides[faceIndex, x, y]];
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
            Console.WriteLine($"Would you like to roate this section {option1} or {option2}");
            actualDirection = Console.ReadLine();
            if (actualDirection.ToUpper() == option1 || actualDirection.ToUpper() == option2)
            {
                return actualDirection.ToUpper();
            }
        }
    
    
    return "";
   }


   private void makeChange(int row, string rotationDirection, string flipDirection)
   {
        string[] curTemp = {"", "", ""};
        string[] nextTemp = {"", "", ""};
        int sideLoopCount = 0;
        int[] faceIndicies = {0, 0, 0, 0};

               


        //Stores each of the sides sqaures of the front face (face 0) in a temp string before they get moved
        curTemp[0] = this.sides[0, row, 0];
        curTemp[1] = this.sides[0, row, 1];
        curTemp[2] = this.sides[0, row, 2];

        Console.WriteLine("temp 0 " + curTemp[0] + " temp 1 " + curTemp[1] + " temp 2 " + curTemp[2]);

        var swap_sequence = new Dictionary<string, int[]>{
            {"LEFT", new int[] {0, 1, 2, 3}},
            {"RIGHT", new int[] {0, 3, 2, 1}},
            {"UP", new int[] {0, 4, 2, 5}},
            {"DOWN", new int[] {0, 5, 2, 4}},
        }; 

        if (flipDirection == "HORIZONTAL")
        {
             curTemp[0] = this.sides[0, row, 0];
             curTemp[1] = this.sides[0, row, 1];
             curTemp[2] = this.sides[0, row, 2];


            //I can't remember how this works - several months later still don't know
            foreach (int side in swap_sequence[rotationDirection])
            {
                 for (int rowNo = 0; rowNo < 3; rowNo++)
                 {
                    if (sideLoopCount < 3)
                    {
                        nextTemp[rowNo] = sides[side + 1, row, rowNo];
                        sides[side + 1, row, rowNo] = curTemp[rowNo];
                        curTemp[rowNo] = nextTemp[rowNo];
                    }
                    else 
                    {
                         this.sides[0, row, rowNo] = curTemp[rowNo]; 
                        
                    }
                 }
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
                 for (int rowNo = 0; rowNo < 3; rowNo++)
                 {
                    if (sideLoopCount < 3)
                    {
                        nextTemp[rowNo] = sides[side + 1, rowNo, row];
                        sides[side + 1, rowNo, row] = curTemp[rowNo];
                        curTemp[rowNo] = nextTemp[rowNo];
                    }
                    else 
                    {
                         this.sides[0, rowNo, row] = curTemp[rowNo];                         
                    }
                 }
                sideLoopCount++;
            }  
        }
   }
    
    //Finds the index of the face that the user wants to find
    private int findRowIndex(string direction, string row)
    {
        //Upper case means that it doesn't matter where the user puts their uppercase characters
        direction = direction.ToUpper();
        row = row.ToUpper();

       

        if (direction == "STOP") return -1;

         var direction_dictionary = new Dictionary<string, Dictionary<string, int>>
        {
          {"VERTICAL", new Dictionary<string, int> {{"RIGHT", 0}, {"MIDDLE", 1}, {"LEFT", 2}}},
          {"HORIZONTAL", new Dictionary<string, int> {{"TOP", 0}, {"MIDDLE", 1}, {"BOTTOM", 2}}}
        };

        return direction_dictionary[direction][row];
    }

  
}


}