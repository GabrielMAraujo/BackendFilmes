using System.Collections.Generic;
using BackendFilmes.Model;

namespace BackendFilmes.Service.Interfaces
{
    public interface IJsonService
    {
        public string MakeJsonWithAdditionalFields(List<Movie> moviesList, List<string> additionalParametersList, int page, int totalPages);
    }
}
