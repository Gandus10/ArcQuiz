using ArcQuiz.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Reflection;
using System;
using Newtonsoft.Json.Linq;
using ArcQuiz.View;

namespace ArcQuiz.ViewModel
{


    class QuestionViewModel : INotifyPropertyChanged
    {
        private static readonly Color colorAnswerCorrect = Color.FromHex("#27ae60");
        private static readonly Color colorAnswerIncorrect = Color.FromHex("#d63031");
        private static readonly Color colorButton = Color.White;

        private bool canClick;
        public bool CanClick
        {
            get => canClick;
            set
            {
                canClick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CanClick"));
            }
        }
        private string FileName { get; set; }
        private int TotalNumberOfQuestions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CheckResponseCommand { get; private set; }

        public QuestionViewModel(string fileName)
        {
            canClick = true;
            FileName = fileName;
            CheckResponseCommand = new Command<string>(CheckResponse);
            LoadJson();
            TotalNumberOfQuestions = listQuestion.Count;
            labelProgression = "QuestionModel 1/" + TotalNumberOfQuestions;
            LoadNextQuestion();
        }

        private List<QuestionModel> listQuestion;

        private int score = 0;
        public int Score
        {
            get { return score; }
            set
            {
                if (score != value)
                {
                    score = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Score"));
                }
            }
        }


        private string labelProgression;
        public string LabelProgression
        {
            get => labelProgression;
            set
            {
                labelProgression = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LabelProgression"));
            }
        }

        private Color colorButton1;
        public Color ColorButton1
        {
            get => colorButton1;
            set
            {
                if (colorButton1 != value)
                {
                    colorButton1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorButton1"));
                }
            }
        }
        private Color colorButton2;
        public Color ColorButton2
        {
            get => colorButton2;
            set
            {
                if (colorButton2 != value)
                {
                    colorButton2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorButton2"));
                }
            }
        }
        private Color colorButton3;
        public Color ColorButton3
        {
            get => colorButton3;
            set
            {
                if (colorButton3 != value)
                {
                    colorButton3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorButton3"));
                }
            }
        }
        private Color colorButton4;
        public Color ColorButton4
        {
            get => colorButton4;
            set
            {
                if (colorButton4 != value)
                {
                    colorButton4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorButton4"));
                }
            }
        }

        private string question;
        public string Question
        {
            get => question;
            set
            {
                if (question != value)
                {
                    question = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Question"));
                }
            }
        }

        private string Response { get; set; }

        private string response1;
        public string Response1
        {
            get => response1;
            set
            {
                if (response1 != value)
                {
                    response1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Response1"));
                }
            }
        }

        private string response2;
        public string Response2
        {
            get => response2;
            set
            {
                if (response2 != value)
                {
                    response2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Response2"));
                }
            }
        }

        private string response3;
        public string Response3
        {
            get => response3;
            set
            {
                if (response3 != value)
                {
                    response3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Response3"));
                }
            }
        }


        private string response4;
        public string Response4
        {
            get => response4;
            set
            {
                if (response4 != value)
                {
                    response4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Response4"));
                }
            }
        }

        public void LoadJson()
        {
            listQuestion = new List<QuestionModel>();
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("ArcQuiz.JSON." + FileName);

            using (var reader = new System.IO.StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                listQuestion = JsonConvert.DeserializeObject<List<QuestionModel>>(json);
            }
        }

        async void CheckResponse(string responseSelected)
        {
            CanClick = false;
            Debug.WriteLine("Response selected: " + responseSelected);
            bool correctAnswerFinded = false;
            if (responseSelected == Response)
            {
                correctAnswerFinded = true;
                Score++;
            }

            if (!correctAnswerFinded) //Set the button clicked to red
            {
                if (responseSelected.Equals(response1))
                    ColorButton1 = colorAnswerIncorrect;
                else if (responseSelected.Equals(response2))
                    ColorButton2 = colorAnswerIncorrect;
                else if (responseSelected.Equals(response3))
                    ColorButton3 = colorAnswerIncorrect;
                else
                    ColorButton4 = colorAnswerIncorrect;
            }

            //Set the correct answer button to green
            if (Response.Equals(response1))
                ColorButton1 = colorAnswerCorrect;
            else if (Response.Equals(response2))
                ColorButton2 = colorAnswerCorrect;
            else if (Response.Equals(response3))
                ColorButton3 = colorAnswerCorrect;
            else
                ColorButton4 = colorAnswerCorrect;


            await Task.Delay(1000);

            if (listQuestion.Count != 0)
                LoadNextQuestion();
            else //Show Result
            {
                //Save the result and display it
                ScoreModel scoreModel = new ScoreModel
                {
                    GameMode = "Solo",
                    Date = DateTime.Now.ToString("d/M/yyy"),
                    TotalNumberOfPoints = TotalNumberOfQuestions,
                    TotalOfCorrectedAnswers = score,
                    Score = (int)((Score / (double)TotalNumberOfQuestions) * 100)

                };
                await App.Database.SaveScoreAsync(scoreModel);
                await Application.Current.MainPage.Navigation.PushAsync(new GameResultView((Score / (double)TotalNumberOfQuestions), FileName));
                //Remove last page from navigation to avoid user go back to Game
                Application.Current.MainPage.Navigation.RemovePage(
                    Application.Current.MainPage.Navigation.NavigationStack[
                        Application.Current.MainPage.Navigation.NavigationStack.Count - 2
                    ]
                    );
            }
        }

        private void LoadNextQuestion()
        {
            ResetButtonColors();
            QuestionModel question = listQuestion[0];
            listQuestion.RemoveAt(0);


            Response = question.ResponseCorrect;
            Question = question.Question;

            List<string> questionsResponse = new List<string>
            {
                question.ResponseCorrect,
                question.ResponseFalse1,
                question.ResponseFalse2,
                question.ResponseFalse3
            };

            Util.Shuffle(questionsResponse);

            Response1 = questionsResponse[0];
            Response2 = questionsResponse[1];
            Response3 = questionsResponse[2];
            Response4 = questionsResponse[3];

            LabelProgression = "QuestionModel " + (TotalNumberOfQuestions - listQuestion.Count) + "/" + TotalNumberOfQuestions;
            CanClick = true;
        }

        private void ResetButtonColors()
        {
            ColorButton1 = colorButton;
            ColorButton2 = colorButton;
            ColorButton3 = colorButton;
            ColorButton4 = colorButton;
        }
    }
}
