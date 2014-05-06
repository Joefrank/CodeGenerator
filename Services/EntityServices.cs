using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.DomainModels;
using CodeGenerator.Services.Infrastructure;
using System.Xml;

namespace CodeGenerator.Services.Editor
{
    public class EntityServices : BaseService
    {
        public string XMLSourceFile { get; set; }

        private readonly XmlDocument _xmldoc = null;

        public EntityServices()
        {

        }

        public EntityServices(string xmlsourcefile)
        {
            XMLSourceFile = xmlsourcefile;
            _xmldoc = new XmlDocument();
            _xmldoc.Load(xmlsourcefile);
        }
     

        public void CreateEntity(string entityxml)
        {
            try
            {
                var entitiesNode = _xmldoc.SelectSingleNode("//entities");
                var xmlReader = new XmlTextReader(new StringReader(entityxml));
                var node = _xmldoc.ReadNode(xmlReader);
                entitiesNode.AppendChild(node);
                _xmldoc.Save(XMLSourceFile);
            }
            catch (Exception ex)
            {
                
            }
        }
             

        public XmlNode GetEntity(int entityid)
        {
            try
            {
                return _xmldoc.SelectSingleNode(string.Format("//entities/Entity[@Id={0}]", entityid));
                /*var entityfields = entitiesNode.SelectNodes("//Fields/Field");
                foreach (XmlNode node in entityfields)
                {
                    EntityField field = Deserialize<EntityField>(node.OuterXml);
                }*/
                
            }
            catch (Exception ex)
            {
                return null;
            }

            
        }

        public int UpdateEntity(Entity entity)
        {
            return 1;
        }
    }
}
