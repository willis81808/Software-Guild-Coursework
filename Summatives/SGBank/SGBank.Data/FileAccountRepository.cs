using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Accounts.txt");
        private static List<Account> _accountList;
        private static List<Account> AccountList
        {
            get
            {
                if (_accountList == null)
                {
                    var data = File.ReadLines(FilePath);
                    var accounts = from line in data
                                   select line.Split(',') into parts
                                   select new Account
                                   {
                                       AccountNumber = parts[0],
                                       Name = parts[1],
                                       Balance = decimal.Parse(parts[2]),
                                       Type = ReadType(parts[3])
                                   };
                    _accountList = accounts.ToList();
                }

                return _accountList;
            }
        }

        private static AccountType ReadType(string letter)
        {
            switch (letter)
            {
                case "F":
                    return AccountType.Free;
                case "B":
                    return AccountType.Basic;
                case "P":
                    return AccountType.Premium;
                default:
                    throw new Exception($"Unknown account abbreviation entered: {letter}");
            }
        }
        
        public Account LoadAccount(string AccountNumber)
        {
            return AccountList.Where(a => a.AccountNumber == AccountNumber).FirstOrDefault();
        }

        public void SaveAccount(Account account)
        {
            // update account
            for (int i = 0; i < AccountList.Count; i++)
            {
                if (AccountList[i].AccountNumber == account.AccountNumber)
                {
                    AccountList.RemoveAt(i);
                    AccountList.Add(account);
                }
            }

            // serialize changes
            StringBuilder sb = new StringBuilder();
            foreach (var a in AccountList)
            {
                sb.Append(a.AccountNumber);
                sb.Append(',');
                sb.Append(a.Name);
                sb.Append(',');
                sb.Append(a.Balance.ToString());
                sb.Append(',');
                sb.Append(a.Name[0]);
                sb.AppendLine();
            }
            File.WriteAllText(FilePath, sb.ToString());
        }
    }
}
