using SDHDotNetCore.ThemeMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.WebSockets;
using System.Collections.Generic;

namespace SDHDotNetCore.ThemeMvcApp.Controllers
{
    public class HighChartsController : Controller
    {
        public IActionResult PieWithLegendChart()
        {
            HighChartsPieWithLegendDataModel data = new HighChartsPieWithLegendDataModel
            {
                ChartTitle = "Browser market shares in March, 2022",
                ChartData = new List<HighChartsPieWithLegendModel>
                {
                    new HighChartsPieWithLegendModel { Name = "Chrome", Y = 74.77, Sliced = true, Selected = true },
                    new HighChartsPieWithLegendModel { Name = "Edge", Y = 12.82 },
                    new HighChartsPieWithLegendModel { Name = "Firefox", Y = 4.63 },
                    new HighChartsPieWithLegendModel { Name = "Safari", Y = 2.44 },
                    new HighChartsPieWithLegendModel { Name = "Internet Explorer", Y = 2.02 },
                    new HighChartsPieWithLegendModel { Name = "Other", Y = 3.28 }
                }
            };

            HighChartsPieWithLegendResponseModel model = new HighChartsPieWithLegendResponseModel
            {
                ChartTitle = data.ChartTitle,
                ChartData = data.ChartData
            };
            return View(model);
        }

        public IActionResult RadialBarChart()
        {
            List<HighChartsRadialBarChartModel> medalData = new List<HighChartsRadialBarChartModel>
            {
                new HighChartsRadialBarChartModel { Country = "Norway", GoldMedals = 148, SilverMedals = 113, BronzeMedals = 124 },
                new HighChartsRadialBarChartModel { Country = "United States", GoldMedals = 113, SilverMedals = 122, BronzeMedals = 95 },
                new HighChartsRadialBarChartModel { Country = "Germany", GoldMedals = 104, SilverMedals = 98, BronzeMedals = 65 },
                new HighChartsRadialBarChartModel { Country = "Austria", GoldMedals = 71, SilverMedals = 88, BronzeMedals = 91 },
                new HighChartsRadialBarChartModel { Country = "Canada", GoldMedals = 77, SilverMedals = 72, BronzeMedals = 76 }
            };

            HighChartsRadialBarChartResponseModel model = new HighChartsRadialBarChartResponseModel
            {
                MedalData = medalData
            };

            return View(model);
        }

       

    }

}

