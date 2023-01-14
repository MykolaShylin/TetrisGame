using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TetrisWinFormsApp
{
    public class User
    {
        public string Name { get; }
        public string SecondName { get; }
        public string Age { get; }
        public string Score { get; set; }
        public string FallenBlocksCount { get; set;}

        public User(string name, string secondName, string age)
        {
            Name = name;
            SecondName = secondName;
            Age = age;
        }

    }
}
