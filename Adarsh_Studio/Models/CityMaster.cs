using System;
using System.Collections.Generic;

namespace Adarsh_Studio.Models;

public partial class CityMaster
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<BookingMaster> BookingMasterCurrentCityNavigations { get; set; } = new List<BookingMaster>();

    public virtual ICollection<BookingMaster> BookingMasterLocationOfShootingNavigations { get; set; } = new List<BookingMaster>();
}
