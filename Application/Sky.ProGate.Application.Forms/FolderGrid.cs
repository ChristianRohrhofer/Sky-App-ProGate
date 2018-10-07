
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sky.Library.Forms;
using Sky.ProGate.Application.Objects;


namespace Sky.ProGate.Application.Forms
{
    public partial class FolderGrid : TableDataGridView
    {
        protected List<DataGridViewRow> RowList { get; set; } = null;
        protected int ColumnWidth{ get; set; } = Global.IntegerNull;
        protected int RowHeight{ get; set; } = Global.IntegerNull;
        protected bool Expanded { get; set; } = Global.BooleanNull;

        protected static Font HeaderFont = new Font("Tahoma", 8f, FontStyle.Bold);
        protected static Font ValueFont = new Font("Tahoma", 8f, FontStyle.Regular);

        public FolderGrid()
        { InitializeComponent(); }

        public void InitGrid(int nColCnt, int nHdrColWdt, int nValColWdt, int nRowHgt, Color HdrCol)
        {
            ColumnWidth = nValColWdt;
            RowHeight = nRowHgt;

            RowList = new List<DataGridViewRow>();
            Expanded = true;

            InitGrid(nColCnt + 2, 1);

            UpdateColumn(Columns[0], 20, false, true);
            UpdateColumn(Columns[1], nHdrColWdt, true, true);

            UpdateRow(Rows[0], RowHeight, false, true);
            UpdateCells(Rows[0], true, HdrCol, HeaderFont, DataGridViewContentAlignment.MiddleCenter);

            for (int nIdx = 2; nIdx < nColCnt; nIdx++)
                UpdateColumn(Columns[nIdx], ColumnWidth, false, nIdx < 2);
        }

        public void AddRow(Color ValCol)
        {
            DataGridViewRow Row = null;

            Row = AddRow();

            UpdateRow(Row, RowHeight, false, false);
            UpdateCells(Row, false, ValCol, ValueFont, DataGridViewContentAlignment.MiddleRight);

            if (!Expanded)
            {
                Rows.RemoveAt(1);
                RowList.Add(Row);
            }
        }

        protected void UpdateCells(DataGridViewRow Row, bool bReadOnly, Color BkCol, Font Font, DataGridViewContentAlignment Alig)
        {
            DataGridViewCell Cell = null;

            for (int nIdx = 0; nIdx < Row.Cells.Count; nIdx++)
            {
                Cell = Row.Cells[nIdx];
                Cell.Style.Font = Font;

                UpdateCell(Cell, nIdx < 2 ? false : bReadOnly, BkCol, nIdx == 0 ? DataGridViewContentAlignment.MiddleCenter : nIdx == 1 ? DataGridViewContentAlignment.MiddleLeft : Alig, null);
                Cell.Value = nIdx == 0 ? ((char)0x95).ToString() : null;
            }
        }

        protected void Expand()
        {

        }

        protected void Collapse()
        {

        }

    }


}
