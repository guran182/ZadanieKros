using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Kros
{
    public class TreeBuilder
    {
        public static void Build(TreeView tree, krosDBDataSet ds)
        {
            tree.BeginUpdate();
            tree.Nodes.Clear();

            var firma = ds.firma.FirstOrDefault();

            if (firma == null)
                return;

            // Firma
            var root = tree.Nodes.Add(firma.nazov);

            // Majitel firmy
            root.Nodes.Add(
                new TreeNode(firma.zamestnanecRow.titul_meno_priezvisko) { ForeColor = Color.Blue }
            );

            // Divizie
            foreach (var divizia in ds.divizia)
            {
                var diviziaTreeNode = new TreeNode(divizia.nazov);

                // Veduci divizie
                diviziaTreeNode.Nodes.Add(
                    new TreeNode(divizia.zamestnanecRow.titul_meno_priezvisko) { ForeColor = Color.Blue }
                );

                // Projekty
                foreach (var projekt in divizia.GetprojektRows())
                {
                    var projektTreeNode = new TreeNode(projekt.nazov);

                    // Veduci projektu
                    projektTreeNode.Nodes.Add(
                        new TreeNode(projekt.zamestnanecRow.titul_meno_priezvisko) { ForeColor = Color.Blue }
                    );

                    // Oddelenia
                    foreach (var oddelenie in projekt.GetoddelenieRows())
                    {
                        var oddelenieTreeNode = new TreeNode(oddelenie.nazov);

                        // Veduci oddelenia
                        oddelenieTreeNode.Nodes.Add(
                            new TreeNode(oddelenie.zamestnanecRow.titul_meno_priezvisko) { ForeColor = Color.Blue }
                        );

                        // Zamestnanci
                        foreach (var zamestnanec in oddelenie.Getoddelenie_zamestnanciRows())
                        {
                            var zamestnanecTreeNode = new TreeNode(zamestnanec.zamestnanecRow.titul_meno_priezvisko);

                            oddelenieTreeNode.Nodes.Add(zamestnanecTreeNode);
                        }

                        projektTreeNode.Nodes.Add(oddelenieTreeNode);
                    }

                    diviziaTreeNode.Nodes.Add(projektTreeNode);
                }

                root.Nodes.Add(diviziaTreeNode);
            }

            tree.ExpandAll();
            tree.EndUpdate();
        }
    }
}
