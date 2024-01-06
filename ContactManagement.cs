using ContactManagement.ContactManagementClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactManagement
{
    public partial class ContactManagement : Form
    {
        public ContactManagement()
        {
            InitializeComponent();

            //Pour afficher la table des contacts il faut :

            // 1) Instancier la classe ContactClass
            ContactClass c = new ContactClass();

            // 2)  Afficher la liste des contacts dans la DataGridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        ContactClass c = new ContactClass();

        
        

        /*private void label1_Click(object sender, EventArgs e)
        {

        }*/

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // *) Event Button "Add"
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Assurez-vous que tous les champs obligatoires sont renseignés
            if (string.IsNullOrEmpty(txtboxFirstName.Text) || string.IsNullOrEmpty(txtboxLastName.Text) || string.IsNullOrEmpty(txtBoxContactNumber.Text) || string.IsNullOrEmpty(txtBoxAddress.Text) || string.IsNullOrEmpty(cmbGender.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.");
                return;
            }

            //Get The value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting Data into Database using the method we created in previeous.
            bool success = c.Insert(c);
            if(success==true)
            {
                //Successfully Inserted
                MessageBox.Show("New Contact Successfully Inserted");
                //Call the clear Methode Here
                Clear();
            }
            else
            {
                //Failed to Add contact 
                MessageBox.Show("Failed to Add New Contact. Try Again.");
            }

            //Afficher la liste des contacts dans la DataGridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }


        // *) Event "GirdView"
        private void dgvContactList_CellContentClick(object sender, EventArgs e)
        {
            //Failed Data on Data GridView
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }


        //*) Event Picture "Close window"
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Method to clear fields
        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }


        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

            //Get the Data from texboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            // Testing Data in the consol 
            /*Console.WriteLine("ContactID: " + txtboxContactID.Text);
            Console.WriteLine("FirstName: " + txtboxFirstName.Text);
            Console.WriteLine("LastName: " + txtboxLastName.Text);
            Console.WriteLine("ContactNo: " + txtBoxContactNumber.Text);
            Console.WriteLine("Address: " + txtBoxAddress.Text);
            Console.WriteLine("Gender: " + cmbGender.Text);*/


            //update data in Database
            bool success = c.Update(c);
            if(success==true)
            {
                //updated successfully
                MessageBox.Show("Contact has been sucessfully updated.");

                //Afficher la liste des contacts dans la DataGridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                //Call Clear Method
                Clear();
            }
            else
            {
                //Failed to update
                MessageBox.Show("Failed to Update Contact.Tray Again.");
            }
        }

        // *)Event lorsque on clique sur la ligne qui l'on peut le modifier il affiche le contenue de la ligne dans les champs correspondants
        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get Data from data Grid View load it to the textboxes respectively
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }


        //*) Event Button "Clear"
        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call Clear Method Here
            Clear();
        }


        //*) Event Button "Delete"
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the Contact ID from en application 
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if (success == true) 
            {
                //Successfully Deleted
                MessageBox.Show("Contact Successfully deleted. ");

                //Refresh Data on Data GRIDView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                //Call the Clear Method
                Clear();
            }
            else
            {
                //Failed to delete Contact
                MessageBox.Show("Failed to Delete Contact. Try Again ");
            }
        }


        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from text box :
            string keyword = txtboxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Table_Contact WHERE FirstName LIKE '%"+keyword+"%'OR lastName LIKE '%"+keyword+ "%' OR Address LIKE '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
