using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Services
{
    internal static class Validation
    {
        public static double? TotalMark(string totalMarkString)
        {
            bool inputValid = double.TryParse(totalMarkString, out double totalMark);
            if (inputValid && totalMark < 100 && totalMark > 0)
                return totalMark;
            else
                return null;
        }

        public static double? OralMark(string oralMarkString, double? totalMark)
        {
            bool inputValid = double.TryParse(oralMarkString, out double oralMark);

            if (totalMark.HasValue)
            {
                if (inputValid && oralMark <= totalMark)
                    return oralMark;
                else
                    return null;
            }
            else
            {
                if (inputValid)
                    return oralMark;
                else
                    return null;
            }
        }

        public static DateTime? Date(string dateString)
        {
            bool dateValid = DateTime.TryParse(dateString, out DateTime dateTime);
            DateTime? date;

            if (dateValid)      // For some reason ternary didn't work here (needed C# 9)
                date = dateTime;
            else
                date = null;

            return date;
        }

        public static bool? EndAfterStart(DateTime? startDate, DateTime? endDate)
        {
            if (startDate is null || endDate is null)
                return null;
            else
                return DateTime.Compare(startDate.Value, endDate.Value) < 0;
        }

        public static string Name(string nameToCheck)
        {
            if (string.IsNullOrEmpty(nameToCheck))
                return null;

            bool valid = true;
            foreach (char input in nameToCheck) // Check if all characters of the input string are letters.
            {
                if (!Char.IsLetter(input))
                {
                    valid = false;
                    break;
                }
            }

            return valid ? nameToCheck : null;
        }

        public static Subject? Subject(string subjectString)
        {
            if (Enum.TryParse<Subject>(subjectString, out Subject subject))
                return subject;
            else
                return null;
        }

        public static decimal? Fees(string feesString)
        {
            bool feesValid = decimal.TryParse(feesString, out decimal feesValue);
            decimal? fees;

            if (feesValid)      // For some reason ternary didn't work here (needed C# 9)
                fees = feesValue;
            else
                fees = null;

            return fees;

        }
        public static Stream? Stream(string streamString)
        {
            bool streamValid = Enum.TryParse<Stream>(streamString, out Stream streamValue);

            Stream? stream;

            if (streamValid)      // For some reason ternary didn't work here (needed C# 9)
                stream = streamValue;
            else
                stream = null;

            return stream;
        }

        public static Type? Type(string typeString)
        {
            bool typeValid = Enum.TryParse<Type>(typeString, out Type typeValue);

            Type? type;

            if (typeValid)      // For some reason ternary didn't work here (needed C# 9)
                type = typeValue;
            else
                type = null;

            return type;
        }

    }
}
