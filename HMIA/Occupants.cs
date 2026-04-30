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
        public static void ViewInfo(string searchBy = "", int index = 0)
        {
            

            Console.WriteLine("\n\t┌-----------------------------------------------------------------------------------------------------------------┐");
            if (Program.tenants.GetLength(0) > 0)
            {
                Console.WriteLine($"\t|{"Name",-30}{"ROOM_TYPE",-15}{"ROOM #",-9}{"HOURS",-7}{"DATE",-12}{"CHECK-IN",-10}" +
                    $"{"PROCESS_TYPE",-14}{"PAYMENT",-9}{"BALANCE",-7}|");
                Console.WriteLine("\t|-----------------------------------------------------------------------------------------------------------------|");

                for (int i = 0; i < Program.tenants.GetLength(0); i++)
                {


                    if (searchBy == "" && index == 0)
                    {
                        string processType = Program.tenants[i, 7];
                        int payment = Convert.ToInt32(Program.tenants[i, 8]);
                        string balance = (processType == "RESERVE ROOM" ? ((payment * 2) - payment).ToString() : "PAID");
                        Console.WriteLine($"\t|{Program.tenants[i, 0] + " " + Program.tenants[i, 1],-30}{Program.tenants[i, 2],-15}" +
                            $"{Program.tenants[i, 3],-9}{Program.tenants[i, 4],-7}{Program.tenants[i, 5],-12}{Program.tenants[i, 6],-10}{processType,-14}" +
                            $"{payment.ToString(),-9}{balance,-7}|");

                        if (i != Program.tenants.GetLength(0) - 1)
                            Console.WriteLine("\t|-----------------------------------------------------------------------------------------------------------------|");
                    }
                    else
                    {
                        string processType = Program.tenants[index, 7];
                        int payment = Convert.ToInt32(Program.tenants[index, 8]);
                        string balance = (processType == "RESERVE ROOM" ? ((payment * 2) - payment).ToString() : "PAID");

                        Console.WriteLine($"\t|{Program.tenants[index, 0] + " " + Program.tenants[index, 1],-30}{Program.tenants[index, 2],-15}" +
                            $"{Program.tenants[index, 3],-9}{Program.tenants[index, 4],-7}{Program.tenants[index, 5],-12}{Program.tenants[index, 6],-10}{processType,-14}" +
                            $"{payment.ToString(),-9}{balance,-7}|");

                        break;
                    }

                }
            }
            else
            {
                Console.WriteLine("\t|\t\t\t\t\t\t\tNO OCCUPANTS.\t\t\t\t\t\t\t   |");
            }
            Console.WriteLine("\t└-----------------------------------------------------------------------------------------------------------------┘\n");
        }
    }
}
