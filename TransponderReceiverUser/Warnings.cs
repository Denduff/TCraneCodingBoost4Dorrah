﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace TransponderReceiverUser
{
    public class Warnings : IWarnings
    {
        public List<Track> isInList { get; set; }

        public Warnings()
        {
            isInList = new List<Track>();
        }

        public bool checkY(Track plane)
        {
            if (10000 < plane.YCoordinate && plane.YCoordinate < 90000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkZ(Track plane)
        {
            if (500 < plane.Altitude && plane.Altitude < 20000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkX(Track plane)
        {
            if (10000 < plane.XCoordinate && plane.XCoordinate < 90000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addPlane(Track plane)
        {
            if (checkX(plane) && checkY(plane) && checkZ(plane) )
            {
                if(PlanesInOurList(plane))
                isInList.Add(plane);
            }
        }

        public void removePlaneIfOutOfAirspace(Track plane)
        {

        }

        // sort planes only in airspace.
        public bool PlanesInOurList(Track plane)
        {
            bool exists = false;
            lock (isInList)
            {
                for (int i = 0; i < isInList.Count; i++)
                {
                    if (isInList[i].Tag == plane.Tag)
                    {
                        exists = true;
                    }
                    else
                    {
                        exists = false;
                    }
                }

                return exists;

            }
        }

        public bool PlanesAreTooDamnClose(Track data)
        {
            lock (isInList)
            {
                foreach (var item in isInList.ToList().Where(item => item.Tag != data.Tag))
                {
                        if (Math.Abs((data.XCoordinate - item.XCoordinate)) < 100000 &&
                            Math.Abs((data.YCoordinate - item.YCoordinate)) < 100000 &&
                            Math.Abs((data.Altitude - item.Altitude)) < 200000)
                        {
                            Console.WriteLine("WARNING!! {0} and {1} are TOO DAMN CLOSE!!", data.Tag, item.Tag);
                        }
                }
            }

            return true;
        }
    }
  


}




