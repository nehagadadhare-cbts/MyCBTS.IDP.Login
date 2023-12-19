using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.Entities
{
    public class MailTemplate
    {
        public const string ManualChargeTemplate = "~/App_Data/MailTemplate/ManualChargeTemplate.txt";
        public const string ManualChargeFailedTemplate = "~/App_Data/MailTemplate/ManualChargeInvalidFile.txt";

    }

    public enum MailType
    {
        ManualChargeTemplate = 0,
        ManualChargeFailedTemplate
    }
}
