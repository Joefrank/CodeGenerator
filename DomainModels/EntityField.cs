using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeGenerator.DomainModels
{
    [Serializable]
    [XmlType("EntityField")]
    public class EntityField
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Type")]
        public int Type { get; set; }

        [XmlElement("DataType")]
        public int DataType { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Mandatory")]
        public bool Mandatory { get; set; }

        [XmlElement("ParentId")]
        public int ParentEntityId { get; set; }

         [XmlIgnore()]
        public Entity Parent { get; set; }
    }
}
