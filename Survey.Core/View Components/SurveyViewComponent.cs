using Microsoft.AspNetCore.Mvc;
using SurveyPackage.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace SurveyPackage.View_Components
{
    public class SurveyViewComponent
        : ViewComponent
    {

        public IViewComponentResult Invoke(IEnumerable<IPublishedElement> list)
        {
            Survey model = new Survey();
            List<string> titlelist = new List<string>();
            var data = list.ToList();
            foreach (var item in data)
            {
                string title = item.GetProperty("Title").GetValue().ToString();
                titlelist.Add(title);

            }
            model.categorylist = titlelist;
            return View(model);
        }

    }
}
