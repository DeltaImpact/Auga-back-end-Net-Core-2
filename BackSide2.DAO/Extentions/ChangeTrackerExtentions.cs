﻿using System;
using Auga.DAO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Auga.DAO.Extentions
{
    public static class ChangeTrackerExtensions
    {
        public static void ApplyAuditInformation(this ChangeTracker changeTracker)
        {
            foreach (var entry in changeTracker.Entries())
            {
                if (!(entry.Entity is BaseEntity baseAudit)) continue;

                var now = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Modified:
                        baseAudit.Modified = now;
                        break;

                    case EntityState.Added:
                        baseAudit.Created = now;
                        break;
                }
            }
        }
    }
}
