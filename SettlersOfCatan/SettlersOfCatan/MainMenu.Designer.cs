﻿namespace SettlersOfCatan
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.GameLabel = new System.Windows.Forms.Label();
			this.NewGameButton = new System.Windows.Forms.Button();
			this.LoadGameButton = new System.Windows.Forms.Button();
			this.RulesButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// GameLabel
			// 
			this.GameLabel.AutoSize = true;
			this.GameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GameLabel.Location = new System.Drawing.Point(265, 48);
			this.GameLabel.Name = "GameLabel";
			this.GameLabel.Size = new System.Drawing.Size(381, 55);
			this.GameLabel.TabIndex = 0;
			this.GameLabel.Text = "Settlers of Catan";
			// 
			// NewGameButton
			// 
			this.NewGameButton.Location = new System.Drawing.Point(374, 193);
			this.NewGameButton.Name = "NewGameButton";
			this.NewGameButton.Size = new System.Drawing.Size(152, 74);
			this.NewGameButton.TabIndex = 1;
			this.NewGameButton.Text = "New Game";
			this.NewGameButton.UseVisualStyleBackColor = true;
			this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
			// 
			// LoadGameButton
			// 
			this.LoadGameButton.Location = new System.Drawing.Point(374, 305);
			this.LoadGameButton.Name = "LoadGameButton";
			this.LoadGameButton.Size = new System.Drawing.Size(152, 74);
			this.LoadGameButton.TabIndex = 2;
			this.LoadGameButton.Text = "Load Game";
			this.LoadGameButton.UseVisualStyleBackColor = true;
			// 
			// RulesButton
			// 
			this.RulesButton.Location = new System.Drawing.Point(374, 419);
			this.RulesButton.Name = "RulesButton";
			this.RulesButton.Size = new System.Drawing.Size(152, 74);
			this.RulesButton.TabIndex = 3;
			this.RulesButton.Text = "Rules";
			this.RulesButton.UseVisualStyleBackColor = true;
			this.RulesButton.Click += new System.EventHandler(this.RulesButton_Click);
			// 
			// MainMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(947, 620);
			this.Controls.Add(this.RulesButton);
			this.Controls.Add(this.LoadGameButton);
			this.Controls.Add(this.NewGameButton);
			this.Controls.Add(this.GameLabel);
			this.Name = "MainMenu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settlers of Catan - Main Menu";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameLabel;
        private System.Windows.Forms.Button NewGameButton;
        private System.Windows.Forms.Button LoadGameButton;
        private System.Windows.Forms.Button RulesButton;
    }
}

