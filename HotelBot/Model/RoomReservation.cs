using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace HotelBot.Model
{
  public enum BedOptions
  {
    King,
    Queen,
    Single
  }

  public enum ExtraAmneties
  {
    swimmingpool,
    gym,
    library,
    wifi,
    laundry
  }

  [Serializable]
  public class RoomReservation
  {

    public BedOptions? BedSizeOptions;
    public int? NoOfOccupants;
    public int? NoOfDaysToStay;
    public DateTime? CheckInDate;
    public List<ExtraAmneties> Amneties;
    public static IForm<RoomReservation> BuildForm()
    {
      return new FormBuilder<RoomReservation>()
        .Message("Welcome to the booking section!!! Alternatively you can visit [Hotel Site](www.arunthomas.com) for booking.")
        .OnCompletion(async(context, state) =>
        {
          await context.PostAsync($"Booking done for {state.NoOfOccupants} on {state.CheckInDate.ToString()}");
        })
        .Build();
   }

   
  }
}