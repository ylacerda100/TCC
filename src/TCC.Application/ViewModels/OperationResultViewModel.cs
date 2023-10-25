namespace TCC.Application.ViewModels
{
    public class OperationResultViewModel
    {
        public bool Ok { get; set; }
        public string? ErrorMessage { get; set; }

        public string? Title { get; set; }

        public OperationResultViewModel()
        {
            Ok = true;
        }

        public OperationResultViewModel(string error, string title)
        {
            Ok = false;
            ErrorMessage = error;
            Title = title;
        }
    }
}
