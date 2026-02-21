using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kros
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Validatory
        private void Validator_Zamestnanec(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string errorText = null,
                   headerText = zamestnanecDataGridView.Columns[e.ColumnIndex].HeaderText;

            errorText = Validator.InvalidateZamestnanec(headerText, e.FormattedValue.ToString());

            if (false == string.IsNullOrEmpty(errorText))
            {
                ((DataGridView)sender).Rows[e.RowIndex].ErrorText = errorText;
                e.Cancel = true;
            }
            else
            {
                zamestnanecDataGridView.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void Validator_Firma(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string errorText = null, 
                   headerText = firmaDataGridView.Columns[e.ColumnIndex].HeaderText;

            errorText = Validator.InvalidateFirma(headerText, e.FormattedValue.ToString());

            if(false == string.IsNullOrEmpty(errorText))
            {
                ((DataGridView)sender).Rows[e.RowIndex].ErrorText = errorText;
                e.Cancel = true;
            }
            else
            {
                firmaDataGridView.Rows[e.RowIndex].ErrorText = String.Empty;
            }
        }

        private void firmaDataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (krosDBDataSet.firma.Rows.Count >= 1)
            {
                MessageBox.Show("Môžete vytvoriť iba jednu firmu.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                firmaDataGridView.CancelEdit();
            }
        }

        #endregion Validatory

        #region Globalne akcie formulara
        private void Form1_Load(object sender, EventArgs e)
        {
            this.oddelenie_zamestnanci_pekneTableAdapter.Fill(this.krosDBDataSet.oddelenie_zamestnanci_pekne);
            this.zamestnanecTableAdapter.Fill(this.krosDBDataSet.zamestnanec);
            this.firmaTableAdapter.Fill(this.krosDBDataSet.firma);
            this.diviziaTableAdapter.Fill(this.krosDBDataSet.divizia);
            this.projektTableAdapter.Fill(this.krosDBDataSet.projekt);
            this.oddelenieTableAdapter.Fill(this.krosDBDataSet.oddelenie);
            this.oddelenie_zamestnanciTableAdapter.Fill(this.krosDBDataSet.oddelenie_zamestnanci);

            this.firmaDataGridView.CellValidating += Validator_Firma;
            this.zamestnanecDataGridView.CellValidating += Validator_Zamestnanec;

            TreeBuilder.Build(treeViewHierachia, krosDBDataSet);
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

        private bool ZrusitZmenyDataSetu()
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
            => ZrusitZmenyDataSetu();

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (krosDBDataSet != null && krosDBDataSet.HasChanges())
                if(false == ZrusitZmenyDataSetu())
                    e.Cancel = true;
        }

        #endregion Akcie formulara

        private void obnovHierarchiu_Click(object sender, EventArgs e)
            => TreeBuilder.Build(treeViewHierachia, krosDBDataSet);

    }
}
