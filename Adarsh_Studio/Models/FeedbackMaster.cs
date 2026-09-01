using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class FeedbackMaster
{
    public int FeedbackId { get; set; }

    public string Name { get; set; } = null!;

    public string? EmailId { get; set; }

    public long MobileNo { get; set; }

    public string? TitleOfFeedback { get; set; }

    public string? FeedbackMsg { get; set; }

    public int StarRating { get; set; }

    public DateTime CreatedOn { get; set; }
}
