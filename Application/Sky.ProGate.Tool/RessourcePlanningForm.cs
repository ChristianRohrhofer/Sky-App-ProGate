
using System;
using System.Windows.Forms;
using System.Drawing;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Tool
{
    public partial class RessourcePlanningForm : Form
    {
        #region Members

        protected Application.Objects.Application Application { get; set; } = null;

        public RessourcePlanningForm()
        { InitializeComponent(); }

        #endregion Members

        #region Events

        protected void OnFormLoad(object sender, EventArgs e)
        { Init(); }

        protected void OnExit(object sender, EventArgs e)
        { Exit(); }

        protected void OnAddRow(object sender, EventArgs e)
        { AddRow(); }

        protected void OnRemoveRow(object sender, EventArgs e)
        { RemoveRow(); }

        protected void OnExpand(object sender, EventArgs e)
        { Expand(); }

        protected void OnCollapse(object sender, EventArgs e)
        { Collapse(); }


        #endregion Events


        protected void Init()
        {

            Application = new Application.Objects.Application("p-m-sbm3.pfad.biz", "sky_orga_admin", "skyorgaadmin");

            InitFoldergrid();
            InitProjectPlanningGrid();

        }

        protected void Exit()
        {
            Application?.Dispose();
            Close();
        }

        protected void AddRow()
        {
            DataGridViewColumn Col = null;

            ProjectFolderGrid.AddRow(Color.LavenderBlush);


        }

        protected void RemoveRow()
        {
            
        }

        protected void Expand()
        {

        }

        protected void Collapse()
        {

        }




        protected void InitFoldergrid()
        {
            ProjectFolderGrid.InitGrid(20, 150, 30, 30, Color.LightSteelBlue);

            for (int nIdx = 2; nIdx < ProjectFolderGrid.Rows[0].Cells.Count; nIdx++)
                ProjectFolderGrid.Rows[0].Cells[nIdx].Value = nIdx.ToString();
        }

        protected void InitProjectPlanningGrid()
        {
            FormProjectPlanningGrid.InitCalendarGrid(Application.GetCalendar(), new DateTime(2016, 2, 1), new DateTime(2018, 12, 31));

        }



        protected void TEST()
        {
            SBMObjectList<Project> PrjLst = null;
            AppObjectList<CalendarWeek> CalWkLst;
            Project Prj;
            Category Cat;
            LineItem LnItem;
            CostCenter CosCen;
            CalendarWeek CalWk;
            SBMObjectList<Planning> PlnLst = null;
            Planning Pln;
            PlanningRate PlnRat;
            object objVal;

            Application.GetCategories();

            PrjLst = Application.GetProjects();

            for (int nPrjIdx = 0; nPrjIdx < PrjLst.Count; nPrjIdx++)
            {
                Prj = PrjLst[nPrjIdx];
                Cat = Prj.GetCategory();
                LnItem = Prj.GetLineItem();
                CosCen = Prj.GetCostCenter();

                if (Prj.GetNumber() == 500097)
                {
                    PlnLst = Prj.GetPlannings();

                    for (int nPlnIdx = 0; nPlnIdx < PlnLst.Count; nPlnIdx++)
                    {
                        Pln = PlnLst[nPlnIdx];

                        for (int nRatIdx = 0; nRatIdx < Pln.GetPlanningRates().Count; nRatIdx++)
                        {
                            PlnRat = Pln.GetPlanningRates().GetAt(nRatIdx);
                            CalWk = PlnRat.GetCalendarWeek();
                        }

                    }
                }
            }

            CalWkLst = Application.GetCalendar().GetCalendarWeeks();

            for (int nIdx = 0; nIdx < CalWkLst.Count; nIdx++)
            {
                CalWk = CalWkLst[nIdx];
                objVal = CalWk.FirstDay;
                objVal = CalWk.LastDay;
                objVal = CalWk.OrderIndex;
            }
        }
    }


}
