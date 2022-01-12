using Microsoft.Data.SqlClient;
using ParkingAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingAppWebApi.FunctionsDbParking
{
    public static class ParkingFunctions
    {
        
        public static decimal GetPriceFromParkingDB(int idUser, int idZone, int idParking, string fromDate, string fromTime, string toDate, string toTime, int idCar)
        {
            string conString = "Data Source=192.168.11.166;Initial Catalog=Parking;Persist Security Info=True;User ID=******;Password=*******";
            using SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select dbo.GetPrice(@idUser,@idZone,@idParking,@fromDate,@fromTime,@toDate,@toTime,@idCar)", con);
            cmd.Parameters.AddWithValue("@idUser", idUser);
            cmd.Parameters.AddWithValue("@idZone", idZone);
            cmd.Parameters.AddWithValue("@idParking", idParking);
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@fromTime", fromTime);
            cmd.Parameters.AddWithValue("@toDate", toDate);
            cmd.Parameters.AddWithValue("@toTime", toTime);
            cmd.Parameters.AddWithValue("@idCar", idCar);
            decimal gettinPriceDB = Convert.ToDecimal(cmd.ExecuteScalar());
            con.Close();

            return gettinPriceDB;

        }

        public static DataTable GetAllowedPayments(string serNum, string serDate)
        {
            DataSet ds = new DataSet();
            string conString = "Data Source=192.168.11.166;Initial Catalog=Parking;Persist Security Info=True;User ID=mhmIzzat;Password=Mail1994";
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                string sql = "exec sp_allowed_payments @serialNumber='" + serNum + "', @searchingDate='" + serDate + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                ds = new DataSet();
                adapter.Fill(ds);
                con.Close();

            }
            return ds.Tables[0];
        }




        //public static DataTable GetAllowedPayments( string serialNum ,string searchDate)
        //{
        //    string conString = "Data Source=192.168.11.166;Initial Catalog=Parking;Persist Security Info=True;User ID=mhmIzzat;Password=Mail1994";
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("exec sp_allowed_payments @serialNumber = '@serialNumber', @searchingDate = '@serialNumber'", con);
        //        cmd.Parameters.AddWithValue("@serialNumber", serialNum);
        //        cmd.Parameters.AddWithValue("@searchingDate", searchDate);
                
               
        //        con.Close();

        //    }
        //}
    }
}
