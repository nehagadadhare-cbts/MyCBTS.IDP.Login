using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.DataAccess.Entities
{
    public enum ActionTypes
    {
        AddBillItem = 1,
        DeleteBillItem = 2,
        AddCharge = 3,
        UpdateCharge = 4,
        DeleteCharge = 5,
        AddTelephone = 6,
        DeleteTelephone = 7,
        ChangeTelephone = 8,
    };

    enum CSGActionTypes
    {
        AddService = 1,
        AddServiceFeature = 2,
        RemoveServiceFeature = 3,
        RemoveService = 4
    };
}
