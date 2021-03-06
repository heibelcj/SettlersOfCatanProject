﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using SettlersOfCatan.Properties;

namespace SettlersOfCatan
{
	public class Hand
	{
		private ResourceManager rm = Resources.ResourceManager;
		private string language = Global_Variables.language;

		private int ore;
		private int wool;
		private int lumber;
		private int grain;
		private int brick;
		private int knights;
		private int freeRoadPoints;
		private int freeSettlementPoints;

		private List<DevelopmentCard> devCards;

		public Hand()
		{
			this.ore = 0;
			this.wool = 0;
			this.lumber = 0;
			this.grain = 0;
			this.brick = 0;
			this.devCards = new List<DevelopmentCard>();
			this.knights = 0;
		}

		public void incrementAllResources(int number)
		{
			this.ore += number;
			this.wool += number;
			this.lumber += number;
			this.grain += number;
			this.brick += number;
		}

		public int getResources()
		{
			return this.ore + this.wool + this.lumber + this.grain + this.brick;
		}

		public int getOre()
		{
			return this.ore;
		}

		public int getWool()
		{
			return this.wool;
		}

		public int getLumber()
		{
			return this.lumber;
		}

		public int getGrain()
		{
			return this.grain;
		}

		public int getBrick()
		{
			return this.brick;
		}

		public int getKnights()
		{
			return this.knights;
		}

		public int getDevCardCount()
		{
			return this.devCards.Count();
		}

		public int getResource(String resourceType)
		{
			switch (resourceType.ToLower())
			{
				case "ore":
				case "mineral":
					return getOre();
				case "wool":
				case "lana":
					return getWool();
				case "lumber":
				case "maderas":
					return getLumber();
				case "grain":
				case "grano":
					return getGrain();
				case "brick":
				case "ladrillo":
					return getBrick();
			}
			throw new ArgumentException();
		}

		public void modifyResources(String resourceType, int amount)
		{
			switch (resourceType.ToLower())
			{
				case "ore":
				case "mineral":
					modifyOre(amount);
					break;
				case "wool":
				case "lana":
					modifyWool(amount);
					break;
				case "lumber":
				case "maderas":
					modifyLumber(amount);
					break;
				case "grain":
				case "grano":
					modifyGrain(amount);
					break;
				case "brick":
				case "ladrillo":
					modifyBrick(amount);
					break;
			}
		}

		public void modifyOre(int amount)
		{
			if (this.ore < amount*-1)
			{
				throw new System.ArgumentException(rm.GetString(language + "NegativeOre"));
			}
			else
			{
				this.ore += amount;
			}
		}

		public void modifyWool(int amount)
		{
			if (this.wool < amount*-1)
			{
				throw new System.ArgumentException(rm.GetString(language + "NegativeWool"));
			}
			else
			{
				this.wool += amount;
			}
		}

		public void modifyLumber(int amount)
		{
			if (this.lumber < amount*-1)
			{
				throw new System.ArgumentException(rm.GetString(language + "NegativeLumber"));
			}
			else
			{
				this.lumber += amount;
			}
		}

		public void modifyGrain(int amount)
		{
			if (this.grain < amount*-1)
			{
				throw new System.ArgumentException(rm.GetString(language + "NegativeGrain"));
			}
			else
			{
				this.grain += amount;
			}
		}

		public void modifyBrick(int amount)
		{
			if (this.brick < amount*-1)
			{
				throw new System.ArgumentException(rm.GetString(language + "NegativeBrick"));
			}
			else
			{
				this.brick += amount;
			}
		}

		public void addDevCard(List<DevelopmentCard> cards)
		{
			this.devCards.AddRange(cards);
		}

		public void removeDevCard(String cardType)
		{
			switch (cardType)
			{
				case "knight":
				{
					if (devCardsContains("knight"))
					{
						var itemToRemove = this.devCards.First(r => r.getType() == "knight");
						this.devCards.Remove(itemToRemove);
						break;
					}
					else
					{
						throw new ArgumentException(rm.GetString(language + "KnightException"));
						break;
					}
				}
				case "victoryPoint":
				{
					if (devCardsContains("victoryPoint"))
					{
						var itemToRemove = this.devCards.First(r => r.getType() == "victoryPoint");
						this.devCards.Remove(itemToRemove);
						break;
					}
					else
					{
						throw new ArgumentException(rm.GetString(language + "VictoryPointException"));
						break;
					}
				}
				case "monopoly":
				{
					if (devCardsContains("monopoly"))
					{
						var itemToRemove = this.devCards.First(r => r.getType() == "monopoly");
						this.devCards.Remove(itemToRemove);
						break;
					}
					else
					{
						throw new ArgumentException(rm.GetString(language + "MonopolyException"));
						break;
					}
				}
				case "roadBuilder":
				{
					if (devCardsContains("roadBuilder"))
					{
						var itemToRemove = this.devCards.First(r => r.getType() == "roadBuilder");
						this.devCards.Remove(itemToRemove);
						break;
					}
					else
					{
						throw new ArgumentException(rm.GetString(language + "RoadBuilderException"));
						break;
					}
				}
				case "yearOfPlenty":
				{
					if (devCardsContains("yearOfPlenty"))
					{
						var itemToRemove = this.devCards.First(r => r.getType() == "yearOfPlenty");
						this.devCards.Remove(itemToRemove);
						break;
					}
					else
					{
						throw new ArgumentException(rm.GetString(language + "YearOfPlentyException"));
						break;
					}
				}
			}
		}

		public bool devCardsContains(String cardType)
		{
			while (this.devCards.Count > 0)
			{
				switch (cardType)
				{
					case "knight":
					{
						foreach (DevelopmentCard card in this.devCards)
						{
							if (card.getType() == "knight")
							{
								return true;
							}
						}
						return false;
					}
					case "monopoly":
					{
						foreach (DevelopmentCard card in this.devCards)
						{
							if (card.getType() == "monopoly")
							{
								return true;
							}
						}
						return false;
					}
					case "victoryPoint":
					{
						foreach (DevelopmentCard card in this.devCards)
						{
							if (card.getType() == "victoryPoint")
							{
								return true;
							}
						}
						return false;
					}
					case "roadBuilder":
					{
						foreach (DevelopmentCard card in this.devCards)
						{
							if (card.getType() == "roadBuilder")
							{
								return true;
							}
						}
						return false;
					}
					case "yearOfPlenty":
					{
						foreach (DevelopmentCard card in this.devCards)
						{
							if (card.getType() == "yearOfPlenty")
							{
								return true;
							}
						}
						return false;
					}
				}
			}
			return false;
		}

		public bool hasRoadResources()
		{
			return ((brick >= 1) && (lumber >= 1)) || this.freeRoadPoints >= 1;
		}

		public bool hasSettlementResources()
		{
			return ((brick >= 1) && (grain >= 1) && (wool >= 1) && (lumber >= 1)) || (this.freeSettlementPoints > 0);
		}

		public bool hasCityResources()
		{
			return (grain >= 2) && (ore >= 3);
		}

		public bool hasDevCardResources()
		{
			return (wool >= 1) && (grain >= 1) && (ore >= 1);
		}

		public void incrementKnightsPlayed()
		{
			this.knights++;
		}

		public bool hasFreeRoadPoints()
		{
			return (this.freeRoadPoints != 0);
		}

		public void modifyFreeRoadPoints(int i)
		{
			this.freeRoadPoints += i;
		}

		public void modifyFreeSettlementPoints(int i)
		{
			this.freeSettlementPoints += i;
		}

		internal bool hasFreeSettlementPoints()
		{
			return (freeSettlementPoints > 0);
		}
	}
}