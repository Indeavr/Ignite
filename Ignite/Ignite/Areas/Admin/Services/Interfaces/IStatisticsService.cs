﻿using Ignite.Areas.Admin.ViewModels;
using Ignite.Areas.Admin.ViewModels.statistics;
using Ignite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Ignite.Areas.Admin.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task CheckForOverdueAndUpdate();

        List<OverdueCourse> GetAllOverdue();

        Object GetDataFromServer(int rows, int page);

        object SearchAndGetData(string filters);
    }
}