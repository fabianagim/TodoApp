using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDoClient.Model
{
    public interface IMessageBox
    {
        MessageBoxResult Show(string message, string title);

        MessageBoxResult Show(string message, string title, MessageBoxButton button);

        MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage icon);
    }
}
