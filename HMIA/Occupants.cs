using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIA
{
    internal class Occupants
    {
        public static void AddNew(ref string[,] tenants, string[] addTenants)
        {
            int row = tenants.GetLength(0);
            int col = tenants.GetLength(1);

            string[,] newTenants = new string[row + 1, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    newTenants[i, j] = tenants[i, j];
                }
            }

            for (int k = 0; k < col; k++)
            {
                newTenants[row, k] = addTenants[k];
            }

            tenants = newTenants;
        }

        public static string[,] CheckOUT(string roomNumber)
        {
            int row = Program.tenants.GetLength(0);
            string[,] nwTenants = new string[row - 1, 10];
            for (int i=0,idx=0;i<row;i++)
            {
                if (Program.tenants[i,4].ToUpper() != roomNumber.ToUpper())
                {
                    for(int k=0; k<10; k++)
                    {
                        nwTenants[idx, k] = Program.tenants[i, k];
                    }
                    
                }
                idx++;
            }
            return nwTenants;
        }

        public static void Receipt(string fname,string lname,string room,int pax,
                                string checkIn,string checkOut,int duration,float payment,char process)
        {
            string dash = string.Concat(Enumerable.Repeat("-", 40));
            float totalAmount = Program.GetAmountToPay(room,duration,pax);
            float excessAmount = 0;
            float perNightRate = 0;
            float paxExcessRate = 0;
            int p = 0;
            
            

            int index = Room.Index(room,Program.luxuryRooms);
            if (index >-1)
            {
                int capacity = int.Parse(Program.luxuryRooms[index, 2]);
                perNightRate = float.Parse(Program.luxuryRooms[index, 4].ToString().Replace("₱", "").Replace(",", ""));
                paxExcessRate = float.Parse(Program.luxuryRooms[index, 5].ToString().Replace("₱", "").Replace(",", ""));

                p = pax;
                if (pax > capacity) 
                {
                    p = pax - capacity;
                    excessAmount = p * paxExcessRate;
                } 
                

            }
            else
            {
                index = Room.Index(room, Program.standardRooms);
                if (index > -1)
                {
                    int capacity = int.Parse(Program.standardRooms[index, 2]);
                    perNightRate = float.Parse(Program.standardRooms[index, 4].ToString().Replace("₱", "").Replace(",", ""));
                    paxExcessRate = float.Parse(Program.standardRooms[index, 5].ToString().Replace("₱", "").Replace(",", ""));

                    if (pax > capacity)
                    {
                        p = pax - capacity;
                        excessAmount = p * paxExcessRate;
                    }
                }
                    
            }
            

            if(index > -1)
            {
                
                Console.WriteLine("\t┌{0,-39}┐", dash);
                Console.WriteLine("\t|\t  LAAR's HOTEL!\t\t\t |");
                Console.WriteLine("\t|\tTRANSACTION RECEIPT.\t\t |");
                Console.WriteLine("\t|{0,-40}|", dash);
                Console.WriteLine("\t|{0,-40}|", "Name: " + fname + " " + lname);
                Console.WriteLine("\t|{0,-40}|", "Room: " + room.ToUpper());
                Console.WriteLine("\t|{0,-40}|", "Number of Pax: " + pax);
                Console.WriteLine("\t|{0,-40}|", "Check In: " + checkIn);
                Console.WriteLine("\t|{0,-40}|", "Check Out: " + checkOut);
                Console.WriteLine("\t|{0,-40}|", "Excess pax: " + excessAmount.ToString("N0"));
                Console.WriteLine("\t|{0,-40}|", "Room per night: " + perNightRate.ToString("N0"));
                Console.WriteLine("\t|{0,-40}|", "Days stay: " + duration.ToString("N0"));
                Console.WriteLine("\t|{0,-39}|", dash);
                Console.WriteLine("\t|{0,-40}|", "Total:" + totalAmount.ToString("N0"));
                Console.WriteLine("\t|{0,-40}|", "Paid :" + payment.ToString("N0"));
                if(process != '1')
                    Console.WriteLine("\t|{0,-40}|", "Change : " + (payment - totalAmount).ToString("N0"));
                Console.WriteLine("\t└{0,-39}┘",dash );
            }

        }

        public static bool BookExist(string fname,string lname,string checkIn,string checkOut,int index = -1)
        {
            bool isExist = false;
            if (Program.tenants.GetLength(0)>0)
            {
                for (int i=0;i<Program.tenants.GetLength(0);i++)
                {
                    
                    if (fname.ToUpper() == Program.tenants[i, 2].ToUpper() && lname.ToUpper() == Program.tenants[i, 3].ToUpper() &&
                    checkIn == Program.tenants[i, 6] && checkOut == Program.tenants[i, 7])
                    {
                        if(index == -1)
                        {
                            isExist = true;
                            break;
                        }else if (index > -1 && i !=  index)
                        {
                            isExist = true;
                            break;
                        }
                        
                    }
                    
                    
                }
            }

            return isExist;
        }

        public static void ViewInfo(char role,string searchBy = "", int index = 0)
        {
            string dash = "";
            if (Program.tenants.GetLength(0) > 0)
            {
                dash = (role == '1') ? String.Concat(Enumerable.Repeat("-", 118)) : String.Concat(Enumerable.Repeat("-", 92));
                Console.WriteLine("\n\t┌{0}┐", dash);
                if (role == '1')
                {
                    Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}{7,-14}{8,-12}|",
                        "NAME", "ROOM_CODE", "PAXS", "CHECK-IN", "CHECK-OUT", "PAID","TOTAL","PROCESS_TYPE","ROLE");
                    
                }
                else
                {
                    Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}|",
                        "NAME", "ROOM_CODE", "PAXS", "CHECK-IN", "CHECK-OUT", "PAID","TOTAL");
                    
                }
                Console.WriteLine("\t|{0}|",dash);

                for (int i = 0; i < Program.tenants.GetLength(0); i++)
                {
                    if (searchBy == "" && index == 0)
                    {
                        if (role == '1')
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}{7,-14}{8,-12}|", 
                                Program.tenants[i, 2] + " " + Program.tenants[i, 3], Program.tenants[i, 4],
                                Program.tenants[i, 5], Program.tenants[i, 6], Program.tenants[i, 7],
                                "₱" + Program.tenants[i, 8], "₱" + Program.tenants[i, 9],
                                Program.tenants[i, 1], Program.tenants[i, 0]);
                        }
                        else
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}|",
                                Program.tenants[i, 2] + " " + Program.tenants[i, 3], Program.tenants[i, 4],
                                Program.tenants[i, 5], Program.tenants[i, 6], Program.tenants[i, 7],
                                "₱" + Program.tenants[i, 8], "₱" + Program.tenants[i, 9]);
                        }

                        if (i != Program.tenants.GetLength(0) - 1)
                            Console.WriteLine("\t|{0}|",dash);
                    }
                    else
                    {
                        if (role == '1')
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}{7,-14}{8,-12}|",
                                Program.tenants[index, 2] + " " + Program.tenants[index, 3], Program.tenants[index, 4],
                                Program.tenants[index, 5], Program.tenants[index, 6], Program.tenants[index, 7],
                                "₱" + Program.tenants[index, 8], "₱" + Program.tenants[index, 9],
                                Program.tenants[index, 1], Program.tenants[index, 0]);
                        }
                        else
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}|",
                                Program.tenants[index, 2] + " " + Program.tenants[index, 3], Program.tenants[index, 4],
                                Program.tenants[index, 5], Program.tenants[index, 6], Program.tenants[index, 7],
                                "₱" + Program.tenants[index, 8], "₱" + Program.tenants[i, 9]);
                        }

                        break;
                    }

                }
                Console.WriteLine("\t└{0}┘\n", dash);
            }
            else
            {
                dash = string.Concat(Enumerable.Repeat("-",15));
                Console.WriteLine("\n\t\t\t┌{0}┐", dash);
                Console.WriteLine("\t\t\t|{0,-15}|", "NO OCCUPANTS.");
                Console.WriteLine("\t\t\t└{0}┘\n", dash);
            }
            
        }
    }
}
