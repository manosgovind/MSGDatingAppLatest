using System.Collections.Generic;
using System.Linq;
using DatingAppLatest.API.Models;
using Newtonsoft.Json;

namespace DatingAppLatest.API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            // this class is used to seed data from an external source(Data/UserSeedData.json) to our DB
            // this needs to be run only once and so it is to be called in Program.cs file Main Method
            // usually we did this kind of callings in startup.cs, but the best practice to call a seed data would be in Program.cs
            
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt =passwordSalt;
                    user.UserName = user.UserName.ToLower();
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // there are lot other ways to apply the password has, we are using this as example
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //generating a salt and hash value using the password and store it in the db ,
                //, And while logging in using the same salt we stored in db, we will generate the salt again and compare
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}