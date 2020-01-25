using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Premium Account",
            Balance = 1000.00M,
            AccountNumber = "54321",
            Type = AccountType.Premium
        };

        public Account LoadAccount(string AccountNumber)
        {
            return _account.AccountNumber == AccountNumber ? _account : null;
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}
