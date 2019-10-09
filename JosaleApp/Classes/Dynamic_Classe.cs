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
//using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace JosaleApp.Classes
{
    class Dynamic_Classe
    {
        SqlDataAdapter da = null;
        SqlConnection con;
        DataSet ds;
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
                ds = new DataSet();
                da.Fill(ds, tablename);
                con.Close();
                return ds.Tables[0];

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable Get_Data_combo(string tablename, string champ, string champ2)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                con = (SqlConnection)ImplementeConnexion.Instance.Conn;
                da = new SqlDataAdapter("SELECT * FROM " + tablename + "", con);
                ds = new DataSet();
                da.Fill(ds, tablename);
                con.Close();
                return ds.Tables[0];

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
