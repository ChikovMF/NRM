using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRM.Services.RoleService
{
    [Flags]
    public enum RoleAccess : sbyte
    {
        AddParcel = 100,
        DeleteParcel = 1,
    }
}
