using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PS_QuotaZero
{
    public partial class frmChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (!Page.IsPostBack)
            {
                SelectContractorFromSQL();
                SelectDataGridView();
            }

        }

        public void SelectContractorFromSQL()
        {
            DataTable DataTable = new DataTable();
            var SelectSql = "Select P_CarContractorID, P_CarContractorName, P_CarContractorLastName From Cane_CarContractor WHERE P_CarContractorType = 'เหมา ตัดใน' ";
            DataTable = GsysSQL.fncGetQueryData(SelectSql, DataTable);

            int Datarowcount = DataTable.Rows.Count;

            cmb_Contractor.Items.Add("");

            for (int i = 0; i < Datarowcount; i++)
            {
                var DataID = DataTable.Rows[i]["P_CarContractorID"].ToString();
                var DataName = DataTable.Rows[i]["P_CarContractorName"].ToString();
                var DataLastName = DataTable.Rows[i]["P_CarContractorLastName"].ToString();
                var Result = DataID + ":" + DataName + " " + DataLastName;
                cmb_Contractor.Items.Add(Result);
            }
        }

        public void SelectDataGridView()
        {
            DataTable dataTable = new DataTable();
            var SelectSql = "Select Id, Quota, Q_FirstName, Q_LastName, Contractor, P_CarContractorName, P_CarContractorLastName From Con_ChangeZero A INNER JOIN Cane_Quota B ON A.Quota = B.Q_ID " +
                "INNER JOIN Cane_CarContractor C ON A.Contractor = C.P_CarContractorID ORDER BY Quota ASC";
            dataTable = GsysSQL.fncGetQueryData(SelectSql, dataTable);

            int Datarowcount = dataTable.Rows.Count;
            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("Id");
            dataTable2.Columns.Add("Idshow");
            dataTable2.Columns.Add("Quota");
            dataTable2.Columns.Add("Contractor");
            dataTable2.Columns.Add("Price");

            for (int i = 0; i < Datarowcount; i++)
            {
                var Id = dataTable.Rows[i]["Id"].ToString();
                var Idshow = i + 1;
                var Quota = dataTable.Rows[i]["Quota"].ToString() + ":" + dataTable.Rows[i]["Q_FirstName"].ToString() + " " + dataTable.Rows[i]["Q_LastName"].ToString();
                var Contractor = dataTable.Rows[i]["Contractor"].ToString() + ":" + dataTable.Rows[i]["P_CarContractorName"].ToString() + " " + dataTable.Rows[i]["P_CarContractorLastName"].ToString();
                var Price = "0 บาท";

                dataTable2.Rows.Add(new object[] { Id, Idshow, Quota, Contractor, Price });
            }

            GridData.DataSource = null;
            GridData.DataSource = dataTable2;
            GridData.DataBind();
        }

        protected void txt_Quota_TextChanged(object sender, EventArgs e)
        {
            var Quota = txt_Quota.Text;
            txt_Name.Text = GsysSQL.fncGetFullName(Quota);
        }

        protected void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            bool Checkaddall = chk_All.Checked;

            if (Checkaddall)
            {
                txt_Quota.Enabled = false;
                txt_Quota.Text = "";
                txt_Name.Text = "";
                txt_Quotaall.Enabled = true;
            }
            else
            {
                txt_Quota.Enabled = true;
                txt_Quotaall.Enabled = false;
            }
        }

        public void AddData()
        {
            try
            {
                bool Checkaddall = chk_All.Checked;

                if (Checkaddall)
                {
                    string[] Quotasplit = txt_Quotaall.Text.Split(',');
                    string[] Contractorsplit = cmb_Contractor.Text.Split(':');

                    for (int i = 0; i < Quotasplit.Length; i++)
                    {
                        int Quota = GsysFunc.fncToInt(Quotasplit[i]);
                        int Contractor = GsysFunc.fncToInt(Contractorsplit[0]);
                        string AddDatasql = "INSERT INTO Con_ChangeZero (Quota, Contractor) Values ('" + Quota + "', '" + Contractor + "')";
                        string Result = GsysSQL.fncExecuteQueryData(AddDatasql);
                    }
                }
                else if (txt_Quota.Text != "" && !Checkaddall)
                {
                    int quota = GsysFunc.fncToInt(txt_Quota.Text);
                    string[] Contractorsplit = cmb_Contractor.Text.Split(':');
                    int Contractor = GsysFunc.fncToInt(Contractorsplit[0]);
                    string AddDatasql = "INSERT INTO Con_ChangeZero (Quota, Contractor) Values ('" + quota + "', '" + Contractor + "')";
                    string Result = GsysSQL.fncExecuteQueryData(AddDatasql);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                myModalPopupShow("แจ้งเตือน", ex.ToString());
            }

        }

        public void UpdateData()
        {
            try
            {
                int Quota = GsysFunc.fncToInt(txt_QuotaUpdate.Text);
                string[] ContractorSplit = cmb_ContractorUpdate.Text.Split(':');
                int Contractor = GsysFunc.fncToInt(ContractorSplit[0]);

                string lvSQL = "Update Con_ChangeZero SET Quota = '" + Quota + "', Contractor = '" + Contractor + "' WHERE Id = '" + GVar.gvId + "' ";
                string result = GsysSQL.fncExecuteQueryData(lvSQL);
                
            }
            catch (Exception ex)
            {
                myModalPopupShow("แจ้งเตือน", ex.ToString());
            }
        }

        public void OnDeleteClick(object sender, EventArgs e)
        {
            GVar.gvMode = "del";
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            string QuotaId = id.ToString();
            GVar.gvId = QuotaId;
            myModalPopupAcc("ยืนยันการลบข้อมูล", "ต้องการลบข้อมูลใช่หรือไม่?");
        }

        public void DeleteData()
        {
            try
            {
                string lvSQL = "Delete From Con_ChangeZero WHERE Id = '" + GVar.gvId + "' ";
                string result = GsysSQL.fncExecuteQueryData(lvSQL);
            }
            catch (Exception ex)
            {
                myModalPopupShow("แจ้งเตือน", ex.ToString());
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool Checkaddall = chk_All.Checked;
            GVar.gvMode = "new";

            if (txt_Quota.Text == "" && !Checkaddall)
            {
                myModalPopupShow("แจ้งเตือน", "กรุณากรอกข้อมูลโควต้าก่อน!");
                GVar.gvMode = "";
                return;
            }
            else if (Checkaddall && cmb_Contractor.Text == "")
            {
                myModalPopupShow("แจ้งเตือน", "กรุณากรอกข้อมูลโควต้าก่อน!");
                GVar.gvMode = "";
                return;
            }
            else
            {
                myModalPopupAcc("ยืนยันการบันทึกข้อมูล", "ต้องการบันทึกข้อมูลใช่หรือไม่?");
            }


        }

        public void myModalPopupAcc(string lvModalHeader, string lvModalBody)
        {
            lblModalTitle.Text = lvModalHeader;
            lblModalBody.Text = lvModalBody;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalAcc", "$('#myModalAcc').modal('show');", true);
            upModal.Update();
        }

        public void myModalPopupShow(string lvModalHeader, string lvModalBody)
        {
            lbshow1.Text = lvModalHeader;
            lbshow2.Text = lvModalBody;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalShow", "$('#myModalShow').modal('show');", true);
            upModalshow.Update();
        }

        protected void OnAcceptClick(object sender, EventArgs e)
        {
            if(GVar.gvMode == "new")
            {
                AddData();
            }
            else if(GVar.gvMode == "del")
            {
                DeleteData();
            }
            else
            {

            }
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalAcc", "$('#myModalAcc').modal('hide');", true);
            upModal.Update();
            myModalPopupShow("แจ้งเตือน", "บันทึกข้อมูลสำเร็จ!");
            ClearData();
            SelectDataGridView();
        }

        public void ClearData()
        {
            txt_Quota.Enabled = true;
            txt_Quota.Text = "";
            txt_Name.Text = "";
            chk_All.Checked = false;
            txt_Quotaall.Enabled = false;
            txt_Quotaall.Text = "";
            cmb_Contractor.Text = "";
            txt_QuotaUpdate.Text = "";
            cmb_ContractorUpdate.Text = "";
            GVar.gvMode = "";
            GVar.gvId = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void GridData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelectDataGridView();
            GridData.PageIndex = e.NewPageIndex;
            GridData.DataBind();
        }

        protected void OnEditClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            string QuotaId = id.ToString();
            GVar.gvId = QuotaId;
            GVar.gvMode = "edit";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalUpdate", "$('#myModalUpdate').modal('show');", true);
            UpdateModal.Update();

            selectToContractorUpdate();

            DataTable DT = new DataTable();
            string selectupdate = "Select Id, Quota, Q_FirstName, Q_LastName, Contractor, P_CarContractorName, P_CarContractorLastName From Con_ChangeZero A INNER JOIN Cane_Quota B ON A.Quota = B.Q_ID " +
                "INNER JOIN Cane_CarContractor C ON A.Contractor = C.P_CarContractorID WHERE Id = '" + QuotaId + "' ";
            DT = GsysSQL.fncGetQueryData(selectupdate, DT);

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                txt_QuotaUpdate.Text = DT.Rows[i]["Quota"].ToString();
                txt_FullnameUpdate.Text = DT.Rows[i]["Q_FirstName"].ToString() + " " + DT.Rows[i]["Q_LastName"].ToString(); ;
                cmb_ContractorUpdate.Text = DT.Rows[i]["Contractor"].ToString() + ":" + DT.Rows[i]["P_CarContractorName"].ToString() + " " + DT.Rows[i]["P_CarContractorLastName"].ToString();
            }
        }

        private void selectToContractorUpdate()
        {
            DataTable DataTable = new DataTable();
            var SelectSql = "Select P_CarContractorID, P_CarContractorName, P_CarContractorLastName From Cane_CarContractor WHERE P_CarContractorType = 'เหมา ตัดใน' ";
            DataTable = GsysSQL.fncGetQueryData(SelectSql, DataTable);

            int Datarowcount = DataTable.Rows.Count;

            cmb_ContractorUpdate.Items.Add("");

            for (int i = 0; i < Datarowcount; i++)
            {
                var DataID = DataTable.Rows[i]["P_CarContractorID"].ToString();
                var DataName = DataTable.Rows[i]["P_CarContractorName"].ToString();
                var DataLastName = DataTable.Rows[i]["P_CarContractorLastName"].ToString();
                var Result = DataID + ":" + DataName + " " + DataLastName;
                cmb_ContractorUpdate.Items.Add(Result);
            }
        }

        protected void btnUpdateClick(object sender, EventArgs e)
        {
            UpdateData();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalUpdate", "$('#myModalUpdate').modal('hide');", true);
            UpdateModal.Update();

            myModalPopupShow("แจ้งเตือน", "แก้ไขข้อมูลสำเร็จ!");
            SelectDataGridView();
            ClearData();
        }
    }
    
}