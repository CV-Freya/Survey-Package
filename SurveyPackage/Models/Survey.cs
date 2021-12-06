
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SurveyPackage.Models
{
    public class Survey
    {
        [Required(ErrorMessage = "Please select any category")]
        public string category { get; set; }
        public List<string> categorylist { get; set; }
    }
}
