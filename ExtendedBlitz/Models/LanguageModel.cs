using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlitz.Models
{
    internal class LanguageModel
    {
        public string Name { get; set; }
        public string Language { get; set; }

        public string Language_Name
        {
            get { return $"{Language} - {Name}"; }
        }
    }
}
