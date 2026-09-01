using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class ServicePicMaster
{
    public int PicId { get; set; }

    public int? ServiceId { get; set; }

    public string? PicFileName { get; set; }

    public string? PicFolderName { get; set; }

    public string? PicType { get; set; }

    public double? PicSizeInKb { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ServiceMaster? Service { get; set; }
}
