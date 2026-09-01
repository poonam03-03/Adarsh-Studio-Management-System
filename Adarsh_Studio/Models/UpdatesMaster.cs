using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class UpdatesMaster
{
    public int UpdateId { get; set; }

    public string? UpdateMsg { get; set; }

    public DateTime? CreatedOn { get; set; }
}
