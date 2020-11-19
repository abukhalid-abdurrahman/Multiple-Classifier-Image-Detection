using System.Drawing;

namespace ImageDetection
{
    interface IDetectionImage
    {
        ClassifierResearchResult Detect(Bitmap bitmap, string _classifierName);
    }
}
