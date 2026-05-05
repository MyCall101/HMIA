using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIA
{
    internal class Validate
    {
        public static void InputLength(ref bool isValid,string _value,int _min,int _max,string fieldName,int fieldNumber,int defFieldNumber)
        {
            if (isValid && fieldNumber == defFieldNumber && _value.Trim() != "")
            {
                int valueLength = _value.Trim().Length;

                if (valueLength > 0)
                {
                    if (valueLength < _min || valueLength > _max)
                    {
                        Console.WriteLine("\n\t-> {0} : should have minimum of {1} & maximum of {2} character.", 
                                        fieldName, _min, _max);
                        isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            if (_value.Trim() == "") isValid = false;

        }
        public static void InputCharacter(ref bool isValid, string _value, string fieldName, int fieldNumber, int defFieldNumber,string[] validChar)
        {
            if (isValid && fieldNumber == defFieldNumber && _value.Trim() != "")
            {
                int index = validChar.ToList().IndexOf(_value.ToUpper());
                if(index < 0)
                {
                    Console.WriteLine("\n\t-> {0} : Invalid choice.", fieldName);
                    isValid = false;
                }

            }
            if (_value.Trim() == "") isValid = false;
        }
        public static void CheckDate(ref bool isValid, string _value, string fieldName, int fieldNumber, int defFieldNumber)
        {
            if (isValid && fieldNumber == defFieldNumber && _value.Trim() != "")
            {
                DateTime _date;
                bool valid = DateTime.TryParseExact(_value, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _date);
                if (!valid)
                {
                    Console.WriteLine("\n\t-> {0} : Invalid date.",fieldName);
                    isValid = false;
                }
            }
            if (_value.Trim() == "") isValid = false;
        }
        public static void RoomAvailable(ref bool isValid, string _value, string fieldName, int fieldNumber, int defFieldNumber)
        {
            if (isValid && fieldNumber == defFieldNumber && _value.Trim() != "")
            {
                bool isAvailable = true;
                for(int i = 0; i < Program.luxuryRooms.GetLength(0); i++)
                {
                    if (Program.luxuryRooms[i,0].ToString().ToUpper() == _value.ToUpper() &&
                        Program.luxuryRooms[i,1] != "0")
                    {
                        isAvailable = false;
                        break;
                    }
                }
                if (isAvailable)
                {
                    for (int k = 0; k < Program.standardRooms.GetLength(0); k++)
                    {
                        if (Program.standardRooms[k, 0].ToString().ToUpper() == _value.ToUpper() &&
                            Program.standardRooms[k, 1] != "0")
                        {
                            isAvailable = false;
                            break;
                        }
                    }
                }
                if (!isAvailable)
                    Console.WriteLine("\n\t-> {0} : Room not available.",fieldName);
            }
            if (_value.Trim() == "") isValid = false;
        }
    }
}
