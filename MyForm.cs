using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace CyberRunner
{
    public class MyForm : Form
    {
        const int ClientSizeX = 1280;
        const int ClientSizeY = 960;
        const int ButtonWidth = 165;
        const int ButtonHeight = 60;
        private Player myPlayer;
        private TextBox textBoxPlayer;
        private Panel mainButtonsPanel = new Panel()
        {
            Location = new Point(0,ClientSizeY - ButtonHeight - 40),
            Size =  new Size(ClientSizeX, ButtonHeight + 40),
            BorderStyle = BorderStyle.FixedSingle,
            //BackColor = Color.Black,
        };
        public MyForm(Player player)
        {
            // Устанавливаем нужный шрифт
            var fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\PostModernOne.ttf"); // файл шрифта
            var family = fontCollection.Families[0];
            // Создаём шрифт и используем далее
            var font = new Font(family, 40);
            
            myPlayer = player;
            Controls.Add(mainButtonsPanel);
            
            //форма
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(ClientSizeX, ClientSizeY);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MyForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Окно";

            //textbox
            textBoxPlayer = new TextBox
            {
                Size = new Size(4 * ButtonWidth, ClientSizeY - 2 * ButtonHeight - 20),
                Location = new Point(20, 20),
                Text = player.ToString(),
                TextAlign = HorizontalAlignment.Left,
                ReadOnly = true,
                Font = font,
                Multiline = true,
                BorderStyle = BorderStyle.None,
            };

            var useBuffButton = new Button
            {

            };
            
            #region ButtonsWithChooseCharacter

            var ch1 = new Button
            {
                Name = "charButton1",
                Size = new Size(ButtonWidth, ButtonHeight),
                Text = "Intellegent",
                Location = new Point(20, mainButtonsPanel.Height - ButtonHeight - 20),
            };
            var ch2 = new Button
            {
                Name = "charButton2",
                Size = new Size(165, 60),
                Text = "Psycho",
                Location = new Point(mainButtonsPanel.Width - 20 - ButtonWidth,
                    mainButtonsPanel.Height - ButtonHeight - 20),
            };
            var ch3 = new Button
            {
                Name = "charButton3",
                Size = new Size(ButtonWidth, ButtonHeight),
                Text = "Strong",
                Location = new Point(mainButtonsPanel.Width / 2 - ButtonWidth / 2,
                    mainButtonsPanel.Height - ButtonHeight - 20),
                
            };
            Controls.Add(textBoxPlayer);
            mainButtonsPanel.Controls.Add(ch1);
            mainButtonsPanel.Controls.Add(ch2);
            mainButtonsPanel.Controls.Add(ch3);
            ch1.Click += ChooseCharacter;
            ch2.Click += ChooseCharacter;
            ch3.Click += ChooseCharacter;
            #endregion
        }
        private void ChooseCharacter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch (button.Name)
            {
                case "charButton1":
                    myPlayer = new Player(1,1,1,1,1,1,1);
                    textBoxPlayer.Text = myPlayer.ToString();
                    break;
                case "charButton2":
                    myPlayer = new Player(5,5,5,5,5,5,5);
                    textBoxPlayer.Text = myPlayer.ToString();
                    break;
                case "charButton3":
                    myPlayer = new Player(8, 8, 8, 8, 8, 8, 8);
                    textBoxPlayer.Text = myPlayer.ToString();
                    break;
            }
            mainButtonsPanel.Controls.Clear();
        }
    }
}