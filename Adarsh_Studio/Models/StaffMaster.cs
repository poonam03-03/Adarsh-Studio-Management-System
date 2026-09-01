using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class StaffMaster
{
    public int StaffId { get; set; }

    public string? Name { get; set; }

    public string? ImgFileName { get; set; }

    public string? ImgFolderName { get; set; }

    public string? ImgType { get; set; }

    public double? ImgSizeInKb { get; set; }

    public string? Role { get; set; }

    public string? Specialization { get; set; }

    public long? Contact { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedOn { get; set; }
}
