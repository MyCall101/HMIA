using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIA
{
    internal class Validate
    {
        public static void InputLength(ref bool isValid,string _value,int _min,int _max,string fieldName,int fieldNumber,int defFieldNumber)
        {
            if (isValid && fieldNumber == defFieldNumber)
            {
                int valueLength = _value.Trim().Length;

                if (valueLength > 0)
                {
                    if (valueLength < _min || valueLength > _max)
                    {
                        Console.WriteLine($"\n\t-> {fieldName} : should have minimum of {_min} & maximum of {_max} character.");
                        isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            
        }
        public static void InputCharacter(ref bool isValid, string _value, string fieldName, int fieldNumber, int defFieldNumber,string[] validChar)
        {
            if (isValid && fieldNumber == defFieldNumber && _value.Trim() != "")
            {
                int index = validChar.ToList().IndexOf(_value);
                if(index < 0)
                {
                    Console.WriteLine($"\n\t-> {fieldName} : Invalid choice.");
                    isValid = false;
                }

            }
        }
    }
}
