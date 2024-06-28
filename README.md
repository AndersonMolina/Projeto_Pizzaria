Olá!

Esse projeto trata-se de um exemplo utilizando .NET 8; ASP.NET Core MVC e SQL Server. 

Utilizei metodologia Code First e métodos assíncronos.

Esse projeto tem a finalidade de mostrar o uso e o funcionamento das tecnologias/métodos.

Observações: 

1) Não esqueça de ajustar o arquivo appsettings.json da solução, alterando o nome do servidor e o nome da base de dados utilizando os dados do seu servidor.
Esses dados estão no bloco ConnectionStrings.

2) Não esqueça de fazer a Migration para que a tabela do projeto seja criada no seu banco de dados.

Isso é feito através do próprio Visual Studio, no menu Ferramentas > Gerenciador de Pacote do Nuget > Console do Gerenciador de Pacotes.

Na janela que abrir na parte inferior do visual studio digite:
add-migration CriandoBanco 
(Eu usei "CriandoBanco", mas pode ser qualquer nome que desejar)

Caso não ocorra nenhum problema no seu banco de dados, a mensagem "Build Succeeded" deverá ser exibida.

Após isso serão criados os arquivos de Migration na pasta "Migration" da solução.

Em seguida digite:
update-database

Se não houver erro, a tabela será criada no servidor e a mensagem "Build Succeeded" deverá ser exibida.

Forte abraços!
Anderson Molina
