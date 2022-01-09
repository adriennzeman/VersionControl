﻿using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Abstractions;
using UnitTestExample.Entities;

namespace UnitTestExample.Services
{
    public class AccountManager : IAccountManager
    {
        public BindingList<Account> Accounts { get; } = new BindingList<Account>();

        public Account CreateAccount(Account account)
        {
            var oldAccount = (from a in Accounts
                              where a.Email.Equals(account.Email)
                              select a).FirstOrDefault();

            if (oldAccount != null)
                throw new ValidationException (
                    "Már létezik felhasználó ilyen e-mail címmel!");
            account.ID = Guid.NewGuid();
            Accounts.Add(account);

            return account;
        }
    }
}
