﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;

namespace SettlersOfCatan
{
    public class Player
    {
        private readonly int MAX_CITIES = 4;
        private readonly int MAX_SETTLEMENTS = 5;
        private readonly int MAX_ROADS = 15;

        private int points;
        private int citiesPlayed;
        private int settlementsPlayed;
        private int roadsPlayed;
        //only public for testing
        public Hand playerHand;
        private Player playerToTradeWith;
        private int[] toTrade;
        private int[] toReceive;
        private String name;
        private Color color;
        private bool hasWon;
        private World world;
        public bool hasLongestRoad;
        public bool hasLargestArmy;

        public Player()
        {
            this.points = 0;
            this.citiesPlayed = 0;
            this.settlementsPlayed = 0;
            this.roadsPlayed = 0;
            this.playerHand = new Hand();
            this.toTrade = new int[5] { 0, 0, 0, 0, 0 };
            this.toReceive = new int[5] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith = null;
            this.hasWon = false;
            this.world = new World();
            this.hasLongestRoad = false;
            this.hasLargestArmy = false;
        }

        public Player(String playerName, Color playerColor, World world1)
        {
            this.name = playerName;
            this.color = playerColor;
            this.points = 0;
            this.citiesPlayed = 0;
            this.settlementsPlayed = 0;
            this.roadsPlayed = 0;
            this.playerHand = new Hand();
            this.toTrade = new int[5] { 0, 0, 0, 0, 0 };
            this.toReceive = new int[5] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith = null;
            this.hasWon = false;
            this.world = world1;
            this.hasLongestRoad = false;
            this.hasLargestArmy = false;
        }

        public String getName()
        {
            return this.name;
        }

        public Color getColor()
        {
            return this.color;
        }

        public int getCitiesRemaining()
        {
            return MAX_CITIES - citiesPlayed;
        }

        public int getSettlementsRemaining()
        {
            return MAX_SETTLEMENTS - settlementsPlayed;
        }

        public int getRoadsRemaining()
        {
            return MAX_ROADS - roadsPlayed;
        }

        public bool incrementCities()
        {
            if (getCitiesRemaining() > 0)
            {
                citiesPlayed++;
                this.points += 2;
                return true;
            }
            else
                return false;
        }

        public bool incrementSettlements()
        {
            if (getSettlementsRemaining() > 0)
            {
                settlementsPlayed++;
                this.points += 1;
                return true;
            }
            else
                return false;
        }

        public bool incrementRoads()
        {
            if (getRoadsRemaining() > 0)
            {
                roadsPlayed++;
                return true;
            }
            else
                return false;
        }

        public void proposeTrade(Player player, int[] trade, int[] receive)
        {
            this.toTrade = trade;
            this.toReceive = receive;
            this.playerToTradeWith = player;
            player.toReceive = trade;
            player.toTrade = receive;
            player.playerToTradeWith = this;
        }

        private bool canAcceptTrade()
        {
            return (this.playerToTradeWith.playerHand.getOre() >= this.toReceive[0] &&
                this.playerToTradeWith.playerHand.getWool() >= this.toReceive[1] &&
                this.playerToTradeWith.playerHand.getLumber() >= this.toReceive[2] &&
                this.playerToTradeWith.playerHand.getGrain() >= this.toReceive[3] &&
                this.playerToTradeWith.playerHand.getBrick() >= this.toReceive[4] &&
                this.playerHand.getOre() >= this.toTrade[0] &&
                this.playerHand.getWool() >= this.toTrade[1] &&
                this.playerHand.getLumber() >= this.toTrade[2] &&
                this.playerHand.getGrain() >= this.toTrade[3] &&
                this.playerHand.getBrick() >= this.toTrade[4]);
        }

        public void tradeCards(int[] trade, int[] receive)
        {
            this.playerHand.modifyOre(receive[0] - trade[0]);
            this.playerHand.modifyWool(receive[1] - trade[1]);
            this.playerHand.modifyLumber(receive[2] - trade[2]);
            this.playerHand.modifyGrain(receive[3] - trade[3]);
            this.playerHand.modifyBrick(receive[4] - trade[4]);
        }

        public void makeTrade()
        {
            if (this.canAcceptTrade())
            {
                tradeCards(this.toTrade, this.toReceive);
                this.playerToTradeWith.tradeCards(this.playerToTradeWith.toTrade, this.playerToTradeWith.toReceive);
                this.declineTrade();
            }
            else
            {
                throw new ArgumentException("Player's cards are such that trade cannot be performed");
            }
        }

        public void declineTrade()
        {
            this.toTrade = new int[] { 0, 0, 0, 0, 0 };
            this.toReceive = new int[] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith.toTrade = new int[] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith.toReceive = new int[] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith.playerToTradeWith = null;
            this.playerToTradeWith = null;
        }

