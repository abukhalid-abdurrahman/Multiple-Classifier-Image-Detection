using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageDetection
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ProccessJob(ofd.FileName);
                }
            }
        }

        private void ProccessJob(string fileName)
        {
            List<ClassifierProperty> classifierProperties = new List<ClassifierProperty>();
            ClassifierResearchResult classifierResearchResult = new ClassifierResearchResult();
            List<ClassifierResearchResult> classifierResearchResults = new List<ClassifierResearchResult>();
            classifierProperties.Add(
                new ClassifierProperty
                {
                    ClassifierFileName = "haarcascade_sneakers_alt_tree.xml",
                    ClassifierName = "Sneakers"
                });
            classifierProperties.Add(
                new ClassifierProperty
                {
                    ClassifierFileName = "haarcascade_shorts_alt_tree.xml",
                    ClassifierName = "Shorts"
                });


            foreach (var classifier in classifierProperties)
            {
                var cascadeClassifier = new CascadeClassifier(classifier.ClassifierFileName);
                Bitmap bitmap = LoadImage(fileName);
                classifierResearchResults.Add(new ImageDetection(cascadeClassifier).Detect(bitmap, classifier.ClassifierName));
            }

            classifierResearchResult = classifierResearchResults.FirstOrDefault(x => x.IsCompleted == true);
            if (classifierResearchResult != null)
            {
                imageBox.Image = classifierResearchResult.Bitmap;
                categoryLabel.Text = classifierResearchResult.ClassifierName;
            }
        }

        private Bitmap LoadImage(string _filename)
        {
            try
            {
                if (string.IsNullOrEmpty(_filename.Trim()))
                    return null;

                var bitmap = new Bitmap(_filename);
                return bitmap;
            }
            catch
            {
                return null;
            }
        }
    }
}
