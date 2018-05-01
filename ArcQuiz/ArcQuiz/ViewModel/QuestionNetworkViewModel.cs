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
using ArcQuiz.View;
using System.Text;
using System.Net.WebSockets;
using System.Linq;
using System.Threading;
using System.Collections.ObjectModel;
using Plugin.DeviceInfo;

namespace ArcQuiz.ViewModel
{
    class QuestionNetworkViewModel : INotifyPropertyChanged
    {
        private static readonly Color colorAnswerCorrect = Color.FromHex("#27ae60");
        private static readonly Color colorAnswerIncorrect = Color.FromHex("#d63031");
        private static readonly Color colorButton = Color.White;
        private ClientWebSocket client;
        public CancellationTokenSource cts = new CancellationTokenSource();


        Command<string> sendMessageCommand;
        ObservableCollection<MessageModel> messages;
        string messageText;

        public bool IsConnected => client.State == WebSocketState.Open;

        public Command SendMessage => sendMessageCommand ??
          (sendMessageCommand = new Command<string>(SendMessageAsync, CanSendMessage));

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

        public QuestionNetworkViewModel(string fileName)
        {
            client = new ClientWebSocket();
            canClick = true;
            FileName = fileName;
            messages = new ObservableCollection<MessageModel>();
            ConnectToServerAsync();
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

        public string MessageText
        {
            get
            {
                return messageText;
            }
            set
            {
                messageText = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MessageText"));

                //sendMessageCommand.ChangeCanExecute();
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

        public ObservableCollection<MessageModel> Messages => messages;


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

            SendMessageAsync(responseSelected);

                
            bool correctAnswerFinded = false;
            if (responseSelected == Response)
            {
                correctAnswerFinded = true;
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
            else
                await Application.Current.MainPage.Navigation.PushAsync(new CategoryView(true));
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

        async void ConnectToServerAsync()
        {

            //await client.ConnectAsync(new Uri("ws://localhost:5000"), cts.Token);

            
            
            Task tsk =  client.ConnectAsync(new Uri("ws://157.26.104.83:5000"), cts.Token);
            tsk.Wait();
            

            //Debug.WriteLine("After connect");

            UpdateClientState();

            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    WebSocketReceiveResult result;
                    var message = new ArraySegment<byte>(new byte[4096]);
                    do
                    {
                        result = await client.ReceiveAsync(message, cts.Token);
                        var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                        string serialisedMessage = Encoding.UTF8.GetString(messageBytes);

                        try
                        {
                             var msg = JsonConvert.DeserializeObject<MessageModel>(serialisedMessage);
                            Messages.Add(msg);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Invalide message format. {ex.Message}");
                        }

                    } while (!result.EndOfMessage);
                }
            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            void UpdateClientState()
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Isconnected"));
                //sendMessageCommand.ChangeCanExecute();
                Debug.WriteLine($"Websocket state {client.State}");
            }
        }

        bool CanSendMessage(string message)
        {
            Debug.WriteLine(IsConnected);
            return IsConnected && !string.IsNullOrEmpty(message);
        }

        async Task ReadMessage()
        {
            WebSocketReceiveResult result;
            var message = new ArraySegment<byte>(new byte[4096]);
            do
            {
                result = await client.ReceiveAsync(message, cts.Token);
                if (result.MessageType != WebSocketMessageType.Text)
                    break;
                var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
                string receivedMessage = Encoding.UTF8.GetString(messageBytes);
                Console.WriteLine("Received: {0}", receivedMessage);
            }
            while (!result.EndOfMessage);
        }

        async void SendMessageAsync(string message)
        {
            
            var msg = new MessageModel
            {
                Name = "laurent",
                MessagDateTime = DateTime.Now,
                Text = message,
                UserId = CrossDeviceInfo.Current.Id
            };


            string serialisedMessage = JsonConvert.SerializeObject(msg);

            var byteMessage = Encoding.UTF8.GetBytes(serialisedMessage);
            var segmnet = new ArraySegment<byte>(byteMessage);

            //Debug.WriteLine(client);

            await client.SendAsync(segmnet, WebSocketMessageType.Text, true, cts.Token);
            MessageText = string.Empty;
        }
    }
}

