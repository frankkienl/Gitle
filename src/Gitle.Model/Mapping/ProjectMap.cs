﻿namespace Gitle.Model.Mapping
{
    public class ProjectMap : ModelBaseMap<Project>
    {
        public ProjectMap()
        {
            Map(x => x.Name);
            Map(x => x.Slug);
            Map(x => x.Repository);
            Map(x => x.MilestoneId);
            Map(x => x.MilestoneName);
            Map(x => x.HourPrice);
            Map(x => x.FreckleId);
            Map(x => x.FreckleName);
            Map(x => x.Information).CustomSqlType("nvarchar(max)");
            Map(x => x.Comments).CustomSqlType("nvarchar(max)");
            HasMany(x => x.Users).Cascade.All();
            HasMany(x => x.Labels).Cascade.AllDeleteOrphan();
            HasMany(x => x.Issues).Cascade.AllDeleteOrphan();
            References(x => x.Customer);
        }
    }
}