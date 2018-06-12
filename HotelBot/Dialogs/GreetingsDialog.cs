using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace HotelBot.Dialogs
{
  [Serializable]
  public class GreetingsDialog : IDialog
  {
    public async Task StartAsync(IDialogContext context)
    {
      await context.PostAsync("Hi, This is a bot developed by Arun");
      context.Wait(MessageReceivedAsync);
    }

    private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
    {
      var message = await result;
      var userName = string.Empty;
      var getName = false;

      context.UserData.TryGetValue<Boolean>("GetName", out getName); 
      context.UserData.TryGetValue<string>("Name", out userName);

      if (getName)
      {
        userName = message.Text;
        context.UserData.SetValue("Name", userName);
        context.UserData.SetValue("GetName", false);
      }

      if (string.IsNullOrEmpty(userName))
      {
        await context.PostAsync("What is your name?");
        context.UserData.SetValue<Boolean>("GetName", true);
      }
      else
      {
        await context.PostAsync(string.Format("Hi {0}, how can I help you today?", userName));
      }
      context.Wait(MessageReceivedAsync);
    }
  }
}