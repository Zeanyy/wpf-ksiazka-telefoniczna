using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonkatyWPF
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public char gender { get; set; }
        public string email { get; set; }
        public string state { get; set; }
        public int number { get; set; }

        public Person(string name, string surname, string gender, string email, string state, int number)
        {
            this.name = name;
            this.surname = surname;
            this.gender = Convert.ToChar(gender);
            this.email = email;
            this.state = state;
            this.number = number;
        }
        public Person(int id, string name, string surname, string gender, string email, string state, int number)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.gender = Convert.ToChar(gender);
            this.email = email;
            this.state = state;
            this.number = number;
        }
    }
}
