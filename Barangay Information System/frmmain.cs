using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;

namespace Barangay_Information_System
{
    public partial class searchengine : Form
    {
        string adminID = "";
        DatabaSeserver obj11 = new DatabaSeserver();
        public searchengine()
        {
            InitializeComponent();

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort();
        }
        string gender, placeofbirth, company, searchstatement = "";
        //string gender, city, company, searchstatement = "";
        private void sort()
        {
            if (comboBox2.Text != "Gender")
            {

                gender = "Gender='" + comboBox2.Text + "'";
            }
            else
            {
                gender = "";
            }
            if (comboBox3.Text != "Place of Birth" && comboBox2.Text != "Gender")
            {
                placeofbirth = " AND placeofbirth='" + comboBox3.Text + "'";
            }
            else if (comboBox3.Text != "Place Of Birth" && comboBox2.Text == "Gender")
            {
                placeofbirth = "placeofbirth='" + comboBox3.Text + "'";
            }
            else
            {
                placeofbirth = "";
            }
            if (comboBox4.Text != "Company" && (comboBox2.Text != "Gender" || comboBox3.Text != "Place Of Birth"))
            {
                company = " AND company='" + comboBox4.Text + "'";
            }
            else if (comboBox4.Text != "Company" && (comboBox2.Text == "Gender" && comboBox3.Text == "Place of Birth"))
            {
                company = "company='" + comboBox4.Text + "'";
            }
            else
            {
                company = "";
            }
            if (gender != "" || placeofbirth != "" || company != "")
            {
                searchstatement = "select * from tblhousehold where " + gender + placeofbirth + company;

            }
            else
            {
                searchstatement = "select * from tblhousehold";
            }
            MessageBox.Show(searchstatement);
            loaddataintogrid(searchstatement);

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obj11.savecontacts("INSERT INTO tblhousehold VALUES('" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtmiddlename.Text + "','" + dateofbirth.Text + "','" + cboplaceofbirth.Text + "','" + txtgender.Text + "','" + txtcivilstatus.Text + "','" + txtcitizenship.Text + "','" + txtphonenumber.Text + "','" + txtprofesion.Text + "','" + cbopurok.Text + "');");
            loaddataintogrid("Select * from tblhousehold");
            addcombodata();
            contentdisable();

        }
        private void loaddataintogrid(string statementt)
        {
            SQLiteDataReader sdr = obj11.dgwdata(statementt);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Last Name", typeof(string)));
            dt.Columns.Add(new DataColumn("First Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Middle Name", typeof(string)));
       //   dt.Columns.Add(new DataColumn("Date of Birth", typeof(string)));
            dt.Columns.Add(new DataColumn("Place of Birth", typeof(string)));
            dt.Columns.Add(new DataColumn("Gender", typeof(string)));
            dt.Columns.Add(new DataColumn("Civil Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Citizenship", typeof(string)));
            dt.Columns.Add(new DataColumn("Phone Number", typeof(string)));
            dt.Columns.Add(new DataColumn("Profession/Occupation", typeof(string)));
            dt.Columns.Add(new DataColumn("Purok", typeof(string)));

            using (sdr)
            {

                while (sdr.Read())
                {
                    dt.Rows.Add(
                      sdr.GetValue(0),
                      sdr.GetValue(1),
                      sdr.GetValue(2),
                      sdr.GetValue(4),
                      sdr.GetValue(5),
                      sdr.GetValue(6),
                      sdr.GetValue(7),
                      sdr.GetValue(8),
                      sdr.GetValue(9),
                      sdr.GetValue(10)
                    );

                }
            }
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loaddataintogrid("select * from tblhousehold");
        }
        private void addcombodata()
        {
            SQLiteDataReader sdr = obj11.dgwdata("SELECT placeofbirth,gender FROM tblhousehold GROUP BY placeofbirth, gender");
            using (sdr)
            {

                while (sdr.Read())
                {
                    comboBox3.Items.Add(sdr.GetValue(0));
                    comboBox4.Items.Add(sdr.GetValue(1));

                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from tblhousehold where upper(name) like '%" + txtlastname.Text.ToUpper() + "%' OR upper(name) like '%" + txtlastname.Text.ToUpper() + "' OR upper(name) like '" + txtlastname.Text.ToUpper() + "%' OR upper(name)='" + txtlastname.Text.ToUpper() + "%");

        }

        private void button7_Click(object sender, EventArgs e)
        {


        }

        private void button8_Click(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from tblhousehold where upper(email)='" + txtcitizenship.Text.ToUpper() + "'");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            contentenable();
            btnabortedit.Enabled = true;
        }
        private void contentdisable()
        {
            dateofbirth.Enabled = false;
            cboplaceofbirth.Enabled = false;
            txtgender.Enabled = false;
            txtcivilstatus.Enabled = false;
            txtfirstname.Enabled = false;
            txtmiddlename.Enabled = false;
            btnsave.Enabled = false;
            btndelete.Enabled = false;
            btnupdate.Enabled = false;
            btnabortedit.Enabled = false;

        }
        private void contentenable()
        {
            dateofbirth.Enabled = true;
            cboplaceofbirth.Enabled = true;
            txtgender.Enabled = true;
           txtcivilstatus.Enabled = true;
            txtfirstname.Enabled = true;
            txtmiddlename.Enabled = true;
            btnsave.Enabled = true;
            btndelete.Enabled = true;
            btnupdate.Enabled = true;
            cboplaceofbirth.Text = "";
            dateofbirth.Text = "";
            txtgender.Text = "";
            txtphonenumber.Text = "";
            txtprofesion.Text = "";
            cbopurok.Text = "";
            txtcivilstatus.Text = "";
            txtcitizenship.Text = "";
            txtcitizenship.Text = "";
            txtfirstname.Text = "";
            txtlastname.Text = "";
            txtmiddlename.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are u sure to delete this Personal Information with name " + txtlastname.Text, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                obj11.commandexecutor("delete from tblhousehold where upper(lastname)='" + txtlastname.Text.ToUpper() + "' OR phonenumber='" + txtphonenumber.Text + "' OR profession='" + txtprofesion.Text + "' OR purok='" + cbopurok.Text + "'");
                MessageBox.Show("Record is deleted");
            }
            else
            {
                // Do something else
            }
        }

       


        private void button8_Click_1(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from tblhousehold where upper(citizenship)='" + txtcitizenship.Text.ToUpper() + "'");
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            sort();
        }

        private void txtlastname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateofbirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            sort();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtphonenumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from tblhousehold where upper(lastname) like '%" + txtlastname.Text.ToUpper() + "%' OR upper(lastname) like '%" + txtlastname.Text.ToUpper() + "' OR upper(lastname) like '" + txtlastname.Text.ToUpper() + "%' OR upper(lastname)='" + txtlastname.Text.ToUpper() + "%");
        }

