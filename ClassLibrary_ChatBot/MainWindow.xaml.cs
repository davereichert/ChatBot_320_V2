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




        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this; // this is needed for the binding to work
            Messages = new ObservableCollection<ChatMessage>(ChatHistoryManager.LoadChatHistory());
            ScrollHelper.ScrollToBottom(ChatHistoryBox);



        }
        // event handler for the button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {

                // get the user input
                string userText = UserInputBox.Text;

                // add the user input to the chat box
                Messages.Add(new ChatMessage { Sender = "You:", Message = userText, Color = "YourColor", Alignment = HorizontalAlignment.Right });
                ChatHistoryManager.SaveChatHistory(Messages);
                ScrollHelper.ScrollToBottom(ChatHistoryBox);



                // clear the user input
                UserInputBox.Text = "";

                // send the input to the bot
                string botResponse = BotResponseManager.GetResponse(userText.ToLower());

                // add the bot's response to the chat box (assuming you're using the ObservableCollection approach)
                Messages.Add(new ChatMessage { Sender = "Bot:", Message = botResponse, Color = "BotColor", Alignment = HorizontalAlignment.Left });
                ChatHistoryManager.SaveChatHistory(Messages);
                ScrollHelper.ScrollToBottom(ChatHistoryBox);


            }
            else
            {
                MessageBox.Show("Please enter a message");
            }

        }
        // event handler for the clear button mit ClearButton_Click
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Messages.Clear();
            ChatHistoryManager.ClearChatHistory();
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
