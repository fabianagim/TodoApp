using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoClient.Model
{
    public class ToDoMessageBox : IMessageBox
    {
        MessageBoxResult IMessageBox.Show(string message, string title)
        {
            return MessageBox.Show(message, title);
        }

        MessageBoxResult IMessageBox.Show(string message, string title, MessageBoxButton button)
        {
            return MessageBox.Show(message, title, button);
        }

        MessageBoxResult IMessageBox.Show(string message, string title, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(message, title, button, icon);
        }
    }
}
