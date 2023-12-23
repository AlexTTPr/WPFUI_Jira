﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Stores.Interfaces;
public interface IAccountStore
{
    User CurrentAccount { get; set; }

	event Action CurrentAccountChanged;
}
