﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SettlersOfCatan
{
    class ResourceHex : PictureBox
    {
        private String resourceType;

        private readonly Size HEX_SIZE = new Size(150, 150);
        private static readonly Random r = new Random();

        public ResourceHex()
        {
            resourceType = "None";
            this.BackColor = Color.FromArgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
            this.Size = HEX_SIZE;
        }

        public ResourceHex(String type)
        {
            resourceType = type;
            this.BackColor = Color.FromArgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
            this.Size = HEX_SIZE;
        }


    }
}