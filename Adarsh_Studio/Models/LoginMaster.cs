using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class LoginMaster
{
    public string AdminId { get; set; } = null!;

    public string AdminPass { get; set; } = null!;

    public int? LoginCount { get; set; }

    public DateTime? LastLoginDt { get; set; }

    public bool? IsBlocked { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? VerificationCode { get; set; }

    public DateTime? VerificationCodeExpiry { get; set; }
}
