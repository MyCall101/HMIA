using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIA
{
    internal class Room
    {

        public static void DisplayHrsRates()
        {
            Console.WriteLine("\t┌----------------------------------------------------┐");
            Console.WriteLine("\t|\t\tLUXURY ROOM HOURS RATES\t\t     |");
            Console.WriteLine("\t| 3HRS - ₱" + (Program.LUXTHREEHRSRATE.ToString("F2")) + " | 5HRS - ₱" + (Program.LUXFIVEHRSRATE.ToString("F2")) + " | 12HRS - ₱" + (Program.LUXTWELVEHRSRATE.ToString("F2")) + " |");
            Console.WriteLine("\t|----------------------------------------------------|");
            Console.WriteLine("\t|\t\tSTANDARD ROOM HOURS RATES\t     |");
            Console.WriteLine("\t| 3HRS - ₱" + (Program.STANDTHREEHRSRATE.ToString("F2")) + " | 5HRS - ₱" + (Program.STANDFIVEHRSRATE.ToString("F2")) + " | 12HRS - ₱" + (Program.STANDTWELVEHRSRATE.ToString("F2")) + " |");
            Console.WriteLine("\t└----------------------------------------------------┘");
        }

        public static void DesignDraw()
        {

        }
        
        public static void Draw(string[,] rooms, ref int availableCtr, int dispRowCtr)
        {
            string dash = string.Concat(Enumerable.Repeat("-", 37));
            //Console.WriteLine("\t┌-------------------------------------┐");
            Console.WriteLine("\t┌{0}┐",dash);
            for (int n = 0; n < rooms.GetLength(0); n++)
            {
                if (dispRowCtr <= 5)
                {
                    if (rooms[n, 1] == "0")
                    {

                        //Console.Write("\t| " + (Convert.ToInt32(rooms[n, 0]).ToString("00")) + " |");
                        Console.Write("\t| " + rooms[n, 0] + " |");

                        dispRowCtr++;
                        availableCtr++;
                    }
                    else
                    {
                        Console.Write("\t| OC |");
                        dispRowCtr++;
                        availableCtr--;
                    }
                }
                if (dispRowCtr > 5)
                {
                    dispRowCtr = 1;
                    if (n < 5)
                    {
                        //Console.WriteLine("\n\t|-------------------------------------|");
                        Console.WriteLine("\n\t|{0}|",dash);
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                }

            }
            if (availableCtr == 0)
                Console.WriteLine("\t|\tNo Rooms Available. \t     |");

            //Console.WriteLine("\t└-------------------------------------┘");
            Console.WriteLine("\t└{0}┘",dash);
        }
        public static void DisplayAvailableRooms(ref int availableCtrLuxRom, ref int availableCtrStandRom)
        {
            // LUXURY ROOMS
            Console.WriteLine("\n\tAvailable Luxury Rooms:");
            Console.WriteLine("\n\tAmeties: Free Tissue,");
            int ctr = 1;
            //DrawRooms(Program.luxuryRooms, ref availableCtrLuxRom, ctr);
            //Draw(Program.luxuryRooms, Program.standardRooms, ref availableCtrLuxRom, ctr);

            // STANDARD ROOMS
            Console.WriteLine("\n\tAvailable Standard Rooms:");
            //DrawRooms(Program.standardRooms, ref availableCtrStandRom, ctr);

        }

        public static void SetDesigned(ref string[,] luxuryRooms, ref string[,] standardRooms)
        {
            luxuryRooms = new string[3, 5]
            {
                { "L01","0","Single","1","High thread-count sheets, plush mattresses, and curated pillow menus." +
                                          "Rain showers, deep soaking tubs, heated floors, and designer toiletries." +
                                          "Smart room controls (voice/tablet), high-end sound systems, and streaming-ready TVs." },
                { "L02","0","Double","2","High thread-count sheets, plush mattresses, and curated pillow menus." +
                                          "Rain showers, deep soaking tubs, heated floors, and designer toiletries." +
                                          "Smart room controls (voice/tablet), high-end sound systems, and streaming-ready TVs." },
                { "L03","0","Triple/Quad","3-4","High thread-count sheets, plush mattresses, and curated pillow menus." +
                                          "Rain showers, deep soaking tubs, heated floors, and designer toiletries." +
                                          "Smart room controls (voice/tablet), high-end sound systems, and streaming-ready TVs."}
            };

            standardRooms = new string[3, 5]
            {
                { "S01","0","Single","1","Clean linens, basic mattress, 1–2 pillows." +
                                          "Shower/tub combo, basic soap, shampoo, and hairdryer." +
                                          "Standard TV, basic Wi-Fi, and telephone." },
                { "S02","0","Double","2","Clean linens, basic mattress, 1–2 pillows." +
                                          "Shower/tub combo, basic soap, shampoo, and hairdryer." +
                                          "Standard TV, basic Wi-Fi, and telephone." },
                { "S03","0","Triple/Quad","3-4","Clean linens, basic mattress, 1–2 pillows." +
                                          "Shower/tub combo, basic soap, shampoo, and hairdryer." +
                                          "Standard TV, basic Wi-Fi, and telephone."}
            };
        }
        public static void Sets(ref string[,] luxuryRooms, ref string[,] standardRooms)
        {
            luxuryRooms = new string[10, 2];
            standardRooms = new string[10, 2];

            for (int i = 0; i < luxuryRooms.GetLength(0); i++)
            {
                luxuryRooms[i, 0] = "L" + (i + 1).ToString("00");
                luxuryRooms[i, 1] = "0";
                standardRooms[i, 0] = "S" + ((i + 1) + 10).ToString("00");
                standardRooms[i, 1] = "0";
            }

        }
        public static void UpdateRoomStatus(char rType, int roomNum)
        {
            Console.WriteLine("\n\t***ROOMS STATUS UPDATE***");
            if (rType == 'L' || rType == 'l')
            {
                for (int i = 0; i < Program.luxuryRooms.GetLength(0); i++)
                {
                    if (Program.luxuryRooms[i, 0] == roomNum.ToString())
                    {
                        Program.luxuryRooms[i, 1] = "1";
                        break;
                    }
                }
            }
            else if (rType == 'S' || rType == 's')
            {
                for (int i = 0; i < Program.standardRooms.GetLength(0); i++)
                {
                    if (Program.standardRooms[i, 0] == roomNum.ToString())
                    {
                        Program.standardRooms[i, 1] = "1";
                        break;
                    }
                }
            }
        }

    }
}
