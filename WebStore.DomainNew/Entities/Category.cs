﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    [Table("Categories")]
    public class Category : NamedEntity, IOrderedEntity
    {
        /// <summary>
        /// Родительская категория (при наличии)
        /// </summary>
        public int? ParentId { get; set; }

        public int Order { get; set; }

        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
