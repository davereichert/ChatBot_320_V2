using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ChatBot_320
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ChatMessage> Messages { get; set; } = new ObservableCollection<ChatMessage>();
        private BotResponseManager botManager;




        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Messages = new ObservableCollection<ChatMessage>(ChatHistoryManager.LoadChatHistory());
            ScrollHelper.ScrollToBottom(ChatHistoryBox);

            // Initialisieren Sie den BotResponseManager mit der gewünschten JSON-Datei
            botManager = new BotResponseManager(new JsonResponseLoader(), new MessageBoxShower(), "botResponses.json");

        }
        /// <summary>
        /// eventhandler für send Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    private void Button_Click(object sender, RoutedEventArgs e)
        {
            // get the user input
            string userText = UserInputBox.Text;

            if (string.IsNullOrWhiteSpace(userText))
            {
                return;
            }
            else
            {

                // add the user input to the chat box
                Messages.Add(new ChatMessage { Sender = "you:", Message = userText, Color = "#ADD8E6", Alignment = HorizontalAlignment.Right });
                ChatHistoryManager.SaveChatHistory(Messages);
                ScrollHelper.ScrollToBottom(ChatHistoryBox);



                // clear the user input
                UserInputBox.Text = "";

                // send the input to the bot
                string botResponse = botManager.GetResponse(userText.ToLower());

                // add the bot's response to the chat box (assuming you're using the ObservableCollection approach)
                Messages.Add(new ChatMessage { Sender = "bot:", Message = botResponse, Color = "#90EE90", Alignment = HorizontalAlignment.Left });
                ChatHistoryManager.SaveChatHistory(Messages);
                ScrollHelper.ScrollToBottom(ChatHistoryBox);

            }
            
              

        }
        /// <summary>
        /// eventhandler für den Lösch button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Messages.Clear();
            ChatHistoryManager.ClearChatHistory();
        }

        /// <summary>
        /// evendhandler für enter senden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }

        
    }
}
