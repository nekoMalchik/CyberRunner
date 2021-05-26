using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static CyberRunner.Script;

namespace CyberRunner
{
    public class MyForm : Form
    {
        private Dictionary<string, Control> controlDictionary;
        private PrivateFontCollection fonts;
        private void fontsProjects()
        {
            fonts = new PrivateFontCollection();
            fonts.AddFontFile(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\PostModernOne.ttf");
            fonts.AddFontFile(@"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\Consolas.ttf");
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
            Name = "mainButtonsPanel",
            Location = new Point(0,ClientSizeY - ButtonHeight - 40),
            Size =  new Size(ClientSizeX, ButtonHeight + 40),
            BorderStyle = BorderStyle.None,
        };
        
        private Panel choiceButtonsPanel = new Panel()
        {
            Name = "choiceButtonsPanel",
            Location = new Point(20,ClientSizeY - 6 * ButtonHeight - 40),
            Size =  new Size(ClientSizeX - 20 - 3 * ButtonWidth, 4 * ButtonHeight + 40),
            BorderStyle = BorderStyle.FixedSingle,
            Visible = false,
        };
        
        public MyForm(Player player, Game game)
        {
            controlDictionary = new Dictionary<string, Control>();
            fontsProjects();
            BackColor = Color.White;
            myGame = game;
            myPlayer = player;
            Controls.Add(mainButtonsPanel);

            //форма
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(ClientSizeX, ClientSizeY);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "myForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Окно";

            //Статы
            textBoxPlayer = new TextBox
            {
                Name = "mainTextBox",
                BackColor = Color.White,
                Size = new Size(3 * ButtonWidth + 40, ClientSizeY - 4 * ButtonHeight - 40),
                Location = new Point(20, 20),
                Text = "Выберете персонажа",
                TextAlign = HorizontalAlignment.Left,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font(fonts.Families[1], 40),
                Multiline = true,
                BorderStyle = BorderStyle.None,
                //Enabled = false,
            };

            #region Buttons
            
            //Создание Main кнопок
            var inventory = CreateDefaultButton(
                "Inventory",
                new Point(mainButtonsPanel.Width - ButtonWidth - 20, mainButtonsPanel.Height - ButtonHeight - 20),
                "Подтвердить");
            inventory.Enabled = false;

            var mBtn1 = CreateDefaultButton(
                "mBtn1",
                new Point(20, mainButtonsPanel.Height - ButtonHeight - 20),
                "Умник");
 
            var mBtn2 = CreateDefaultButton(
                "mBtn2",
                new Point(mBtn1.Location.X + ButtonWidth + 20,
                    mainButtonsPanel.Height - ButtonHeight - 20),
                "Псих");
     
            var mBtn3 = CreateDefaultButton(
                "mBtn3",
                new Point(mBtn2.Location.X + ButtonWidth + 20,
                    mainButtonsPanel.Height - ButtonHeight - 20),
                "Качок");
            #endregion
            
            Controls.Add(textBoxPlayer);
            mainButtonsPanel.Controls.Add(mBtn1);
            mainButtonsPanel.Controls.Add(mBtn2);
            mainButtonsPanel.Controls.Add(mBtn3);
            mainButtonsPanel.Controls.Add(inventory);
            foreach (Control control in mainButtonsPanel.Controls)
                controlDictionary.Add(control.Name, control);
            foreach (Control button in mainButtonsPanel.Controls)
               if (button != controlDictionary["Inventory"])
                    button.Click += ChooseCharacter;
            inventory.Click += CreateGame;
        }
        private void ChooseCharacter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var backgroundFile = "";
            switch (button.Name)
            {
                case "mBtn1":
                    myPlayer = new Player(6,4,4,5,3,5,7);
                    backgroundFile =
                        @"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\intellegent.png";
                    break;
                case "mBtn2":
                    myPlayer = new Player(10,10,10,10,10,10,10);
                    backgroundFile = 
                        @"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\psycho.png";
                    break;
                case "mBtn3":
                    myPlayer = new Player(1, 1, 1, 1, 1, 1, 1);
                    backgroundFile =
                        @"C:\Users\Пользователь\Documents\GitHub\CyberRunner\resourses\powerful.png";
                    break;
            }
            BackgroundImage = new Bitmap(backgroundFile);
            myPlayer.Background = backgroundFile;
            controlDictionary["Inventory"].Enabled = true;
            textBoxPlayer.Text = myPlayer.ToString();

        }
        private void CreateGame(object sender, EventArgs e)
        {
            myGame.CurrentChapterNumber = 0;
            textBoxPlayer.Clear();
            textBoxPlayer.Size = new Size(ClientSizeX - 20 - 3 * ButtonWidth, ClientSizeY - 7 * ButtonHeight - 20);
            Controls.Add(choiceButtonsPanel);
            choiceButtonsPanel.Visible = true;
            myGame.GameList.AddFirst(new Chapter(
                Nodes[myGame.GameList.Count].Item1.Text,
                Nodes[myGame.GameList.Count].Item2));
            textBoxPlayer.Text = myGame.GameList.First?.Value.CurrentChapterText;
            textBoxPlayer.Font = new Font(fonts.Families[0], 25);
            
            //Постановка Choice кнопок
            PlaceChoiceButtons(Nodes[myGame.GameList.Count - 1].Item2.Length, Nodes[myGame.GameList.Count - 1].Item2);
            //PlaceChoiceButtons(ScriptsAndChoices[myGame.CurrentChapterNumber].Item2.Length, ScriptsAndChoices[myGame.CurrentChapterNumber].Item2);
            //Перестановка Main кнопок
            controlDictionary["mBtn3"].Visible = false;
            controlDictionary["mBtn1"].Text = "Предыдущий";
            controlDictionary["mBtn1"].Enabled = false;
            controlDictionary["mBtn2"].Enabled = false;
            controlDictionary["mBtn2"].Text = "Следующий";
            controlDictionary["Inventory"].Text = "Инвентарь";
            foreach (Button button in mainButtonsPanel.Controls)
            {
                button.Click -= ChooseCharacter;
                button.Click -= CreateGame;
            }
        }

