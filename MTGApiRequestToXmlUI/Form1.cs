using MTGApiRequestToXml;

namespace MTGApiRequestToXmlUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //TODO::to Write Load contents
            MTGApiRequestToXml.Program.Load();
            LoadImagesToPanel();

        }

        private void LoadImagesToPanel()
        {
            string folderPath = @"C:\Users\BATSAIKHAN_0001\source\repos\MTGApiRequestToXml\MTGApiRequestToXml\Asset\Images";

            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("Failed to Load images. Please try again.");
                return;
            }

            string[] imageFiles = Directory.GetFiles(folderPath);

            foreach (string imageFile in imageFiles)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(imageFile);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 344; // Höhe anpassen
                pictureBox.Width = 244; // Breite anpassen
                pictureBox.Margin = new Padding(10); // Abstand zum nächsten Bild
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
