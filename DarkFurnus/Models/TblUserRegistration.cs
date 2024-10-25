using System;
using System.Collections.Generic;

namespace DarkFurnus.Models;

public partial class TblUserRegistration
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public DateOnly? Dob { get; set; }

    public int? CountryId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
