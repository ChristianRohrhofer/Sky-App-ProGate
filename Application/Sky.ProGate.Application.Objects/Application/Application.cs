
using System;
using System.Collections.Generic;
using Sky.Library.SBM;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Objects
{
    public class Application : SBMObject
    {
        #region Members

        protected Server Server { get; set; } = null;
        protected Calendar Calendar { get; set; } = null;
        protected SBMObjectList<Category> Categories { get; set; } = null;
        protected SBMObjectList<LineItem> LineItems { get; set; } = null;
        protected SBMObjectList<CostCenter> CostCenters { get; set; } = null;
        protected SBMObjectList<Person> Ressources { get; set; } = null;
        protected SBMObjectList<Project> Projects { get; set; } = null;

        public Application(string sSvrName, string sAccName, string sPwd)
            : base()
        {
            //--- Set the applicatinon and login to the server
            Application = this;
            Login(sSvrName, sAccName, sPwd);
        }

        public void Dispose()
        {
            //--- Dispose teh server if exists
            if (Server != null)
                Server.Logout();
        }

        #endregion Members

        #region Server

        public Server GetServer()
        { return Server == null ? Server = new Server() : Server; }

        public void Login(string sSvrName, string sAccName, string sPwd)
        { Server = new Server(sSvrName, sAccName, sPwd); }

        public void Logout()
        { Server.Logout(); }

        #endregion Server

        #region Objects

        public Calendar GetCalendar()
        { return Calendar = Calendar == null ? Calendar = new Calendar(this) : Calendar; }

        public SBMObjectList<Category> GetCategories()
        { return Categories = ReadObjects(Categories, Category.TabName_Cat); }

        public SBMObjectList<LineItem> GetLineItems()
        { return LineItems = ReadObjects(LineItems, LineItem.TabName_LnItem); }

        public SBMObjectList<CostCenter> GetCostCenters()
        { return CostCenters = ReadObjects(CostCenters, CostCenter.TabName_CosCen); }

        public SBMObjectList<Person> GetRessources()
        { return Ressources = ReadObjects(Ressources, Person.TabName_Per); }

        public SBMObjectList<Project> GetProjects()
        { return Projects = ReadObjects(Projects, Project.TabName_Prj); }

        #endregion Objects
    }
}
