using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIA
{
    internal class Room
    {

        public static void DesignDraw(string[,] rooms, string title)
        {
            // Outer thin lines
            string dash = string.Concat(Enumerable.Repeat("-", 111));

            // Inner double lines for the room "Cards"
            string cardTop = string.Concat(Enumerable.Repeat("═", 30));

            string stickHead = @" O";
            string stickBody = @"/|\";
            string stickLeg = @"/ \";

            string goodFor1 = "Good for pax: " + rooms[0, 2];
            string goodFor2 = "Good for pax: " + rooms[1, 2];
            string goodFor3 = "Good for pax: " + rooms[2, 2];

            string rate1 = "Rate per night: " + rooms[0, 4];
            string rate2 = "Rate per night: " + rooms[1, 4];
            string rate3 = "Rate per night: " + rooms[2, 4];

            string excess1 = "Excess pax: " + rooms[0, 5];
            string excess2 = "Excess pax: " + rooms[1, 5];
            string excess3 = "Excess pax: " + rooms[2, 5];

            //string paxFor1 = (rooms[0, 1] == "0") ? goodFor1 : stickHead;
            //string perRate1 = (rooms[0, 1] == "0") ? rate1 : stickBody;
            //string xcess1 = (rooms[0, 1] == "0") ? excess1 : stickLeg;

            //string paxFor2 = (rooms[1, 1] == "0") ? goodFor2 : stickHead;
            //string perRate2 = (rooms[1, 1] == "0") ? rate2 : stickBody;
            //string xcess2 = (rooms[1, 1] == "0") ? excess2 : stickLeg;

            //string paxFor3 = (rooms[2, 1] == "0") ? goodFor3 : stickHead;
            //string perRate3 = (rooms[2, 1] == "0") ? rate3 : stickBody;
            //string xcess3 = (rooms[2, 1] == "0") ? excess3 : stickLeg;

            string paxFor1 = (rooms[0, 1] == "0") ? goodFor1 : goodFor1;
            string perRate1 = (rooms[0, 1] == "0") ? rate1 : rate1;
            string xcess1 = (rooms[0, 1] == "0") ? excess1 : excess1;

            string paxFor2 = (rooms[1, 1] == "0") ? goodFor2 : goodFor2;
            string perRate2 = (rooms[1, 1] == "0") ? rate2 : rate2;
            string xcess2 = (rooms[1, 1] == "0") ? excess2 : excess2;

            string paxFor3 = (rooms[2, 1] == "0") ? goodFor3 : goodFor3;
            string perRate3 = (rooms[2, 1] == "0") ? rate3 : rate3;
            string xcess3 = (rooms[2, 1] == "0") ? excess3 : excess3;

            // DRAW ASCII ART HEADERS
            ConsoleColor themeColor = ConsoleColor.White;

            if (title == "LUXURY")
            {
                themeColor = ConsoleColor.Yellow;
                Console.ForegroundColor = themeColor;
                string luxLogo = @"
                            ░█░░░█░█░▀▄▀░█░█░█▀▄░█░█░░░█▀█░█▀█░█▀█░█▄█
                            ░█░░░█░█░░█░░█░█░█▀▄░░█░░░░█▀▄░█░█░█░█░█░█
                            ░▀▀▀░▀▀▀░▀░▀░▀▀▀░▀░▀░░▀░░░░▀░▀░▀▀▀░▀▀▀░▀░▀";
                Console.WriteLine("\n" + luxLogo);
                Console.ResetColor();
            }
            else if (title == "STANDARD")
            {
                themeColor = ConsoleColor.Cyan;
                Console.ForegroundColor = themeColor;
                string stdLogo = @"
                        ░█▀▀░▀█▀░█▀█░█▀█░█▀▄░█▀█░█▀▄░█▀▄░░░█▀█░█▀█░█▀█░█▄█
                        ░▀▀█░░█░░█▀█░█░█░█░█░█▀█░█▀▄░█░█░░░█▀▄░█░█░█░█░█░█
                        ░▀▀▀░░▀░░▀░▀░▀░▀░▀▀░░▀░▀░▀░▀░▀▀░░░░▀░▀░▀▀▀░▀▀▀░▀░▀";
                Console.WriteLine("\n" + stdLogo);
                Console.ResetColor();
            }

            Console.WriteLine("┌{0}┐", dash);
            string amenities = "";
            if (title == "LUXURY")
            {
                amenities = "Sheets, mattresses & curated pillow menus. Rain showers & deep soaking tubs. " +
                            "Smart room controls.";               
            }
            else if (title == "STANDARD")
            {
                amenities = "Clean linens, basic mattress, 1–2 pillows. Shower/tub combo. " +
                            "Standard TV, Wi-Fi, and telephone.";
            }

            // TOP OF CARDS (Applying Theme Color to the inner cards)
            Console.Write("|");
            Console.ForegroundColor = themeColor;
            Console.Write("╔{0}╗\t╔{1}╗\t╔{2}╗", cardTop, cardTop, cardTop);
            Console.ResetColor();
            Console.WriteLine("|");

            // ROOM NAMES
            Console.Write("|");
            Console.ForegroundColor = themeColor;
            Console.Write("║{0,-30}║\t║{1,-30}║\t║{2,-30}║", "  ROOM " + rooms[0, 0], "  ROOM " + rooms[1, 0], "  ROOM " + rooms[2, 0]);
            Console.ResetColor();
            Console.WriteLine("|");

            // DIVIDER LINE
            Console.Write("|");
            Console.ForegroundColor = themeColor;
            Console.Write("╠{0}╣\t╠{1}╣\t╠{2}╣", cardTop, cardTop, cardTop);
            Console.ResetColor();
            Console.WriteLine("|");

            // ROOM DETAILS
            Console.Write("|");
            Console.ForegroundColor = themeColor;
            Console.Write("║"); Console.ResetColor(); Console.Write("{0,-30}", paxFor1); Console.ForegroundColor = themeColor; Console.Write("║\t║");
            Console.ResetColor(); Console.Write("{0,-30}", paxFor2); Console.ForegroundColor = themeColor; Console.Write("║\t║");
            Console.ResetColor(); Console.Write("{0,-30}", paxFor3); Console.ForegroundColor = themeColor; Console.Write("║");
            Console.ResetColor(); Console.WriteLine("|");

            Console.Write("|");
            Console.ForegroundColor = themeColor;
            Console.Write("║"); Console.ResetColor(); Console.Write("{0,-30}", perRate1); Console.ForegroundColor = themeColor; Console.Write("║\t║");
            Console.ResetColor(); Console.Write("{0,-30}", perRate2); Console.ForegroundColor = themeColor; Console.Write("║\t║");
            Console.ResetColor(); Console.Write("{0,-30}", perRate3); Console.ForegroundColor = themeColor; Console.Write("║");
            Console.ResetColor(); Console.WriteLine("|");

            Console.Write("|");
            Console.ForegroundColor = themeColor;
            Console.Write("║"); Console.ResetColor(); Console.Write("{0,-30}", xcess1); Console.ForegroundColor = themeColor; Console.Write("║\t║");
            Console.ResetColor(); Console.Write("{0,-30}", xcess2); Console.ForegroundColor = themeColor; Console.Write("║\t║");
            Console.ResetColor(); Console.Write("{0,-30}", xcess3); Console.ForegroundColor = themeColor; Console.Write("║");
            Console.ResetColor(); Console.WriteLine("|");

            // BOTTOM OF CARDS
            Console.Write("|");
            Console.ForegroundColor = themeColor;
            Console.Write("╚{0}╝\t╚{1}╝\t╚{2}╝", cardTop, cardTop, cardTop);
            Console.ResetColor();
            Console.WriteLine("|");

            // AMENITIES FOOTER
             Console.Write("| Amenities: ");
            
            if (title == "LUXURY")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (title == "STANDARD")
                Console.ForegroundColor = ConsoleColor.Cyan;

            // 98 is the remaining width (109 total - 11 for "Amenities: ")
            Console.Write("{0,-98}", amenities);
            
            Console.ResetColor();
            Console.WriteLine(" |");
            Console.WriteLine("└{0}┘", dash);
        }

        public static void DisplayAvailableRooms()
        {
            // LUXURY ROOMS
            Room.DesignDraw(Program.luxuryRooms, "LUXURY");

            // STANDARD ROOMS
            Room.DesignDraw(Program.standardRooms, "STANDARD");

        }

        public static void SetDesigned(ref string[,] luxuryRooms, ref string[,] standardRooms)
        {
            luxuryRooms = new string[3, 6]
            {
                { "L01","0","1","Sheets,mattresses & curated pillow menus.Rain showers & deep soaking tubs." +
                            "Smart room controls (voice)","₱1,500","₱1,000"},
                { "L02","0","2","Sheets,mattresses & curated pillow menus.Rain showers & deep soaking tubs." +
                            "Smart room controls (voice)","₱3,000","₱1,000"},
                { "L03","0","5","Sheets,mattresses & curated pillow menus.Rain showers & deep soaking tubs." +
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
                { "S03","0","5","Clean linens, basic mattress, 1–2 pillows." +
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

        public static void UpdateRoomStatus(string roomCode, ref int availableCtrLuxRom, ref int availableCtrStandRom, bool available)
        {
            string romType = roomCode.ToUpper().Substring(0, 1);
            if (romType == "L")
            {
                for (int i = 0; i < Program.luxuryRooms.GetLength(0); i++)
                {
                    if (Program.luxuryRooms[i, 0].ToUpper() == roomCode.ToUpper())
                    {
                        if (available)
                        {
                            Program.luxuryRooms[i, 1] = "0";
                            availableCtrLuxRom++;
                        }
                        else
                        {
                            Program.luxuryRooms[i, 1] = "1";
                            availableCtrLuxRom--;
                        }

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
                        if (available)
                        {
                            Program.standardRooms[i, 1] = "0";
                            availableCtrStandRom++;
                        }
                        else
                        {
                            Program.standardRooms[i, 1] = "1";
                            availableCtrStandRom--;
                        }

                        break;
                    }
                }
            }
        }

        public static int Index(string roomNumber, string[,] roomType)
        {
            if (roomNumber != "")
            {
                for (int i = 0; i < roomType.GetLength(0); i++)
                {
                    if (roomType[i, 0] == roomNumber.ToString().ToUpper())
                        return i;
                }
            }
            return -1;
        }

        public static bool NotAvailable(string roomNumber, string checkIn, string checkOut, int index = -1)
        {
            bool isExist = false;
            if (Program.tenants.GetLength(0) > 0)
            {
                for (int i = 0; i < Program.tenants.GetLength(0); i++)
                {

                    DateTime _date1 = DateTime.Parse(checkIn);
                    DateTime _date2 = DateTime.Parse(checkOut);

                    DateTime dataDate1 = DateTime.Parse(Program.tenants[i, 6]);
                    DateTime dataDate2 = DateTime.Parse(Program.tenants[i, 7]);
                    if (roomNumber.ToUpper() == Program.tenants[i, 4].ToUpper() &&
                    ((_date1 >= dataDate1 && _date1 < dataDate2) ||
                    (_date1 <= dataDate1 && _date1 < dataDate2)))
                    {
                        if (index == -1)
                        {
                            isExist = true;
                            break;
                        }
                        else if (index > -1 && i != index)
                        {
                            isExist = true;
                            break;
                        }

                    }


                }
            }

            return isExist;
        }

    }
}