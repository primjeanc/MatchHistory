using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocacaoBiblioteca.Controller;
using LocacaoBiblioteca.Model;


namespace InterfaceBiblioteca
{
    class Program
    {              
        static LivroController livroController = new LivroController();//Instanciamos "Carregamos para memoria, nosso controlador de livros
        static UsuarioController usuarioController = new UsuarioController();
        static Usuario logado = null; //classe.objeto que ira receber a informacoes do login para apresentar quem esta logado
        static void Main(string[] args)
        {
            
            Console.WriteLine("SISTEMA DE LOCAÇÃO DE LIVROS 2.0");
            TrocaDeUsuario();
            MostraMenuSistema();
            Console.ReadKey();

        }
        private static void TrocaDeUsuario()// chama o teste de usuario, caso login/senha INVALIDOS, fica travado no login e acessa MENU
        {
            //while (RealizaLoginSistema()== null)
            //    Console.WriteLine("Login ou senha inválido.");
            
            logado = RealizaLoginSistema();
            while (logado == null)
            {
                logado = RealizaLoginSistema();
            }
        }

        /// <summary>
        /// Mostra no Console o Menu apos logar em sistema
        /// </summary>
        private static void MostraMenuSistema()
        {
            
            var opcao = int.MinValue;//variavel iniciada com menor valor de int possivel
            
            while (opcao != 0)//Menu em LOOP até que aperte 0 "zero"
            {
                Console.Clear();
                Console.WriteLine("SISTEMA DE LOCAÇÃO DE LIVROS 2.0");
                //Mostras as opcoes do menu em sistema
                
                Console.WriteLine("Menu Sistema:");
                Console.WriteLine($"Conectado: [{logado.Login}]");
                
                Console.WriteLine("1 - Listar Livros");                
                Console.WriteLine("2 - Cadastrar Livro");
                Console.WriteLine("3 - Atualizar Livros");                
                Console.WriteLine("4 - Remover Livro");
                Console.WriteLine("5 - Listar Usuarios");
                Console.WriteLine("6 - Cadastrar Usuário");
                Console.WriteLine("7 - Atualizar Usuários");
                Console.WriteLine("8 - Remover Usuário");
                Console.WriteLine("9 - Trocar de Usuário");
                Console.WriteLine("0 - Sair");
                opcao = int.Parse(Console.ReadKey(true).KeyChar.ToString());
                switch (opcao)
                {
                    
                    case 1:
                        ListagemLivros(); Console.ReadKey();
                        break;                    
                    case 2:
                        AdicionarLivro(); 
                        break;
                    case 3:
                        AtualizarLivro();
                        break;
                    case 4:
                        RemoverLivro();
                        break;
                    case 5:
                        ListagemUsuarios(); Console.ReadKey();
                        break;
                    case 6:
                        AdicionarUsuario();
                        break;
                    case 7:
                        AtualizarUsuario();
                        break;
                    case 8:
                        RemoverUsuario();
                        break;
                    case 9:
                        TrocaDeUsuario();
                        break;
                    case 0:
                        return;
                    default:
                        break;
                }  
        }             

        }
        /// <summary>
        /// Realiza login em sistema [entrando login/senha]. Retorna teste TRUE/FALSE do login [validação]
        /// </summary>
        /// <returns>Returna  TRUE-FALSE quando informado login e senha</returns>
        private static Usuario RealizaLoginSistema()
        {

            Console.WriteLine("Informe seu login e senha para acessar o sistema:");

            Console.WriteLine("Login: ");
            var loginDoUsuario = Console.ReadLine();
            Console.WriteLine("Senha: ");
            var senhaDoUsuario = Console.ReadLine();

            //UsuarioController usuarioController = new UsuarioController();//esse cara esta RESETANDO A LISTA NA HORA DE VALIDAR NOVOSO USUARIOS
            //o sistema cadastra usuarios e lista eles, mas quando vai LOGAR, os novos logins nao funcionam
            /* * Usuario usuario = new Usuario();//objeto usuario recebe Classe Usuario inicializada 'new'
            usuario.Login = loginDoUsuario;//atribui Login ao loginDoUsuario
            usuario.Senha = senhaDoUsuario;
            *///item para senhaDo...

            return usuarioController.GetUsuarios().FirstOrDefault(x => x.Login == loginDoUsuario && x.Senha == senhaDoUsuario);

        }

        /// <summary>
        /// Lista todos os livros registrados
        /// </summary>
        private static void ListagemLivros()//"Retorna..Livros" =private List<Livro> ListaDeLivros {get;set;} mas usado em metodo PUBLIC para conseguir LER e nao ESCREVER
        {
            //livroController.GetLivros().ToList().ForEach(l => Console.WriteLine($"ID: {l.Id} -- Nome: {l.Nome}"));//imprime todos os livros cadastrados
            string template = "ID: {0,-3} | Nome: {1,-35} | Ultima Atualização:{2,20}";
            livroController.GetLivros().ToList<Livro>().ForEach(v => 
            Console.WriteLine(String.Format(template, v.Id, v.Nome, v.DataAlteracao)));
        }
        private static void ListagemUsuarios()//"Retorna..Livros" =private List<Livro> ListaDeLivros {get;set;} mas usado em metodo PUBLIC para conseguir LER e nao ESCREVER
        {
            //livroController.GetLivros().ToList().ForEach(l => Console.WriteLine($"ID: {l.Id} -- Nome: {l.Nome}"));//imprime todos os livros cadastrados
            string template = "ID: {0,-3} | Login: {1,-30} | Senha {2,-30}";
            usuarioController.GetUsuarios().ToList<Usuario>().ForEach(v =>
            Console.WriteLine(String.Format(template, v.Id, v.Login, v.Senha)));
        }

