using System;
using System.ComponentModel;
using Loan.Application.Infrastructure.PropertyEditor.Converters;
using Loan.Application.Infrastructure.Enumerations.DropDownItems;

namespace Loan.Application.Infrastructure.Popups
{
    public class PnNoStartDateEndDate
    {
        //public string PN_NO;
        //public DateTime START_DATE;
        //public DateTime END_DATE;

        [Category("(Main)")]
        public string PN_NO { get; set; }

        [Category("Dates")]
        public DateTime START_DATE { get; set; }

        [Category("Dates")]
        public DateTime END_DATE { get; set; }

        bool ShouldSerializePN_NO() { return false; }
        bool ShouldSerializeSTART_DATE() { return false; }
        bool ShouldSerializeEND_DATE() { return false; }
    }
}
