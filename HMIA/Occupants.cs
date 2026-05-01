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
