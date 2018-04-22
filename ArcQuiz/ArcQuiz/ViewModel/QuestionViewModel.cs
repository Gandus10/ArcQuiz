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

#if __IOS__
var resourcePrefix = "ArcQuiz.iOS.";
#endif
#if __ANDROID__
var resourcePrefix = "ArcQuiz.Droid.";
#endif
#if WINDOWS_PHONE
var resourcePrefix = "ArcQuiz.WinPhone.";
#endif



namespace ArcQuiz.ViewModel
{


    class QuestionViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CorrectResponseCommand { get; private set; }
        public ICommand FalseResponseCommand { get; private set; }
        public QuestionViewModel()
        {
            CorrectResponseCommand = new Command(CorrectResponseAsync);
            FalseResponseCommand = new Command(FalseResponseAsync);

            //LoadJson();
            PrintQuestion(0);
        }

        private List<Question> listQuestion;

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

        private Color colorButtonCorrect;
        public Color ColorButtonCorrect
        {
            get { return colorButtonCorrect; }
            set {
                if (colorButtonCorrect != value)
                {
                    colorButtonCorrect = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorButtonCorrect"));
                }
            }
        }

        private Color colorButtonFalse;
        public Color ColorButtonFalse
        {
            get { return colorButtonFalse; }
            set
            {
                if (colorButtonFalse != value)
                {
                    colorButtonFalse = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ColorButtonFalse"));
                }
            }
        }

        private string question;
        public string Question
        {
            get { return question; }
            set {
                if (question != value)
                {
                    question = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Question"));
                }
            }
        }

        private string responseCorrect;
        public string ResponseCorrect
        {
            get { return responseCorrect; }
            set
            {
                if (responseCorrect != value)
                {
                    responseCorrect = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ResponseCorrect"));
                }
            }
        }

        private string responseFalse1;
        public string ResponseFalse1
        {
            get { return responseFalse1; }
            set
            {
                if (responseFalse1 != value)
                {
                    responseFalse1 = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ResponseFalse1"));
                }
            }
        }

        private string responseFalse2;
        public string ResponseFalse2
        {
            get { return responseFalse2; }
            set
            {
                if (responseFalse2 != value)
                {
                    responseFalse2 = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ResponseFalse2"));
                }
            }
        }


        private string responseFalse3;
        public string ResponseFalse3
        {
            get { return responseFalse3; }
            set
            {
                if (responseFalse3 != value)
                {
                    responseFalse3 = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ResponseFalse3"));
                }
            }
        }

        public void LoadJson()
        {
            
            listQuestion = new List<Question>();
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(EngineerPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream
                ("ArcQuiz.Android." + "JSON/arc_quiz_ingeneer.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                
                string json = reader.ReadToEnd();
                listQuestion = JsonConvert.DeserializeObject<List<Question>>(json);
            }      
        }  

        public void PrintQuestion( int id)
        {
            ColorButtonCorrect = Color.Gray;
            ColorButtonFalse = Color.Gray;

            //Question = listQuestion[0].question;
            //ResponseCorrect = listQuestion[0].ResponseCorrect;
            //ResponseFalse1 = listQuestion[0].ResponseFalse1;
            //ResponseFalse2 = listQuestion[0].ResponseFalse2;
            //ResponseFalse3 = listQuestion[0].ResponseFalse3;

            

            if (id == 0)
            {

                Question = "Question";
                ResponseCorrect = "Reponse 1";
                ResponseFalse1 = "Reponse 2";
                ResponseFalse2 = "Reponse 3";
                ResponseFalse3 = "Reponse 4";
            }
            else
            {
                Question = "Question 2";
                ResponseCorrect = "Reponse 10";
                ResponseFalse1 = "Reponse 20";
                ResponseFalse2 = "Reponse 30";
                ResponseFalse3 = "Reponse 40";
            }
           
        }

        public async void CorrectResponseAsync()
        {
            ColorButtonCorrect = Color.Green;
            ColorButtonFalse = Color.Red;
            Score = score + 1;
            await Task.Delay(1000);
            PrintQuestion(1);

        }

        public async void FalseResponseAsync()
        {
            ColorButtonFalse = Color.Red;
            ColorButtonCorrect = Color.Green;
            await Task.Delay(1000);
            PrintQuestion(1);
        }


    }
}
