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
      await Respond(context);
      context.Wait(MessageReceivedAsync);
    }

    public static async Task Respond(IDialogContext context)
    {
      var userName = string.Empty;
      context.UserData.TryGetValue<string>("Name", out userName);

      if (string.IsNullOrEmpty(userName))
      {
        await context.PostAsync("What's Your Name?");
        context.UserData.SetValue<bool>("GetName", true);
      }
      else
      {
        await context.PostAsync($"Hi {userName}. How Can I Help You Today? ");
      }
    }
    public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
      var message = await argument;
      var userName = string.Empty;
      var getName = false;

      context.UserData.TryGetValue<string>("Name", out userName);
      context.UserData.TryGetValue<bool>("GetName", out getName);

      if (getName)
      {
        userName = message.Text;
        context.UserData.SetValue<string>("Name", userName);
        context.UserData.SetValue<bool>("GetName", false);
      }

      await Respond(context);
      context.Done(message);
    }
  }
}