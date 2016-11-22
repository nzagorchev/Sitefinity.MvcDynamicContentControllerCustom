using System;

namespace SitefinityWebApp.Mvc.Models
{
    public class MessageModel
    {
        private string message;

        public virtual string Message
        {
            get 
            { 
                return message;
            }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    message = defaultMessage;
                }
                else
                {
                    message = value;
                }
            }
        }

        protected static string defaultMessage = "Hello, World!";
    }
}