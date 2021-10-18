using System;

namespace UtilitiesLib
{
    public class ClassUtilities
    {
        public int ConvertStringToInteger(string strNum)
        {
            if (!Int32.TryParse(strNum, out int intNum))
                return 0;
            return intNum;
        }

        public int ConvertStringToInteger(string strNum, int lowLimit, int highLimit)
        {
            if (!Int32.TryParse(strNum, out int intNum) || intNum < lowLimit || intNum > highLimit)
                return 0;
            return intNum;
        }

        public decimal ConvertStringToDecimal(string strNum)
        {
            if (!Decimal.TryParse(strNum, out decimal decNum))
                return 0;
            return decNum;
        }

        public decimal ConvertStringToDecimal(string strNum, decimal lowLimit, decimal highLimit)
        {
            if (!Decimal.TryParse(strNum, out decimal intNum) || intNum < lowLimit || intNum > highLimit)
                return 0;
            return intNum;
        }
    }
}
