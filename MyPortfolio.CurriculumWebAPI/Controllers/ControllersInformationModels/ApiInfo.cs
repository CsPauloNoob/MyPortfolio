using System.Reflection;

namespace CurriculumWebAPI.App.Controllers.ControllersInformationModels
{
    public class ApiInfo
    {
        public string HttpMethod { get; set; }
        public string Route { get; set; }
        public List<ParameterInfo> Parameters { get; set; }
    }
}
