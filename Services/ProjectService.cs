using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CodeGenerator.Services.Infrastructure;
using CodeGenerator.DomainModels;
using System.IO;
using System.Xml;

namespace CodeGenerator.Services
{
    public class MyNullable { public string Error { get; set; } }

    public class ProjectService : BaseService
    {

        public Project CreateBlankProject(string name, int id )
        {
            Project project = new Project();
            project.Name = name;
            project.Id = id;
            project.TopDomainSeed = 1;
            project.TopEntitySeed = 1;
            project.TopFieldSeed = 1;
            project.CreatedOn = DateTime.Now;

            Domain domain = new Domain();
            domain.Id = 1;
            domain.Name = "Initial";
            domain.ParentDomainId = 0;
            domain.Description = string.Empty;

            Entity entity = new Entity();
            entity.Id = 1;
            entity.Name = "Dummy Entity";            
            entity.Description = string.Empty;
            entity.ParentDomainId = 1;

            EntityField field = new EntityField();
            field.Id = 1;
            field.Name = "Dummy Field";
            field.Description = string.Empty;
            field.Type = 0;
            field.DataType = 0;
            field.Mandatory = false;
            field.ParentEntityId = 1;
            field.Parent = entity;

            entity.LstFields.Add(field);

            project.lstEntities.Add(entity);
            project.lstDomains.Add(domain);

            return project;
        }


        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
            }
        }


        public object DeserializeObject<T>(string filename) 
        {
            FileStream fs = null;

            try
            {
                if (new FileInfo(filename).Length < 20)
                {
                    Project project = CreateBlankProject("Dummy Project", 1);
                    SerializeObject<Project>(project, filename);
                    return project;
                }

                // Create an instance of the XmlSerializer specifying type and namespace.
                XmlSerializer serializer = new
                XmlSerializer(typeof(T));

                // A FileStream is needed to read the XML document.
                fs = new FileStream(filename, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);

                // Declare an object variable of the type to be deserialized.
                var projectx = (T)serializer.Deserialize(reader);
                fs.Close();
                return projectx;
            }
            catch (Exception e)
            {
                return (new MyNullable(){Error = e.Message});
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

        }

    }
}
