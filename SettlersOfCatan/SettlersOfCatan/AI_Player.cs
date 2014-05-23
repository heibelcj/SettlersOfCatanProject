﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlersOfCatan;
using System.Drawing;

namespace SettlersOfCatan
{
    public class AI_Player : Player
    {
        private String name;
        private Color color;
        //private bool hasWon = false;
        private World world;
        public Hand playerHand = new Hand();
        private Point intersectionCoordsToBuild;

        public AI_Player(String playerName, Color playerColor, World world1) : base(playerName, playerColor, world1)
        {
			this.name = playerName;
			this.color = playerColor;
			this.world = world1;
        }
        /*
        new public String getName()
        {
            //return base.getName();
            return this.name;
        }
         * */

        new public String takeYourTurn(int roundsCompleted)
        {
            String result = "none";
            if (roundsCompleted < 2)
            {
                //this.playerHand.modifyFreeRoadPoints(1);
               // this.playerHand.modifyFreeSettlementPoints(1);
                List<Point> points = new List<Point>();
                points.Add(new Point(2, 2));
                points.Add(new Point(3, 3));
                points.Add(new Point(1, 2));
                points.Add(new Point(3, 2));
                points.Add(new Point(1, 5));
                points.Add(new Point(3, 4));
                points.Add(new Point(2, 7));
                points.Add(new Point(4, 5));
                points.Add(new Point(4, 3));
                //  Pick a few arbitrary spots to try placing a settlement
                foreach (Point p in points)
                {
                    if (this.world.aiCheckIfCanBuildHere(p))
                    {
                        this.intersectionCoordsToBuild = p;
                        result = "settlement";
                        break; // means that this player has successfully built a
                        // settlement; stop searching for a spot to build
                    }
                }

            }
            else
            {
                //  int diceRollNum;
                // 1. Roll the dice
                this.world.rollDice();
                //  diceRollNum = this.world.getRollNumber();
                // 1a. Rolled a 7?
                // 1a-i. Get rid of half of your cards?
                // 1a-ii. Move the robber
                // 1a-iii. Steal a card
                // 1b. Not a 7?  Resources generated automatically
                // 2. Build road if player has sufficient resources
                if (this.playerHand.hasRoadResources())
                {
                    List<Point> locs = this.getSettlementLocations();
                    foreach (Point p in locs)
                    {
                        Intersection i = world.catanMap.getIslandMap().getIntAtIndex(p);
                        if (i.hasOpenRoad())
                        {
                            this.world.roadButtonClicked(i.getAnOpenRoad());
                            break;
                        }
                    }

                }
            }
            //this.world.endTurn();   
            return result;
        }

        public Point getIntersectionCoordsToBuild()
        {
            return this.intersectionCoordsToBuild;
        }

    }
}