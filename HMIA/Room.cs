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
            //Console.WriteLine("\t┌----------------------------------------------------┐");
            //Console.WriteLine("\t|\t\tLUXURY ROOM HOURS RATES\t\t     |");
            //Console.WriteLine("\t| 3HRS - ₱" + (Program.LUXTHREEHRSRATE.ToString("F2")) + " | 5HRS - ₱" + (Program.LUXFIVEHRSRATE.ToString("F2")) + " | 12HRS - ₱" + (Program.LUXTWELVEHRSRATE.ToString("F2")) + " |");
            //Console.WriteLine("\t|----------------------------------------------------|");
            //Console.WriteLine("\t|\t\tSTANDARD ROOM HOURS RATES\t     |");
            //Console.WriteLine("\t| 3HRS - ₱" + (Program.STANDTHREEHRSRATE.ToString("F2")) + " | 5HRS - ₱" + (Program.STANDFIVEHRSRATE.ToString("F2")) + " | 12HRS - ₱" + (Program.STANDTWELVEHRSRATE.ToString("F2")) + " |");
            //Console.WriteLine("\t└----------------------------------------------------┘");
        }

        public static void DesignDraw(string[,] rooms, ref int availableCtr,string title)
        {
            string dash = string.Concat(Enumerable.Repeat("-", 111));
            string dash2 = string.Concat(Enumerable.Repeat("-", 30));

            string stickHead = @" O";
            string stickBody = @"/|\";
            string stickLeg = @"/ \";

            string goodFor1 = "Good for pax: " + rooms[0, 2];
            string goodFor2 = "Good for pax: " + rooms[1, 2];
            string goodFor3 = "Good for pax: " + rooms[2, 2];

            string rate1 = "Rate per night: " + rooms[0, 4];
            string rate2 = "Rate per night: " + rooms[1, 4];
            string rate3 = "Rate per night: " + rooms[2, 4];

            string excess1 = "Excess rate: " + rooms[0, 5];
            string excess2 = "Excess rate: " + rooms[1, 5];
            string excess3 = "Excess rate: " + rooms[2, 5];

            string paxFor1 = (rooms[0,1] == "0")? goodFor1: stickHead;
            string perRate1 = (rooms[0, 1] == "0") ? rate1 : stickBody;
            string xcess1 = (rooms[0, 1] == "0") ? excess1 : stickLeg;
            availableCtr = (rooms[0, 1] == "0") ? availableCtr++ : availableCtr--;


            string paxFor2 = (rooms[1, 1] == "0") ? goodFor2 : stickHead;
            string perRate2 = (rooms[1, 1] == "0") ? rate2 : stickBody;
            string xcess2 = (rooms[1, 1] == "0") ? excess2 : stickLeg;
            availableCtr = (rooms[1, 1] == "0") ? availableCtr++ : availableCtr--;

            string paxFor3 = (rooms[2, 1] == "0") ? goodFor3 : stickHead;
            string perRate3 = (rooms[2, 1] == "0") ? rate3 : stickBody;
            string xcess3 = (rooms[2, 1] == "0") ? excess3 : stickLeg;
            availableCtr = (rooms[2, 1] == "0") ? availableCtr++ : availableCtr--;

            availableCtr = (availableCtr < 0) ? 0 : availableCtr;


            Console.WriteLine("┌{0}┐", dash);
            string amenities = "";
            if(title == "LUXURY")
            {
                Console.WriteLine("|\t\t\t\t\t\t{0} ROOM\t\t\t\t\t\t\t|", title);
                amenities = "Sheets,mattresses & curated pillow menus.Rain showers & deep soaking tubs." +
                            "Smart room controls (voice)";
            }
            else
            {
                Console.WriteLine("|\t\t\t\t\t\t{0} ROOM\t\t\t\t\t\t\t|", title);
                amenities = "Clean linens, basic mattress, 1–2 pillows. Shower/tub combo." +
                            " Standard TV, basic Wi-Fi, and telephone.";
            }
            
            Console.WriteLine("|{0}|",dash);
            
            Console.WriteLine("|┌{0}┐\t┌{1}┐\t┌{2}┐|", dash2,dash2,dash2);
            Console.WriteLine("||{0,-30}|\t|{1,-30}|\t|{2,-30}||","ROOM " + rooms[0, 0],"ROOM " + rooms[1, 0],"ROOM " + rooms[2, 0]);
            Console.WriteLine("||{0}|\t|{1}|\t|{2}||", dash2, dash2, dash2);

            Console.WriteLine("||{0,-30}|\t|{1,-30}|\t|{2,-30}||", paxFor1, paxFor2, paxFor3);
            Console.WriteLine("||{0,-30}|\t|{1,-30}|\t|{2,-30}||", perRate1, perRate2, perRate3);
            Console.WriteLine("||{0,-30}|\t|{1,-30}|\t|{2,-30}||", xcess1, xcess2, xcess3);

            Console.WriteLine("|└{0}┘\t└{1}┘\t└{2}┘|", dash2,dash2,dash2);
            Console.WriteLine("|Amenities:{0}|",amenities);
            Console.WriteLine("└{0}┘", dash);
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
            //Console.WriteLine("\n\tAvailable Luxury Rooms:");
            //Console.WriteLine("\n\tAmeties: Free Tissue,");
            //int ctr = 1;
            Room.DesignDraw(Program.luxuryRooms, ref availableCtrLuxRom,"LUXURY");
            //DrawRooms(Program.luxuryRooms, ref availableCtrLuxRom, ctr);

            // STANDARD ROOMS
            //Console.WriteLine("\n\tAvailable Standard Rooms:");
            Room.DesignDraw(Program.standardRooms, ref availableCtrStandRom,"STANDARD");
            //DrawRooms(Program.standardRooms, ref availableCtrStandRom, ctr);

        }

        public static void SetDesigned(ref string[,] luxuryRooms, ref string[,] standardRooms)
        {
            luxuryRooms = new string[3, 6]
            {
                { "L01","0","1","Sheets,mattresses & curated pillow menus.Rain showers & deep soaking tubs." +
                            "Smart room controls (voice)","₱1,500","₱1,000"},
                { "L02","0","2","Sheets,mattresses & curated pillow menus.Rain showers & deep soaking tubs." +
                            "Smart room controls (voice)","₱3,000","₱1,000"},
                { "L03","0","3-5","Sheets,mattresses & curated pillow menus.Rain showers & deep soaking tubs." +
                            "Smart room controls (voice)","₱5,000","₱1,000"}
            };

            standardRooms = new string[3, 6]
            {
                { "S01","0","1","Clean linens, basic mattress, 1–2 pillows." +
                                          "Shower/tub combo, basic soap, shampoo, and hairdryer." +
                                          "Standard TV, basic Wi-Fi, and telephone.",
                                          "₱800",
                                          "₱500"},
                { "S02","0","2","Clean linens, basic mattress, 1–2 pillows." +
                                          "Shower/tub combo, basic soap, shampoo, and hairdryer." +
                                          "Standard TV, basic Wi-Fi, and telephone.",
                                          "₱1,600",
                                          "₱500"},
                { "S03","0","3-5","Clean linens, basic mattress, 1–2 pillows." +
                                          "Shower/tub combo, basic soap, shampoo, and hairdryer." +
                                          "Standard TV, basic Wi-Fi, and telephone.",
                                          "₱3,200",
                                          "₱500"}
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

        public static void UpdateRoomStatus(string roomCode)
        {
            string romType = roomCode.ToUpper().Substring(0, 1);
            if (romType == "L")
            {
                for (int i = 0; i < Program.luxuryRooms.GetLength(0); i++)
                {
                    if (Program.luxuryRooms[i, 0].ToUpper() == roomCode.ToUpper())
                    {
                        Program.luxuryRooms[i, 1] = "1";
                        break;
                    }
                }
            }
            else if (romType == "S")
            {
                for (int i = 0; i < Program.standardRooms.GetLength(0); i++)
                {
                    if (Program.standardRooms[i, 0].ToUpper() == roomCode.ToUpper())
                    {
                        Program.standardRooms[i, 1] = "1";
                        break;
                    }
                }
            }
        }
        //public static void UpdateRoomStatus(char rType, int roomNum)
        //{


        //    Console.WriteLine("\n\t***ROOMS STATUS UPDATE***");
        //    if (rType == 'L' || rType == 'l')
        //    {
        //        for (int i = 0; i < Program.luxuryRooms.GetLength(0); i++)
        //        {
        //            if (Program.luxuryRooms[i, 0] == roomNum.ToString())
        //            {
        //                Program.luxuryRooms[i, 1] = "1";
        //                break;
        //            }
        //        }
        //    }
        //    else if (rType == 'S' || rType == 's')
        //    {
        //        for (int i = 0; i < Program.standardRooms.GetLength(0); i++)
        //        {
        //            if (Program.standardRooms[i, 0] == roomNum.ToString())
        //            {
        //                Program.standardRooms[i, 1] = "1";
        //                break;
        //            }
        //        }
        //    }
        //}

    }
}
