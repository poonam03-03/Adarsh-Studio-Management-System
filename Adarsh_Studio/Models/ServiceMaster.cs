using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class ServiceMaster
{
    public int ServiceId { get; set; }

    public string? ServiceType { get; set; }

    public string? Category { get; set; }

    public int? Budget { get; set; }

    public int? DiscountedRate { get; set; }

    public string? Description { get; set; }

    public string? Inclusions { get; set; }

    public string? Exclusions { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<ServicePicMaster> ServicePicMasters { get; set; } = new List<ServicePicMaster>();
}
