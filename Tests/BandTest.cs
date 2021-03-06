using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
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
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfTheSame()
    {
      //Arrange, Act
      Band firstBand = new Band("Deftones");
      Band secondBand = new Band("Deftones");

      //Assert
      Assert.Equal(firstBand, secondBand);
    }
    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Band testBand = new Band("Deftones");

      //Act
      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Band testBand = new Band("Deftones");

      //Act
      testBand.Save();
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int testId = testBand.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    public void Test_Find_FindInDatabase()
    {
      //Arrange
      Band testBand = new Band("Deftones");
      testBand.Save();

      //Act
      Band foundBand = Band.Find(testBand.GetId());

      //Assert
      Assert.Equal(testBand, foundBand);
    }
    [Fact]
    public void Test_AddVenueToBand_AddsVenueToBand()
    {
      //Arrange
      Venue testVenue = new Venue("dad's garage");
      testVenue.Save();

      Band testBand = new Band("Deftones");
      testBand.Save();

      //Act
      testBand.AddVenueToBand(testVenue.GetId());

      List<Venue> result = testBand.GetVenueByBand();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_GetVenueByBand_GetsVenuesByBand()
    {
      //Arrange
      Band testBand = new Band("Deftones");
      testBand.Save();

      Venue testVenue1 = new Venue("dad's garage");
      testVenue1.Save();

      Venue testVenue2 = new Venue("Madison Square Garden");
      testVenue2.Save();

      //Act
      testBand.AddVenueToBand(testVenue1.GetId());
      List<Venue> result = testBand.GetVenueByBand();
      List<Venue> testList = new List<Venue> {testVenue1};

      //Assert
      Assert.Equal(testList, result);
    }
    public void Test_Delete_DeletesFromDatabase()
    {
      //Arrange
      Band testBand1 = new Band("Deftones");
      testBand1.Save();

      Band testBand2 = new Band("...And You Will Know Us by the Trail of Dead");
      testBand2.Save();

      //Act
      testBand1.Delete();
      List<Band> resultBands = Band.GetAll();

      //Assert
      Assert.Equal(1, resultBands.Count);
    }
  }
}
