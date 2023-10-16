namespace TCC.Application.ViewModels
{
    public class OperationResultViewModel
    {
        public bool Ok { get; set; }
        public string? ErrorMessage { get; set; }

        public OperationResultViewModel()
        {
            Ok = true;
        }

        public OperationResultViewModel(string error)
        {
            Ok = false;
            ErrorMessage = error;
        }
    }
}
