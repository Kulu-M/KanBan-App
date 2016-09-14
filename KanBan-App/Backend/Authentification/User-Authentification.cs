using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Authentification
{
    public class User_Authentification
    {
        static readonly Random rnd = new Random();

        /// <summary>
        /// Creates a new key and save to DB and returns the key
        /// </summary>
        /// <param name="eMail"></param>
        /// <returns></returns>
        public static string generateUserKey(string eMail)
        {
            var newKey = generateRandomKey();
            using (var db = new APIAppDbContext())
            {
                var user = db.User.FirstOrDefault(u => u.EMail == eMail);
                user.VerificationKey = newKey;
                db.SaveChanges();
            }
            return newKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eMail"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool validateUserKey(string eMail, string key)
        {
            var keyIsTheSame = false;
            using (var db = new APIAppDbContext())
            {
                var existingUser = db.User.FirstOrDefault(u => u.EMail == eMail);
                if (existingUser.VerificationKey == key)
                {
                    keyIsTheSame = true;
                }
            }
            return keyIsTheSame;
        }

        public static string generateRandomKey()
        {
            const int length = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length).Select(r => r[rnd.Next(r.Length)]).ToArray());
        }
    }
}
