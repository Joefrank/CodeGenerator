using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeGenerator.Models;
using CodeGenerator.Services.Editor;
using CodeGenerator.Services;
using CodeGenerator.DomainModels;

namespace CodeGenerator.Controllers
{
    public class EditorController : Controller
    {
        private  EntityServices _entityService;
        private ProjectService _projservice;

        private const string _editormainfile = @"~/Resources/Entities.xml";

        public static string EntityFilePath
        {
            get; set;           
        }

        public EditorController()
        {           
            _entityService = new EntityServices();
            _projservice = new ProjectService();
        }

        public string GetFileFromContext(string filename)
        {
            return ControllerContext.HttpContext.Server.MapPath(filename);
        }

        public ActionResult Index()
        {
           
            //_entityService.XMLSourceFile = GetFileFromContext(_editormainfile);

           Project project =  (Project)_projservice.DeserializeObject<Project>(GetFileFromContext(_editormainfile));

            return View();
        }

      
        public ActionResult ManageEntities()
        {
            /* just testing entityid should be dynamic */
            return View();
        }

        public ActionResult EditEntity(int id)
        {
            _entityService = new EntityServices(GetFileFromContext(_editormainfile));

            //XmlNode node = _entityService.GetEntity(id);
            return View(new EntityMashupViewModel { EntityId = id, EntityFieldXML = string.Empty });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveEntity(EntityMashupViewModel mashup)
        {
            _entityService = new EntityServices(GetFileFromContext(_editormainfile));
           
             var contenttosave = string.Format(@"<Entity Id=""{0}""><Fields>{1}</Fields></Entity>", mashup.EntityId, mashup.EntityFieldXML);
            _entityService.CreateEntity(contenttosave);
            return View("EditEntity", mashup);
        }
    }
}
