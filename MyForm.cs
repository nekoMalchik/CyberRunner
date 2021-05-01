using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CyberRunner
{
    public class MyForm : Form
    {
        PrivateFontCollection font;
        private void fontsProjects()
        {
            font = new PrivateFontCollection();
            font.AddFontFile(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\bin\Debug\font\PostModernOne.ttf");
            font.AddFontFile(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\bin\Debug\font\Consolas.ttf");
        }
        const int ClientSizeX = 1280;
        const int ClientSizeY = 960;
        const int ButtonWidth = 165;
        const int ButtonHeight = 60;
        private Player myPlayer;
        private Game myGame;
        private TextBox textBoxPlayer;
        private Panel mainButtonsPanel = new Panel()
        {
            Location = new Point(0,ClientSizeY - ButtonHeight - 40),
            Size =  new Size(ClientSizeX, ButtonHeight + 40),
            BorderStyle = BorderStyle.None,
            //BackColor = Color.Black,
        };
        public MyForm(Player player, Game game)
        {
            fontsProjects();
            BackColor = Color.White;
            myGame = game;
            myPlayer = player;
            Controls.Add(mainButtonsPanel);
            
            //форма
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(ClientSizeX, ClientSizeY);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MyForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Окно";

            //Статы
            textBoxPlayer = new TextBox
            {
                Size = new Size(3 * ButtonWidth + 40, ClientSizeY - 4 * ButtonHeight - 20),
                Location = new Point(20, 20),
                Text = "",
                TextAlign = HorizontalAlignment.Left,
                ReadOnly = true,
                Font = new Font(font.Families[1], 40),
                Multiline = true,
                BorderStyle = BorderStyle.FixedSingle,
            };

            var BuffButton = new Button
            {
                Text = "Подтвердить",
                Size = new Size(ButtonWidth, ButtonHeight),
                Location = new Point(mainButtonsPanel.Width - ButtonWidth - 20,
                    mainButtonsPanel.Height - ButtonHeight - 20),
            };
            
            #region ButtonsWithChooseCharacter

            var ch1 = new Button
            {
                Name = "charButton1",
                Size = new Size(ButtonWidth, ButtonHeight),
                Text = "Умник",
                Location = new Point(20, mainButtonsPanel.Height - ButtonHeight - 20),
            };
            var ch2 = new Button
            {
                Name = "charButton2",
                Size = new Size(165, 60),
                Text = "Псих",
                Location = new Point(ch1.Location.X + ButtonWidth + 20,
                    mainButtonsPanel.Height - ButtonHeight - 20),
            };
            var ch3 = new Button
            {
                Name = "charButton3",
                Size = new Size(ButtonWidth, ButtonHeight),
                Text = "Качок",
                Location = new Point(ch2.Location.X + ButtonWidth + 20,
                    mainButtonsPanel.Height - ButtonHeight - 20),
                Visible = true,
            };
            
            Controls.Add(textBoxPlayer);
            mainButtonsPanel.Controls.Add(ch1);
            mainButtonsPanel.Controls.Add(ch2);
            mainButtonsPanel.Controls.Add(ch3);
            mainButtonsPanel.Controls.Add(BuffButton);
            foreach (Control button in mainButtonsPanel.Controls)
            {
                button.Click += ChooseCharacter;
            }
            BuffButton.Click += CreateGame;

            #endregion
        }
        private void ChooseCharacter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch (button.Name)
            {
                case "charButton1":
                    myPlayer = new Player(6,4,4,5,3,5,7);
                    BackgroundImage =
                        new Bitmap(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\intellegent.png");
                    break;
                case "charButton2":
                    myPlayer = new Player(5,5,5,5,5,5,5);
                    BackgroundImage =
                        new Bitmap(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\psycho.png");
                    break;
                case "charButton3":
                    myPlayer = new Player(8, 8, 8, 8, 8, 8, 8);
                    BackgroundImage =
                        new Bitmap(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\powerful.png");
                    break;
            }
            textBoxPlayer.Text = myPlayer.ToString();

        }
        private void CreateGame(object sender, EventArgs e)
        {
            textBoxPlayer.Clear();
            textBoxPlayer.Width = ClientSizeX - 20 - 3 * ButtonWidth;
            textBoxPlayer.Text = myGame.CurrentText;
            textBoxPlayer.Font = new Font(font.Families[0], 20);
            foreach (Button button in mainButtonsPanel.Controls)
            {
                if (button.Name == "charButton3")
                    button.Visible = false;
                if (button.Name == "charButton3" || button.Name == "charButton2" || button.Name == "charButton1")
                    button.Click -= ChooseCharacter;
            }
            
            // textBoxPlayer = new TextBox
            // {
            //     Size = new Size(ClientSizeX - 20 - 3 * ButtonWidth, ClientSizeY - 4 * ButtonHeight - 20),
            //     Location = new Point(20, 20),
            //     Text = myGame.CurrentText,
            //     TextAlign = HorizontalAlignment.Left,
            //     ReadOnly = true,
            //     Font = myGame.MainFont,
            //     Multiline = true,
            //     BorderStyle = BorderStyle.FixedSingle,
            // };
        }
    }
}