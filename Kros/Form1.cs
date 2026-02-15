using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kros
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'krosDBDataSet.oddelenie_zamestnanci_pekne' table. You can move, or remove it, as needed.
            this.oddelenie_zamestnanci_pekneTableAdapter.Fill(this.krosDBDataSet.oddelenie_zamestnanci_pekne);
            // TODO: This line of code loads data into the 'krosDBDataSet.zamestnanec' table. You can move, or remove it, as needed.
            this.zamestnanecTableAdapter.Fill(this.krosDBDataSet.zamestnanec);
            // TODO: This line of code loads data into the 'krosDBDataSet.firma' table. You can move, or remove it, as needed.
            this.firmaTableAdapter.Fill(this.krosDBDataSet.firma);
            // TODO: This line of code loads data into the 'krosDBDataSet.divizia' table. You can move, or remove it, as needed.
            this.diviziaTableAdapter.Fill(this.krosDBDataSet.divizia);
            // TODO: This line of code loads data into the 'krosDBDataSet.projekt' table. You can move, or remove it, as needed.
            this.projektTableAdapter.Fill(this.krosDBDataSet.projekt);
            // TODO: This line of code loads data into the 'krosDBDataSet.oddelenie' table. You can move, or remove it, as needed.
            this.oddelenieTableAdapter.Fill(this.krosDBDataSet.oddelenie);
            // TODO: This line of code loads data into the 'krosDBDataSet.oddelenie_zamestnanci' table. You can move, or remove it, as needed.
            this.oddelenie_zamestnanciTableAdapter.Fill(this.krosDBDataSet.oddelenie_zamestnanci);
        }

        private void zamestnanecBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void zamestnanecBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ulozitZmeny_Click(object sender, EventArgs e)
        {
            this.Validate();

            this.zamestnanecBindingSource.EndEdit();
            this.firmaBindingSource.EndEdit();
            this.diviziaBindingSource.EndEdit();
            this.projektBindingSource.EndEdit();
            this.oddelenieBindingSource.EndEdit();
            this.oddelenie_zamestnanciBindingSource.EndEdit();

            zamestnanecTableAdapter.Update(this.krosDBDataSet.zamestnanec);
            firmaTableAdapter.Update(this.krosDBDataSet.firma);
            diviziaTableAdapter.Update(this.krosDBDataSet.divizia);
            projektTableAdapter.Update(this.krosDBDataSet.projekt);
            oddelenieTableAdapter.Update(this.krosDBDataSet.oddelenie);
            oddelenie_zamestnanciTableAdapter.Update(this.krosDBDataSet.oddelenie_zamestnanci);
        }

        private Boolean zrusitZmenyDataSetu()
        {
            var retVal = MessageBox.Show("Naozaj chcete zahodiť všetky zmeny od posledného uloženia?"
                , "Zahodiť zmeny"
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Question);

            if (retVal == DialogResult.Yes) {
                krosDBDataSet.RejectChanges();

                return true;
            }

            return false;
        }

        private void zrusitZmeny_Click(object sender, EventArgs e)
        {
            zrusitZmenyDataSetu();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (krosDBDataSet != null && krosDBDataSet.HasChanges())
                if(false == zrusitZmenyDataSetu())
                    e.Cancel = true;
        }
    }
}
