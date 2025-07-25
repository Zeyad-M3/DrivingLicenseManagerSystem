using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace _3tr.Applications
{
    public partial class ShowApplication : UserControl
    {
        public int IdofApplication { get; set; }
        public ShowApplication(int numIdApplication)
        {
            IdofApplication = numIdApplication;
            InitializeComponent();
            FillTheTextForControl();
        }

        // class to fill all the text in the control

        private void FillTheTextForControl()
        {
            try
            {
                var application = clsApplication.GetApplicationById(IdofApplication);
                if (application != null)
                {
                    // عرض بيانات مفيدة بدلاً من ToString()
                    label9.Text = application.Id.ToString();
                    label13.Text = application.PersonID.ToString();
                    label10.Text = application.Status.ToString();
                    label11.Text=application.ApplicationFee.ToString();
                    label12.Text= application.ApplicationType.ToString();
                    label14.Text = application.ApplicationDate.ToString();
                    label15.Text = application.PaymentDate.ToString();


                }
                else
                {
                    label9.Text = "No application found for the given ID.";
                }
            }
            catch (Exception ex)
            {
                label9.Text = $"Error: {ex.Message}";
                Console.WriteLine($"Error in FillTheTextForControl: {ex.Message}");
            }
        }

        private void EditAndShowApplication_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var application = clsApplication.GetApplicationById(IdofApplication);


            frmAddPerson frm = new frmAddPerson(application.PersonID);
            frm.ShowDialog();
        }
    }
}
