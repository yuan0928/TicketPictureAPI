using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPictureAPI.Interface;

namespace TicketPictureAPI.Services
{
    public class TicketConvert :ITicketConvert
    {
        public string ConvertYear(string year) 
        {
            Dictionary<string, string> convertYear = new()
            {
                { "5","2015"},{ "6","2016"},{ "7","2017"},{ "8","2018"},{ "9","2019"},
                { "0","2020"},{ "B","2021"},{ "C","2022"},{ "D","2023"},{ "E","2024"},
                { "F","2025"},{ "G","2026"},{ "H","2027"},{ "I","2028"},{ "J","2029"},
                { "K","2030"},{ "L","2031"},{ "M","2032"},{ "N","2033"},{ "O","2034"},
                { "P","2035"},{ "Q","2036"},{ "R","2037"},{ "S","2038"},{ "T","2039"},
                { "U","2040"},{ "V","2041"},{ "W","2042"},{ "X","2043"},{ "Y","2044"},
                { "Z","2040"},
            };

            return convertYear[year];
        }
        public string ConvertMonth(string month)
        {
            Dictionary<string, string> convertMonth = new()
            {
                { "1","01"},{ "2","02"},{ "3","03"},{ "4","04"},{ "5","05"},{ "6","06"},
                { "7","07"},{ "8","08"},{ "9","09"},{ "A","10"},{ "B","11"},{ "C","12"},

            };

            return convertMonth[month];
        }
    }
}
