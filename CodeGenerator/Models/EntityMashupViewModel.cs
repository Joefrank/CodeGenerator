using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeGenerator.DomainModels;

namespace CodeGenerator.Models
{
    public class EntityMashupViewModel
    {
        public int EntityId { get; set; }

        public string EntityFieldXML { get; set; }
    }
}