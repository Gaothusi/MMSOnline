Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmStats
    Private binderset As New BindingSource
    Private dataset As New DataSet
    Private datatable, tempset, collectionset As New DataTable
    Private name, temp As String
    Private fin, ins, pt, at, atf, ext, deals, prop, tire, ski, cover, rock, lock, safe, twentyhr, winterize, gross, grossf, grossc As Decimal
    Dim loaded As Boolean = False

    Public persistant As PreFetch

    Private Sub frmStats_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If persistant.myuserLEVEL > 3 Then
            cmbviewby.SelectedItem = "Finance Manager"
        Else
            cmbviewby.Enabled = False
            cmbviewby.SelectedItem = "Salesman"
        End If

        DateStart.Value = CDate(Now.Month.ToString + " 1 " + Now.Year.ToString)
        DateEnd.Value = Now
        dataset.Tables.Add(datatable)
        binderset.DataSource = dataset
        binderset.DataMember = dataset.Tables(0).TableName
        DataGridView1.DataSource = binderset
        getcollection()

        loaded = True

        'refreshdata()
    End Sub

    Private Sub refreshdata()


        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        conn.ConnectionString = persistant.myconnstring
        cmd.Connection = conn
        conn.Open()

        datatable.Clear()
        While datatable.Columns.Count > 0
            datatable.Columns.RemoveAt(0)
        End While

        Try
            If cmbviewby.SelectedItem = "Finance Manager" Then
                datatable.Columns.Add("Name", System.Type.GetType("System.String"))
                datatable.Columns.Add("Deals", System.Type.GetType("System.String"))
                datatable.Columns.Add("Financed", System.Type.GetType("System.String"))
                datatable.Columns.Add("Insured", System.Type.GetType("System.String"))
                datatable.Columns.Add("Ext Warranty", System.Type.GetType("System.String"))
                datatable.Columns.Add("Protech", System.Type.GetType("System.String"))
                datatable.Columns.Add("Anti-theft", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross - F", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross - C", System.Type.GetType("System.String"))
                datatable.Columns.Add("Total Gross", System.Type.GetType("System.String"))
            End If

            If cmbviewby.SelectedItem = "Store" Then
                datatable.Columns.Add("Name", System.Type.GetType("System.String"))
                datatable.Columns.Add("Deals", System.Type.GetType("System.String"))
                datatable.Columns.Add("Financed", System.Type.GetType("System.String"))
                datatable.Columns.Add("Insured", System.Type.GetType("System.String"))
                datatable.Columns.Add("Ext Warranty", System.Type.GetType("System.String"))
                datatable.Columns.Add("Protech", System.Type.GetType("System.String"))
                datatable.Columns.Add("Anti-theft", System.Type.GetType("System.String"))
                datatable.Columns.Add("S. Tire", System.Type.GetType("System.String"))
                datatable.Columns.Add("Cover", System.Type.GetType("System.String"))
                datatable.Columns.Add("Ski Pkg", System.Type.GetType("System.String"))
                datatable.Columns.Add("Rock Guard", System.Type.GetType("System.String"))
                datatable.Columns.Add("S. Prop", System.Type.GetType("System.String"))
                datatable.Columns.Add("Safety Pkg", System.Type.GetType("System.String"))
                datatable.Columns.Add("Lock pkg", System.Type.GetType("System.String"))
                datatable.Columns.Add("20 Hr", System.Type.GetType("System.String"))
                datatable.Columns.Add("Winterize", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross - F", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross - C", System.Type.GetType("System.String"))
            End If

            If cmbviewby.SelectedItem = "Salesman" Then
                datatable.Columns.Add("Name", System.Type.GetType("System.String"))
                datatable.Columns.Add("Deals", System.Type.GetType("System.String"))
                datatable.Columns.Add("S. Tire", System.Type.GetType("System.String"))
                datatable.Columns.Add("Cover", System.Type.GetType("System.String"))
                datatable.Columns.Add("Ski Pkg", System.Type.GetType("System.String"))
                datatable.Columns.Add("Rock Guard", System.Type.GetType("System.String"))
                datatable.Columns.Add("S. Prop", System.Type.GetType("System.String"))
                datatable.Columns.Add("Safety Pkg", System.Type.GetType("System.String"))
                datatable.Columns.Add("Lock pkg", System.Type.GetType("System.String"))
                datatable.Columns.Add("20 Hr", System.Type.GetType("System.String"))
                datatable.Columns.Add("Winerize", System.Type.GetType("System.String"))
                datatable.Columns.Add("Financed", System.Type.GetType("System.String"))
                datatable.Columns.Add("Insured", System.Type.GetType("System.String"))
                datatable.Columns.Add("Ext Warranty", System.Type.GetType("System.String"))
                datatable.Columns.Add("Protech", System.Type.GetType("System.String"))
                datatable.Columns.Add("Anti-theft", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross - F", System.Type.GetType("System.String"))
                datatable.Columns.Add("AVG Gross - C", System.Type.GetType("System.String"))
            End If
 
        Catch ex As Exception
        End Try

        For x As Integer = 0 To collectionset.Rows.Count - 1
            Try
                name = collectionset.Rows(x).Item(0)
            Catch ex As Exception
                name = ""
            End Try



            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            deals = cmd.ExecuteScalar


            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND fincode > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND fincode > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND fincode > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            fin = cmd.ExecuteScalar


            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND inssold > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND inssold > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND inssold > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            ins = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND protech_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND protech_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND protech_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            pt = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND Ext_warranty_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND Ext_warranty_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND Ext_warranty_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            ext = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND antitheft_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND antitheft_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND antitheft_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            at = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND s_tire_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND s_tire_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND s_tire_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            tire = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND cover_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND cover_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND cover_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            cover = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND ski_pack_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND ski_pack_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND ski_pack_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            ski = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND rock_guard_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND rock_guard_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND rock_guard_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            rock = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND s_prop_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND s_prop_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND s_prop_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            prop = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND safety_pkg_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND safety_pkg_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND safety_pkg_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            safe = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND Lock_pkg_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND Lock_pkg_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND Lock_pkg_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            lock = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND 20_Hour_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND 20_Hour_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND 20_Hour_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            twentyhr = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND winterize_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND winterize_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND winterize_price > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
            End If
            winterize = cmd.ExecuteScalar

            If cmbviewby.SelectedItem = "Finance Manager" Then
                cmd.CommandText = "SELECT count(store) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND antitheft_price > 1 and fincode > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                atf = cmd.ExecuteScalar

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where bizman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    gross = CDec(cmd.ExecuteScalar)
                    gross = gross - (at * 38)
                Catch ex As Exception
                    gross = 0
                End Try

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where bizman = '" + name + "' AND fincode > 1 and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    grossf = CDec(cmd.ExecuteScalar)
                    grossf = grossf - (atf * 38)
                Catch ex As Exception
                    grossf = 0
                End Try

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where bizman = '" + name + "' AND fincode = 1 and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    grossc = CDec(cmd.ExecuteScalar)
                    grossc = grossc - ((at - atf) * 38)
                Catch ex As Exception
                    grossc = 0
                End Try
            End If

            If cmbviewby.SelectedItem = "Store" Then
                cmd.CommandText = "SELECT count(store) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND antitheft_price > 1 and fincode > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                atf = cmd.ExecuteScalar

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where store = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    gross = CDec(cmd.ExecuteScalar)
                    gross = gross - (at * 38)
                Catch ex As Exception
                    gross = 0
                End Try

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where store = '" + name + "' AND fincode > 1 and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    grossf = CDec(cmd.ExecuteScalar)
                    grossf = grossf - (atf * 38)
                Catch ex As Exception
                    grossf = 0
                End Try

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where store = '" + name + "' AND fincode = 1 and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    grossc = CDec(cmd.ExecuteScalar)
                    grossc = grossc - ((at - atf) * 38)
                Catch ex As Exception
                    grossc = 0
                End Try
            End If

            If cmbviewby.SelectedItem = "Salesman" Then
                cmd.CommandText = "SELECT count(store) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 AND antitheft_price > 1 and fincode > 1 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                atf = cmd.ExecuteScalar

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where salesman = '" + name + "' and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    gross = CDec(cmd.ExecuteScalar)
                    gross = gross - (at * 38)
                Catch ex As Exception
                    gross = 0
                End Try

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where salesman = '" + name + "' AND fincode > 1 and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    grossf = CDec(cmd.ExecuteScalar)
                    grossf = grossf - (atf * 38)
                Catch ex As Exception
                    grossf = 0
                End Try

                cmd.CommandText = "SELECT ((SUM(Antitheft_price) + SUM(Protech_price) + SUM(Ext_warranty_price) + SUM(RESERVE) + SUM(financefee) + (SUM(inssold) / 2))-(SUM(warranty_cost) + SUM(PTcost))) from bos where salesman = '" + name + "' AND fincode = 1 and Status IN  ('4', '5', '7', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20') AND total >  10000 and date_delivered >= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateStart.Value.ToShortDateString), "yyyy-MM-dd") + "' and date_delivered <= '" + Microsoft.VisualBasic.Strings.Format(CDate(DateEnd.Value.ToShortDateString), "yyyy-MM-dd") + "'"
                Try
                    grossc = CDec(cmd.ExecuteScalar)
                    grossc = grossc - ((at - atf) * 38)
                Catch ex As Exception
                    grossc = 0
                End Try
            End If

            If deals > 0 Then
                If fin = 0 Then fin = 1
                Select Case cmbviewby.SelectedItem
                    Case Is = "Finance Manager"
                        If deals - fin = 0 Then
                            Dim temp() As String = {name, deals.ToString, Format((fin / deals), "Percent"), Format((ins / fin), "Percent"), Format((ext / deals), "Percent"), Format((pt / deals), "Percent"), Format((at / deals), "Percent"), Format((gross / deals), "Currency"), Format((grossf / fin), "Currency"), Format((0), "Currency"), Format((gross), "Currency")}
                            datatable.Rows.Add(temp)
                        Else
                            Dim temp() As String = {name, deals.ToString, Format((fin / deals), "Percent"), Format((ins / fin), "Percent"), Format((ext / deals), "Percent"), Format((pt / deals), "Percent"), Format((at / deals), "Percent"), Format((gross / deals), "Currency"), Format((grossf / fin), "Currency"), Format((grossc / (deals - fin)), "Currency"), Format((gross), "Currency")}
                            datatable.Rows.Add(temp)
                        End If

                    Case Is = "Store"
                        If deals - fin = 0 Then
                            Dim temp() As String = {name, deals.ToString, Format((fin / deals), "Percent"), Format((ins / fin), "Percent"), Format((ext / deals), "Percent"), Format((pt / deals), "Percent"), Format((at / deals), "Percent"), Format((tire / deals), "Percent"), Format((cover / deals), "Percent"), Format((ski / deals), "Percent"), Format((rock / deals), "Percent"), Format((prop / deals), "Percent"), Format((safe / deals), "Percent"), Format((lock / deals), "Percent"), Format((twentyhr / deals), "Percent"), Format((winterize / deals), "Percent"), Format((gross / deals), "Currency"), Format((grossf / fin), "Currency"), Format((0), "Currency")}
                            datatable.Rows.Add(temp)
                        Else
                            Dim temp() As String = {name, deals.ToString, Format((fin / deals), "Percent"), Format((ins / fin), "Percent"), Format((ext / deals), "Percent"), Format((pt / deals), "Percent"), Format((at / deals), "Percent"), Format((tire / deals), "Percent"), Format((cover / deals), "Percent"), Format((ski / deals), "Percent"), Format((rock / deals), "Percent"), Format((prop / deals), "Percent"), Format((safe / deals), "Percent"), Format((lock / deals), "Percent"), Format((twentyhr / deals), "Percent"), Format((winterize / deals), "Percent"), Format((gross / deals), "Currency"), Format((grossf / fin), "Currency"), Format((grossc / (deals - fin)), "Currency")}
                            datatable.Rows.Add(temp)
                        End If

                    Case Is = "Salesman"
                        If deals - fin = 0 Then
                            Dim temp() As String = {name, deals.ToString, Format((tire / deals), "Percent"), Format((cover / deals), "Percent"), Format((ski / deals), "Percent"), Format((rock / deals), "Percent"), Format((prop / deals), "Percent"), Format((safe / deals), "Percent"), Format((lock / deals), "Percent"), Format((twentyhr / deals), "Percent"), Format((winterize / deals), "Percent"), Format((fin / deals), "Percent"), Format((ins / fin), "Percent"), Format((ext / deals), "Percent"), Format((pt / deals), "Percent"), Format((at / deals), "Percent"), Format((gross / deals), "Currency"), Format((grossf / fin), "Currency"), Format((0), "Currency")}
                            datatable.Rows.Add(temp)
                        Else
                            Dim temp() As String = {name, deals.ToString, Format((tire / deals), "Percent"), Format((cover / deals), "Percent"), Format((ski / deals), "Percent"), Format((rock / deals), "Percent"), Format((prop / deals), "Percent"), Format((safe / deals), "Percent"), Format((lock / deals), "Percent"), Format((twentyhr / deals), "Percent"), Format((winterize / deals), "Percent"), Format((fin / deals), "Percent"), Format((ins / fin), "Percent"), Format((ext / deals), "Percent"), Format((pt / deals), "Percent"), Format((at / deals), "Percent"), Format((gross / deals), "Currency"), Format((grossf / fin), "Currency"), Format((grossc / (deals - fin)), "Currency")}
                            datatable.Rows.Add(temp)
                        End If
                End Select
            End If
        Next
        conn.Close()
        conn.Dispose()
        DataGridView1.AutoResizeColumns()
        PictureBox2.Visible = False


    End Sub

    Private Sub getcollection()
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim adpt As New MySqlDataAdapter
        collectionset = New DataTable

        Try
            conn.ConnectionString = persistant.myconnstring
            cmd.Connection = conn
            conn.Open()

            Select Case cmbviewby.SelectedItem
                Case Is = "Finance Manager"
                    cmd.CommandText = "SELECT DISTINCT bizman from bos"
                Case Is = "Store"
                    cmd.CommandText = "SELECT DISTINCT store from bos"
                Case Is = "Salesman"
                    cmd.CommandText = "SELECT DISTINCT Salesman from bos"
            End Select

            adpt.SelectCommand = cmd
            adpt.Fill(collectionset)

            adpt.Dispose()
            conn.Close()
        Catch ex As Exception
        End Try
        conn.Dispose()
    End Sub

    Private Sub Date_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateStart.ValueChanged, DateEnd.ValueChanged
        DataGridView1.Visible = False
        Me.Refresh()

        If loaded Then
            getcollection()
            'refreshdata()
            PictureBox2.Visible = True
        End If

        DataGridView1.Visible = True

    End Sub

    Private Sub cmb_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbviewby.SelectedValueChanged
        DataGridView1.Visible = False
        Me.Refresh()

        If loaded Then
            getcollection()
            '   refreshdata()
            PictureBox2.Visible = True
        End If

        DataGridView1.Visible = True

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        DateStart.Value = CDate("9 1 " + (Now.Year - 1).ToString)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        PictureBox2.Visible = False
        DataGridView1.Visible = False
        Me.Refresh()
        refreshdata()
        DataGridView1.Visible = True
    End Sub
End Class