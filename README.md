README - Backend Filmes

-Descrição

	O backend possui apenas uma rota: GET /api/movie. Esta rota retorna os filmes a serem lançados no cinema de acordo com a API do TheMovieDB. Os dados são retornados via JSON, com paginação dos resultados. Além dos filmes, a rota também retorna dois dados de paginação: page (página atual) e total_pages (número total de páginas).

-Parâmetros de query

	*additionalParams

	Por padrão, a rota retorna os filmes com três parâmetros: title, genres e release_date. Porém, a rota pode retornar muitos outros parâmetros dos filmes em conjunto com esses. Para tal, deve ser enviada, por meio do additionalParams, uma lista dos parâmetros desejados, separados por vírgula.

	Segue a lista dos possíveis parâmetros adicionais:
		popularity
		vote_count
		video
		poster_path
		id
		adult
		backdrop_path
		original_language
		original_title
		vote_average
		overview

	*page

	Esse parâmetro passa qual página deve ser retornada. Se não passado, este valor é 1.

	*pageSize

	Esse parâmetro estabelece o tamanho das páginas a serem retornadas. O valor padrão é definido por meio da variável de ambiente DEFAULT_PAGE_SIZE.
	Obs.:Ao mudar o tamanho das páginas, o total_pages retornado pela rota também muda.



-Configuração

	Porta

	O projeto configura a API para escutar por padrão na porta 5000.

	Variáveis de ambiente

	O projeto possui algumas variáveis de ambiente, para que o usuário possa modificar de maneira fácil dados que possam mudar de acordo com cada tipo de uso. São elas:

		TMDB_API_TOKEN : token para uso da API do TMDB
		TMDB_API_ADDRESS : endereço base de acesso da API do TMDB (sem a rota)
		DEFAULT_PAGE_SIZE : tamanho padrão da página retornada pela rota

-Exemplo de chamada da rota

	Passando parâmetros adicionais id e overview, página 2 e tamanho de página 5

	http://localhost:5000/api/movie?additionalParams=id,overview&page=2&pageSize=5


-Padrões utilizados

	O projeto tem o padrão estrutural baseado no Facade, de forma que a única parte do projeto que pode ser acessada externamente são as Controllers, contidas no projeto BackendFilmes.API . Além disso, toda a parte de regra de negócio está completamente separada das Controllers, e contida somente no projeto BackendFilmes.Service.
	Além disso, é possível perceber que a arquitetura da API também implementa a separação em camadas (API, Service, Model e Test), onde cada camada possui uma função bem definida e distinta das outras camadas, o que reforça a utilização do princípio de responsabilidade única nas funções implementadas.
	Outro princípio muito utilizado no projeto é a injeção de dependência, que é utilizado para exibir os serviços para a camada API por meio de interfaces implementadas.

-Dependências

	O projeto utiliza algumas dependências externas. Serão listadas abaixo algumas das dependências e o motivo de sua utilização:

		Web API: criação das controllers e endereçamento das rotas

		Newtonsoft.JSON: serialização das classes de modelo para JSON e customização do JSON para envio na resposta

		Reflection: obtenção de propriedades de um objeto genérico e modificação do valor destas propriedades

		Linq: operações sobre listas de objetos

-Detalhes relevantes

	Por possuir requisições a outra API, a aplicação foi construída de forma a minimizar este número de requisições ao máximo. A solução encrontrada foi a seguinte: a aplicação guarda alguns dados da requisição, como o número total de páginas da API e as páginas que já foram requisitadas. Além disso, os dados dos filmes requisitados ficam guardados em um lista.
	Quando a API requisita uma quantidade de dados de filmes, é primeiro checado se estes dados já existem localmente. Se não existem, são feitas requisições à API do TMDB (função RequestLatestMovies) pedindo as páginas necessárias (lembrando que o tamanho de página desta API é fixo, de valor 20).
	Uma vez que os dados necessários existam localmente, o código faz um cálculo do tamanho de página e número da página requisitados, e por fim extrai os dados pedidos e os exporta em uma lista (função GetMoviePage).