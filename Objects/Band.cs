using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Band
  {
    private string _name;
    private int _id;

    public Band(string Name, int Id=0)
    {
      _id = Id;
      _name = Name;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allBands;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands; DELETE FROM venue_band;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