        public Hand getHand()
        {
            return playerHand;
        }

        public void incrementPoints(int amount)
        {
            this.points += amount;
            if (this.points >= 10)
                this.hasWon = true;
        }

        public int getPoints()
        {
            return this.points;
        }

        public bool hasWonGame()
        {
            return this.hasWon;
        }


        public void tradeWithBank(String resourceToTradeIn, String resourceToGain)
        {
            if (resourceToTradeIn.ToLower().Equals("ore"))
            {
                if (getHand().getOre() >= 4)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("ore", 4);
                        this.playerHand.modifyOre(-4);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }
            }
            else if (resourceToTradeIn.ToLower().Equals("wool"))
            {
                if (getHand().getWool() >= 4)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("wool", 4);
                        this.playerHand.modifyWool(-4);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }

            }
            else if (resourceToTradeIn.ToLower().Equals("lumber"))
            {
                if (getHand().getLumber() >= 4)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("lumber", 4);
                        this.playerHand.modifyLumber(-4);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }

            }
            else if (resourceToTradeIn.ToLower().Equals("grain"))
            {
                if (getHand().getGrain() >= 4)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("grain", 4);
                        this.playerHand.modifyGrain(-4);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }

            }
            else if (resourceToTradeIn.ToLower().Equals("brick"))
            {
                if (getHand().getBrick() >= 4)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("brick", 4);
                        this.playerHand.modifyBrick(-4);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }
            }
        }

        // Need to know if port trades 2 or 3 resources in for something 
        // but for now, I'm assuming 3 resources
        public void tradeAtPort(int portType, String resourceToGain, String resourceToTrade)
        {
            if (resourceToTrade.ToLower().Equals("ore"))
            {
                if (getHand().getOre() >= 3)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("ore", 3);
                        this.playerHand.modifyOre(-3);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }
            }
            else if (resourceToTrade.ToLower().Equals("wool"))
            {
                if (getHand().getWool() >= 3)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("wool", 3);
                        this.playerHand.modifyWool(-3);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }

            }
            else if (resourceToTrade.ToLower().Equals("lumber"))
            {
                if (getHand().getLumber() >= 3)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("lumber", 3);
                        this.playerHand.modifyLumber(-3);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }

            }
            else if (resourceToTrade.ToLower().Equals("grain"))
            {
                if (getHand().getGrain() >= 3)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("grain", 3);
                        this.playerHand.modifyGrain(-3);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }

            }
            else if (resourceToTrade.ToLower().Equals("brick"))
            {
                if (getHand().getBrick() >= 3)
                {
                    try
                    {
                        this.world.bank.modifyResource(resourceToGain, -1);
                        this.world.bank.modifyResource("brick", 3);
                        this.playerHand.modifyBrick(-3);
                        modifyResourceInHand(resourceToGain);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw;
                    }
                }
            }
        }

        public void tradeForDevCard()
        {
            if (this.playerHand.getOre() >= 1 && this.playerHand.getGrain() >= 1 && this.playerHand.getWool() >= 1)
            {
                try
                {
                    List<DevelopmentCard> cards = this.world.bank.drawDevCard(1);
                    this.world.bank.modifyResource("ore", 1);
                    this.world.bank.modifyResource("wool", 1);
                    this.world.bank.modifyResource("grain", 1);
                    this.playerHand.modifyOre(-1);
                    this.playerHand.modifyWool(-1);
                    this.playerHand.modifyGrain(-1);
                    this.playerHand.addDevCard(cards);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }
        }

        private void modifyResourceInHand(String resource)
        {
            switch (resource)
            {
                case "ore":
                    {
                        this.playerHand.modifyOre(1);
                        break;
                    }
                case "wool":
                    {
                        this.playerHand.modifyWool(1);
                        break;
                    }
                case "lumber":
                    {
                        this.playerHand.modifyLumber(1);
                        break;
                    }
                case "grain":
                    {
                        this.playerHand.modifyGrain(1);
                        break;
                    }
                case "brick":
                    {
                        this.playerHand.modifyBrick(1);
                        break;
                    }
            }
        }

        public void generateOre()
        {
            for (int i = 0; i < this.citiesPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("ore", -2);
                    this.playerHand.modifyOre(2);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }

            for (int i = 0; i < this.settlementsPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("ore", -1);
                    this.playerHand.modifyOre(1);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }
        }

        public void generateWool()
        {
            for (int i = 0; i < this.citiesPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("wool", -2);
                    this.playerHand.modifyWool(2);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }

            for (int i = 0; i < this.settlementsPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("wool", -1);
                    this.playerHand.modifyWool(1);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }
        }

        public void generateLumber()
        {
            for (int i = 0; i < this.citiesPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("lumber", -2);
                    this.playerHand.modifyLumber(2);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }

            for (int i = 0; i < this.settlementsPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("lumber", -1);
                    this.playerHand.modifyLumber(1);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }
        }

        public void generateGrain()
        {
            for (int i = 0; i < this.citiesPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("grain", -2);
                    this.playerHand.modifyGrain(2);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }

            for (int i = 0; i < this.settlementsPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("grain", -1);
                    this.playerHand.modifyGrain(1);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }
        }

        public void generateBrick()
        {
            for (int i = 0; i < this.citiesPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("brick", -2);
                    this.playerHand.modifyBrick(2);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }

            for (int i = 0; i < this.settlementsPlayed; i++)
            {
                try
                {
                    this.world.bank.modifyResource("brick", -1);
                    this.playerHand.modifyBrick(1);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw;
                }
            }
        }

        public bool canBuildCity()
        {
            return (this.playerHand.hasCityResources());

        }

        public bool canBuildSettlement()
        {
            return (this.playerHand.hasSettlementResources());
        }

        public bool canBuildRoad()
        {
            return (this.playerHand.hasRoadResources());
        }

        public void buildRoad()
        {
            if (this.getHand().hasFreeRoadPoints())
                this.getHand().modifyFreeRoadPoints(-1);
            else
            {
                this.playerHand.modifyBrick(-1);
                this.playerHand.modifyLumber(-1);
            }
            incrementRoads();
        }

        public void buildCity()
        {
            this.playerHand.modifyGrain(-3);
            this.playerHand.modifyOre(-2);
            incrementCities();
            incrementPoints(1); // One point is already counted for the settlement that was there
        }

        public void buildSettlement()
        {
            if (this.getHand().hasFreeSettlementPoints())
                this.getHand().modifyFreeSettlementPoints(-1);
            else
            {
                this.playerHand.modifyGrain(-1);
                this.playerHand.modifyLumber(-1);
                this.playerHand.modifyBrick(-1);
                this.playerHand.modifyWool(-1);
            }
            incrementSettlements();
            incrementPoints(1);
        }

        public int getRoadsPlayed()
        {
            return this.roadsPlayed;
        }

        public void playDevCard(String cardType, String resourceOne, String resourceTwo)
        {
            switch (cardType)
            {
                case "knight":
                    {
                        if (this.playerHand.devCardsContains("knight"))
                        {
                            this.playerHand.incrementKnightsPlayed();
                            this.playerHand.removeDevCard("knight");
                            break;
                        }
                        else
                        {
                            throw new ArgumentException("You don't have any Knights to play");
                        }
                    }
                case "victoryPoint":
                    {
                        if (this.playerHand.devCardsContains("victoryPoint"))
                        {
                            incrementPoints(1);
                            this.playerHand.removeDevCard("victoryPoint");
                            break;
                        }
                        else
                        {
                            throw new ArgumentException("You don't have any Victory Point cards to play");
                        }
                    }
                case "roadBuilder":
                    {
                        if (this.playerHand.devCardsContains("roadBuilder"))
                        {
                            this.getHand().modifyFreeRoadPoints(2);
                            this.playerHand.removeDevCard("roadBuilder");
                            break;
                        }
                        else
                        {
                            throw new ArgumentException("You don't have any Road Builder cards to play");
                        }
                    }
                case "monopoly":
                    {
                        if (this.playerHand.devCardsContains("monopoly"))
                        {
                            int resourceAmount = 0;

                            for (int i = 0; i < this.world.players.Count(); i++)
                            {
                                if (i != this.world.currentPlayerNumber)
                                {
                                    int amountLost = this.world.players[i].playerHand.getResource(resourceOne);
                                    resourceAmount += amountLost;
                                    this.world.players[i].playerHand.modifyResources(resourceOne, -amountLost);
                                }
                            }
                            this.playerHand.modifyResources(resourceOne, resourceAmount);
                            this.playerHand.removeDevCard("monopoly");
                            break;
                        }
                        else
                        {
                            throw new ArgumentException("You don't have any Monopoly cards to play");
                        }
                    }
                case "yearOfPlenty":
                    {
                        if (this.playerHand.devCardsContains("yearOfPlenty"))
                        {
                            this.playerHand.modifyResources(resourceOne, 1);
                            this.playerHand.modifyResources(resourceTwo, 1);
                            this.world.bank.modifyResource(resourceOne, -1);
                            this.world.bank.modifyResource(resourceTwo, -1);
                            this.playerHand.removeDevCard("yearOfPlenty");
                            break;
                        }
                        else
                        {
                            throw new ArgumentException("You don't have any Year of Plenty cards to play");
                        }
                    }
            }
        }
    }
}

