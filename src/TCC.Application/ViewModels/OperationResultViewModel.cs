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

    public class OperationResultViewModel<T> : OperationResultViewModel
    {
        public T Result { get; set; }

        public OperationResultViewModel(T result, bool ok)
        {
            if (!ok)
            {
                throw new System.Exception("Não é possível usar esse metódo construtor para resultados com erros.");
            }

            this.Ok = true;
            this.Result = result;
        }
    }
}
