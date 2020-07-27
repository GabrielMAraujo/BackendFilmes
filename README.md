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

-Bibliotecas externas

-Detalhes relevantes