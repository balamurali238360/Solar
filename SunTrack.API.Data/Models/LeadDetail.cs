using System;
using System.Collections.Generic;

namespace SunTrack.API.Data.Models;

public partial class LeadDetail
{
    public int Id { get; set; }

    public int LeadId { get; set; }

    public DateTime SiteVisitSchedule { get; set; }

    public string? SitevisitPhotos { get; set; }

    public string? SitevisistStatus { get; set; }

    public string? Notes { get; set; }

    public virtual Lead Lead { get; set; } = null!;
}
