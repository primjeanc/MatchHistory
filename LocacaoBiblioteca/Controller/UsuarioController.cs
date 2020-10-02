using LocacaoBiblioteca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoBiblioteca.Controller
{
    /// <summary>
    /// Classe que contem os metodos de usuarios do sistema
    /// </summary>
    public class UsuarioController
    {
        private LocacaoContext contextDB = new LocacaoContext();//contextDB recebe LocacaoContext para que 
                                                                //o conteudo de LocacaoContext (como ListaDeUsuaris) seja acessivel em 'contextDB.Lista...'

        /// <summary>
        /// Metodo que realiza o login dentro do nosso sistema
        /// Para realizar login padrao use:
        /// Login: Admin
        /// Senha: Admin
        /// </summary>
        /// <param name="Usuario">Passamos um objeto de nome Ususario como parametro</param>        
        /// <returns>Retorna verdadeiro quando existir o usuario com este login e senha</returns>
        public bool LoginSistema(Usuario usuarios)//Usuarios= id,login,senha etc
        {
            //como a lista ja foi inicializada e salva na memoria na propria classe, o teste na LISTA de USUARIOS fica mais simples

            return contextDB.Usuarios.ToList().Exists(u => u.Login == usuarios.Login && u.Senha == usuarios.Senha);

            /*if (usuarios.Login == "Admin" && usuarios.Senha == "Admin")
                return true;
            else
                return false;*/// antigo teste
        }
     
        /// <summary>
        /// Metodo que retorna relação de usuarios
        /// </summary>
        /// <returns></returns>
        public IQueryable<Usuario> GetUsuarios()
        {
            return contextDB.Usuarios.Where(x => x.Ativo == true);// nao precisaria do '== true', como padrao ja seria TRUE
        }
        /// <summary>
        /// Metodo para adicionar usuarios
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddUsuario(Usuario item)
        {
            if (string.IsNullOrWhiteSpace(item.Login))// metodo que identifica espacos em branco
                return false;
            if (string.IsNullOrWhiteSpace(item.Senha))// metodo que identifica espacos em branco
                return false;

            contextDB.Usuarios.Add(item);
            contextDB.SaveChanges();

            return true;
        }
        /// <summary>
        /// Metodo que desativa um regstro de usuario cadastrado em nossa lista
        /// </summary>
        /// <param name="intentificadoID">intendifica usuario a ser desativado</param>
        public void RemoverUsuarioPorID(int intentificadoID)
        {
            //aqui usamos FirstOrDefault para localiza nosso usuario dentro da lista
            //com isso conseguimos acessar as propriedades dele e desativar o registro
            contextDB.Usuarios.FirstOrDefault(x => x.Id == intentificadoID).Ativo = false;
        }
        public bool AtualizarUsuario(Usuario item)
        {
            var usu =//variavel para o celular
            contextDB.Usuarios.FirstOrDefault(x => x.Id == item.Id);
            //BD      tabela    busca na tabela o celular
            //Compara os IDs e verifica se encontrou o celular
            if (usu == null)
                return false;
            else
                //celular = item;
                usu.DataAlteracao = DateTime.Now;
            contextDB.SaveChanges();// salva apos confirmar IDs iguais
            return true;
        }

    }
}
