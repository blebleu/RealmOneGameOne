﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeroPathing : MonoBehaviour
{
    /*
    * Author: Brent
    * HeroPathing handles the path that heroes will us to move along the grid. 
    *    
    */
    
        
    /* TODO:
     * 1. Convert map string into our tilemap or our tilemap into a string
     * 2. Convert vector 2 x,y to int x and int y
     * 3. Have method return a list of vector 2s
     * 4. Pass the list to main gameobject
     * 5. Have enemy move along the given path
     */



    //test map console 
    string[] map = new string[]
    {
        "+------+",
        "|      |",
        "|A X   |",
        "|XXX   |",
        "|  XX X|",
        "| B    |",
        "|      |",
        "+------+",
    };


    Location current;
    Location start = new Location { X = 0, Y = 0 };
    Location target = new Location { X = 10, Y = 10 };
    List<Location> openList = new List<Location>();
    List<Location> closedList = new List<Location>();
    int g = 0;

    class Location
    {
        public int X;
        public int Y;
        //F is G+H
        public int F;
        //G is the distance from the start
        public int G;
        //H is the estimated distance from the target
        public int H;
        public Location Parent;
    }


    void Start()
    {
        openList.Add(start);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<Vector2> GetPath()
    {
        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(loc => loc.F);
            current = openList.First(loc => loc.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(loc => loc.X == target.X && loc.Y == target.Y) != null)
                break;
            var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
            g++;


            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(loc => loc.X == adjacentSquare.X
                        && loc.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(loc => loc.X == adjacentSquare.X
                        && loc.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                    }
                }
            }
        }
        return List<>;
    }
    static List<Location> GetWalkableAdjacentSquares(int x, int y, string[] map)
    {
            var proposedLocations = new List<Location>()
        {
            new Location { X = x, Y = y - 1 },
            new Location { X = x, Y = y + 1 },
            new Location { X = x - 1, Y = y },
            new Location { X = x + 1, Y = y },
        };

        return proposedLocations.Where(
            loc => map[loc.Y][loc.X] == ' ' || map[loc.Y][loc.X] == 'B').ToList();
    }
    static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Mathf.Abs(targetX - x) + Mathf.Abs(targetY - y);
    }
}