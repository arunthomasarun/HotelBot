using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.Bot.Builder.FormFlow;
using HotelBot.Model;
using System.Threading.Tasks;

namespace HotelBot.Dialogs
{
  public class HotelBookingDialog
  {
    public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
            new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (context, text) =>
            {
              return Chain.ContinueWith(new GreetingsDialog(), AfterGreetingContinuation);             
            }),
            new RegexCase<IDialog<string>>(new Regex("^book", RegexOptions.IgnoreCase), (context, text) =>
            {
              return Chain.ContinueWith(FormDialog.FromForm(RoomReservation.BuildForm, FormOptions.PromptInStart), AfterBookingContinuation);
            }),
            new DefaultCase<string, IDialog<string>>((context, text) =>
            {
              return Chain.ContinueWith(FormDialog.FromForm(RoomReservation.BuildForm, FormOptions.PromptInStart), AfterBookingContinuation);
            }))
           
            .Unwrap()
            .PostToUser();

    private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
    {
      var token = await item;
      var name = "User";
      context.UserData.TryGetValue<string>("Name", out name);
      //context.UserData.TryGetValue<>
      return Chain.Return($"Thanks For Using Hotel Bot {name}. Redirecting to Booking Section.");

    }
    private async static Task<IDialog<string>> AfterBookingContinuation(IBotContext context, IAwaitable<object> item)
    {
      var token = await item;
      var name = "User";
      context.UserData.TryGetValue<string>("Name", out name);
      return Chain.Return($"Thanks For Using Hotel Reservation Bot {name}");

    }
  }
}