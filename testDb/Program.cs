// See https://aka.ms/new-console-template for more information
using DataBase.Models;
using DataBase.Utilities;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

UsersDb Udb = new UsersDb();
//Console.WriteLine(SaltGen.GenerateSalt(10));

//Udb.Role.Add(new Role("Ordinary_user"));
//Udb.Role.Add(new Role("VIP_user"));

//Salt salt = new Salt(Guid.NewGuid(), SaltGen.GenerateSalt(10));

//User user = new User(Guid.NewGuid(), "Egor", "egorolinek@gmail.com", "egorpvp1" + salt.SaltEntity, salt);

//Users_Role UR = new Users_Role(Guid.NewGuid(), user.Id, 2);

//Udb.Salt.Add(salt);

//Udb.User.Add(user);

//Udb.Users_Role.Add(UR);

//Udb.SaveChanges();

var result = (from p in Udb.Users_Role select p).Include(p => p.User).Include(p => p.Role).ToList();

var saltResult = (from s in Udb.Salt where result.First().User.Id == s.UserId select s).ToList();

foreach (var item in result)
{
    Console.WriteLine(item);
}