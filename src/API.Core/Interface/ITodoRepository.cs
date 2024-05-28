﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core;

public interface ITodoRepository : IGenericRepository<Todo>, IDisposable
{
    Task<IEnumerable<Todo>> GetByUser(string user);
}