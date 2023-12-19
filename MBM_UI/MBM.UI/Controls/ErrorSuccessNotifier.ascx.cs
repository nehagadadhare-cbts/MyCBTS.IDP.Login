using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_ErrorSuccessNotifier : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public enum MessageType
    {
        Success,
        Warning,
        Error
    }

    public class NotificationMessage
    {
        public string Text { get; set; }
        public MessageType Type { get; set; }
        public bool AutoHide { get; set; }
    }

    private const string KEY_NOTIFICATION_MESSAGES = "NotificationMessages";

    public static void AddMessage(NotificationMessage msg)
    {
        List<NotificationMessage> messages = NotificationMessages;
        if (messages == null)
        {
            messages = new List<NotificationMessage>();
        }
        messages.Add(msg);
        HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES] = messages;
    }

    private static void ClearMessages()
    {
        HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES] = null;
    }

    private static List<NotificationMessage> NotificationMessages
    {
        get
        {
            List<NotificationMessage> messages = (List<NotificationMessage>)
                HttpContext.Current.Session[KEY_NOTIFICATION_MESSAGES];
            return messages;
        }
    }

    public static void AddSuccessMessage(string msg)
    {
        AddMessage(new NotificationMessage()
        {
            Text = msg,
            Type = MessageType.Success,
            AutoHide = false
        });
    }

    public static void AddWarningMessage(string msg)
    {
        AddMessage(new NotificationMessage()
        {
            Text = msg,
            Type = MessageType.Warning,
            AutoHide = false
        });
    }

    public static void AddErrorMessage(string msg)
    {
        AddMessage(new NotificationMessage()
        {
            Text = msg,
            Type = MessageType.Error,
            AutoHide = false
        });
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (NotificationMessages != null)
        {
            int index = 1;
            foreach (var msg in NotificationMessages)
            {
                Panel msgPanel = new Panel();
                msgPanel.CssClass = "PanelNotificationBox Panel" + msg.Type;
                if (msg.AutoHide)
                {
                    msgPanel.CssClass += " AutoHide";
                }
                msgPanel.ID = msg.Type + "Msg" + index;
                Literal msgLiteral = new Literal();
                msgLiteral.Mode = LiteralMode.Encode;
                msgLiteral.Text = msg.Text;
                msgPanel.Controls.Add(msgLiteral);
                this.Controls.Add(msgPanel);
                index++;
            }
            ClearMessages();

        }
    }
   
}