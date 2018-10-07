
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Forms
{
    public partial class ProjectPlanningGrid : DataGridView
    {
        #region Members

        protected const int
            ColWdt_Ctrl = 20,
            ColWdt_Hdr = 150,
            ColWdt_Pln = 30,
            RowHgt_Cal = 30,
            RowHgt_Pln = 30,
            FonHgt_Grd = 8,
            ColIdx_Ctrl = 0,
            ColIdx_Hdr = 1;

        protected const string
            FonName_Tah = "Tahoma";

        protected static Color
            CellCol_Ctrl = Color.DarkGray,
            CellCol_Hdr = Color.DarkGray,
            CellCol_CalWk_Now = Color.Salmon,
            CellCol_CalWk_Oth = Color.LightSalmon;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected static Font ValueFont = new Font(FonName_Tah, FonHgt_Grd, FontStyle.Regular);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected static Font ControlFont = new Font(FonName_Tah, FonHgt_Grd, FontStyle.Bold);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected int FirstPlanningIndex { get; set; } = Global.IntegerNull;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected int LastPlanningIndex { get; set; } = Global.IntegerNull;

        public ProjectPlanningGrid()
        { InitializeComponent(); }

        public void InitCalendarGrid(Calendar Cal, DateTime dtFirstDay, DateTime dtLastDay)
        {
            DataGridViewColumn Col = null;
            DataGridViewRow Row = null;
            int nNowPlnIdx = -1;

            //--- Clear the rows and the columns
            Rows.Clear();
            Columns.Clear();

            //--- Get the first and last planning index
            FirstPlanningIndex = GetPlanningIndex(Cal, dtFirstDay);
            LastPlanningIndex = GetPlanningIndex(Cal, dtLastDay);
            nNowPlnIdx = GetPlanningIndex(Cal, DateTime.Now);

            //--- Add the control column
            Col = AddColumn(ColWdt_Ctrl, false, true);
            //...

            //--- Add the header column
            Col = AddColumn(ColWdt_Hdr, true, true);
            //...

            //--- Add the planning columns
            for (int nIdx = FirstPlanningIndex; nIdx <= LastPlanningIndex; nIdx++)
            {
                Col = AddColumn(ColWdt_Pln, false, false);
                //...
            }

            //--- Add the calendar week row
            Row = AddRow(RowHgt_Cal, true, CellCol_CalWk_Oth, true);
            Row.Cells[nNowPlnIdx].Style.BackColor = CellCol_CalWk_Now;
            

            Row.Tag = new CalendarRow(Cal);
        }

        protected int GetPlanningIndex(Calendar Cal, DateTime dtDat)
        { return Cal.GetCalendarWeeks().IndexOf(Cal.GetCalendarWeekByDate(dtDat)); }

        #endregion Members

        #region Forms

        public void ExpandCalendarRow()
        {
        }

        public void CollapseCalendarRow()
        {
        }

        public void AddProjectRow(Project Prj)
        {
        }

        public void InsertProjectRow(Project Prj, int nIdx)
        {
        }

        public void RemoveProjectRow(Project Prj)
        {
        }

        public void RemoveProjectRow(DataGridViewRow Row)
        { RemoveProjectRow((Project)Row.Tag); }

        public void ExpandProjectRow(Project Prj)
        {
            DataGridViewRow Row = null;

            Row = (DataGridViewRow)Prj.Tag;
            //....
        }

        public void ExpandProjectRow(DataGridViewRow Row)
        { ExpandProjectRow(((ProjectRow)Row.Tag).Project); }

        public void CollapseProjectRow(Project Prj)
        {

        }

        public void CollapsPerojectRow(DataGridViewRow Row)
        { CollapseProjectRow((Project)Row.Tag); }


        public void AddPlanningRow(Planning Pln)
        {
            DataGridViewRow PrjRow = null;

            PrjRow = (DataGridViewRow)Pln.Parent.Tag;


        }

        protected DataGridViewColumn AddColumn(int nWdt, bool bRes, bool bFroz)
        {
            DataGridViewColumn Col = null;
            int nIdx;

            //--- Add and get the column
            nIdx = Columns.Add(null, null);
            Col = Columns[nIdx];

            //--- Set the column properties
            Col.Width = nWdt;
            Col.Resizable = bRes ? DataGridViewTriState.True : DataGridViewTriState.False;
            Col.Frozen = bFroz;
            Col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            return Col;
        }

        protected DataGridViewRow AddRow(int nHgt, bool bFroz, Color PlnCellCol, bool bPlanCellReadOnly)
        {
            DataGridViewRow Row = null;
            DataGridViewCell Cell = null;
            int nRowIdx;

            //--- Add the row
            nRowIdx = Rows.Add(null, null);
            Row = Rows[nRowIdx];

            //--- Set the row properties
            Row.Height = nHgt;
            Row.Resizable = DataGridViewTriState.False;
            Row.Frozen = bFroz;
            Row.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            for (int nIdx = 0; nIdx < Row.Cells.Count; nIdx++)
            {
                Cell = Row.Cells[nIdx];

                if (nIdx == ColIdx_Ctrl)
                {
                    Cell.Style.Font = ValueFont;
                    Cell.ReadOnly = true;
                    Cell.Style.BackColor = CellCol_Ctrl;
                }
                else if (nIdx == ColIdx_Hdr)
                {
                    Cell.Style.Font = ValueFont;
                    Cell.ReadOnly = true;
                    Cell.Style.BackColor = CellCol_Hdr;
                }
                else
                {
                    Cell.Style.Font = ValueFont;
                    Cell.ReadOnly = bPlanCellReadOnly;
                    Cell.Style.BackColor = PlnCellCol;
                }
            }

            return Row;
        }

        #endregion Forms
    }

    public class AppObjectRow
    {
        public enum enExpandMode { None = 0, Collapsed, Expanded }

        public enExpandMode ExpandMode { get; protected set; } = enExpandMode.None;

        public AppObjectRow()
        { }
    }

    public class CalendarRow : AppObjectRow
    {
        public Calendar Calendar { get; protected set; } = null;

        public CalendarRow(Calendar Cal)
        { Calendar = Cal; }
    }

    public class ProjectRow : AppObjectRow
    {
        public Project Project { get; protected set; } = null;
    }


}



