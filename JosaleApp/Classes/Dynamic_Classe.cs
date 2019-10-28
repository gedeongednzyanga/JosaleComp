using System;
using Connexion_manager;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parametre;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WinForms;
using JosaleApp.DataSet;
using Prets_Lib;

namespace JosaleApp.Classes
{
    class Dynamic_Classe
    {                                                  
        SqlDataAdapter da = null;
        SqlConnection con;
        System.Data.DataSet ds;
        public static Dynamic_Classe dynC;

        public static Dynamic_Classe Instance()
        {
            if (dynC == null)
                dynC = new Dynamic_Classe();
            return dynC;
        }

        public DataTable Get_Data(string tablename)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                con = (SqlConnection)ImplementeConnexion.Instance.Conn;
                da = new SqlDataAdapter("SELECT * FROM " + tablename + "", con);
                ds = new System.Data.DataSet();
                da.Fill(ds, tablename);
                con.Close();
                return ds.Tables[0];

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Get_Data_one(string tablename, int code, string champ, Label label)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + champ + " FROM " + tablename + " where ref_cli = '" + code + "'";
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        label.Text = dr[champ].ToString()+"$";
                    }
                    dr.Close();
                    cmd.Dispose();
                }
             
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Get_Annee_combo(ComboBox combAnne)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "select DATEPART(YEAR, date_pret) as annee from prets group by DATEPART(YEAR, date_pret) order by DATEPART(YEAR, date_pret) desc";
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        combAnne.Items.Add(dr["annee"].ToString());
                    }
                    dr.Close();
                    cmd.Dispose();
                }
                ImplementeConnexion.Instance.Conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Load_gage(int id, ListView liste)
        {

            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT_ONE_GAGE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListViewItem list = new ListViewItem(dr["Code"].ToString());
                        list.SubItems.Add(dr["Designation"].ToString());
                        list.SubItems.Add(dr["Valeur"].ToString());
                        list.SubItems.Add(dr["Nombre"].ToString());
                        list.SubItems.Add(dr["Nombre"].ToString());
                        liste.Items.Add(list);
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message, "Error");
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }
        public void Load_Credit(DataGridView liste)
        {
            int compteur = 1;
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT_CREDIT_FOR_CUSTOMER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Rows.Add(compteur, dr["N°"].ToString(), dr["Customer"].ToString(), dr["Mount"].ToString());
                        compteur++;
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message, "Error");
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }

        public void Load_Emprunt(DataGridView liste)
        {
            int compteur = 1;
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT_EMPRUNT_FOR_TIER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Rows.Add(compteur, dr["N°"].ToString(), dr["Tier"].ToString(), dr["Mount"].ToString());
                        compteur++;
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message, "Error");
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }

        public void Search_Credit(DataGridView liste, string champ)
        {
            liste.Rows.Clear();
            int compteur = 1;
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "RECHERCHE_CLIENT_FOR_PAYEMENT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@noms", 50, DbType.String, champ));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Rows.Add(compteur, dr["N°"].ToString(), dr["Customer"].ToString(), dr["Mount"].ToString());
                        compteur++;
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message, "Error");
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }
        public void Search_Emprunt(DataGridView liste, string champ)
        {
            liste.Rows.Clear();
            int compteur = 1;
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "RECHERCHE_TRIER_FOR_PAYEMENT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@noms", 50, DbType.String, champ));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Rows.Add(compteur, dr["N°"].ToString(), dr["Tier"].ToString(), dr["Mount"].ToString());
                        compteur++;
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message, "Error");
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }

        public int Count_data(string table, string champ, int number)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(" + champ + ")  as Somme from " + table + " ";
                IDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) {
                    if (dr["Somme"] == DBNull.Value)
                        number = 0;
                    else
                        number = Convert.ToInt32(dr["Somme"].ToString());
                }
                dr.Dispose();
                ImplementeConnexion.Instance.Conn.Close();
            }
            return number;
        }

        public void Get_Somme_debt(Label number, int mois)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "select count(code_emprunt) as somme from emprunts  where DATEPART(month, date_rembu) ="+mois+"";
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["somme"] == DBNull.Value)
                            number.Text = "Total debt : 0";
                        else
                            number.Text = "Total debt : "+dr["somme"].ToString();
                    }
                    dr.Close();
                    cmd.Dispose();
                }
                ImplementeConnexion.Instance.Conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Get_Somme_debt_annee(Label number, int annee)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "select count(code_emprunt) as somme from emprunts  where DATEPART(YEAR, date_rembu) =" + annee + "";
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["somme"] == DBNull.Value)
                            number.Text = "Total debt : 0";
                        else
                            number.Text = "Total debt : " + dr["somme"].ToString();
                    }
                    dr.Close();
                    cmd.Dispose();
                }
                ImplementeConnexion.Instance.Conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
           
        //Call Reports

        public void Call_Report(ReportViewer reportView, string path, int codePret)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "select * from Recu where Numéro = " + codePret + "";
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    ds = new System.Data.DataSet();
                    //Remplissage du DataSet via DataAdapter
                    da.Fill(ds, "DataSet_Recu");
                    reportView.LocalReport.DataSources.Clear();
                    //Source du reportViewr
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_Recu", ds.Tables[0]));
                    //Specificier le rapport à charger
                    reportView.LocalReport.ReportEmbeddedResource = path;
                    reportView.RefreshReport();
                }
            } catch(InvalidOperationException ex)
            {
                MessageBox.Show("Error "+ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (ImplementeConnexion.Instance.Conn != null)
                {
                    if (ImplementeConnexion.Instance.Conn.State == System.Data.ConnectionState.Open)
                        ImplementeConnexion.Instance.Conn.Close();
                }

                if (da != null)
                    da.Dispose();
                if (ds != null)
                    ds.Dispose();
            }
        }

        public void Call_Report_Recu_Rembou(ReportViewer reportView, string path, int codeRembou)
        {
            // DataTable dt = new DataTable();
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Affichage_Client_Rembourssement where Numéro = " + codeRembou + "";
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    ds = new System.Data.DataSet();
                    //Remplissage du DataSet via DataAdapter
                    //Remplissage du DataSet via DataAdapter
                    da.Fill(ds, "DataSet_recu_Rembou");
                    reportView.LocalReport.DataSources.Clear();
                    //Source du reportViewr
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_recu_Rembou", ds.Tables[0]));
                    //Specificier le rapport à charger
                    reportView.LocalReport.ReportEmbeddedResource = path;
                    reportView.RefreshReport();
                }

            }catch(InvalidOperationException ex)
            {
                MessageBox.Show("Error "+ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (ImplementeConnexion.Instance.Conn != null)
                {
                    if (ImplementeConnexion.Instance.Conn.State == System.Data.ConnectionState.Open)
                        ImplementeConnexion.Instance.Conn.Close();
                }

                if (da != null)
                    da.Dispose();
                if (ds != null)
                    ds.Dispose();
            }
        }

        public void Call_Report_Historic_Rembou(ReportViewer reportView, string path,int annee, string mois)
        {
            // DataTable dt = new DataTable();
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Affichage_Client_Rembourssement WHERE DATEPART(year, [DateRemb.])="+annee+" and DATENAME(MONTH, [DateRemb.]) = '" + mois + "'";
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    ds = new System.Data.DataSet();
                    //Remplissage du DataSet via DataAdapter
                    da.Fill(ds, "DataSet_RembouHistoric");
                    reportView.LocalReport.DataSources.Clear();
                    //Source du reportViewr
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_RembouHistoric", ds.Tables[0]));
                    //Specificier le rapport à charger
                    reportView.LocalReport.ReportEmbeddedResource = path;
                    reportView.RefreshReport();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (ImplementeConnexion.Instance.Conn != null)
                {
                    if (ImplementeConnexion.Instance.Conn.State == System.Data.ConnectionState.Open)
                        ImplementeConnexion.Instance.Conn.Close();
                }

                if (da != null)
                    da.Dispose();
                if (ds != null)
                    ds.Dispose();
            }
        }

        public void Call_Report_Historic_Debit(ReportViewer reportView, string path, int annee)
        {
            // DataTable dt = new DataTable();
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Affichage_Emprunt WHERE DATEPART(year, date_emprunt)=" + annee + "";
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    ds = new System.Data.DataSet();
                    //Remplissage du DataSet via DataAdapter
                    da.Fill(ds, "DataSet_LoanHistoric");
                    reportView.LocalReport.DataSources.Clear();
                    //Source du reportViewr
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_LoanHistoric", ds.Tables[0]));
                    //Specificier le rapport à charger
                    reportView.LocalReport.ReportEmbeddedResource = path;
                    reportView.RefreshReport();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (ImplementeConnexion.Instance.Conn != null)
                {
                    if (ImplementeConnexion.Instance.Conn.State == System.Data.ConnectionState.Open)
                        ImplementeConnexion.Instance.Conn.Close();
                }

                if (da != null)
                    da.Dispose();
                if (ds != null)
                    ds.Dispose();
            }
        }


        //Méthode pour génération PDF

        public void GeneratePDF(DataTable dtblTable, String strPdfPath, string strHeader)
        {
            FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1, Color.GRAY);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);

            //Auteur ou Entreprise
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 8, 2, Color.GRAY);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("Author : Josale Company", fntAuthor));
            prgAuthor.Add(new Chunk("\nRun Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

            //Ajout de la ligne de separation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            //Table header
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, Color.WHITE);
            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = Color.GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }
            //table Data
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    table.AddCell(dtblTable.Rows[i][j].ToString());
                }
            }
            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }

        //Méthode pour exporter DatagridView en DataTable

        public DataTable ExportDatagrod_toDatatable(DataGridView dgv, DataTable dt)
        {
            while (dt.Columns.Count < dgv.Columns.Count)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    dt.Columns.Add(col.HeaderText.ToString());
                }
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataRow drow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    drow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(drow);
            }
            return dt;
        }
    }
}
