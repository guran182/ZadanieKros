using System.Text.RegularExpressions;

namespace Kros
{
    public static class Validator
    {
        public static string InvalidateZamestnanec(string column, string value) 
        {
            if (column.Equals("Osobné číslo")
                || (column.Equals("Titul") && string.IsNullOrEmpty(value))
                || (column.Equals("Telefón (klapka)") && string.IsNullOrEmpty(value))
                || (column.Equals("E-mail") && string.IsNullOrEmpty(value)))
                return null;

            if (column.Equals("Titul") && false == Regex.IsMatch(value, @"^[A-Za-z\.]+$"))
                return "Titul smie obsahovať iba písmená a bodku";
            else if (column.Equals("Meno") && string.IsNullOrEmpty(value))
                return "Zadajte meno zamestnanca";
            else if (column.Equals("Priezvisko") && string.IsNullOrEmpty(value))
                return "Zadajte priezvisko zamestnanca";
            else
                return null;
        }

        public static string InvalidateFirma(string column, string value)
        {
            if (column.Equals("IČO") && false == int.TryParse(value, out _))
                return "IČO musí byť číslo bez medzier";
            else if (column.Equals("Názov spoločnosti") && string.IsNullOrEmpty(value))
                return "Zadajte názov spoločnosti";
            else if (column.Equals("Riaditeľ") && string.IsNullOrEmpty(value))
                return "Firma musí mať riaditeľa";
            else
                return null;
        }
    }
}
