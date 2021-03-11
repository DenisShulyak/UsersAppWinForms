using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAppWindowsForms.Models;

namespace UsersAppWindowsForms.Data
{
    public class Datas
    {
        public static List<User> Users { get; set; } = new List<User>();

        //public Datas()
        //{
        //    Users.Add(new User { Email = "sdf@mail.ru", FirstName= "Oleg", LastName = "Ivanovich", Login ="oleja21", Password = "123123", PhoneNumber="+73215745121",BirthDay = DateTime.Now});
        //    Users.Add(new User { Email = "ivan431@mail.ru", FirstName= "Ivan", LastName = "Smirnov", Login ="vanyz421", Password = "123123", PhoneNumber="++7242318545",BirthDay = DateTime.Now});
        //}
    }
}
