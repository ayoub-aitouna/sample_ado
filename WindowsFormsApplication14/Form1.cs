using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApplication14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /****************************************/

            /** bdli b conection dyalk hintach ana bdltha bach ntesty **/
            SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-RK64H1O; Initial Catalog=DB_Etudiant;Integrated Security=True;");

            DataTable DT = new DataTable();


            /** hady hia li7ydt 7intach konty m3wdaha joj dl marat*/
      
            
            //public void DataGridRemplissage()
            //{
            //    SqlCommand cmd = new SqlCommand("select *from Etudiant", cnx);
            //    SqlDataReader DR = cmd.ExecuteReader();
            //    //DataTable DT = new DataTable();
            //    DT.Load(DR);
            //    dataGridView1.DataSource = DT;

            //}
            //--------------------------------------------
            public void afficher(int p)
            {
                if (p == -1)
                {
                    MessageBox.Show("la personne n'existe pas");
                }
                else { textBox1.Text = DT.Rows[0].ToString(); }
            }
            //--------------------------------------------
            public int recherche()
            {
                int pos = -1;
                for (int i = 0; i <= DT.Rows.Count; i++)
                {
                    //  if (DT.Rows[i][0].ToString[] = textBox1.Text)
                    {
                        pos = i;
                        break;
                    }
                }
                return pos;
            }
            //--------------------------------------------
            public void Vider(Control f)
            {
                foreach (Control ct in f.Controls)
                {
                    if (ct.GetType() == typeof(TextBox))
                    {
                        ct.Text = "";
                    }
                    //if (ct.Controls.Count != 0)
                    //{
                    //    Vider(ct);
                    //}
                }
            }
            //--------------------------------------------
            public void Deconnecter()
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
            }
            //--------------------------------------------
            private bool ControlCODE(int xcode)
            {
                SqlCommand macommand = new SqlCommand("select*from Etudiant where code =@pcode", cnx);
                SqlCommand cmd = new SqlCommand("select*from Etudiant where code =@pcode", cnx);
                cmd.Parameters.AddWithValue("@pcode", SqlDbType.Int).Value = xcode;
                SqlDataReader DR = cmd.ExecuteReader();
                DR.Read();
                bool resultat = DR.HasRows;
                DR.Close();
                return resultat;

            }


            // bouton supprimer
            private void button2_Click(object sender, EventArgs e)
            {

                DialogResult Reponse = MessageBox.Show("voulez vous vraiment supprimé?", "confirmation", MessageBoxButtons.YesNo);
                if (Reponse == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandText = "delete from Etudiant where code =@pcode";
                    cmd.Parameters.AddWithValue("@pcode", SqlDbType.Int).Value = textBox1.Text;
                    int nbre_ligne = cmd.ExecuteNonQuery();
                    if (nbre_ligne == 0)
                    {
                        MessageBox.Show("aucune suppression");
                    }
                    else
                    {
                        MessageBox.Show("suppression bien faite");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        DataGridRemplissage();
                    }

                }
                else MessageBox.Show("suppression annulé");

            }

            private void label3_Click(object sender, EventArgs e)
            {

            }




            private void DataGridRemplissage()
                {
                    /*
                     * hady zdtha bach fina bghity t3mry datagrid tkhwi dakchi likan 9dim hia lwla 
                     */
                    DT.Clear();
                     /****/
                    SqlCommand cmd = new SqlCommand("select *from Etudiant", cnx);
                    SqlDataReader DR = cmd.ExecuteReader();
                    //DataTable DT = new DataTable();
                    DT.Load(DR);
                    dataGridView1.DataSource = DT;

                }





        private void Form1_Load(object sender, EventArgs e)
            {
                if (cnx.State != ConnectionState.Open)
                    cnx.Open();


            DataGridRemplissage();


            }




            //bouton ajouter
            private void ajouter_Click(object sender, EventArgs e)
            {
                if (textBox1.Text != string.Empty)
                {

                    if (ControlCODE(int.Parse(textBox1.Text)) == true)
                    {
                        MessageBox.Show("ce code est deja utilise");

                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cnx;
                        cmd.CommandText = "insert into Etudiant values(@pcode,@pnom,@pprenom,@page)";
                        cmd.Parameters.AddWithValue("@pcode", SqlDbType.Int).Value = textBox1.Text;
                        cmd.Parameters.AddWithValue("@pnom", SqlDbType.VarChar).Value = textBox2.Text;
                        cmd.Parameters.AddWithValue("@pprenom", SqlDbType.VarChar).Value = textBox3.Text;
                        cmd.Parameters.AddWithValue("@page", SqlDbType.Int).Value = textBox4.Text;
                        int n = cmd.ExecuteNonQuery();
                        if (n == 0)
                        {
                            MessageBox.Show("aucune insertion");
                        }
                        else
                        {
                            MessageBox.Show(" insertion bien faite");
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                        }
                        DataGridRemplissage();
                    }
                }
                else
                {
                    MessageBox.Show("veuillez remplir le code");
                }
            }




            // bouton modifier
            private void modifier_Click(object sender, EventArgs e)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "update Etudiant set nom=@pnom,prenom=@pprenom,age=@page where code=@pcode";
                cmd.Parameters.AddWithValue("@pcode", SqlDbType.Int).Value = textBox1.Text;
                cmd.Parameters.AddWithValue("@pnom", SqlDbType.VarChar).Value = textBox2.Text;
                cmd.Parameters.AddWithValue("@pprenom", SqlDbType.VarChar).Value = textBox3.Text;
                cmd.Parameters.AddWithValue("@page", SqlDbType.Int).Value = textBox4.Text;
                int R = cmd.ExecuteNonQuery();
                if (R == 0)
                {
                    MessageBox.Show("aucune mise a jour");
                }
                else

                    MessageBox.Show("mise a jour bien faite");
                DataGridRemplissage();
            }




            //    bouton chercher
            private void chercher_Click(object sender, EventArgs e)
            {
                SqlCommand cmd = new SqlCommand("select*from Etudiant where code =@pcode", cnx);
                cmd.Parameters.AddWithValue("@pcode", SqlDbType.Int).Value = textBox1.Text;
                SqlDataReader DR = cmd.ExecuteReader();
                DR.Read();
                if (DR.HasRows)
                {
                    textBox1.Text = DR[0].ToString();
                    textBox2.Text = DR[1].ToString();
                    textBox3.Text = DR[2].ToString();
                    textBox4.Text = DR[3].ToString();
                }
                else
                {
                    MessageBox.Show("cet etudiant n'existe pas");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                DR.Close();
            }




            private void button1_Click(object sender, EventArgs e)
            {
                afficher(0);


            }




            private void premier_Click(object sender, EventArgs e)
            {
                afficher(0);
            }





            private void dernier_Click(object sender, EventArgs e)
            {
                afficher(DT.Rows.Count - 1);
            }




            private void precedent_Click(object sender, EventArgs e)
            {
                int p = recherche();
                if (p > 0) { afficher(p - 1); }
                else { MessageBox.Show("c'est le premier"); }

            }





            private void suivant_Click(object sender, EventArgs e)
            {
                int p = recherche();
                if (p < DT.Rows.Count - 1) { afficher(p + 1); }
                else { MessageBox.Show("c'est le dernier"); }
            }





            private void button3_Click(object sender, EventArgs e)
            {
                DialogResult d = MessageBox.Show("voulez vous vraiment vider", "confirmation", MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes)
                {

                    Vider(this);
                    MessageBox.Show("tache bien faite");
                }
                else { MessageBox.Show("tache annulé"); }
            }






            private void fermer_Click(object sender, EventArgs e)
            {
                DialogResult R = MessageBox.Show("voulez vous quitter?", "confirmation", MessageBoxButtons.YesNo);
                if (R == DialogResult.Yes)
                {
                    Deconnecter();
                    this.Close();
                }


            }



        /******************************************/




    }
}
