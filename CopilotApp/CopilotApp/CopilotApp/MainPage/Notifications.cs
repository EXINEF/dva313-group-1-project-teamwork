using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CopilotApp
{
    public partial class MainPageViewmodel : INotifyPropertyChanged
    {
        private Queue notificationQueue = new Queue();
        string _notificationMessage; public string notificationMessage { get => _notificationMessage; set { _notificationMessage = value; OnPropertyChanged(nameof(notificationMessage)); } }
        public bool _notificationVisible; public bool notificationVisible { get => _notificationVisible; set { _notificationVisible = value; OnPropertyChanged(nameof(notificationVisible)); } }

        public void AddNotification(string message)
        {
            notificationQueue.Enqueue(message);
            notificationMessage = message;
            notificationVisible = true;
        }

        public static void PushNotification(string message)
        {
            MessagingCenter.Send(Xamarin.Forms.Application.Current, "NewNotification", message);
        }

        public void DismissNotification()
        {
            if(notificationQueue.Count > 0)
            {
                notificationQueue.Dequeue();
            }

            if(notificationQueue.Count > 0)
            {
                notificationMessage = notificationQueue.Peek().ToString();
                notificationVisible = true;
            }
            else
            {
                notificationVisible = false;
            }
        }

    }
}
