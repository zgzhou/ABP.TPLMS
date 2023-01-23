using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ABP.TPLMS.Entitys
{
   public class InStockOrderDetailLoc : Entity<int>, IHasCreationTime
    {
        public InStockOrderDetailLoc()
        {
           
            this.Qty = 0;
            this.SeqNo = 0;
            this.Loc = string.Empty;
            this.CreationTime = DateTime.Now;
            this.InStockOrderDetailId = 0;
            this.OutQty = 0;
        }      

        public int InStockOrderDetailId { get; set; }
        public int SeqNo { get; set; }
        [StringLength(50)]
        public string Loc { get; set; }    
        public decimal Qty { get; set; }
        public DateTime CreationTime { get; set; }
        public decimal OutQty { get; set; }
    }
}
