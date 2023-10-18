using System.Windows;


namespace ChatBot_320
{
    public class MessageBoxShower : IMessageShower
    {
        public void show(string message)
        {
            MessageBox.Show(message);
        }
    }
}
