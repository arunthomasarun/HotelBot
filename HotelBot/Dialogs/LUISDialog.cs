using HotelBot.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HotelBot.Dialogs
{
  [LuisModel("cba28", "505dc0")]
  [Serializable]
  public class LUISDialog : LuisDialog<RoomReservation>
  {
    private readonly BuildFormDelegate<RoomReservation> reserveform;

    public LUISDialog(BuildFormDelegate<RoomReservation> reserveRoom)
    {
      this.reserveform = reserveRoom;
    }

    [LuisIntent("")]
    public async Task None(IDialogContext context, LuisResult result )
    {
      await context.PostAsync("Sorry, I did not understand the request!!! Please rephrase your question.");
      context.Wait(MessageReceived);
    }

    [LuisIntent("Greeting")]
    public async Task Greeting(IDialogContext context, LuisResult result)
    {
       context.Call(new GreetingsDialog(), ResumeCallback);
    }

    private async Task ResumeCallback(IDialogContext context, IAwaitable<object> result)
    {
        context.Wait(MessageReceived);
    }

    [LuisIntent("Reservation")]
    public async Task Reservation(IDialogContext context, LuisResult result)
    {
      var enrollmentForm = new FormDialog<RoomReservation>(new RoomReservation(), this.reserveform, FormOptions.PromptInStart);
      context.Call<RoomReservation>(enrollmentForm, ResumeCallback);
    }

    [LuisIntent("QueryAmneties")]
    public async Task AmnetiyChecker(IDialogContext context, LuisResult result)
    {
      foreach (var entity in result.Entities.Where(Entity => Entity.Type== "AmnetiesEntity"))
      {
        var value = entity.Entity.ToLower();

        if (Enum.IsDefined(typeof(ExtraAmneties), value))
        {
          await context.PostAsync("Yes we have that!!!");
          context.Wait(MessageReceived);
          return;
        }
        else
        {
          await context.PostAsync("Sorry, we dont have that!!!");
          context.Wait(MessageReceived);
          return;
        }
      }
      await context.PostAsync("Sorry, we dont have that!!!");
      context.Wait(MessageReceived);
      return;
    }
  }
}