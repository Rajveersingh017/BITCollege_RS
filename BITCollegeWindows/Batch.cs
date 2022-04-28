using BITCollege_RS.Data;
using BITCollege_RS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class Batch : Form
    {
        BITCollege_RSContext db = new BITCollege_RSContext();
        public Batch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  ensures key is entered
        /// further code to be added
        /// </summary>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //NOTE:  This may be commented out until needed
            /*  if (txtKey.Text == "")
            {
                MessageBox.Show("A 64-bit Key must be entered", "Error");
            }*/

            BatchProcess process = new BatchProcess();

            if (radSelect.Checked)
            {
                process.ProcessTransmission(this.descriptionComboBox.SelectedValue.ToString(), this.txtKey.Text);

                this.rtxtLog.Text += process.WriteLogData();
            }

            if (radAll.Checked)
            {
                for (int i = 0; i < this.descriptionComboBox.Items.Count; i++)
                {
                    this.descriptionComboBox.SelectedIndex = i;
                    process.ProcessTransmission(descriptionComboBox.SelectedValue.ToString(), this.txtKey.Text);
                    this.rtxtLog.Text += process.WriteLogData();
                }
            }
        }

        /// <summary>
        /// given:  open in top right of frame
        /// further code required:
        /// </summary>
        private void Batch_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            IQueryable<AcademicProgram> academicPrograms = db.AcademicPrograms;

            this.academicProgramBindingSource.DataSource = academicPrograms.ToList();
        }
        /// <summary>
        /// sets the combox to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radSelect_CheckedChanged(object sender, EventArgs e)
        {
            descriptionComboBox.Enabled = true;
        }

        /// <summary>
        /// this event is raised when the user clicks the decrypt button.
        /// </summary>
        /// <param name="sender">LnkUpdate, a link button on the form.</param>
        /// <param name="e">Raised when the link button is clicked.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            string plaintextFileName;
            string encryptedFileName;

            plaintextFileName = txtFileName.Text;
            encryptedFileName = plaintextFileName + ".encrypted";

            Utility.Encryption.Decrypt(plaintextFileName, encryptedFileName, txtKey.Text);
            StreamReader reader = new StreamReader(plaintextFileName);
            rtxtLog.Text = reader.ReadToEnd();
            reader.Close();
        }

        
    }
}
