﻿using System;
using System.Collections.Generic;

namespace SunTrack.API.Data.Models;

public partial class ProjectProductMapping
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int ProductId { get; set; }

    public virtual ProductDetail Product { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
