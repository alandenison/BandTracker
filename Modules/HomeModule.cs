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
      Get["/bands"] = _ => {
        return View["bands.cshtml", Band.GetAll()];
      };
      Get["/venue/{id}"]= parameters => {
        int venueId = parameters.id;
        Venue venueFound = Venue.Find(venueId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Band> VenueBands = venueFound.GetBandsFromVenue();
        List<Band> allBands = Band.GetAll();
        model.Add("venue", venueFound);
        model.Add("band", VenueBands);
        model.Add("allBands", allBands);
        return View["venue.cshtml", model];
      };
      Get["/band/{id}"]= parameters => {
        int bandId = parameters.id;
        Band bandFound = Band.Find(bandId);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Venue> BandVenues = bandFound.GetVenueByBand();
        List<Venue> allVenues = Venue.GetAll();
        model.Add("band", bandFound);
        model.Add("venue", BandVenues);
        model.Add("allVenues", allVenues);
        return View["band.cshtml", model];
      };
      Post["/venue-delete"] = _ => {
        Console.WriteLine("hi" + Request.Form["venue-id"]);
        int venueId = Request.Form["venue-id"];
        Venue venueFound = Venue.Find(venueId);
        venueFound.Delete();
        return View["success.cshtml", "/venues"];
      };
      Post["/add-venue"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["venues.cshtml", Venue.GetAll()];
      };
      Post["/delete-all-venues"] = _ => {
      Venue.DeleteAll();
      return View["index.cshtml"];
      };
      Post["/band-delete"] = _ => {
        Console.WriteLine("hi" + Request.Form["band-id"]);
        int bandId = Request.Form["band-id"];
        Band bandFound = Band.Find(bandId);
        bandFound.Delete();
        return View["success.cshtml", "/bands"];
      };
      Post["/add-band"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        return View["bands.cshtml", Band.GetAll()];
      };
      Post["/delete-all-bands"] = _ => {
      Band.DeleteAll();
      return View["index.cshtml"];
      };
      Post["/band/{id}/add-venue"] = parameters => {
        int venueId = Request.Form["add-venue-to-band"];
        int bandId = parameters.id;
        Band thisBand = Band.Find(parameters.id);
        thisBand.AddVenueToBand(venueId);
        return View["success.cshtml", ("band/" + bandId)];
      };
      Post["/delete-venue-from-band"] = _ => {
        int venueId = Request.Form["band-venue-name"];
        Band SelectedBand = Band.Find(Request.Form["band-update"]);
        SelectedBand.DeleteVenueFromBand(venueId);
        int bandId = SelectedBand.GetId();
        return View["success.cshtml", ("/band/" + bandId)];
      };
      Post["/venue/{id}/add-band"] = parameters => {
        int bandId = Request.Form["add-band-to-venue"];
        int venueId = parameters.id;
        Band thisBand = Band.Find(parameters.id);
        thisBand.AddVenueToBand(bandId);
        return View["success.cshtml", ("venue/" + venueId)];
      };
      Post["/delete-band-from-venue"] = _ => {
        int bandId = Request.Form["venue-band-name"];
        Band SelectedBand = Band.Find(Request.Form["venue-update"]);
        SelectedBand.DeleteVenueFromBand(bandId);
        int venueId = SelectedBand.GetId();
        return View["success.cshtml", ("/venue/" + venueId)];
      };
    }
  }
}
