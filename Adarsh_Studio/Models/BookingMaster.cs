using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class BookingMaster
{
    public int BookingId { get; set; }

    public int? ServiceId { get; set; }

    public string? ClientName { get; set; }

    public long? MobileNo { get; set; }

    public string? EmailId { get; set; }

    public int? CurrentCity { get; set; }

    public string? Address { get; set; }

    public int? LocationOfShooting { get; set; }

    public string? Remark { get; set; }

    public DateTime? ShootingDate { get; set; }

    public string? Status { get; set; }

    public int? Price { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual CityMaster? CurrentCityNavigation { get; set; }

    public virtual CityMaster? LocationOfShootingNavigation { get; set; }
}
