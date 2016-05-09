using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeFixedLength
{
    public partial class MakeItFixedLength : Form
    {
        public MakeItFixedLength()
        {
            InitializeComponent();
        }

        private void btn_Convert_Click(object sender, EventArgs e)
        {
            try
            {
                String outputFilePath = Path.Combine(Path.GetDirectoryName(textBox1.Text), "Output", Path.GetFileName(textBox1.Text));
                Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
                if (!String.IsNullOrWhiteSpace(textBox1.Text))
                {
                    Int32 charLength = 0;
                    if (String.IsNullOrWhiteSpace(tbCharLength.Text))
                    {
                        MessageBox.Show("Please enter character length");
                        return;
                    }
                    else
                    {
                        charLength = Convert.ToInt32(tbCharLength.Text);
                    }
                    ConvertFileToFixedLength(outputFilePath, charLength);
                }
                else
                    MessageBox.Show("Please select a file");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ConvertFileToFixedLength(String outputFilePath, Int32 charLength)
        {            
            using (StreamReader reader = new StreamReader(textBox1.Text))
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                string line;
                bool isFirstLine = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!isFirstLine)
                        writer.Write(Environment.NewLine);
                    else
                        isFirstLine = false;

                    if (line.Length >= charLength)
                    {
                        writer.Write(line);
                    }
                    else
                    {
                        writer.Write(line + new String(' ', charLength - line.Length));
                    }
                }
            }
            MessageBox.Show("Converted file available @ "+outputFilePath);
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {            
            DialogResult result = openFileDialog1.ShowDialog(); 
            if (result == DialogResult.OK) // Test result.
            {
               textBox1.Text = openFileDialog1.FileName;               
            }
        }
    }
}