        /// <summary>
        /// Metodo que adiciona ("cadastra") novos livros 
        /// </summary>
        public static void AdicionarLivro()
        {
            Console.WriteLine("Cadastrar livro em sistema:");
            Console.WriteLine("Informe o Nome:");
            var novo = Console.ReadLine();       

            var resultado = livroController.AddLivro(new Livro()
            { Nome = novo});
            if (resultado)
                Console.WriteLine("Livro cadastrado com sucesso!");
            else
                Console.WriteLine("Erro ao cadastrar...");
        }
        /// <summary>
        /// Metodo para adicionar Usuarios
        /// </summary>
        public static void AdicionarUsuario()
        {
            Console.WriteLine("Cadastrar usuário em sistema:");
            Console.WriteLine("Informe Login:");
            var lg = Console.ReadLine();
            Console.WriteLine("Informe Senha:");
            var pw = Console.ReadLine();

            var resultado = usuarioController.AddUsuario(new Usuario()
            {
                Login = lg,
                Senha = pw
            }) ;
            if (resultado)
                Console.WriteLine("Usuário cadastrado com sucesso!");
            else
                Console.WriteLine("Erro ao cadastrar...");
        }
        /// <summary>
        /// Metodo que altera status Ativo do Usuario para false ( a lista so mostra TRUE)
        /// </summary>
        private static void RemoverUsuario()
        {
            Console.WriteLine("Remoção de usuários do registro");
            ListagemUsuarios();//chama o metodo que ja mostrava lista de livros
            Console.WriteLine("Informe o ID do usuário a ser desativado:");
            var usuId = int.Parse(Console.ReadLine());

            usuarioController.RemoverUsuarioPorID(usuId);

            Console.WriteLine("Removido!");//retorna mensagem apos remover/desativar usuario
            Console.ReadKey();
        }
        /// <summary>
        /// Metodo que altera Ativo=false do Livro pelo ID informado, removendo da lista Ativa
        /// </summary>
        private static void RemoverLivro()
        {
            Console.WriteLine("Remoção de exemplar/livro do catálogo");
            ListagemLivros();//chama o metodo que ja mostrava lista de livros
            Console.WriteLine("Informe o ID do livro a ser desativado:");
            var livroID = int.Parse(Console.ReadLine());
            
            livroController.RemoverLivroPorID(livroID);

            Console.WriteLine("Exemplar removido!");//retorna mensagem apos remover/desativar usuario
            Console.ReadKey();
        }
        /// <summary>
        /// Atualiza informações de um livro, informado pelo ID da lista
        /// </summary>
        public static void AtualizarLivro()
        {
            Console.WriteLine("Atualizar Exemplar");
            ListagemLivros();//mostra a lista para identificar o ID que sera alterado
            Console.WriteLine("Informe o ID do exemplar a ser alterado:");
            var livId = int.Parse(Console.ReadLine());//informa ID para alterar
            //cria variavel para comparar e encontra ID
            var atualiz = livroController.GetLivros().FirstOrDefault(x => x.Id == livId);


            if (atualiz == null)
            {
                Console.WriteLine("ID informado inválido");
                return;
            }
            Console.WriteLine("Informe o Nome do exemplar:");
            atualiz.Nome = Console.ReadLine();

            var resultado = livroController.AtualizarLivro(atualiz);
            // apenas mostra mensagem ao final da tentativa de atualizar um produto
            if (resultado)
                Console.WriteLine("Exemplar Atualizado com sucesso!");
            else
                Console.WriteLine("Erro ao atualizar exemplar.");

        }
        public static void AtualizarUsuario()
        {
            Console.WriteLine("Atualizar Exemplar");
            ListagemUsuarios();//mostra a lista para identificar o ID que sera alterado
            Console.WriteLine("Informe o ID do usuário a ser alterado:");
            var usuId = int.Parse(Console.ReadLine());//informa ID para alterar
            //cria variavel para comparar e encontra ID
            var atualiz = usuarioController.GetUsuarios().FirstOrDefault(x => x.Id == usuId);


            if (atualiz == null)
            {
                Console.WriteLine("ID informado inválido");
                return;
            }
            Console.WriteLine("Informe o Login novo:");
            atualiz.Login = Console.ReadLine();
            Console.WriteLine("Informe a Senha nova:");
            atualiz.Senha = Console.ReadLine();

            var resultado = usuarioController.AtualizarUsuario(atualiz);
            // apenas mostra mensagem ao final da tentativa de atualizar um produto
            if (resultado)
                Console.WriteLine("Atualizado com sucesso!");
            else
                Console.WriteLine("Erro ao atualizar exemplar.");

        }

    }
}
