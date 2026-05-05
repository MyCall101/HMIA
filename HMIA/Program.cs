using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
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

        // PER NIGHT RATE
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
        public static int availableCtrLuxRom = 3;
        public static int availableCtrStandRom = 3;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //Room.Sets(ref luxuryRooms, ref standardRooms);
            Room.SetDesigned(ref luxuryRooms, ref standardRooms);
            
            DeterminedUser();
            //DisplayMenu(ref tenants, ref luxuryRooms, ref standardRooms);

        }


        // **************** VOID METHODS ****************

        static void DeterminedUser()
        {
            Console.Clear();
            DisplayWelcome();
            string dash = String.Concat(Enumerable.Repeat("-",24));
            Console.WriteLine("\t\t\t┌{0}┐",dash);
            Console.WriteLine("\t\t\t|  [1] - FRONT DESK\t |");
            Console.WriteLine("\t\t\t|  [2] - GUEST\t\t |");
            Console.WriteLine("\t\t\t|  [3] - EXIT\t\t |");
            Console.WriteLine("\t\t\t└{0}┘\n",dash);
            char role = DynamicInputs<char>("\n\tPlease Identify your self: ",0);
            if(role == '1' || role == '2')
            {
                Console.Clear();
                DisplayMenu(ref tenants, ref luxuryRooms, ref standardRooms, role);
            }
            else if (role == '3')
            {
                Console.WriteLine("\n\tExiting system...\n");
                Environment.Exit(0);
            }

        }

        static void DisplayMenu(ref string[,] tenants, ref string[,] luxuryRooms, ref string[,] standardRooms,char role)
        {
            string dash = String.Concat(Enumerable.Repeat("-",32));
            bool start = true;
            do
            {

                Console.WriteLine("\n\t\t\t┌{0}┐",dash);
                Console.WriteLine("\t\t\t|  [1] - RESERVE ROOM\t\t |");
                if(role == '2') Console.WriteLine("\t\t\t|  [2] - MAIN MENU\t\t |");

                if (role == '1')
                {
                    Console.WriteLine("\t\t\t|  [2] - WALK IN\t\t |");
                    Console.WriteLine("\t\t\t|  [3] - SEARCH\t\t\t |");
                    Console.WriteLine("\t\t\t|  [4] - VIEW ALL OCCUPANTS\t |");
                    Console.WriteLine("\t\t\t|  [5] - VIEW ROOMS AVAILBLE\t |");
                    Console.WriteLine("\t\t\t|  [6] - CHECK-OUT\t\t |");
                    Console.WriteLine("\t\t\t|  [7] - MAIN MENU\t\t |");
                }
                

                Console.WriteLine("\t\t\t└{0}┘\n",dash);
                char process = DynamicInputs<char>("\tPlease choose process: ", 1);
                Console.WriteLine("Process : " + process);

                if (role == '1' || role == '2')
                {
                    Console.Clear();
                    if (process == '1' || (role == '1' && process=='2'))
                    {
                        
                        Room.DisplayAvailableRooms();
                        if (availableCtrLuxRom > 0 || availableCtrStandRom > 0)
                        {
                            Process(role,ref tenants, process);
                            
                        }
                    }
                    if (role == '1')
                    {
                        if (process == '3')
                        {
                            // SEARCH
                            SearchMethod(ref tenants, ref luxuryRooms, ref standardRooms, role);
                        }
                        else if (process == '4')
                        {
                            //VIEW ALL OCCUPANTS
                        }
                        else if (process == '5')
                        {
                            //VIEW AVAILABLE ROOM
                        }
                        else if (process == '6')
                        {
                            //CHECK OUT
                        }
                    }
                    if ((role == '2' && process == '2') || (role == '1' && process == '7'))
                    {
                        start = false;
                        //back to identify yourself
                        DeterminedUser();
                        
                    }
                    
                }
            } while (start);

        }
        
        static void SearchMethod(ref string[,] tenants, ref string[,] luxuryRooms, ref string[,] standardRooms, char role)
        {
            bool isSearch = true;
            do
            {
                
                Console.WriteLine("\t\t\t┌------------------------┐");
                Console.WriteLine("\t\t\t|[O] - OCCUPANTS\t |");
                Console.WriteLine("\t\t\t|[R] - ROOM CODE\t |");
                Console.WriteLine("\t\t\t|[D] - DATES\t\t |");
                Console.WriteLine("\t\t\t|[X] - BACK TO PROCESS   |");
                Console.WriteLine("\t\t\t└------------------------┘");
                char choose = DynamicInputs<char>("\tPlease choose search by: ",10); //Console.ReadLine().ToUpper();
                if (char.ToUpper(choose) == 'O')
                {
                    Console.Write("\n\tPlease enter Firstname or Lastname: ");
                    string name = Console.ReadLine();
                    SearchBy(choose, name,role);
                }else if (char.ToUpper(choose) == 'R')
                {
                    Console.Write("\n\tPlease enter the Room Code: ");
                    string number = Console.ReadLine();
                    SearchBy(choose, number,role);
                }
                else if (char.ToUpper(choose) == 'D')
                {
                    Console.Write("\n\tCheck In date: ");
                    string date1 = Console.ReadLine();
                    Console.Write("\n\tCheck Out date: ");
                    string date2 = Console.ReadLine();
                    SearchBy(choose, date1,date2, role);
                }
                else if (char.ToUpper(choose) == 'X')
                {
                    Console.Clear();
                    isSearch = false;
                    DisplayMenu(ref tenants, ref luxuryRooms, ref standardRooms, role);
                }
                
            } while (isSearch);
            
        }

        static void SearchBy(char by, string value, string value2, char role)
        {
            bool found = false;
            if (tenants.GetLength(0) > 0)
            {
                for (int i = 0; i < tenants.GetLength(0); i++)
                {
                    if (by.ToString().ToUpper() == "D")
                    {
                        DateTime _date1, _date2,_checkin,_checkout;
                        bool valid1 = DateTime.TryParseExact(value,"MM-dd-yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None ,out _date1);
                        bool valid2 = DateTime.TryParseExact(value2, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _date2);
                        bool valid3 = DateTime.TryParseExact(tenants[i, 6], "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _checkin);
                        bool valid4 = DateTime.TryParseExact(tenants[i, 7], "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _checkout);
                        if (valid1 && valid2 && valid3 && valid4) {
                            if (_checkin == _date1 && _checkout == _date2)
                            {
                                Occupants.ViewInfo(role, by.ToString(), i);
                                found = true;
                                break;
                            }
                        }
                        
                    }
                }
                if (!found)
                    Console.WriteLine("\n\t No Record Found.");
            }
            else
            {
                Console.WriteLine("\n\t No Data.");
            }
        }

        static void SearchBy(char by,string value,char role)
        {
            bool found = false;
            if (tenants.GetLength(0)>0)
            {
                for (int i=0;i<tenants.GetLength(0);i++)
                {
                    if(by.ToString().ToUpper() == "O")
                    {
                        if (value.ToUpper() == tenants[i, 2].ToUpper() || value.ToUpper() == tenants[i, 3].ToUpper())
                        {
                            Occupants.ViewInfo(role,by.ToString(),i);
                            found = true;
                            break;
                        }
                    }else if (by.ToString().ToUpper() == "R")
                    {
                        if (value.ToUpper() == tenants[i,4].ToUpper())
                        {
                            Occupants.ViewInfo(role,by.ToString(), i);
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
            bool start = true;
            do
            {
                string[] addTenants;
                string fname = DynamicInputs<string>("\n\tPlease enter your Firstname: ", 2);
                string lname = DynamicInputs<string>("\n\tPlease enter your Lastname: ", 3);
                string roomNumber = DynamicInputs<string>("\n\tPlease choose room number: ", 4);
                int pax = DynamicInputs<int>("\n\tHow many pax: ", 5);
                string checkIn = DynamicInputs<string>("\n\tCheck-In (MM-dd-yyyy): ", 6);
                string checkOut = DynamicInputs<string>("\n\tCheck-Out (MM-dd-yyyy): ", 7);
                float payment = DynamicInputs<float>("\n\tPayment: ", 8);
                char confirm = DynamicInputs<char>("\n\tConfirm reserve booking (Y/N)?: ", 9);

                string dash = string.Concat(Enumerable.Repeat("-",25));
                if (char.ToUpper(confirm) == 'Y')
                {
                    addTenants = new string[] {GetRole(role.ToString()),ProceDescription(process.ToString()), fname, lname,roomNumber.ToUpper(), pax.ToString(), checkIn, checkOut,
                         payment.ToString()};

                    Occupants.AddNew(ref tenants, addTenants);
                    //Console.Clear();
                    //Occupants.ViewInfo(role); // Ven Ecomment ni siya alisdi sa Receipt Transaction
                    Room.UpdateRoomStatus(roomNumber, ref availableCtrLuxRom, ref availableCtrStandRom, false);
                    // dito mo ilagay yong receipt display
                    Console.WriteLine("\n\t\t->Successfully Room Reserved.<-");
                    //Room.DisplayAvailableRooms(ref availableCtrLuxRom, ref availableCtrStandRom);

                    bool isAsking = true;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\n\t\t┌{0}┐", dash);
                        Console.WriteLine("\t\t|[1] - UPDATE BOOKING\t |");
                        Console.WriteLine("\t\t|[2] - BOOK AGAIN\t |");
                        Console.WriteLine("\t\t|[3] - EXIT\t |");
                        Console.WriteLine("\t\t└{0}┘", dash);

                        char choose = DynamicInputs<char>("\n\tChoose action: ", 12);
                        switch (choose)
                        {
                            case '1':
                                UpdateBooking(role, ref tenants);
                                start = false;
                                break;
                            case '2':
                                isAsking = false;
                                break;
                            case '3':
                                start = false;
                                isAsking = false;
                                break;
                        }
                    } while (isAsking);
                    
                }

            } while (start);
            
        }

        static void UpdateBooking(char role, ref string[,] tenants)
        {
            string dash = string.Concat(Enumerable.Repeat("-", 25));
            if (role == '1')
            {

            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\t\t┌{0}┐", dash);
                Console.WriteLine("\t\t|{0,-25}|","UPDATE" );
                Console.WriteLine("\t\t|{0}|", dash);
                Console.WriteLine("\t\t|[1] - FIRST NAME\t |");
                Console.WriteLine("\t\t|[2] - LAST NAME\t |");
                Console.WriteLine("\t\t|[3] - ROOM NUMBER\t |");
                Console.WriteLine("\t\t|[4] - NUMBER OF PAX\t |");
                Console.WriteLine("\t\t|[5] - CHECK-IN & OUT\t |");
                Console.WriteLine("\t\t|[6] - EXIT\t |");
                Console.WriteLine("\t\t└{0}┘", dash);

                int index = tenants.GetLength(0)-1;

                string fname = DynamicInputs<string>("\n\tPlease enter your Firstname: ", 2);
                string lname = DynamicInputs<string>("\n\tPlease enter your Lastname: ", 3);
                string roomNumber = DynamicInputs<string>("\n\tPlease choose room number: ", 4);
                int pax = DynamicInputs<int>("\n\tHow many pax: ", 5);
                string checkIn = DynamicInputs<string>("\n\tCheck-In (MM-dd-yyyy): ", 6);
                string checkOut = DynamicInputs<string>("\n\tCheck-Out (MM-dd-yyyy): ", 7);
                float payment = DynamicInputs<float>("\n\tPayment: ", 8);

                string tempRomNumber = tenants[index, 4];
                Room.UpdateRoomStatus(tempRomNumber, ref availableCtrLuxRom, ref availableCtrStandRom, true);

                tenants[index, 2] = fname;
                tenants[index, 3] = lname;
                tenants[index, 4] = roomNumber;
                tenants[index, 5] = pax.ToString();
                tenants[index, 6] = checkIn;
                tenants[index, 7] = checkOut;
                tenants[index, 8] = payment.ToString();

                Room.UpdateRoomStatus(roomNumber, ref availableCtrLuxRom, ref availableCtrStandRom, false);
                // dito mo ilagay yong receipt display
                Console.WriteLine("\n\t\t->Updated successfully.<-");

            }
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

        static string GetRole(string role)
        {
            string _type = "";
            if (role == "1")
            {
                _type = "FRONT DESK";
            }else if (role == "2")
            {
                _type = "GUEST";
            }
            return _type;
        }

        static string ProceDescription(string process)
        {
            string rmType = "";
            if (process == "1")
            {
                rmType = "RESERVE ROOM";
            }
            else if (process == "2")
            {
                rmType = "WALK IN";
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
                            Validate.InputCharacter(ref isValid, _char.ToString(), "ROLE", fieldNumber, 0, 
                                new string[] { "1", "2","3" });//role menu
                            Validate.InputCharacter(ref isValid, _char.ToString(), "PROCESS", fieldNumber, 1, 
                                new string[] { "1", "2", "3", "4", "5", "6", "7" });//process menu
                            Validate.InputCharacter(ref isValid, _char.ToString(), "CONFIRMATION", fieldNumber, 9,
                                new string[] { "Y", "N"});// confirmation
                            Validate.InputCharacter(ref isValid, _char.ToString(), "SEARCH BY", fieldNumber, 10,
                                new string[] { "O", "R","D", "X"});// search menu

                            Validate.InputCharacter(ref isValid, _char.ToString(), "UPDATE", fieldNumber, 12,
                                new string[] { "1", "2", "3"}); //update booking menu

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
                        Validate.InputLength( ref isValid,input, 4, 15, "FIRSTNAME",fieldNumber, 2);//firstname
                        Validate.InputLength(ref isValid, input, 4, 15, "LASTNAME", fieldNumber, 3);//lastname

                        Validate.InputCharacter(ref isValid, input, "ROOM NUMBER", fieldNumber, 4,//room number
                            new string[] {"L01", "L02" , "L03" , "S01" , "S02" , "S03" });
                        Validate.RoomAvailable(ref isValid, input, "ROOM NUMBER", fieldNumber, 4);//room number

                        Validate.CheckDate(ref isValid, input, "CHECK-IN", fieldNumber, 6);//check in
                        Validate.CheckDate(ref isValid, input, "CHECK-OUT", fieldNumber, 7);//check out

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
