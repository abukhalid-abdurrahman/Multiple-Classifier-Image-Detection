using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace ImageDetection
{
    class ImageDetection : IDetectionImage
    {
        private readonly CascadeClassifier cascadeClassifier;
        public ImageDetection(CascadeClassifier _cascadeClassifier)
        {
            this.cascadeClassifier = _cascadeClassifier;
        }
        public ClassifierResearchResult Detect(Bitmap _bitmap, string _classifierName)
        {
            bool isResearchCompletedSuccessfuly = false;
            try
            {
                Image<Bgr, byte> grayImage = new Image<Bgr, byte>(_bitmap);
                Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage);
                foreach (var rect in rectangles)
                {
                    using (Graphics graph = Graphics.FromImage(_bitmap))
                    {
                        using (Pen pen = new Pen(Color.Black, 1))
                        {
                            graph.DrawRectangle(pen, rect);
                            isResearchCompletedSuccessfuly = true;
                        }
                    }
                }
                return new ClassifierResearchResult { Bitmap = _bitmap, ClassifierName = _classifierName, IsCompleted = isResearchCompletedSuccessfuly };
            }
            catch
            {
                return new ClassifierResearchResult { Bitmap = _bitmap, ClassifierName = _classifierName, IsCompleted = isResearchCompletedSuccessfuly };
            }
        }
    }
}
