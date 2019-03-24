﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.DTO
{
    public class VLCExpenseDTO
    {
        public int VLCExpenseId { get; set; }
        public int VLCId { get; set; }
        public int ExpenseReason { get; set; }
        public decimal PaymentCrAmount { get; set; }
        public decimal PaymentDrAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseComments { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
