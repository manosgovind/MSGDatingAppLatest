using System;
using System.Threading.Tasks;
using DatingAppLatest.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingAppLatest.API.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> IsUserExists(string userName)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == userName))
                return true;


            return false;

        }

        public async Task<User> Login(string userName, string password)
        {
            //getting the user details from db using the username
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
                return null;

            // verify the password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //generating the hash again  using the key/salt that we got while we generating the hash
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != passwordHash[i]) return false;

                }

                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
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