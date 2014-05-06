using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace CodeGenerator.DomainModels
{
    [Serializable]
    [XmlType("Entity")]
    public class Entity
    {
        
        [XmlArray(ElementName = "Fields")]
        [XmlArrayItem("Field")]
        public EntityField[] Fields
        {
            get { return LstFields.ToArray(); }
        }

        public Entity()
        {
            LstFields = new List<EntityField>();
        }

        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlIgnore()]
        public int NoOfFields { get { return LstFields.Count; }}

        [XmlElement("ParentDomainId")]
        public int ParentDomainId { get; set; }


        public List<EntityField> LstFields { get; set; } 
    }
}
