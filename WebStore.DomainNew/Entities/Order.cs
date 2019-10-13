﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WebStore.Domain.Entities.Base;

namespace WebStore.DomainNew.Entities
{
    public class Order : BaseEntity
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }


        public virtual User User { get; set; } // внешний ключ в БД
        public virtual Collection<OrderItem> OrderItems { get; set; }
    }
}
