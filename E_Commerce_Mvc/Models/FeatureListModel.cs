namespace E_Commerce_Mvc.Models
{
    public class FeatureListModel
    {
        public int ProductId { get; set; }
        public string[] FeatureName { get; set; } = new string[5];
    }
}
