namespace CurriculumWebAPI.App
{
    public static class Configuration
    {
        public static string CorsPolicyName = "wasm";

        public static string DevEnv_BackendUrl = "http://localhost:5260";
        
        public static string DevEnv_FrontendUrl = "http://localhost:5273";

        public static string ProdEnv_FrontendUrl = "https://paulodev.online";
    }
}