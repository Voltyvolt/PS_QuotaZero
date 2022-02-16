<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmChange.aspx.cs" Inherits="PS_QuotaZero.frmChange" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="https://fonts.googleapis.com/css2?family=Prompt:wght@500&display=swap" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
     <style>
        h1{
             font-family: 'Prompt', sans-serif;
        }
        .body, .Modal{
            font-family: 'Prompt', sans-serif;
        }

        .container-center {
            height: 100%;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        table.mygridview {
            font-family: 'Prompt', sans-serif;
            text-align: center;
            border-collapse: collapse;
            width: 100%;
        }

            table.mygridview td, table.mygridview th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            table.mygridview tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            table.mygridview tr:hover {
                background-color: #ddd;
            }

            table.mygridview th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: center;
                background-color: #4CAF50;
                text-shadow: 2px 2px black;
                color: white;
            }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <header>
        <br />
        <center><h1>แก้ไขราคาผู้รับเหมาโควต้า</h1></center>
    </header>

    <br />
    <br />


    <formview>
        <div class="body">

            <div class="row form-group">
                <asp:GridView runat="server" ID="GridData" AutoGenerateColumns="false" AllowPaging="true" CssClass="mygridview" OnPageIndexChanging="GridData_PageIndexChanging">
                    <Columns>
                        <asp:BoundField ItemStyle-Width="0px" DataField="Id" HeaderText="Id" Visible="false"/>
                        <asp:BoundField ItemStyle-Width="30px" DataField="Idshow" HeaderText="#" Visible="true"/>
                        <asp:BoundField ItemStyle-Width="100px" DataField="Quota" HeaderText="โควต้า" Visible="true"/>
                         <asp:BoundField ItemStyle-Width="100px" DataField="Contractor" HeaderText="ผู้รับเหมา" Visible="true"/>
                        <asp:BoundField ItemStyle-Width="100px" DataField="Price" HeaderText="ราคา" Visible="true"/>
                        <asp:TemplateField ItemStyle-Width="120px" HeaderText="จัดการ">
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnEdit" runat="server" CssClass="btn" CommandArgument='<%# Eval("Id") %>' ToolTip="แก้ไขข้อมูล" OnClick="OnEditClick"><i class="fa fa-edit"></i></asp:LinkButton>
                                 <asp:LinkButton ID="BtnDelete" runat="server" CssClass="btn" CommandArgument='<%# Eval("Id") %>' ToolTip="ลบข้อมูล" OnClick="OnDeleteClick"><i class="fa fa-eraser"></i></asp:LinkButton>
                            </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <br />
            <br />
            <br />                    

            <div class="panel panel-default">
              <div class="panel-heading">เพิ่มแบบโควต้าเดียว</div>
              <div class="panel-body">
                  <div class="form-group row">
                        <asp:Label Text="เลขโควต้า" class="col-sm-1 col-form-label" runat="server"></asp:Label>
                        <div class="col-sm-5">
                             <asp:TextBox ID="txt_Quota" runat="server" CssClass="form-control" placeholder="โควต้า" OnTextChanged="txt_Quota_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group row">
                        <asp:Label Text="ชื่อ - สกุล" class="col-sm-1 col-form-label" runat="server"></asp:Label>
                        <div class="col-sm-5">
                             <asp:TextBox ID="txt_Name" runat="server" CssClass="form-control" placeholder="ชื่อ - สกุล" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

              </div>
            </div>

            <div class="panel panel-default">
              <div class="panel-heading">เพิ่มแบบหลายโควต้า</div>
              <div class="panel-body">
                   <div class="form-group row">
                <div class="col-sm-5">
                      <asp:CheckBox runat="server" AutoPostBack="true" ID="chk_All" CssClass="form-check-input" Visible="true" OnCheckedChanged="chk_All_CheckedChanged" />
                    <label class="form-check-label">เพิ่มแบบหลายโควต้า</label>
                </div>
            </div>

            <div class="form-group row">
                <asp:Label Text="เลขโควต้า" class="col-sm-1 col-form-label" runat="server"></asp:Label>
                <div class="col-sm-5">
                     <asp:TextBox ID="txt_Quotaall" runat="server" CssClass="form-control" placeholder="กรอกโควต้าตามด้วย ',' เช่น xxxx,xxxx" Enabled="false"></asp:TextBox>
                </div>
            </div>
              </div>
            </div>

           <div class="panel panel-default">
              <div class="panel-heading">ข้อมูลผู้รับเหมา</div>
              <div class="panel-body">
              <div class="form-group row">
                <asp:Label Text="ผู้รับเหมา" class="col-sm-1 col-form-label" runat="server"></asp:Label>
                <div class="col-sm-5">
                     <asp:DropDownList runat="server" placeholder="รหัสผู้รับเหมา" AutoPostBack="true" ID="cmb_Contractor" CssClass="form-control">

                     </asp:DropDownList>
                </div>
            </div>
              </div>
            </div>

            <div>
                 <div class="d-flex align-items-center justify-content-center">
                    <asp:Button runat="server" Text="บันทึก" ID="btnSubmit" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                    <asp:Button runat="server" Text="ยกเลิก" ID="btnCancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                </div>
            </div>
               
        </div>
        
    </formview>            
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="Modal">
        <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myModalAcc" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" Text="ตกลง" ID="btn_Accept" CssClass="btn btn-success" OnClick="OnAcceptClick" />
                                    <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">ปิด</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="Modal">
        <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myModalShow" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModalshow" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="lbshow1" runat="server" Text=""></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lbshow2" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button class="btn btn-danget" data-dismiss="modal" aria-hidden="true">ปิด</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
    </div>

     <div class="Modal">
        <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myModalUpdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="UpdateModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title"><asp:Label ID="Label1" runat="server" Text="อัพเดทข้อมูล"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                   <div class="form-group row">
                                        <asp:Label Text="เลขโควต้า" class="col-sm-2 col-form-label" runat="server"></asp:Label>
                                        <div class="col-sm-5">
                                             <asp:TextBox ID="txt_QuotaUpdate" runat="server" CssClass="form-control" placeholder="โควต้า" OnTextChanged="txt_Quota_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <asp:Label Text="ชื่อ - สกุล" class="col-sm-2 col-form-label" runat="server"></asp:Label>
                                        <div class="col-sm-5">
                                             <asp:TextBox ID="txt_FullnameUpdate" runat="server" CssClass="form-control" placeholder="ชื่อ - สกุล" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <asp:Label Text="ผู้รับเหมา" class="col-sm-2 col-form-label" runat="server"></asp:Label>
                                        <div class="col-sm-7">
                                             <asp:DropDownList runat="server" placeholder="รหัสผู้รับเหมา" AutoPostBack="true" ID="cmb_ContractorUpdate" CssClass="form-control">

                                             </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" Text="บันทึก" ID="btnUpdate" CssClass="btn btn-success" OnClick="btnUpdateClick" />
                                    <button class="btn btn-danget" data-dismiss="modal" aria-hidden="true">ปิด</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
    </div>
    
   
</asp:Content>
