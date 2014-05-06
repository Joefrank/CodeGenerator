using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeGenerator.DomainModels
{
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlRoot("Project")]
    public class Project
    {
        [XmlElement("Id", Order=0)]
        public int Id { get; set; }

        [XmlElement("Name", Order = 1)]
        public string Name { get; set; }

        [XmlElement("CreatedOn", Order = 2)]
        public DateTime CreatedOn { get; set; }

        [XmlElement("TopDomainSeed", Order = 3)]
        public int TopDomainSeed { get; set; }

        [XmlElement("TopEntitySeed", Order = 4)]
        public int TopEntitySeed { get; set; }

        [XmlElement("TopFieldSeed", Order = 5)]
        public int TopFieldSeed { get; set; }

        [XmlArray(ElementName = "Domains", Order = 6)]
        [XmlArrayItem("Domain")]
        public Domain[] Domains { get { return lstDomains.ToArray(); } }

        [XmlIgnore()]
        public List<Domain> lstDomains { get; set; }

        [XmlArray(ElementName = "Entities", Order = 7)]
        [XmlArrayItem("Entity")]
        public Entity[] Entities { get; set; }

        [XmlIgnore()]
        public List<Entity> lstEntities { get; set; }

        public Project()
        {
            lstDomains = new List<Domain>();
            lstEntities = new List<Entity>() ;
        }
    }
}
