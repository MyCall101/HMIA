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
                Console.WriteLine("\t|{0,-39}|", dash);
                Console.WriteLine("\t|Name: {0,-34}|", fname + " " + lname);
                Console.WriteLine("\t|Room: {0,-34}|", room.ToUpper());
                Console.WriteLine("\t|Number of Pax: {0,-25}|", pax);
                Console.WriteLine("\t|Check In: {0,-30}|", checkIn);
                Console.WriteLine("\t|Check Out: {0,-29}|", checkOut);
                Console.WriteLine("\t|Excess pax: {0,-28}|", excessAmount.ToString("N0"));
                Console.WriteLine("\t|Room per night: {0,-24}|", perNightRate.ToString("N0"));
                Console.WriteLine("\t|Days stay: {0,-29}|", duration.ToString("N0"));
                Console.WriteLine("\t|{0,-39}|", dash);
                Console.WriteLine("\t|Total : {0,-25}|", totalAmount.ToString("N0"));
                Console.WriteLine("\t|Paid : {0,-27}|", payment.ToString("N0"));
                if(process != '1')
                    Console.WriteLine("\t|Change : {0,-24}|", (payment - totalAmount).ToString("N0"));
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

            string dash = (role == '1')? String.Concat(Enumerable.Repeat("-",108)): String.Concat(Enumerable.Repeat("-", 82));
            Console.WriteLine("\n\t┌{0}┐",dash);
            if (Program.tenants.GetLength(0) > 0)
            {
                if (role == '1')
                {
                    Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-14}{7,-12}|",
                        "NAME", "ROOM_CODE", "PAXS", "CHECK-IN", "CHECK-OUT", "PAYMENT", "PROCESS_TYPE","ROLE");
                    
                }
                else
                {
                    Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}|",
                        "NAME", "ROOM_CODE", "PAXS", "CHECK-IN", "CHECK-OUT", "PAYMENT");
                    
                }
                Console.WriteLine("\t|{0}|",dash);

                for (int i = 0; i < Program.tenants.GetLength(0); i++)
                {
                    if (searchBy == "" && index == 0)
                    {
                        if (role == '1')
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-14}{7,-12}|", 
                                Program.tenants[i, 2] + " " + Program.tenants[i, 3], Program.tenants[i, 4],
                                Program.tenants[i, 5], Program.tenants[i, 6], Program.tenants[i, 7],
                                Program.tenants[i, 8],Program.tenants[i, 1], Program.tenants[i, 0]);
                        }
                        else
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}|",
                                Program.tenants[i, 2] + " " + Program.tenants[i, 3], Program.tenants[i, 4],
                                Program.tenants[i, 5], Program.tenants[i, 6], Program.tenants[i, 7],
                                Program.tenants[i, 8]);
                        }

                        if (i != Program.tenants.GetLength(0) - 1)
                            Console.WriteLine("\t|{0}|",dash);
                    }
                    else
                    {
                        if (role == '1')
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-14}{7,-12}|",
                                Program.tenants[index, 2] + " " + Program.tenants[index, 3], Program.tenants[index, 4],
                                Program.tenants[index, 5], Program.tenants[index, 6], Program.tenants[index, 7],
                                Program.tenants[index, 8],Program.tenants[index, 1], Program.tenants[index, 0]);
                        }
                        else
                        {
                            Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}|",
                                Program.tenants[index, 2] + " " + Program.tenants[index, 3], Program.tenants[index, 4],
                                Program.tenants[index, 5], Program.tenants[index, 6], Program.tenants[index, 7],
                                Program.tenants[index, 8]);
                        }

                        break;
                    }

                }
            }
            else
            {
                Console.WriteLine("\t|\t\t\t\t\t\t\tNO OCCUPANTS.\t\t\t\t\t\t\t   |");
            }
            Console.WriteLine("\t└{0}┘\n",dash);
        }
    }
}
