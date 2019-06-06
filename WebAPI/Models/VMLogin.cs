using System;

namespace WebAPI.Models
{
    public class VMLogin
    {
            public Guid UserID { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public Nullable<bool> IsActive { get; set; }
            public string Message { get; set; }
    }
}