﻿using Microsoft.EntityFrameworkCore;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Models.Entities;

namespace OrientoonApi.Data.Repositories.Implementations
{
    public class StatusRepository : GenericRepository<StatusModel>, IStatusRepository
    {
        public StatusRepository(OrientoonContext context) : base(context)
        {
        }
    }
}
