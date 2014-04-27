﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SettlersOfCatan;

namespace SettlersOfCatan
{
    public class Intersection
    {
        public List<Connection> connections = new List<Connection>(3);
        public List<Hex> resourceHexes = new List<Hex>(3);
        private Point coord;
        private Global_Variables.GAME_PIECE currentPiece = Global_Variables.GAME_PIECE.NONE;
        public System.Drawing.Color color;
        private Player owner;

        public Intersection(Point p)
        {
            coord = p;
            for (int i = 0; i < connections.Capacity; i++)
            {
                connections.Add(new Connection(null));
            }
            owner = null;
        }

        public void build(Global_Variables.GAME_PIECE piece)
        {
            currentPiece = piece;
        }

        public bool hasABuilding()
        {
            if (currentPiece != Global_Variables.GAME_PIECE.NONE) return true;
            else return false;
        }

        public bool areAllThreeConnectionsAvailableToBuild()
        {
            bool available = true;
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].getIntersection() != null)
                    available = available && !(connections[i].getIntersection().hasABuilding());
            }
            return available;
        }

        public bool canBuildAtIntersection()
        {
            bool canBuild;
            if (this.hasABuilding()) // Cannot build here if this intersection has a building
            {
                canBuild = false;
            }
            else if (this.areAllThreeConnectionsAvailableToBuild()) // We can build here because nobody around already built something!
            {
                canBuild = true;
            }
            else // There is a conflict because a surrounding intersection already has a building
            {
                canBuild = false;
            }
            return canBuild;
        }

        public Player getPlayer()
        {
            return owner;
        }

        public void setPlayer(Player p)
        {
            owner = p;
        }


        public int getNumOfResourcesToGenerate()
        {
            if (this.currentPiece == Global_Variables.GAME_PIECE.SETTLEMENT) return 1;
            else if (this.currentPiece == Global_Variables.GAME_PIECE.CITY) return 2;
            else return 0;
        }

        public Global_Variables.GAME_PIECE getPieceType()
        {
            return this.currentPiece;
        }

    }
}