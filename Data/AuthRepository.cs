using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace learn.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context){
            _context = context;
        }
        public async Task<ServiceResponse<string>> Login(string userName, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.UserName.ToLower() == userName.ToLower());
            if(user == null){
                response.Success = false;
                response.Message = "User not found";
            } else if (!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt)){
                response.Success = false;
                response.Message = "incorect password";
            } else{
                response.Data = user.Id.ToString();
            }
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();
            if(await UserExist(user.UserName)){
                response.Success=false;
                response.Message = "user already exist";
                return response;
            }

            CreatePasswordHash(password,out byte[] passwordHash, out byte[]PasswordSalt);
            user.passwordHash=passwordHash;
            user.passwordSalt = PasswordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data= user.Id;
            return response;
        }

        public async Task<bool> UserExist(string username)
        {
            if(await _context.Users.AnyAsync(c=> c.UserName.ToLower() == username.ToLower() )){
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[]passwordHash, out byte[] passwordSalt){
            using (var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt= hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt){
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
               var  computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}