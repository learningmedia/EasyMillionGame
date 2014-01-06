using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace MillionGamePlayer
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Button[] buttons;

        private readonly Image[] images;

        public MainWindow()
        {
            InitializeComponent();

            buttons = new[] { btn_answer1, btn_answer2, btn_answer3, btn_answer4 };
            images = new[] { img_joker50, img_jokerPhone, img_jokerPublic };
            this.ShowQuestionButtons(false);
            this.EnableJokerImages(false);
            this.ShowJokerImages(false);
            lb_score.Visibility = Visibility.Hidden;
        }

        private void OnOpenClick(object sender, RoutedEventArgs e)
        {  
            OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Easy-Million-Game-Dateien (*.emg)|*.emg", 
                    Multiselect = false
                };

            bool result = (bool) ofd.ShowDialog();

            if (!result)
            {
                return;
            }

            string fileName = ofd.FileName;

            if (!string.IsNullOrEmpty(fileName))
            {
                MillionGameModel model = new MillionGameModel(new Game(fileName));
                this.DataContext = model;
                this.InitializeGame();
            }
        }

        /// <summary>
        /// Setzt die Ansicht auf den Anfangszustand.
        /// </summary>
        private void InitializeGame()
        {
            this.ShowQuestionButtons(true);
            this.EnableJokerImages(true);
            this.ShowJokerImages(true);

            this.lb_score.Visibility = Visibility.Visible;
            this.tb_question.Visibility = Visibility.Visible;
            this.tb_validation.Visibility = Visibility.Hidden;
            btn_nextQuestion.Visibility = Visibility.Hidden;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            MillionGameModel model = (MillionGameModel) DataContext;
            Game game = model.Game;
            Answer answer = (Answer) ((Button) sender).Content;
            btn_nextQuestion.Tag = answer;

            tb_question.Visibility = Visibility.Hidden;

            tb_validation.Visibility = Visibility.Visible;
            tb_validation.Text = model.GetCommendationMessage(answer);
            btn_nextQuestion.Visibility = Visibility.Visible;

            if (game.Level == game.LevelCount - 1)
            {
                btn_nextQuestion.Content = "Auswertung?";
            }
        }

        private void OnNextQuestionClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Answer answer = (Answer)button.Tag;
            MillionGameModel model = (MillionGameModel)DataContext;
            model.Game.Answer(answer);

            if ("Auswertung?" == button.Content.ToString())
            {
                this.ShowStatistics(model);
                return;
            }

            tb_validation.Visibility = Visibility.Hidden;
            tb_validation.Text = string.Empty;

            this.ShowQuestionButtons(true);

            btn_nextQuestion.Visibility = Visibility.Hidden;
            tb_question.Visibility = Visibility.Visible;
            tb_validation.Visibility = Visibility.Hidden;
        }

        private void ShowStatistics(MillionGameModel model)
        {
            Statistics statitics = model.Game.Statistics;
            string n = Environment.NewLine + Environment.NewLine;
            string statistic =
                string.Format(
                    "Richtige Antworten: {0}{1}Falsche Antworten: {2}{3}Benutzte Joker: {4}",
                    statitics.RightAnswers,
                    n,
                    statitics.WrongAnswers,
                    n,
                    statitics.JokersUsed);

            if (MessageBoxResult.OK == MessageBox.Show(statistic))
            {
                this.btn_nextQuestion.Content = "Nächste Frage";
                this.tb_question.Visibility = Visibility.Hidden;
                this.lb_score.Visibility = Visibility.Hidden;
                this.btn_nextQuestion.Visibility = Visibility.Hidden;
                this.tb_validation.Visibility = Visibility.Hidden;
                this.ShowJokerImages(false);
                this.ShowQuestionButtons(false);
            }
        }

        private void OnJokerClick(object sender, MouseButtonEventArgs e)
        {
            MillionGameModel model = (MillionGameModel)this.DataContext;
            Game game = model.Game;

            Image jokerImage = (Image) sender;
            Joker joker = (Joker) jokerImage.Tag;

            switch (joker.Type)
            {
                case JokerType.FiftyFifty:
                    this.SetFiftyFifty();
                    BitmapImage img1 = new BitmapImage(new Uri("Images/jokericonfiftyfiftydisabled.png", UriKind.Relative));
                    img_joker50.Source = img1;
                    img_joker50.IsEnabled = false;
                    break;
                case JokerType.Audience:
                    JokerResult resultAudience = game.UseJoker(joker);
                    AudienceJokerWindow audienceJokerWindow = new AudienceJokerWindow(resultAudience, buttons);
                    audienceJokerWindow.Show();
                    BitmapImage img2 = new BitmapImage(new Uri("Images/jokericonaudiencedisabled.png", UriKind.Relative));
                    img_jokerPublic.Source = img2;
                    img_jokerPublic.IsEnabled = false;
                    break;
                case JokerType.Telephone:
                    JokerResult resultTelefone = game.UseJoker(joker);
                    TelephoneJokerWindow telephoneJokerWindow = new TelephoneJokerWindow(resultTelefone);
                    telephoneJokerWindow.Show();
                    BitmapImage img3 = new BitmapImage(new Uri("Images/jokericontelephonedisabled.png", UriKind.Relative));
                    img_jokerPhone.Source = img3;
                    img_jokerPhone.IsEnabled = false;
                    break;
            }
        }

        private void SetFiftyFifty()
        {
            MillionGameModel model = (MillionGameModel) this.DataContext;
            Game game = model.Game;

            Joker joker5050 = game.Jokers.Single(x => x.Type == JokerType.FiftyFifty);
            game.UseJoker(joker5050);

            for (int i = 0; i < game.Answers.Count; i++)
            {
                buttons[i].Visibility = game.Answers[i].IsVisible ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void EnableJokerImages(bool value)
        {
            img_joker50.IsEnabled = value;
            this.img_joker50.Source = value
                                          ? new BitmapImage(
                                                new Uri("Images/jokericonfiftyfiftyenabled.png", UriKind.Relative))
                                          : new BitmapImage(
                                                new Uri("Images/jokericonfiftyfiftydisabled.png", UriKind.Relative));
            
            img_jokerPublic.IsEnabled = value;
            this.img_jokerPublic.Source = value
                                              ? new BitmapImage(
                                                    new Uri("Images/jokericonaudienceenabled.png", UriKind.Relative))
                                              : new BitmapImage(
                                                    new Uri("Images/jokericonaudiencedisabled.png", UriKind.Relative));

            img_jokerPhone.IsEnabled = value;
            this.img_jokerPhone.Source = value
                                             ? new BitmapImage(
                                                   new Uri("Images/jokericontelephoneenabled.png", UriKind.Relative))
                                             : new BitmapImage(
                                                   new Uri("Images/jokericontelephonedisabled.png", UriKind.Relative));
        }

        private void ShowQuestionButtons(bool value)
        {
            foreach (Button button in buttons)
            {
                button.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void ShowJokerImages(bool value)
        {
            foreach (Image image in images)
            {
                image.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }


        private void OnInfoClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(
                    "Easy Million Game (Player){0}{0}" +
                    "© 2009, Ulrich Kaiser und Andreas Helmberger{0}" +
                    "https://github.com/musikisum/EasyMillionGame",
                    Environment.NewLine),
                "Hinweis",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