        private void showdata_Enter(object sender, EventArgs e)
        {

        }

 

        private void btnsave_Click(object sender, EventArgs e)
        {
            obj11.savecontacts("INSERT INTO tblhousehold VALUES('" + txtlastname.Text + "','" + txtfirstname.Text + "','" + txtmiddlename.Text + "','" + dateofbirth.Text + "','" + cboplaceofbirth.Text + "','" + txtgender.Text + "','" + txtcivilstatus.Text + "','" + txtcitizenship.Text + "','" + txtphonenumber.Text + "','" + txtprofesion.Text + "','" + cbopurok.Text + "');");
            loaddataintogrid("Select * from tblhousehold");
            addcombodata();
            contentdisable();
        }

        private void btnaddnew_Click(object sender, EventArgs e)
        {
            contentenable();
            btnabortedit.Enabled = true;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are u sure to delete this Household with name " + txtlastname.Text, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                obj11.commandexecutor("delete from tblhousehold where upper(lastname)='" + txtlastname.Text.ToUpper() + "' OR phonenumber='" + txtphonenumber.Text + "' OR profession='" + txtprofesion.Text + "' OR purok='" + cbopurok.Text + "'");
                MessageBox.Show("Record is deleted");
            }
            else
            {
                // Do something else
            }
        }

