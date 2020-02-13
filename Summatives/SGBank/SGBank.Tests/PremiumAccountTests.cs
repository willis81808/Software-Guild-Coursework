using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class PremiumAccountTests
    {
        [TestCase("54321", "Premium Account", 100, AccountType.Free, 250, false)]
        [TestCase("54321", "Premium Account", 100, AccountType.Premium, -100, false)]
        [TestCase("54321", "Premium Account", 100, AccountType.Premium, 250, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            var rule = new NoLimitDepositRule();
            var account = new Account()
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };
            var response = rule.Deposit(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("54321", "Premium Account", 1000, AccountType.Basic, -1000, 1000, false)]
        [TestCase("54321", "Premium Account", 1000, AccountType.Premium, -2000, 1000, false)]
        [TestCase("54321", "Premium Account", 1000, AccountType.Premium, 1000, 0, false)]
        [TestCase("54321", "Premium Account", 1000, AccountType.Premium, -1000, 0, true)]
        [TestCase("54321", "Premium Account", 1000, AccountType.Premium, -1200, -200, true)]
        public void BasicAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            var rule = new PremiumAccountWithdrawRule();
            var account = new Account()
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };
            var response = rule.Withdraw(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
            if (response.Success)
            {
                Assert.AreEqual(newBalance, response.Account.Balance);
            }
        }
    }
}
