using System;
using System.Globalization;


namespace _tools
{
    public class CTools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string NullToString(object value)
        {
            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return value == null ? "" : value.ToString();

            // If this is not what you want then this form may suit you better, handles 'Null' and DBNull otherwise tries a straight cast
            // which will throw if Value isn't actually a string object.
            //return Value == null || Value == DBNull.Value ? "" : (string)Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string NullToNumber(object value)
        {
            return value == null ? "0" : (string.IsNullOrEmpty(value.ToString().Trim()) ? "0" : value.ToString());
        }
        public string Terbilang(double x)
        {
            string[] bilangan = {" ", "satu", "dua", "tiga", "empat", "lima",
                "enam", "tujuh", "delapan", "sembilan", "sepuluh",
                "sebelas"};
            string temp = "";

            if (x < 12)
            {
                temp = " " + bilangan[(int)x];
            }
            else if (x < 20)
            {
                temp = Terbilang(x - 10).ToString(CultureInfo.InvariantCulture) + " belas";
            }
            else if (x < 100)
            {
                temp = Terbilang(x / 10) + " puluh" + Terbilang(x % 10);
            }
            else if (x < 200)
            {
                temp = " seratus" + Terbilang(x - 100);
            }
            else if (x < 1000)
            {
                temp = Terbilang(x / 100) + " ratus" + Terbilang(x % 100);
            }
            else if (x < 2000)
            {
                temp = " seribu" + Terbilang(x - 1000);
            }
            else if (x < 1000000)
            {
                temp = Terbilang(x / 1000) + " ribu" + Terbilang(x % 1000);
            }
            else if (x < 1000000000)
            {
                temp = Terbilang(x / 1000000) + " juta" + Terbilang(x % 1000000);
            }
            else if (x < 1000000000000)
            {
                temp = Terbilang(x / 1000000000) + " milyar" + Terbilang(x % 1000000000);
            }
            else if (x < 1000000000000000)
            {
                temp = Terbilang(x / 1000000000000) + " trilyun" + Terbilang(x % 1000000000000);
            }

            return temp;
        }
        /// <summary>
        /// Function to convert numeric into "amount" text. I currently grab this code somewhere over the web, hence i cannot give
        /// any credit to any single person, because im not sure the web author from which i get this code is the original algorithm
        /// inventor of this code. Im not also, acquiring this as my own code. I get this somewhere over the web.
        /// </summary>
        /// <param name="amount">double value which will be converted into text values</param>
        /// <returns></returns>
        public string TerbilangKoma(double amount)
        {
            string word = "";
            double divisor = 1000000000000.00;
            string[] prefix = { "SE", "DUA ", "TIGA ", "EMPAT ", "LIMA ", "ENAM ", "TUJUH ", "DELAPAN ", "SEMBILAN " };
            string[] sufix = { "SATU ", "DUA ", "TIGA ", "EMPAT ", "LIMA ", "ENAM ", "TUJUH ", "DELAPAN ", "SEMBILAN " };

            double largeAmount = Math.Abs(Math.Truncate(amount));
            double tinyAmount = Math.Round((Math.Abs(amount) - largeAmount) * 100);

            if (largeAmount > divisor)
                return "OUT OF RANGE";

            while (divisor >= 1)
            {
                double dividen = Math.Truncate(largeAmount / divisor);
                largeAmount = largeAmount % divisor;

                string unit = "";
                if (dividen > 0)
                {

                    // ReSharper disable CompareOfFloatsByEqualityOperator
                    if (divisor == 1000000000000.00)


                        unit = "TRILYUN ";
                    else
                        if (divisor == 1000000000.00)
                        unit = "MILYAR ";
                    else
                            if (divisor == 1000000.00)
                        unit = "JUTA ";
                    else
                                if (divisor == 1000.00)
                        unit = "RIBU ";
                }
                string weight1 = "";
                double dummy = dividen;
                if (dummy >= 100)
                    weight1 = prefix[(int)Math.Truncate(dummy / 100) - 1] + "RATUS ";

                dummy = dividen % 100;
                if (dummy < 10)
                {
                    if (dummy == 1 && unit == "RIBU ")
                        weight1 += "SE";
                    else
                        if (dummy > 0)
                        weight1 += sufix[(int)dummy - 1];
                }
                else
                    if (dummy >= 11 && dummy <= 19)
                {
                    weight1 += prefix[(int)(dummy % 10) - 1] + "BELAS ";
                }
                else
                {
                    weight1 += prefix[(int)Math.Truncate(dummy / 10) - 1] + "PULUH ";
                    if (dummy % 10 > 0)
                        weight1 += sufix[(int)(dummy % 10) - 1];
                }
                word += weight1 + unit;
                divisor /= 1000.00;
            }
            if (Math.Truncate(amount) == 0)
                // ReSharper restore CompareOfFloatsByEqualityOperator
                word = "NOL ";

            string follower = "";
            if (tinyAmount < 10)
            {
                if (tinyAmount > 0)
                    follower = "KOMA NOL " + sufix[(int)tinyAmount - 1];
            }
            else
            {
                follower = "KOMA " + sufix[(int)Math.Truncate(tinyAmount / 10) - 1];
                if (tinyAmount % 10 > 0)
                    follower += sufix[(int)(tinyAmount % 10) - 1];
            }

            word += follower;
            if (amount < 0)
                word = "MINUS " + word;

            return word.Trim();
        }
    }
}