        private static Button CreateDefaultButton(string name, Point location, string text)
        {
            return new Button
            {
                Name = name,
                Location = location,
                Size = new Size(ButtonWidth,ButtonHeight),
                Text = text,
            };
        }

        private void SkillCheck(object sender, EventArgs e)
        {
            var button = (Button) sender;
            var buttonNumber = button.Name != "Next" ? int.Parse(button.Name) : Nodes[myGame.CurrentChapterNumber].Item1.Branch;
            var skillCheck = new SkillCheck(0, -1);
            if (myGame.GameList.Last?.Value.Choices.Length != 0)
                skillCheck = myGame.GameList.Last?.Value.Choices[buttonNumber].Check;
            if (skillCheck == null || skillCheck.Power >= myPlayer.PlayerSkills[skillCheck.Skill]) return;
            if (Nodes[myGame.CurrentChapterNumber + 1].Item1.Branch == buttonNumber)
            {
                myGame.CurrentChapterNumber++;
                CreateNextChapter(myGame.CurrentChapterNumber);
            }
            else if (Nodes[myGame.CurrentChapterNumber + 1].Item1.Branch == -1)
            {
                myGame.CurrentChapterNumber++;
                CreateNextChapter(myGame.CurrentChapterNumber);
            }
            else if (Nodes[myGame.CurrentChapterNumber + 1].Item1.Branch != buttonNumber)
            {
                while (true)
                {
                    myGame.CurrentChapterNumber++;
                    if (Nodes[myGame.CurrentChapterNumber + 1].Item1.Branch == buttonNumber || Nodes[myGame.CurrentChapterNumber + 1].Item1.Branch == -1)
                        break;
                }
                myGame.CurrentChapterNumber++;
                CreateNextChapter(myGame.CurrentChapterNumber);
            }
        }

        private void CreateNextChapter(int choice)
        {
            myGame.GameList.AddLast(new Chapter(Nodes[choice].Item1.Text, Nodes[choice].Item2));
            if (Nodes[choice].Item1.Upgrade != 0)
                myPlayer.PlayerSkills[Nodes[choice].Item1.Skill]+= Nodes[choice].Item1.Upgrade;
            if (myPlayer.PlayerSkills[CyberRunner.SkillCheck.SkillList.Health] <= 0)
            {
                textBoxPlayer.Text =
                    "Погоня за андроидами поглотила вас. Участок посылал и других на поиски беглецов, но все они быстро погибли. Вскоре погиб и сам участок.";
                PlaceChoiceButtons(0, new Choice[0]);
                return;
            }
            textBoxPlayer.Text = myGame.GameList.Last?.Value.CurrentChapterText;
            //PlaceChoiceButtons(Nodes[myGame.GameList.Count - 1].Item2.Length, Nodes[myGame.GameList.Count - 1].Item2);
            PlaceChoiceButtons(myGame.GameList.Last.Value.Choices.Length, myGame.GameList.Last.Value.Choices);
        }

        private void PlaceChoiceButtons(int buttonsCount, Choice[] choices)
        {
            choiceButtonsPanel.Controls.Clear();
            if (buttonsCount == 0)
                choiceButtonsPanel.Controls.Add(new Button
                {
                    Name = "Next",
                    Text = "Продолжить",
                    Font = new Font(fonts.Families[1], 40),
                    Location = new Point(20, 20),
                    Size = new Size(choiceButtonsPanel.Size.Width - 40, choiceButtonsPanel.Size.Height - 40),
                    Visible = true,
                });
            else
            {
                var buttonWidth = choiceButtonsPanel.Width - 40;
                var buttonHeight = (choiceButtonsPanel.Height - 20 * buttonsCount - 20) / buttonsCount;
                for (var i = 0; i < buttonsCount; i++)
                {
                    if (i == 0)
                        choiceButtonsPanel.Controls.Add(new Button
                        {
                            Name = $"{i}",
                            Text = choices[i].Text,
                            Location = new Point(20, 20),
                            Size = new Size(buttonWidth, buttonHeight),
                            Visible = true,
                        });
                    else
                        choiceButtonsPanel.Controls.Add(new Button
                        {
                            Name = $"{i}",
                            Text = choices[i].Text,
                            Location = new Point(choiceButtonsPanel.Controls[i - 1].Location.X,
                                choiceButtonsPanel.Controls[i - 1].Location.Y + 20 + buttonHeight),
                            Size = new Size(buttonWidth, buttonHeight),
                        });
                }
            }
            foreach (Button button in choiceButtonsPanel.Controls)
                button.Click += SkillCheck;   

        }
    }
}