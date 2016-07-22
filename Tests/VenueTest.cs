using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfTheSame()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("dad's garage");
      Venue secondVenue = new Venue("dad's garage");

      //Assert
      Assert.Equal(firstVenue, secondVenue);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("dad's garage");

      //Act
      testVenue.Save();
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Venue testVenue = new Venue("dad's garage");

      //Act
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int testId = testVenue.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test_Find_FindInDatabase()
    {
      //Arrange
      Venue testVenue = new Venue("dad's garage");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }
    [Fact]
      public void Test_Update_UpdatesInDatabase()
    {
      //Arrange
      Venue newVenue = new Venue("dad's garage");
      newVenue.Save();
      string newName = "Madison Square Garden";

      //Act
      newVenue.Update(newName);
      string resultNewName = newVenue.GetName();

      //Assert
      Assert.Equal(newName, resultNewName);
    }
    [Fact]
    public void Test_AddBandToVenue_AddsBandToVenue()
    {
      //Arrange
      Venue testVenue = new Venue("dad's garage");
      testVenue.Save();

      Band testBand = new Band("Deftones");
      testBand.Save();

      //Act
      testVenue.AddBandToVenue(testBand.GetId());

      List<Band> result = testVenue.GetBandsFromVenue();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_GetBandsFromVenue_ReturnsAllBandsFromVenue()
    {
      //Arrange
      Venue testVenue = new Venue("dad's garage");
      testVenue.Save();

      Band testBand1 = new Band("Deftones");
      testBand1.Save();

      Band testBand2 = new Band("...And You Will Know Us by the Trail of Dead");
      testBand2.Save();

      //Act
      testVenue.AddBandToVenue(testBand1.GetId());
      List<Band> result = testVenue.GetBandsFromVenue();
      List<Band> testList = new List<Band> {testBand1};

      //Assert
      Assert.Equal(testList, result);
    }
  }
}
