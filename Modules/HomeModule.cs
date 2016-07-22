using System.Collections.Generic;
using System;
using Nancy;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/venues"] = _ => {
        return View["venues.cshtml", Venue.GetAll()];
      };
      Get["/band"] = _ => {
        return View["band.cshtml", Band.GetAll()];
      };
      Get["/venue/{id}"]= parameters => {
        int venueId = parameters.id;
        Venue venueFound = Venue.Find(venueId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> VenueBands = venueFound.GetBandsFromVenue();
        model.Add("venue", venueFound);
        model.Add("band", VenueBands);
        return View["venue.cshtml", model];
      };
      Post["/venue/delete"] = _ => {
        int venueId = Request.Form["venue-id"];
        Venue venueFound = Venue.Find(venueId);
        venueFound.Delete();
        return View["venues.cshtml", Venue.GetAll()];
      };
      Post["/add-venue"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["venues.cshtml", Venue.GetAll()];
      };
    }

  }
}
