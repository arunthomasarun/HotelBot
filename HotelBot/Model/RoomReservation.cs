using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


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
        .Message("Welcome to the booking section!!!")
        .Build();
   }
  }
}