using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Models
{
    public interface ILogin
    {
        public Task<Login> LoginDB(Login login);
        public Task<Login> VerifyToken(string token);
    }
}
