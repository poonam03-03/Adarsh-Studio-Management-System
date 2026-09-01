using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class PackageMaster
{
    public int PackageId { get; set; }

    public string? PackageTitle { get; set; }

    public long? Price { get; set; }

    public string Detail1 { get; set; } = null!;

    public string Detail2 { get; set; } = null!;

    public string Detail3 { get; set; } = null!;

    public string Detail4 { get; set; } = null!;

    public DateTime CreatedOn { get; set; }
}
