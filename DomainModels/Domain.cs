using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeGenerator.DomainModels
{
    [Serializable]
    [XmlType("Domain")]
    public class Domain
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("ParentDomainId")]
        public int ParentDomainId { get; set; }

        [XmlIgnore()]
        public Domain ParentDomain { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
