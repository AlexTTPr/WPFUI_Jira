﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Stores;

//need some polishing
public class AccountStore : IAccountStore
{
	public User CurrentAccount {  get; set; }

	public event Action CurrentAccountChanged;
}
