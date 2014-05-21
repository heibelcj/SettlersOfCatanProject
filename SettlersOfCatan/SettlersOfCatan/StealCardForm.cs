﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettlersOfCatan
{
	public partial class StealCardForm : Form
	{
		private World world;
		//private GameScreen gameScreen;
		private Hex robberHex;
		private object playerToStealFrom;

		private bool canDispose = false;

		public StealCardForm(World world)
		{
			this.world = world;
			//this.gameScreen = gs;
			this.robberHex = world.getRobberHex();
			InitializeComponent();
			updateComboBox();
		}

		private void updateComboBox()
		{
			int owners = world.getRobberHex().owners.Count;

			for (int i = 0; i < owners; i++)
			{
				this.PlayerNameComboBox.Items.Insert(i, world.getRobberHex().owners[i].getName());
			}

			if (this.PlayerNameComboBox.Items.Count == 0)
			{
				this.PlayerNameComboBox.Items.Insert(0, "No one");
			}
		}

		private void StealCardButton_Click(object sender, EventArgs e)
		{
			if (this.PlayerNameComboBox.SelectedIndex > -1)
			{
				this.playerToStealFrom = this.PlayerNameComboBox.SelectedItem;
				if (checkChoice())
				{
					this.Dispose();
				}
			}
			else
			{
				DialogResult num = MessageBox.Show("You must choose one of the choices to steal from.",
	"Stealee Not Chosen",
	MessageBoxButtons.OK,
	MessageBoxIcon.Exclamation);
			}
		}

		private bool checkChoice()
		{
			string playerName = (string) this.playerToStealFrom;
			int owners = this.robberHex.owners.Count;

			if (playerName.Equals("No one"))
			{
				this.canDispose = true;
				return true;
			}

			for (int i = 0; i < owners; i++)
			{
				if (this.robberHex.owners[i].getName().Equals(playerName))
				{
					string stolenResource = this.robberHex.owners[i].rob();
					if (stolenResource == "none")
					{
						this.canDispose = true;
						return true;
					}
					else
					{
						this.world.currentPlayer.playerHand.modifyResources(stolenResource, 1);
						this.canDispose = true;
						return true;
					}
				}
			}
			return false;
		}

		private void StealCardsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.canDispose)
			{
				this.Dispose();
			}
			else
			{
				e.Cancel = true;
			}
		}

	}
}