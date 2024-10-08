﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatsenko.TaskPlanner.Domain.Models;

namespace Yatsenko.TaskPlanner.DataAccess.Abstractions
{
    public interface IWorkItemsRepository
    {
        Guid Add(WorkItem workItem);
        WorkItem Get(Guid id);
        WorkItem[] GetAll();
        bool Update(WorkItem workItem);
        bool Remove(Guid id);
        void SaveChanges();
    }
}