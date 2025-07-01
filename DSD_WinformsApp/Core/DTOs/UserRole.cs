using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DSD_WinformsApp.Core.DTOs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserRole
    {
        All,
        Admin,
        User
    }

}
