using MTGApiRequestToXml;
using MTGApiRequestToXml.Common.Utils;
using MTGApiRequestToXml.Data.Entities;
using MTGApiRequestToXml.Usecases;
using System.Windows.Forms;

namespace MTGApiRequestToXmlUI
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// program
        /// </summary>
        private MTGApiRequestToXml.Program prog;
        
        /// <summary>
        /// cardFilterUtil
        /// </summary>
        private CardFilterUtil cardFilterUtil;

        public Form1()
        {
            InitializeComponent();

            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(flowLayoutPanel1, true, null);

            prog = new MTGApiRequestToXml.Program();
            cardFilterUtil = new CardFilterUtil(prog.attributeBaseClass);
            LoadImagesToPanel();

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
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
                pictureBox.Name = Path.GetFileName(imageFile);
                pictureBox.Image = Image.FromFile(imageFile);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Height = 344; // Höhe anpassen
                pictureBox.Width = 244; // Breite anpassen
                pictureBox.Margin = new Padding(20); // Abstand zum nächsten Bild
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddPicture(Card card)
        {
            string imageFile = Path.Combine(prog.attributeBaseClass.folderPath, "Asset", "Images", string.Format("{0}.jpg", RegExUtil.FormatCardName(card.name)));
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = Path.GetFileName(imageFile);
            pictureBox.Image = Image.FromFile(imageFile);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Height = 344; // Höhe anpassen
            pictureBox.Width = 244; // Breite anpassen
            pictureBox.Margin = new Padding(20); // Abstand zum nächsten Bild
            flowLayoutPanel1.Controls.Add(pictureBox);
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            //TODO::Fix the weird buffer where when clicked just lags and showcases...
            //TODO::Change this logic will ya?

            Card card = await prog.GenerateRandomCommanderCards(1);

            if (card != null)
            {
                AddPicture(card);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // Prompt the user to enter the name of the card
            string cardName = Microsoft.VisualBasic.Interaction.InputBox(
                "Bitte geben Sie den Namen der Karte ein:",
                "Kartenname eingeben",
                "Standardname");

            if (string.IsNullOrWhiteSpace(cardName))
            {
                MessageBox.Show("Kein Kartenname eingegeben.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Card card = await prog.Serialize(cardName);
            
            if (card != null)
            {
                await prog.Imagedownload($"{RegExUtil.FormatCardName(card.name)}.xml");
                AddPicture(card);
                MessageBox.Show($"Karte gefunden: {card.name}", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Karte nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                cardFilterUtil.CardType = comboBox1.SelectedItem.ToString();
            }

            List<string> FilteredCard = await cardFilterUtil.Filter();
            if(FilteredCard.Count != 0)
            {
                flowLayoutPanel1.Visible = false;
                flowLayoutPanel1.SuspendLayout();

                try
                {
                    int i = 0;
                    foreach (Control control in flowLayoutPanel1.Controls)
                    {
                        if (control is PictureBox pictureBox)
                        {
                            pictureBox.Visible = false;

                            if (i < FilteredCard.Count)
                            {
                                string regedName = RegExUtil.FormatCardName(FilteredCard[i]);
                                string FileName = string.Format("{0}.jpg", regedName);

                                if (pictureBox.Name == FileName)
                                {
                                    pictureBox.Visible = true;
                                    i++;
                                }
                            }
                        }
                    }

                }
                finally
                {
                    flowLayoutPanel1.ResumeLayout();
                    flowLayoutPanel1.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Keine Karte gefunden.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox1.Checked;

            cardFilterUtil.White = isChecked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox4.Checked;

            cardFilterUtil.Water = isChecked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox3.Checked;
            
            cardFilterUtil.Black = isChecked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox6.Checked;

            cardFilterUtil.Fire = isChecked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox2.Checked;

            cardFilterUtil.Multi = isChecked;
        }
    }
}
