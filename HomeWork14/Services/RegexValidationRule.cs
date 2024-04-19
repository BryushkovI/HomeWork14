using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HomeWork15.Services
{
    public class RegexValidationRule : ValidationRule
    {
        private string _pattern;
        private Regex _regex;

        public string Pattern
        {
            get => _pattern;
            set
            {
                _pattern = value;
                _regex = new(_pattern,RegexOptions.IgnoreCase);
            }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return int.TryParse(value.ToString(), out _) || value.ToString() == string.Empty
                    ? new ValidationResult(true, null)
                    : new ValidationResult(false, "Введено некорректное значение");
        }
        public RegexValidationRule()
        {
            
        }
    }
}
