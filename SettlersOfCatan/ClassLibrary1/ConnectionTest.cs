﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SettlersOfCatan;
using System.Drawing;

namespace ClassLibrary1
{
	internal class ConnectionTest
	{
		[Test()]
		public void TestThatRoadColorGetsSet()
		{
			Intersection i = new Intersection(new Point(1, 1));
			Intersection i2 = new Intersection(new Point(1, 2));
			Connection c = new Connection(i, i2);
			c.setRoadColor(Color.HotPink);
			Assert.AreEqual(Color.HotPink, c.getRoadColor());
		}

		[Test()]
		public void TestGetCoordinates()
		{
			var target = new Connection(new Intersection(new Point(2, 2)), new Intersection(new Point(2, 3)));
			Assert.AreNotEqual((new Point(2, 2)), target.getCoords());
		}
	}
}