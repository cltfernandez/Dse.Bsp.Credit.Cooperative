using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Loan.Application.Infrastructure.Controls.PropertyEditor.Editors;
using Loan.Application.Infrastructure.Controls.PropertyEditor.Converters;
using Loan.Application.Infrastructure.Enumerations.DropDownItems;

namespace Loan.Application.Infrastructure.Business.Popups
{
    public class ControlDate
    {
        [DisplayName("Admin Date")]
        public DateTime AdminDate { get; set; }        

        [DisplayName("System Date")]
        [ReadOnly(true)]
        public DateTime SystemDate { get; set; }

        private bool ShouldSerializeAdminDate() { return false; }
        private bool ShouldSerializeSystemDate() { return false; }
    }

    public class InputInt
    {
        public InputInt()
        {
            Number = 0;
        }

        [DisplayName("Value")]
        public int Number { get; set; }
        public bool ShouldSerializeNumber() { return false; }
    }

    public class InputText
    {
        public InputText()
        {
            Text = String.Empty;
        }

        [DisplayName("Value")]
        public string Text { get; set; }
        public bool ShouldSerializeText() { return false; }
    }

    public class InputAmount
    {
        public InputAmount()
        {
            Amount = 0;
        }

        [DisplayName("Amount")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal Amount { get; set; }
        public bool ShouldSerializeAmount() { return false; }
    }

    public class InputDate
    {
        public InputDate()
        {
            Date = new DateTime(1900, 1, 1);
        }

        [DisplayName("Date")]
        public DateTime Date { get; set; }
        private bool ShouldSerializeDate() { return false; }
    }

    public class PayrollTextAndAmount
    {
        public PayrollTextAndAmount()
        {
            Text = "CASH IN BANK (PNB)";
            Amount = 0;
        }

        [DisplayName("Source")]
        public string Text { get; set; }
        public bool ShouldSerializeText() { return false; }

        [DisplayName("Amount")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal Amount { get; set; }
        public bool ShouldSerializeAmount() { return false; }
    }

    public class IntRange : Interfaces.IIntRange
    {
        public IntRange()
        {
            IntFrom = "0";
            IntTo = "0";
        }

        [DisplayName("From")]
        public string IntFrom { get; set; }

        [DisplayName("To")]
        public string IntTo { get; set; }

        private bool ShouldSerializeIntFrom() { return false; }
        private bool ShouldSerializeIntTo() { return false; }
    }

    public class DateRange : Interfaces.IDateRange
    {
        public DateRange()
        {
            DateFrom = DateTime.Now.Date;
            DateTo = DateTime.Now.Date;
        }

        [DisplayName("Date From")]
        public System.DateTime DateFrom { get; set; }

        [DisplayName("Date To")]
        public System.DateTime DateTo { get; set; }

        private bool ShouldSerializeDateFrom() { return false; }
        private bool ShouldSerializeDateTo() { return false; }
    }

    public class PnNoRange : Interfaces.ITextRange
    {
        [DisplayName("PN No From")]
        [Editor(typeof(PnNoEditor), typeof(UITypeEditor))]
        public string TextFrom { get; set; }
        
        [DisplayName("PN No To")]
        [Editor(typeof(PnNoEditor), typeof(UITypeEditor))]
        public string TextTo { get; set; }

        private bool ShouldSerializeTextFrom() { return false; }
        private bool ShouldSerializeTextTo() { return false; }
    }

    public class KbciNoRange : Interfaces.ITextRange
    {
        [DisplayName("KBCI No From")]
        [Editor(typeof(KbciNoEditor), typeof(UITypeEditor))]
        public string TextFrom { get; set; }

        [DisplayName("KBCI No To")]
        [Editor(typeof(KbciNoEditor), typeof(UITypeEditor))]
        public string TextTo { get; set; }

        private bool ShouldSerializeTextFrom() { return false; }
        private bool ShouldSerializeTextTo() { return false; }
    }

    public class PnNoAndDateRange : DateRange
    {

        [DisplayName("PN No")]
        [Editor(typeof(PnNoEditor), typeof(UITypeEditor))]
        public string PnNo { get; set; }
        
        private bool ShouldSerializePnNO() { return false; }
    }

    public class KbciNoAndDate : InputDate
    {
        [DisplayName("KBCI No")]
        [Editor(typeof(KbciNoEditor), typeof(UITypeEditor))]
        public string KbciNo { get; set; }
        private bool ShouldSerializeKbciNo() { return false; }
    }

    public class KbciNoAndDateRange : DateRange
    {

        [DisplayName("KBCI No")]
        [Editor(typeof(KbciNoEditor), typeof(UITypeEditor))]
        public string KbciNo { get; set; }
        private bool ShouldSerializeKbciNo() { return false; }
    }

    public class BankAndCheckNo
    {
        [DisplayName("Bank")]
        public string Bank { get; set; }
        private bool ShouldSerializeBank() { return false; }
        
        [DisplayName("Check No")]
        public string CheckNo { get; set; }        
        private bool ShouldSerializeCheckNo() { return false; }
    }

    public class LoanTypeAndDate : InputDate
    {
        [DisplayName("LoanType")]
        public LoanTypeAll LoanType { get; set; }
        private bool ShouldSerializeLoanType() { return false; }
    }

    public class AsOfDateAndDateRange : DateRange
    {
        [DisplayName("As Of Date")]
        public DateTime AsOfDate { get; set; }
        private bool ShouldSerializeAsOfDate() { return false; }
    }

    public class MonthlyRunup : InputDate
    {
        [DisplayName("Show Sub-Total")]
        public bool ShowSubTotal { get; set; }
        private bool ShouldSerializeLoanType() { return false; }
    }
}
