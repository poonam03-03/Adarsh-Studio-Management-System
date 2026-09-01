using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class EnquiryMaster
{
    public int EnquiryId { get; set; }

    public string? Name { get; set; }

    public string? EmailId { get; set; }

    public string? MobNo { get; set; }

    public string? QueryMsg { get; set; }

    public DateTime? CreatedOn { get; set; }
}
