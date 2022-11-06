using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace learn.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum courseType
    {
        Kg = 1,

        Lp = 2,

        Up =3

    }
}