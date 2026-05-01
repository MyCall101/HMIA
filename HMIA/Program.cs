using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Lifetime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HMIA
{
    internal class Program
    {

        // Hourly Fix Rates
        public const float LUXTHREEHRSRATE = 500;
        public const float LUXFIVEHRSRATE = 800;
        public const float LUXTWELVEHRSRATE = 1200;

        public const float STANDTHREEHRSRATE = 350;
        public const float STANDFIVEHRSRATE = 650;
        public const float STANDTWELVEHRSRATE = 1000;

        // Variables
        public static string[,] luxuryRooms = new string[,] { };
        public static string[,] standardRooms = new string[,] { };
        public static string[,] tenants = new string[0, 9];
        public static int availableCtrLuxRom = 0;
        public static int availableCtrStandRom = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Room.Sets(ref luxuryRooms, ref standardRooms);
            DisplayWelcome();
            DeterminedUser();
            //DisplayMenu(ref tenants, ref luxuryRooms, ref standardRooms);

        }


        // **************** VOID METHODS ****************

        static void DeterminedUser()
        {
            string dash = String.Concat(Enumerable.Repeat("-",24));
            Console.WriteLine("\t\t\t┌{0}┐",dash);
            Console.WriteLine("\t\t\t|  [1] - FRONT DESK\t |");
            Console.WriteLine("\t\t\t|  [2] - GUEST\t\t |");
            Console.WriteLine("\t\t\t└{0}┘\n",dash);
            char role = DynamicInputs<char>("\n\tPlease Identify your self: ",0);
            if(role == '1' || role == '2')
            {
                DisplayMenu(ref tenants, ref luxuryRooms, ref standardRooms, role);
            }

        }

        static void DisplayMenu(ref string[,] tenants, ref string[,] luxuryRooms, ref string[,] standardRooms,char role)
        {
            string dash = String.Concat(Enumerable.Repeat("-",32));
            bool start = true;
            do
            {

                Console.WriteLine("\t\t\t┌{0}┐",dash);
                Console.WriteLine("\t\t\t|  [1] - RESERVE A ROOM\t\t |");
                if(role == '2') Console.WriteLine("\t\t\t|  [2] - MAIN MENU\t\t |");
                if (role == '1')
                {
                    Console.WriteLine("\t\t\t|  [2] - WALK IN\t\t |");
                    Console.WriteLine("\t\t\t|  [3] - SEARCH\t\t\t |");
                    Console.WriteLine("\t\t\t|  [4] - VIEW ALL OCCUPANTS\t |");
                    Console.WriteLine("\t\t\t|  [5] - VIEW ROOMS AVAILBLE\t |");
                    Console.WriteLine("\t\t\t|  [6] - MAIN MENU\t\t |");
                }
                

                Console.WriteLine("\t\t\t└{0}┘\n",dash);
                char process = DynamicInputs<char>("\tPlease choose process: ", 0);

                if (role == '1' || role == '2')
                {
                    if (process == '1' || (role == '1' && process=='2'))
                    {
                        //Room.DisplayAvailableRooms(ref availableCtrLuxRom, ref availableCtrStandRom);
                        //if (availableCtrLuxRom > 0 || availableCtrStandRom > 0)
                        //{
                            //Room.DisplayHrsRates();
                            Process(role,ref tenants, process);
                        //}
                    }
                    if (role == '1')
                    {
                        if (process == '3')
                        {

                        }
                        else if (process == '4')
                        {

                        }
                        else if (process == '5')
                        {

                        }
                        else if (process == '6')
                        {

                        }
                    }
                    if (role == '2' && process == '2')
                    {
                        //back to identify yourself
                    }
                    
                }
                else
                {

                }

                    switch (process)
                    {
                        case '1':
                        case '2':
                            Room.DisplayAvailableRooms(ref availableCtrLuxRom, ref availableCtrStandRom);
                            if (availableCtrLuxRom > 0 || availableCtrStandRom > 0)
                            {
                                Room.DisplayHrsRates();
                                Process(ref tenants, process);
                            }

                            break;
                        case '3':
                            SearchMethod();
                            break;
                        case '4':
                            Occupants.ViewInfo();
                            break;

                        case '5':

                            break;
                        case '6':
                            Console.WriteLine("\n\t->System Exited.");
                            start = false;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("\n\t->Message: Invalid action.\n");
                            break;
                    }
            } while (start);

        }
        
        static void SearchMethod()
        {
            bool isSearch = true;
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t┌------------------------┐");
                Console.WriteLine("\t\t\t|\t[O] - OCCUPANT\t |");
                Console.WriteLine("\t\t\t|\t[R] - ROOM\t |");
                Console.WriteLine("\t\t\t|\t[X] - EXIT\t |");
                Console.WriteLine("\t\t\t└------------------------┘");
                Console.Write("\n\tPlease choose search by:");
                string choose = Console.ReadLine().ToUpper();
                switch (choose)
                {
                    case "O":
                        Console.Write("\n\tPlease enter Firstname or Lastname: ");
                        string name = Console.ReadLine();
                        SearchBy(choose, name);
                        break;
                    case "R":
                        Console.Write("\n\tPlease enter the Room Number: ");
                        string number = Console.ReadLine();
                        SearchBy(choose, number);
                        break;
                    case "X":
                        isSearch = false;
                        break;
                    default:
                        Console.WriteLine("\n\t->Invalid choice.!");
                        break;
                }
            } while (isSearch);
            
        }

        static void SearchBy(string by,string value)
        {
            bool found = false;
            if (tenants.GetLength(0)>0)
            {
                for (int i=0;i<tenants.GetLength(0);i++)
                {
                    if(by == "O")
                    {
                        if (value.ToUpper() == tenants[i, 0].ToUpper() || value.ToUpper() == tenants[i, 1].ToUpper())
                        {
                            Occupants.ViewInfo(by,i);
                            found = true;
                            break;
                        }
                    }else if (by == "R")
                    {
                        if (value == tenants[i,3])
                        {
                            Occupants.ViewInfo(by, i);
                            found = true;
                            break;
                        }
                        
                    }
                    
                }
                if(!found)
                    Console.WriteLine("\n\t No Record Found.");
            }
            else{
                Console.WriteLine("\n\t No Data.");
            }
        }

        

        static void Process(char role,ref string[,] tenants,char process)
        {
            string[] addTenants;
            string fname = DynamicInputs<string>("\n\tPlease enter your Firstname: ",1);

            string lname = DynamicInputs<string>("\n\tPlease enter your Lastname: ",2);

            Console.WriteLine("\t\t\t┌------------------------┐");
            Console.WriteLine("\t\t\t|\tROOM TYPE\t |");
            Console.WriteLine("\t\t\t|------------------------|");
            Console.WriteLine("\t\t\t|\t[L] - LUXURY\t |");
            Console.WriteLine("\t\t\t|\t[S] - STANDARD\t |");
            Console.WriteLine("\t\t\t└------------------------┘");

            char roomType = DynamicInputs<char>("\n\tPlease pick room type: ",3);
            int roomNumber = DynamicInputs<int>("\n\tPlease pick room number: ",4);
            int hours = DynamicInputs<int>("\n\tPlease enter how many hours to avail: ",5);
            string date = DynamicInputs<string>("\n\tPlease input date: ",6);
            string checkIn = DynamicInputs<string>("\n\tPlease input check-in time: ",7);
            string processType = process.ToString();
            string payment = "";
            if (char.ToUpper(roomType)=='L')
            {
                payment = DynamicInputs<string>("\n\tEnter the half amount rate(downpayment): ",9);
            }
            else
            {
                payment = DynamicInputs<string>("\n\tEnter the amount rate: ",9);
            }

            addTenants = new string[] { fname, lname,ConvertRoomType(roomType.ToString()), roomNumber.ToString(), hours.ToString(), date, checkIn, ConvertProcessType(processType), payment };

            Occupants.AddNew(ref tenants, addTenants);
            Room.UpdateRoomStatus(roomType, roomNumber);
            Room.DisplayAvailableRooms(ref availableCtrLuxRom, ref availableCtrStandRom);
            

        }
        
        static void AskAgain()
        {
            do
            {

            } while (true);
        }
        static void DisplayWelcome()
        {
            Console.WriteLine("\t┌=======================================================┐");
            Console.WriteLine("\t|\t\t  WELCOME TO LAAR's HOTEL!\t\t|");
            Console.WriteLine("\t|-------------------------------------------------------|");
            Console.WriteLine("\t|\t\tWhere comfort feels like home.\t\t|");
            Console.WriteLine("\t└=======================================================┘\n");
        }

        // **************** RETURN METHODS ****************

        static string ConvertProcessType(string processType)
        {
            string _type = "";
            if (processType == "1")
            {
                _type = "RESERVE ROOM";
            }else if (processType == "2")
            {
                _type = "WALK IN";
            }
            return _type;
        }

        static string ConvertRoomType(string romType)
        {
            string rmType = "";
            if (romType.ToUpper() == "L")
            {
                rmType = "LUXURY";
            }
            else if (romType.ToUpper() == "S")
            {
                rmType = "STANDARD";
            }
            return rmType;
        }
        static T DynamicInputs<T>(string prompt,int fieldNumber)
        {
            
            while (true)
            {
                bool isValid = true;
                Console.Write(prompt);
                string input = Console.ReadLine();
                try
                {
                    if (typeof(T) == typeof(int)) //if type is integer you this one will execute
                    {
                        int _int;
                        // tryparse validating the input
                        if (int.TryParse(input, out _int))
                        {

                            // Validation start here for strings


                            return (T)(object)_int;
                        }
                        else
                        {
                            // return the exception the new message
                            throw new Exception("Invalid Integer.");
                        }

                    }
                    else if (typeof(T) == typeof(double) || typeof(T) == typeof(decimal) ||
                            typeof(T) == typeof(float))
                    {

                        bool success = false;
                        // this is object root parent of all types
                        object result = null;

                        if (typeof(T) == typeof(double))
                        {
                            double _double;
                            // this one return bool true or false & assigning result value
                            success = double.TryParse(input, out _double) && (result = _double) != null;
                        }
                        else if (typeof(T) == typeof(decimal))
                        {
                            decimal _decimal;
                            success = decimal.TryParse(input, out _decimal) && (result = _decimal) != null;
                        }
                        else if (typeof(T) == typeof(float))
                        {
                            float _float;
                            success = float.TryParse(input, out _float) && (result = _float) != null;
                        }

                        if (success)
                        {

                            // Validation start here for strings


                            // if success return result
                            return (T)result;
                        }
                        else
                        {
                            throw new Exception("Invalid decimal number.");
                        }

                    }
                    else if (typeof(T) == typeof(char))
                    {
                        char _char;
                        if (char.TryParse(input, out _char))
                        {
                            // // Validation start here for strings
                            Validate.InputCharacter(ref isValid, _char.ToString(), "ROLE", fieldNumber, 0, new string[] { "1", "2" });
                            Validate.InputCharacter(ref isValid, _char.ToString(), "ROLE", fieldNumber, 1, 
                                new string[] { "1", "2", "3", "4", "5", "6" });

                            if (isValid) return (T)(object)_char;
                        }
                        else
                        {
                            throw new Exception("Invalid input.");
                        }
                    }
                    else
                    {
                        // Validation start here for strings
                        Validate.InputLength( ref isValid,input, 4, 15, "Firstname",fieldNumber, 1);
                        Validate.InputLength(ref isValid, input, 4, 15, "Lastname", fieldNumber, 2);

                        // don't forget the condition isValid
                        if (isValid) return (T)(object)input;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n\t-> " + e.Message + "\n");
                }
            }
        }
    }
}
