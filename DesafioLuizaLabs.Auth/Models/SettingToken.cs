using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioLuizaLabs.Auth.Models
{
    public static class SettingToken
    {
        public static string Secret = new Settings().Secret;
    }
}