        private void btnshowdata_Click(object sender, EventArgs e)
        {
            loaddataintogrid("select * from tblhousehold");
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void btnsearchengine_Click(object sender, EventArgs e)
        {
            loaddataintogrid("Select * from tblhousehold where phonenumber='" + txtphonenumber.Text + "' OR profession='" + txtprofesion.Text + "' OR purok='" + cbopurok.Text + "'");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        private void btnsearchinfo_Click(object sender, EventArgs e)
        {

           
          loaddataintogrid("Select * from tblhousehold where gender='" + cbogendersearch.Text + "' OR civilstatus='" + cbocivilstatussearch.Text + "' OR purok='" + cbopuroksearch.Text + "'");
        }

        private void btnabortedit_Click(object sender, EventArgs e)
        {
            btnupdate.Text = "Edit";
            contentenable();
            contentdisable();
            btnabortedit.Enabled = false;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            string keytxt = txtphonenumber.Text.Trim();
            int rowcount = 0;
            if (btnupdate.Text == "Edit" && txtphonenumber.Text.Length != 0)
            {
                SQLiteDataReader sdr = obj11.dgwdata("SELECT * from tblhousehold where phonenumber='" + txtphonenumber.Text.Trim() + "' OR profession like '%" + txtphonenumber.Text.Trim() + "%' OR purok like '%" + txtphonenumber.Text.Trim() + "%'");
                using (sdr)
                {

                    while (sdr.Read())
                    {

                        txtlastname.Text = sdr.GetValue(0).ToString();
                        txtfirstname.Text = sdr.GetValue(1).ToString();
                        txtmiddlename.Text = sdr.GetValue(2).ToString();
                        dateofbirth.Text = sdr.GetValue(3).ToString();
                        cboplaceofbirth.Text = sdr.GetValue(4).ToString();
                        txtgender.Text = sdr.GetValue(5).ToString();
                        txtcivilstatus.Text = sdr.GetValue(6).ToString();
                        txtcitizenship.Text = sdr.GetValue(7).ToString();
                        txtphonenumber.Text = sdr.GetValue(8).ToString();
                        txtprofesion.Text = sdr.GetValue(9).ToString();
                        cbopurok.Text = sdr.GetValue(10).ToString();
                        rowcount++;
                        txtphonenumber.Enabled = false;
                    }
                    if (rowcount < 1)
                    {
                        MessageBox.Show("No any record found of this purok \nPlese provide correct purok");
                        btnupdate.Text = "Edit";
                        txtphonenumber.Focus();

                    }
                }
                btnsave.Enabled = false;
                btnupdate.Text = "Update";
            }
            else if (btnupdate.Text == "Update" && txtphonenumber.Text.Length != 0)
            {
                obj11.commandexecutor("update tblhousehold set lastname='" + txtlastname.Text.Trim() + "', firstname='" + txtfirstname.Text.Trim() + "', middlename='" + txtmiddlename.Text + "', dateofbirth='" + dateofbirth.Text.Trim() + "', placeofbirth='" + cboplaceofbirth.Text + "', gender='" + txtgender.Text.Trim() + "', civilstatus='" + txtcivilstatus.Text.Trim() + "', citizenship='" + txtcitizenship.Text + "', phonenumber='" + txtphonenumber.Text.Trim() + "', profession='" + txtprofesion.Text.Trim() + "', purok='" + cbopurok.Text.Trim() + "' where phonenumber='" + keytxt + "'");
                loaddataintogrid("select * from tblhousehold");
                MessageBox.Show("Record is Updated");

                txtphonenumber.Enabled = true;
                btnupdate.Text = "Edit";
            }
            else
            {
                MessageBox.Show("Please provide the primary purok to search and update");
                txtphonenumber.Focus();
                btnupdate.Text = "Edit";

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

       

     

        //private void button10_Click(object sender, EventArgs e)
     //   {
       //     USer User = new USer();
       //     User.Show();
      //  }







    }

}