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
        public const float LUXEXCESSPAX = 1000;
        public const float L01RATE = 1500;
        public const float L02RATE = 3000;
        public const float L03RATE = 5000;

        public const float STANDEXCESSPAX = 500;
        public const float S01RATE = 800;
        public const float S02RATE = 1600;
        public const float S03RATE = 3200;

        // Variables
        public static string[,] luxuryRooms = new string[,] { };
        public static string[,] standardRooms = new string[,] { };
        public static string[,] tenants = new string[0, 10];
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
                            Occupants.ViewInfo(role);
                        }
                        else if (process == '5')
                        {
                            //VIEW AVAILABLE ROOM
                            Room.DisplayAvailableRooms();
                        }
                        else if (process == '6')
                        {
                            //CHECK OUT
                            RoomCheckOut();
                            
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

        static void RoomCheckOut()
        {
            string roomNumber = DynamicInputs<string>("\n\tEnter room number: ", 4,"","",true);
            tenants = Occupants.CheckOUT(roomNumber);
            Room.UpdateRoomStatus(roomNumber, ref availableCtrLuxRom, ref availableCtrStandRom,true);
            DisplayFarewell();
            Console.WriteLine("\n\tPress anykey to continue...");
            Console.ReadKey();
        }
        
        static void SearchMethod(ref string[,] tenants, ref string[,] luxuryRooms, ref string[,] standardRooms, char role)
        {
            bool isSearch = true;
            do
            {
                Console.Clear();
                string dash = string.Concat(Enumerable.Repeat("-", 28));
                Console.WriteLine("\t\t\t┌{0}┐",dash);
                Console.WriteLine("\t\t\t|[O] - OCCUPANTS/ROOM NUMBER |");
                //Console.WriteLine("\t\t\t|[R] - ROOM CODE\t |");
                Console.WriteLine("\t\t\t|{0,-28}|", "[D] - DATES");
                Console.WriteLine("\t\t\t|{0,-28}|", "[X] - BACK TO PROCESS");
                Console.WriteLine("\t\t\t└{0}┘",dash);
                char choose = DynamicInputs<char>("\tPlease choose search by: ",10); //Console.ReadLine().ToUpper();
                if (char.ToUpper(choose) == 'O')
                {
                    Console.Write("\n\tPlease enter Firstname or Lastname or Room number: ");
                    string name = Console.ReadLine();
                    SearchBy(choose, name,role);
                    Console.WriteLine("\n\tPress any key to continue...");
                }
                //else if (char.ToUpper(choose) == 'R')
                //{
                //    Console.Write("\n\tPlease enter the Room Code: ");
                //    string number = Console.ReadLine();
                //    SearchBy(choose, number,role);
                //}
                else if (char.ToUpper(choose) == 'D')
                {
                    Console.Write("\n\tCheck In date (MM-dd-yyyy): ");
                    string date1 = Console.ReadLine();
                    Console.Write("\n\tCheck Out date (MM-dd-yyyy): ");
                    string date2 = Console.ReadLine();
                    SearchBy(choose, date1,date2, role);
                }
                else if (char.ToUpper(choose) == 'X')
                {
                    Console.Clear();
                    isSearch = false;
                    DisplayMenu(ref tenants, ref luxuryRooms, ref standardRooms, role);
                }

                Console.ReadKey();

            } while (isSearch);
            
        }

        static void SearchBy(char by, string value, string value2, char role)
        {
            bool found = false;
            if (tenants.GetLength(0) > 0)
            {
                string[,] temp = new string[tenants.GetLength(0), 9];
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
                                //Occupants.ViewInfo(role, by.ToString(), i);
                                temp[i, 0] = tenants[i, 0];
                                temp[i, 1] = tenants[i, 1];
                                temp[i, 2] = tenants[i, 2];
                                temp[i, 3] = tenants[i, 3];
                                temp[i, 4] = tenants[i, 4];
                                temp[i, 5] = tenants[i, 5];
                                temp[i, 6] = tenants[i, 6];
                                temp[i, 7] = tenants[i, 7];
                                temp[i, 8] = tenants[i, 8];
                                temp[i, 9] = tenants[i, 9];
                                found = true;
                                //break;
                            }
                        }
                        
                    }
                }

                if (found)
                {
                    string dash = (role == '1') ? String.Concat(Enumerable.Repeat("-", 118)) : String.Concat(Enumerable.Repeat("-", 92));
                    Console.WriteLine("\n\t┌{0}┐", dash);
                    
                    if (role == '1')
                    {
                        Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}{7,-14}{8,-12}|",
                            "NAME", "ROOM_CODE", "PAXS", "CHECK-IN", "CHECK-OUT", "PAID","TOTAL", "PROCESS_TYPE", "ROLE");

                    }
                    else
                    {
                        Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}|",
                            "NAME", "ROOM_CODE", "PAXS", "CHECK-IN", "CHECK-OUT", "PAID","TOTAL");

                    }
                    Console.WriteLine("\t|{0}|", dash);
                    for (int n=0;n<temp.GetLength(0);n++)
                    {
                        if (temp[n,0]!=null)
                        {
                            if (role == '1')
                            {
                                Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}{7,-14}{8,-12}|",
                                    temp[n, 2] + " " + temp[n, 3], temp[n, 4],
                                    temp[n, 5], temp[n, 6], temp[n, 7],
                                    "₱" + temp[n, 8], "₱" + temp[n, 9], 
                                    temp[n, 1], temp[n, 0]);
                            }
                            else
                            {
                                Console.WriteLine("\t|{0,-30}{1,-11}{2,-7}{3,-12}{4,-12}{5,-10}{6,-10}|",
                                    temp[n, 2] + " " + temp[n, 3], temp[n, 4],
                                    temp[n, 5], temp[n, 6], temp[n, 7],
                                    "₱" + temp[n, 8], "₱" + temp[n, 9]);
                            }

                            if (n != temp.GetLength(0) - 1)
                                Console.WriteLine("\t|{0}|", dash);
                        }
                        
                    }
                    Console.WriteLine("\t└{0}┘\n", dash);
                }
                else
                {
                    Console.WriteLine("\n\t -> No Record Found.");
                }
                    
            }
            else
            {
                Console.WriteLine("\n\t -> No Data.");
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
                        if (value.ToUpper() == tenants[i, 2].ToUpper() || value.ToUpper() == tenants[i, 3].ToUpper() ||
                            value.ToUpper() == tenants[i, 4].ToUpper())
                        {
                            Occupants.ViewInfo(role,by.ToString(),i);
                            found = true;
                            break;
                        }
                    }
                    
                }
                if(!found)
                    Console.WriteLine("\n\t -> No Record Found.");
            }
            else{
                Console.WriteLine("\n\t -> No Data.");
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
                string checkOut = DynamicInputs<string>("\n\tCheck-Out (MM-dd-yyyy): ", 7,checkIn);
                int duration = (DateTime.Parse(checkOut).Subtract(DateTime.Parse(checkIn))).Days;
                float amount = GetAmountToPay(roomNumber, duration, pax);
                Console.WriteLine("\n\tAmount to pay: ₱{0}", amount.ToString("N0"));//F2 = two decimal places, N0 = 1,000
                float payment = DynamicInputs<float>("\n\tPayment: ₱", 8,amount.ToString(),process.ToString());

                char confirm = DynamicInputs<char>("\n\tConfirm reserve booking (Y/N)?: ", 9);

                string dash = string.Concat(Enumerable.Repeat("-",25));
                if (char.ToUpper(confirm) == 'Y')
                {
                    if (!Occupants.BookExist(fname,lname,checkIn,checkOut))
                    {
                        addTenants = new string[] {GetRole(role.ToString()),ProcDescription(process.ToString()), fname, lname,roomNumber.ToUpper(), pax.ToString(), checkIn, checkOut,
                         payment.ToString(),amount.ToString()};

                        Occupants.AddNew(ref tenants, addTenants);
                        //Console.Clear();
                        //Occupants.ViewInfo(role); // Ven Ecomment ni siya alisdi sa Receipt Transaction
                        Room.UpdateRoomStatus(roomNumber, ref availableCtrLuxRom, ref availableCtrStandRom, false);

                        // dito mo ilagay yong receipt display
                        Occupants.Receipt(fname,lname,roomNumber,pax,checkIn,checkOut,duration,payment,process);

                        if(process == '1')
                        {
                            Console.WriteLine("\n\t\t->Reservation confirmed.<-");
                        }
                        else
                        {
                            Console.WriteLine("\n\t\t->Success! You've booked a room.<-");
                        }
                        
                        //Room.DisplayAvailableRooms(ref availableCtrLuxRom, ref availableCtrStandRom);
                        Console.WriteLine("\n\tPress anykey to continue...");
                        Console.ReadKey();

                        bool isAsking = true;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n\t\t┌{0}┐", dash);
                            Console.WriteLine("\t\t|{0,-25}|", "[1] - UPDATE BOOKING");
                            Console.WriteLine("\t\t|{0,-25}|", "[2] - BOOK AGAIN");
                            Console.WriteLine("\t\t|{0,-25}|", "[3] - EXIT");
                            Console.WriteLine("\t\t└{0}┘", dash);

                            char choose = DynamicInputs<char>("\n\tChoose action: ", 12);
                            switch (choose)
                            {
                                case '1':
                                    UpdateBooking(role, ref tenants);
                                    start = false;
                                    break;
                                case '2':
                                    start = true;

                                    if (availableCtrLuxRom > 0 || availableCtrStandRom > 0)
                                    {
                                        isAsking = false;
                                        Room.DisplayAvailableRooms();
                                    }
                                    break;
                                case '3':
                                    start = false;
                                    isAsking = false;
                                    break;
                            }
                        } while (isAsking);
                    }
                    else
                    {
                        Console.WriteLine("\n\t-> Please check, booking already exist.!");
                    }
                    
                }

            } while (start);
            
        }

        static void UpdateBooking(char role, ref string[,] tenants)
        {
            string dash = string.Concat(Enumerable.Repeat("-", 25));
           
            Console.Clear();
            Console.WriteLine("\n\t\t┌{0}┐", dash);
            Console.WriteLine("\t\t|{0,-25}|","UPDATE" );
            Console.WriteLine("\t\t|{0}|", dash);
            Console.WriteLine("\t\t|{0,-25}|", "[1] - FIRST NAME");
            Console.WriteLine("\t\t|{0,-25}|", "[2] - LAST NAME");
            Console.WriteLine("\t\t|{0,-25}|", "[3] - ROOM NUMBER");
            Console.WriteLine("\t\t|{0,-25}|", "[4] - NUMBER OF PAX");
            Console.WriteLine("\t\t|{0,-25}|", "[5] - CHECK-IN & OUT");
            Console.WriteLine("\t\t|{0,-25}|", "[6] - EXIT");
            Console.WriteLine("\t\t└{0}┘", dash);

            char choose = DynamicInputs<char>("\tPlease choice info to update: ", 13);
            int index = tenants.GetLength(0) - 1;
            
            
            string tmpFname = tenants[index, 2];
            string tmpLname = tenants[index, 3];
            string tmpRom = tenants[index, 4];
            string tmpPax = tenants[index, 5];
            string tmpCheckIn = tenants[index, 6];
            string tmpCheckOut = tenants[index, 7];

            char process = GetProcNumber(tenants[index, 1].ToString());
            string fname = tenants[index, 2];
            string lname = tenants[index, 3];
            string roomNumber = tenants[index, 4];
            int pax = int.Parse(tenants[index, 5]);
            string checkIn = tenants[index, 6];
            string checkOut = tenants[index, 7];

            int duration = (DateTime.Parse(checkOut).Subtract(DateTime.Parse(checkIn))).Days;
            float amount = GetAmountToPay(roomNumber, duration, int.Parse(tenants[index, 5]));
            float payment = float.Parse(tenants[index, 8]);

            switch (choose)
            {
                case '1':
                    fname = DynamicInputs<string>("\n\tPlease enter your Firstname: ", 2);
                    //tenants[index, 2] = fname;
                    if (!Occupants.BookExist(fname, lname, checkIn, checkOut, index))
                        Console.WriteLine("\n\t-> Update Firstname from {0} to {1}", tmpFname, fname);
                    break;
                case '2':
                    lname = DynamicInputs<string>("\n\tPlease enter your Lastname: ", 3);
                    //tenants[index, 3] = lname;
                    if (!Occupants.BookExist(fname, lname, checkIn, checkOut, index))
                        Console.WriteLine("\n\t-> Update Lastname from {0} to {1}",tmpLname,lname);
                    break;
                case '3':
                    roomNumber = DynamicInputs<string>("\n\tPlease choose room number: ", 4);
                    //tenants[index, 4] = roomNumber;
                    //string tempRomNumber = tenants[index, 4];

                    amount = GetAmountToPay(roomNumber, duration, int.Parse(tenants[index, 5]));
                    
                    Console.WriteLine("\n\t-> Update Room number from {0} to {1}", tmpRom, roomNumber);
                    if (float.Parse(amount.ToString().Replace(",","")) > float.Parse(tenants[index, 8]))
                    {
                        Console.WriteLine("\n\tAmount to pay: ₱{0}", amount.ToString("N0"));
                        payment = DynamicInputs<float>("\n\tPayment: ₱", 8, amount.ToString(),process.ToString());
                        //tenants[index, 8] = payment.ToString();
                        //Occupants.Receipt(tenants[index, 2], tenants[index, 3], tenants[index, 4], int.Parse(tenants[index, 5]), tenants[index, 6],
                        //        tenants[index, 7], duration, payment);
                    }
                    
                    Room.UpdateRoomStatus(roomNumber, ref availableCtrLuxRom, ref availableCtrStandRom, true);
                    break;
                case '4':
                    pax = DynamicInputs<int>("\n\tHow many pax: ", 5);

                    amount = GetAmountToPay(roomNumber, duration, pax);

                    Console.WriteLine("\n\t-> Update Number of Pax from {0} to {1}", tmpPax, pax);
                    if (float.Parse(amount.ToString().Replace(",", "")) > float.Parse(tenants[index, 8]))
                    {
                        Console.WriteLine("\n\tAmount to pay: ₱{0}", amount.ToString("N0"));
                        payment = DynamicInputs<float>("\n\tPayment: ₱", 8, amount.ToString(), process.ToString());
                        //tenants[index, 8] = payment.ToString();
                        //Occupants.Receipt(tenants[index, 2], tenants[index, 3], tenants[index, 4], int.Parse(tenants[index, 5]), tenants[index, 6],
                        //        tenants[index, 7], duration, payment);
                    }
                        
                    break;
                case '5':
                    checkIn = DynamicInputs<string>("\n\tCheck-In (MM-dd-yyyy): ", 6);
                    checkOut = DynamicInputs<string>("\n\tCheck-Out (MM-dd-yyyy): ", 7, checkIn);

                    duration = (DateTime.Parse(checkOut).Subtract(DateTime.Parse(checkIn))).Days;
                    amount = GetAmountToPay(roomNumber, duration, int.Parse(tenants[index, 5]));

                    Console.WriteLine("\n\t-> Update Check-in from {0} to {1} and Check-out from {2} to {3}", tmpCheckIn, checkIn,tmpCheckOut,checkOut);
                    if (float.Parse(amount.ToString().Replace(",", "")) > float.Parse(tenants[index, 8]))
                    {
                        Console.WriteLine("\n\tAmount to pay: ₱{0}", amount.ToString("N0"));
                        payment = DynamicInputs<float>("\n\tPayment: ₱", 8, amount.ToString(), process.ToString());
                    }
                    break;
                case '6':
                    break;
            }

            if (!Occupants.BookExist(fname, lname, checkIn, checkOut,index))
            {
                tenants[index, 2] = fname;
                tenants[index, 3] = lname;
                tenants[index, 4] = roomNumber;
                tenants[index, 5] = pax.ToString();
                tenants[index, 6] = checkIn;
                tenants[index, 7] = checkOut;
                tenants[index, 8] = payment.ToString();

                Occupants.Receipt(fname, lname, roomNumber, pax, checkIn,
                        checkOut, duration, payment,process);

                Room.UpdateRoomStatus(roomNumber, ref availableCtrLuxRom, ref availableCtrStandRom, false);
                // dito mo ilagay yong receipt display
                
                Console.WriteLine("\n\t\t->Updated successfully.<-");
            }
            else
            {
                Console.WriteLine("\n\t-> Please check, booking already exist.!");
            }
            Console.WriteLine("\n\tPress anykey to continue...");
            Console.ReadKey();

        }
        
        public static void DisplayWelcome()
        {
            string dash = string.Concat(Enumerable.Repeat("-",55));
            Console.WriteLine("\t┌{0}┐",dash);
            Console.WriteLine("\t|\t\t  WELCOME TO LAAR's HOTEL!\t\t|");
            Console.WriteLine("\t|-------------------------------------------------------|");
            Console.WriteLine("\t|\t\tWhere comfort feels like home.\t\t|");
            Console.WriteLine("\t└{0}┘\n",dash);
        }
        public static void DisplayFarewell()
        {
            string dash = string.Concat(Enumerable.Repeat("-", 55));
            Console.WriteLine("\t┌{0}┐", dash);
            Console.WriteLine("\t|\t\t\t  THANK YOU FOR STAYING\t\t|");//TO LAAR's HOTEL!
            Console.WriteLine("\t|\t\t\t  At LAAR's HOTEL!\t\t|");
            Console.WriteLine("\t|-------------------------------------------------------|");
            Console.WriteLine("\t|\t\t\tHave a safe trip.\t\t|");
            Console.WriteLine("\t└{0}┘\n", dash);
        }

        // **************** RETURN METHODS ****************
        public static float GetAmountToPay(string room,int duration,int _pax)
        {
            float excessAmount = 0;
            float durationAmount = 0;
            float amountToPay = 0;
            if (room.ToString().ToUpper() == "L01")
            {
                durationAmount = (float)duration * L01RATE;
                
                if (_pax > 1)
                    excessAmount = (float)(_pax - 1) * LUXEXCESSPAX;
                
            }else if (room.ToString().ToUpper() == "L02")
            {
                durationAmount = (float)duration * L02RATE;

                if (_pax > 2)
                    excessAmount = (float)(_pax - 2) * LUXEXCESSPAX;
            }
            else if (room.ToString().ToUpper() == "L03")
            {
                durationAmount = (float)duration * L03RATE;

                if (_pax > 5)
                    excessAmount = (float)(_pax - 5) * LUXEXCESSPAX;
            }
            else if (room.ToString().ToUpper() == "S01")
            {
                durationAmount = (float)duration * S01RATE;

                if (_pax > 1)
                    excessAmount = (float)(_pax - 1) * STANDEXCESSPAX;
            }
            else if (room.ToString().ToUpper() == "S02")
            {
                durationAmount = (float)duration * S02RATE;

                if (_pax > 2)
                    excessAmount = (float)(_pax - 2) * STANDEXCESSPAX;
            }
            else if (room.ToString().ToUpper() == "S03")
            {
                durationAmount = (float)duration * S03RATE;

                if (_pax > 5)
                    excessAmount = (float)(_pax - 5) * STANDEXCESSPAX;
            }

            return amountToPay = durationAmount + excessAmount;
        }
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

        static string ProcDescription(string process)
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

        public static char GetProcNumber(string process)
        {
            char rmType = '0';
            if (process == "RESERVE ROOM")
            {
                rmType = '1';
            }
            else if (process == "WALK IN")
            {
                rmType = '2';
            }
            return rmType;
        }

        static T DynamicInputs<T>(string prompt,int fieldNumber,string value="",string process = "",bool occupant = false)
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
                            Validate.Payment(ref isValid, result.ToString(), value, "PAYMENT", fieldNumber, 8,process);

                            // if success return result
                            if (isValid) return (T)result;
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

                            Validate.InputCharacter(ref isValid, _char.ToString(), "UPDATE INFO", fieldNumber, 13,
                                new string[] { "1", "2", "3", "4", "5", "6" }); //update booking menu

                            if (isValid) return (T)(object)_char;
                        }
                        else
                        {
                            throw new Exception("Invalid input.");
                        }
                    }
                    else
                    {
                        string formattedDate = DateTime.Now.ToString("MM-dd-yyyy");
                        // Validation start here for strings
                        Validate.InputLength( ref isValid,input, 4, 15, "FIRSTNAME",fieldNumber, 2);//firstname
                        Validate.InputLength(ref isValid, input, 4, 15, "LASTNAME", fieldNumber, 3);//lastname

                        Validate.InputCharacter(ref isValid, input, "ROOM NUMBER", fieldNumber, 4,//room number
                            new string[] {"L01", "L02" , "L03" , "S01" , "S02" , "S03" });
                        if (!occupant)
                        {
                            Validate.RoomAvailable(ref isValid, input, "ROOM NUMBER", fieldNumber, 4);//room number
                        }
                        else
                        {
                            Validate.RoomAvailable(ref isValid, input, "ROOM NUMBER", fieldNumber, 4, occupant);//room number
                        }
                        
                        Validate.CheckDate(ref isValid, input, "CHECK-IN", fieldNumber, 6);//check in
                        Validate.CheckInOut(ref isValid, input,formattedDate, "CHECK-IN", fieldNumber, 6);//check in

                        Validate.CheckDate(ref isValid, input, "CHECK-OUT", fieldNumber, 7);//check out
                        Validate.CheckInOut(ref isValid, input, value, "CHECK-OUT", fieldNumber, 7);//check out

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
