using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;

namespace AI_TEST_01
{
    public class ImageController
    {
        private HaarObjectDetector detector;
        private PictureBox image;
        private Bitmap picture;

        public ImageController(PictureBox image)
        {
            this.image = image;

            /*
            cbMode.DataSource = Enum.GetValues(typeof(ObjectDetectorSearchMode));
            cbScaling.DataSource = Enum.GetValues(typeof(ObjectDetectorScalingMode));

            cbMode.SelectedItem = ObjectDetectorSearchMode.NoOverlap;
            cbScaling.SelectedItem = ObjectDetectorScalingMode.SmallerToGreater;
            */

            //toolStripStatusLabel1.Text = "Please select the detector options and click Detect to begin.";

            HaarCascade cascade = new FaceHaarCascade();
            detector = new HaarObjectDetector(cascade, 30);
        }

        public void LoadImage(string s)
        {
            picture = new Bitmap(s);
            image.Image = picture;
        }

        public void DetectFaces()
        {
            detector.SearchMode = (ObjectDetectorSearchMode)ObjectDetectorSearchMode.NoOverlap;
            detector.ScalingMode = (ObjectDetectorScalingMode)ObjectDetectorScalingMode.SmallerToGreater;
            detector.ScalingFactor = 1.5f;
            detector.UseParallelProcessing = true; //cbParallel.Checked;
            detector.Suppression = 2;

            //Stopwatch sw = Stopwatch.StartNew();

            // Process frame to detect objects
            Rectangle[] objects = detector.ProcessFrame(picture);

            //sw.Stop();

            if (objects.Length > 0)
            {
                RectanglesMarker marker = new RectanglesMarker(objects, Color.Red);
                image.Image = marker.Apply(picture);
            }

            //toolStripStatusLabel1.Text = string.Format("Completed detection of {0} objects in {1}.",
            //    objects.Length, sw.Elapsed);
        }
    }
}
