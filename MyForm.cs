using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static CyberRunner.Script;

namespace CyberRunner
{
    public class MyForm : Form
    {
        private Dictionary<string, Control> controlDictionary;
        private PrivateFontCollection fonts;

        private void FontsProjects()
        {
            fonts = new PrivateFontCollection();
            fonts.AddFontFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                @"Fonts\PostModernOne.ttf"));
            fonts.AddFontFile(Path.Combine(
                new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                @"Fonts\Consolas.ttf"));
        }

        private const int ClientSizeX = 1580;
        private const int ClientSizeY = 960;
        private const int ButtonWidth = 165;
        private const int ButtonHeight = 60;

        private Player myPlayer;
        private Game myGame;
        private PictureBox playerPicture;
        private TextBox textBoxMain;
        private TextBox textBoxStats;

        private Panel mainButtonsPanel = new()
        {
            Name = "mainButtonsPanel",
            Location = new Point(0, ClientSizeY - ButtonHeight - 40),
            Size = new Size(ClientSizeX, ButtonHeight + 40),
            BorderStyle = BorderStyle.None
        };

        private Panel choiceButtonsPanel = new()
        {
            Name = "choiceButtonsPanel",
            Location = new Point(20, ClientSizeY - 6 * ButtonHeight - 40),
            Size = new Size(880, 4 * ButtonHeight + 40),
            BorderStyle = BorderStyle.None,
            Visible = false
        };

        public MyForm(Player player, Game game)
        {
            controlDictionary = new Dictionary<string, Control>();
            FontsProjects();
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

            playerPicture = new PictureBox()
            {
                Name = "playerPicture",
                Location = new Point(900, 20),
                Size = new Size(300, 800)
            };

            //Главный textbox
            textBoxMain = new TextBox
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
                Enabled = false
            };

            //Статы textbox
            textBoxStats = new TextBox()
            {
                Name = "statsTextBox",
                BackColor = Color.White,
                Size = new Size(2 * ButtonWidth, ClientSizeY - 4 * ButtonHeight - 40),
                Location = new Point(ClientSizeX - 3 * ButtonWidth + 100, 20),
                TextAlign = HorizontalAlignment.Right,
                Text = "",
                ReadOnly = true,
                Multiline = true,
                Font = new Font(fonts.Families[1], 25),
                BorderStyle = BorderStyle.None
            };

            #region Buttons

            //Создание Main кнопок
            var submit = CreateDefaultButton(
                "Submit",
                new Point(mainButtonsPanel.Width - ButtonWidth - 20, mainButtonsPanel.Height - ButtonHeight - 20),
                "Подтвердить");
            submit.Enabled = false;

            var mBtn1 = CreateDefaultButton(
                "mBtn1",
                new Point(20, mainButtonsPanel.Height - ButtonHeight - 20),
                "Умник");

            var mBtn2 = CreateDefaultButton(
                "mBtn2",
                new Point(mBtn1.Location.X + ButtonWidth + 20,
                    mainButtonsPanel.Height - ButtonHeight - 20),
                "Имба");

            var mBtn3 = CreateDefaultButton(
                "mBtn3",
                new Point(mBtn2.Location.X + ButtonWidth + 20,
                    mainButtonsPanel.Height - ButtonHeight - 20),
                "Качок");

            #endregion

            Controls.Add(playerPicture);
            Controls.Add(textBoxMain);
            Controls.Add(textBoxStats);
            mainButtonsPanel.Controls.Add(mBtn1);
            mainButtonsPanel.Controls.Add(mBtn2);
            mainButtonsPanel.Controls.Add(mBtn3);
            mainButtonsPanel.Controls.Add(submit);
            foreach (Control control in mainButtonsPanel.Controls)
                controlDictionary.Add(control.Name, control);
            foreach (Control button in mainButtonsPanel.Controls)
                if (button != controlDictionary["Submit"])
                    button.Click += ChooseCharacter;
            submit.Click += CreateGame;
        }

        private void ChooseCharacter(object sender, EventArgs e)
        {
            var button = (Button) sender;
            var playerPictureFile = "";
            switch (button.Name)
            {
                case "mBtn1":
                    myPlayer = new Player(6, 4, 4, 5, 3, 5, 7);
                    playerPictureFile =
                        Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                            @"resources\Images\intellegent2.png");
                    break;
                case "mBtn2":
                    myPlayer = new Player(10, 10, 10, 10, 10, 10, 10);
                    playerPictureFile = Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                        @"resources\Images\psycho2.png");
                    break;
                case "mBtn3":
                    myPlayer = new Player(4, 8, 8, 4, 7, 4, 4);
                    playerPictureFile = Path.Combine(
                        new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName,
                        @"resources\Images\powerful2.png");
                    break;
            }

            playerPicture.Image = Image.FromFile(playerPictureFile);
            controlDictionary["Submit"].Enabled = true;
            textBoxMain.Text = myPlayer.ToString();
        }

        private void CreateGame(object sender, EventArgs e)
        {
            textBoxMain.Enabled = true;
            textBoxStats.Text = textBoxMain.Text;
            myGame.CurrentChapterNumber = 0;
            textBoxMain.Clear();
            textBoxMain.Size = new Size(880, ClientSizeY - 7 * ButtonHeight - 20);
            Controls.Add(choiceButtonsPanel);
            choiceButtonsPanel.Visible = true;
            myGame.GameList.AddFirst(new Chapter(
                Scripts[myGame.GameList.Count].Item1.Text,
                Scripts[myGame.GameList.Count].Item2));
            textBoxMain.Text = myGame.GameList.First?.Value.CurrentChapterText;
            textBoxMain.Font = new Font(fonts.Families[0], 25);

            //Постановка Choice кнопок
            PlaceChoiceButtons(Scripts[myGame.GameList.Count - 1].Item2.Length,
                Scripts[myGame.GameList.Count - 1].Item2);
            //Перестановка Main кнопок
            controlDictionary["mBtn3"].Visible = false;
            controlDictionary["mBtn1"].Text = "Предыдущий";
            controlDictionary["mBtn1"].Click += ShowPreviousSlide;
            controlDictionary["mBtn2"].Text = "Следующий";
            controlDictionary["mBtn2"].Click += ShowNextSlide;
            controlDictionary["Submit"].Text = "Подсказка до концовки";
            controlDictionary["Submit"].Click += FindShortestEnding;
            foreach (Button button in mainButtonsPanel.Controls)
            {
                button.Click -= ChooseCharacter;
                button.Click -= CreateGame;
            }
        }

        private static Button CreateDefaultButton(string name, Point location, string text)
        {
            return new()
            {
                Name = name,
                Location = location,
                Size = new Size(ButtonWidth, ButtonHeight),
                Text = text
            };
        }

        private void SkillCheck(object sender, EventArgs e)
        {
            var button = (Button) sender;
            var buttonNumber = button.Name != "Next"
                ? int.Parse(button.Name)
                : Scripts[myGame.CurrentChapterNumber].Item1.Branch;
            var skillCheck = new SkillCheck(0, -1);
            if (myGame.GameList.Last?.Value.Choices.Length != 0)
                skillCheck = myGame.GameList.Last?.Value.Choices[buttonNumber].Check;
            if (skillCheck == null || skillCheck.Power >= myPlayer.PlayerSkills[skillCheck.Skill]) return;
            if (Scripts[myGame.CurrentChapterNumber + 1].Item1.Branch == buttonNumber)
            {
                myGame.CurrentChapterNumber++;
                CreateNextChapter(myGame.CurrentChapterNumber);
            }
            else if (Scripts[myGame.CurrentChapterNumber + 1].Item1.Branch == -1)
            {
                myGame.CurrentChapterNumber++;
                CreateNextChapter(myGame.CurrentChapterNumber);
            }
            else if (Scripts[myGame.CurrentChapterNumber + 1].Item1.Branch != buttonNumber)
            {
                while (true)
                {
                    myGame.CurrentChapterNumber++;
                    if (Scripts[myGame.CurrentChapterNumber + 1].Item1.Branch == buttonNumber ||
                        Scripts[myGame.CurrentChapterNumber + 1].Item1.Branch == -1)
                        break;
                }

                myGame.CurrentChapterNumber++;
                CreateNextChapter(myGame.CurrentChapterNumber);
            }
        }

        private void CreateNextChapter(int choice)
        {
            myGame.GameList.AddLast(new Chapter(Scripts[choice].Item1.Text, Scripts[choice].Item2));
            if (choice == Scripts.Length - 1)
            {
                choiceButtonsPanel.Controls.Clear();
                return;
            }

            if (Scripts[choice].Item1.Upgrade != 0)
                myPlayer.PlayerSkills[Scripts[choice].Item1.Skill] += Scripts[choice].Item1.Upgrade;
            if (myPlayer.PlayerSkills[CyberRunner.SkillCheck.SkillList.Health] <= 0)
            {
                textBoxMain.Text =
                    "Погоня за андроидами поглотила вас. Участок посылал и других на поиски беглецов, но все они быстро погибли. Вскоре погиб и сам участок.";
                choiceButtonsPanel.Controls.Clear();
                return;
            }

            textBoxMain.Text = myGame.GameList.Last?.Value.CurrentChapterText;
            textBoxStats.Text = myPlayer.ToString();
            PlaceChoiceButtons(myGame.GameList.Last.Value.Choices.Length, myGame.GameList.Last.Value.Choices);
        }

        private void PlaceChoiceButtons(int buttonsCount, Choice[] choices)
        {
            choiceButtonsPanel.Controls.Clear();
            if (buttonsCount == 0)
            {
                choiceButtonsPanel.Controls.Add(new Button
                {
                    Name = "Next",
                    Text = "Продолжить",
                    Font = new Font(fonts.Families[1], 40),
                    Location = new Point(20, 20),
                    Size = new Size(choiceButtonsPanel.Size.Width - 40, choiceButtonsPanel.Size.Height - 40),
                    Visible = true
                });
            }
            else
            {
                var buttonWidth = choiceButtonsPanel.Width - 40;
                var buttonHeight = (choiceButtonsPanel.Height - 20 * buttonsCount - 20) / buttonsCount;
                for (var i = 0; i < buttonsCount; i++)
                    if (i == 0)
                        choiceButtonsPanel.Controls.Add(new Button
                        {
                            Name = $"{i}",
                            Text = choices[i].Text,
                            Location = new Point(20, 20),
                            Size = new Size(buttonWidth, buttonHeight),
                            Visible = true
                        });
                    else
                        choiceButtonsPanel.Controls.Add(new Button
                        {
                            Name = $"{i}",
                            Text = choices[i].Text,
                            Location = new Point(choiceButtonsPanel.Controls[i - 1].Location.X,
                                choiceButtonsPanel.Controls[i - 1].Location.Y + 20 + buttonHeight),
                            Size = new Size(buttonWidth, buttonHeight)
                        });
            }

            foreach (Button button in choiceButtonsPanel.Controls)
                button.Click += SkillCheck;
        }

        private int slideDelta = 0;

        private void ShowPreviousSlide(object sender, EventArgs e)
        {
            var gameArray = myGame.GameList.ToArray();
            slideDelta--;
            if (Math.Abs(slideDelta) == myGame.GameList.Count)
            {
                slideDelta++;
                return;
            }

            choiceButtonsPanel.Visible = false;
            textBoxMain.Text = gameArray[myGame.GameList.Count - 1 + slideDelta].CurrentChapterText;
        }

        private void ShowNextSlide(object sender, EventArgs e)
        {
            var gameArray = myGame.GameList.ToArray();
            slideDelta++;
            if (slideDelta > 0)
            {
                slideDelta--;
                return;
            }

            if (slideDelta == 0)
                choiceButtonsPanel.Visible = true;
            textBoxMain.Text = gameArray[myGame.GameList.Count - 1 + slideDelta].CurrentChapterText;
        }

        private class LocalNode
        {
            public Player LPlayer;
            public LocalNode LowNode;
            public LocalNode HiNode;
            public LocalNode EqNode;
            public int Value = 0;
            public SkillCheck CheckSkill;
            public List<Node> UpgradeList;
        }

        //Ну, не успел :(
        private void FindShortestEnding(object sender, EventArgs e)
        {
            var endList = new List<LocalNode>();
            for (var i = myGame.CurrentChapterNumber; i < Scripts.Length - 1; i++)
            {
                var ListMember = new LocalNode();
                if (Scripts[i].Item1.Branch != -1)
                    continue;
                if (Scripts[i + 1].Item1.Branch != -1)
                {
                    var j = i;
                    while (Scripts[j].Item1.Branch != -1)
                    {
                        var statement = Scripts[j].Item1.Branch;
                        switch (statement)
                        {
                            case 0:
                            {
                                ListMember.LowNode = new LocalNode();
                                ListMember.LowNode.Value += 1;
                                ListMember.LowNode.CheckSkill = Scripts[i].Item2[0].Check;
                                if (Scripts[i].Item1.Upgrade != 0)
                                    ListMember.LowNode.UpgradeList.Add(Scripts[i].Item1);
                                break;
                            }
                            case 1:
                            {
                                ListMember.EqNode = new LocalNode();
                                ListMember.EqNode.Value += 1;
                                ListMember.EqNode.CheckSkill = Scripts[i].Item2[1].Check;
                                if (Scripts[i].Item1.Upgrade != 0)
                                    ListMember.EqNode.UpgradeList.Add(Scripts[i].Item1);
                                break;
                            }
                            case 2:
                            {
                                ListMember.HiNode = new LocalNode();
                                ListMember.HiNode.Value += 1;
                                ListMember.HiNode.CheckSkill = Scripts[i].Item2[2].Check;
                                if (Scripts[i].Item1.Upgrade != 0)
                                    ListMember.HiNode.UpgradeList.Add(Scripts[i].Item1);
                                break;
                            }
                        }

                        j++;
                    }

                    endList.Add(ListMember);
                }
            }
        }
    }
}