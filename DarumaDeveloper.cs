using DarumaMobileFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DarumaMobileFramework
{
    
    public enum TipoDadoDaruma
    {
        Numerico, ValorUn, Porcentagem, Quantidade, Alfanumerico, AlfanumericoVariavel
    };

    /// <summary>
    /// Abstrai os comandos utilizados nas Impressoras Daruma em funções de alto nível.
    /// </summary>
    public class DarumaDeveloper
    {
        private DarumaMobile darumaMobile = null;  
      

        private string strConexao = "";


        private List<string> aliquotas = new List<string>();
        public List<string> ListaAliquotas
        {
            get { return aliquotas; }
        }
        private List<string> aliquotasAux = new List<string>();
        public List<string> ListaAliquotasAux
        {
            get { return aliquotasAux; }
        }

        private List<string> meiosPagto = new List<string>();
        public List<string> ListaMeiosPagto
        {
            get { return meiosPagto; }
        }
        private List<string> naoFiscais = new List<string>();
        public List<string> ListaNaoFiscais
        {
            get { return naoFiscais; }
        }
        private List<string> relatoriosGerais = new List<string>();
        public List<string> ListaRelatoriosGerais
        {
            get { return relatoriosGerais; }
        }


        //C# não suporta char especiais como % ou $ como identificador em um enum
        private List<string> tiposAcrescimosDecrescimos = new List<string>();


        private Dictionary<int, string> erros = new Dictionary<int, string>();
        public Dictionary<int, string> DictionaryErros
        {
            get { return erros; }
        }

        private Dictionary<int, string> avisos = new Dictionary<int, string>();
        public Dictionary<int, string> DictionaryAvisos
        {
            get { return avisos; }
        }


        private int decimaisValorUn = 2;
        public int DecimaisValorUn
        {
            get { return decimaisValorUn; }
        }

        private int decimaisQuantidade = 2;
        public int DecimaisQuantidade
        {
            get { return decimaisQuantidade; }
        }


        private int codigoUltimoErro = 1;
        public int CodigoUltimoErro
        {
            get { return codigoUltimoErro; }
        }

        private int codigoUltimoAviso = 1;
        public int CodigoUltimoAviso
        {
            get { return codigoUltimoAviso; }
        }



        private string iCOOCupomAberto = "";
        public string COOCupomAberto
        {
            get { return iCOOCupomAberto; }
        }

        private string iCCFCupomAberto = "";
        public string CCFCupomAberto
        {
            get { return iCCFCupomAberto; }
        }


        private string iNItemVendido = "";
        public string NItemVendido
        {
            get { return iNItemVendido; }
        }

        private string iTipoDescontoItemVendido = "";
        public string TipoDescontoItemVendido
        {
            get { return iTipoDescontoItemVendido; }
        }

        private string iTotalLiquidoItemVendido = "";
        public string TotalLiquidoItemVendido
        {
            get { return iTotalLiquidoItemVendido; }
        }


        private string iSubTotalCupomTotalizado = "";
        public string SubTotalCupomTotalizado
        {
            get { return iSubTotalCupomTotalizado; }
        }


        private string iSaldoOuTrocoPagamentoEfetuado = "";
        public string SaldoOuTrocoPagamentoEfetuado
        {
            get { return iSaldoOuTrocoPagamentoEfetuado; }
        }


        private string iCCOCupomEncerrado = "";
        public string COOCupomEncerrado
        {
            get { return iCCOCupomEncerrado; }
        }

        private string iTotalLiquidoCupomEncerrado = "";
        public string TotalLiquidoCupomEncerrado
        {
            get { return iTotalLiquidoCupomEncerrado; }
        }


        private string iSerieMFDE = "";
        public string SerieMFDE
        {
            get { return iSerieMFDE; }
        }


        /// <summary>
        /// Inicializa os objetos utilizados pelo framework
        /// </summary>
        private void init()
        {

            //Aliquotas Auxiliares pre cadastradas
            #region
            aliquotasAux.Add("F1");
            aliquotasAux.Add("F2");
            aliquotasAux.Add("I1");
            aliquotasAux.Add("I2");
            aliquotasAux.Add("N1");
            aliquotasAux.Add("N2");
            aliquotasAux.Add("FS1");
            aliquotasAux.Add("FS2");
            aliquotasAux.Add("IS1");
            aliquotasAux.Add("IS2");
            aliquotasAux.Add("NS1");
            aliquotasAux.Add("NS2");
            #endregion

            //C# não suporta char especiais como % ou $ como identificador em um enum
            #region
            tiposAcrescimosDecrescimos.Add("D%");
            tiposAcrescimosDecrescimos.Add("D$");
            tiposAcrescimosDecrescimos.Add("A%");
            tiposAcrescimosDecrescimos.Add("A$");
            #endregion

            //Erros necessários ao framework
            #region
            erros.Add(-3, "Tamanho do parametro superou o máximo permitido");
            erros.Add(-2, "Elemento não cadastrado");
            erros.Add(-1, "Erro não identificado");
            #endregion

            //Erros da Tabela de Erros
            #region
            erros.Add(  1, "ECF com falha mecânica");
            erros.Add(  2, "MF não conectada");
            erros.Add(  3, "MFD não conectada");
            erros.Add(  4, "MFD esgotada");
            erros.Add(  5, "Erro na comunicação com a MF");
            erros.Add(  6, "Erro na comunicação com a MFD");
            erros.Add(  7, "MF não inicializada");
            erros.Add(  8, "MFD não inicializada");
            erros.Add(  9, "MFD já inicializada");
            erros.Add( 10, "MFD foi substituída");
            erros.Add( 11, "MFD já cadastrada");
            erros.Add( 12, "Erro na inicialização da MFD");
            erros.Add( 13, "Faltam parâmetros de inicialização na MF");
            erros.Add( 14, "Comando não suportado");
            erros.Add( 15, "Superaquecimento da cabeça de impressão");
            erros.Add( 16, "Perda de dados da MT");
            erros.Add( 17, "Operação habilitada apenas em MIT");
            erros.Add( 18, "Operação habilitada apenas em modo fiscal");
            erros.Add( 19, "Data inexistente");
            erros.Add( 20, "Data inferior ao do último documento");
            erros.Add( 21, "Intervalo inconsistente");
            erros.Add( 22, "Não existem dados");
            erros.Add( 23, "Clichê de formato inválido");
            erros.Add( 24, "Erro no verificador da comunicação");
            erros.Add( 25, "Senha incorreta");
            erros.Add( 26, "Número de decimais para quantidade inválido");
            erros.Add( 27, "Número de decimais para valor unitário inválido");
            erros.Add( 28, "Tipo de impressão de FD inválido");
            erros.Add( 29, "Caracter não estampável");
            erros.Add( 30, "Caracter não estampável ou em branco");
            erros.Add( 31, "Caracteres não podem ser repetidos");
            erros.Add( 32, "Limite de itens atingido");
            erros.Add( 33, "Todos os totalizadores fiscais já estão programados");
            erros.Add( 34, "Totalizador fiscal já programado");
            erros.Add( 35, "Todos os totalizadores não fiscais já estão programados");
            erros.Add( 36, "Totalizador não fiscal já programado");
            erros.Add( 37, "Todos os relatórios gerenciais já estão programados");
            erros.Add( 38, "Relatório gerencial já programado");
            erros.Add( 39, "Meio de pagamento já programado");
            erros.Add( 40, "Índice inválido");
            erros.Add( 41, "Índice do meio de pagamento inválido");
            erros.Add( 42, "Erro gravando número de decimais na MF");
            erros.Add( 43, "Erro gravando moeda na MF");
            erros.Add( 44, "Erro gravando símbolos de decodificação do GT na MF");
            erros.Add( 45, "Erro gravando número de fabricação da MFD na MF");
            erros.Add( 46, "Erro gravando usuário na MF");
            erros.Add( 47, "Erro gravando GT do usuário anterior na MF");
            erros.Add( 48, "Erro gravando registro de marcação na MF");
            erros.Add( 49, "Erro gravando CRO na MF");
            erros.Add( 50, "Erro gravando impressão de FD na MF");
            erros.Add( 51, "Campo em branco ou zero não permitido");
            erros.Add( 52, "Campo reservado a gravação da moeda na MF esgotado");
            erros.Add( 53, "Campo reservado a gravação da tabela de GT na MF esgotado");
            erros.Add( 54, "Campo reservado a gravação do NS da MFD na MF esgotado");
            erros.Add( 55, "Campo reservado a gravação de usuário na MF esgotado");
            erros.Add( 56, "CNPJ inválido");
            erros.Add( 57, "CRZ e CRO em zero");
            erros.Add( 58, "Intervalo invertido");
            erros.Add( 59, "Utilize apenas 0 ou 1");
            erros.Add( 60, "Configuração permitida apenas imediatamente a RZ");
            erros.Add( 61, "Símbolo gráfico inválido");
            erros.Add( 62, "Falta pelo menos 1 campo no nome da moeda para cheque");
            erros.Add( 63, "Código supera o valor 255");
            erros.Add( 64, "Utilize valores entre 25 e 80");
            erros.Add( 65, "Utilize valores entre 1 e 15");
            erros.Add( 66, "Utilize valores entre 0 e 7250");
            erros.Add( 67, "Data informada não coincide com a data do ECF");
            erros.Add( 68, "Deve ajustar o relógio ( utilize o comando [FS] M <200> )");
            erros.Add( 69, "Erro ao ajustar o relógio");
            erros.Add( 70, "Capacidade da MF esgotada");
            erros.Add( 71, "Versão do SB gravado na MF incorreta");
            erros.Add( 72, "Fim do papel");
            erros.Add( 73, "Nenhum usuário programado");
            erros.Add( 74, "Utilize apenas dígitos numéricos");
            erros.Add( 75, "Campo não pode estar em zero");
            erros.Add( 76, "Campo não pode estar em branco");
            erros.Add( 77, "Valor da operação não pode ser zero");
            erros.Add( 78, "CF aberto");
            erros.Add( 79, "CNF aberto");
            erros.Add( 80, "CCD aberto");
            erros.Add( 81, "RG aberto");
            erros.Add( 82, "CF não aberto");
            erros.Add( 83, "CNF não aberto");
            erros.Add( 84, "CCD não aberto");
            erros.Add( 85, "RG não aberto");
            erros.Add( 86, "CCD ou RG não aberto");
            erros.Add( 87, "Documento já totalizado");
            erros.Add( 88, "RZ do movimento anterior pendente");
            erros.Add( 89, "Já emitiu RZ de hoje");
            erros.Add( 90, "Totalizador sem alíquota programada");
            erros.Add( 91, "Campo de código ausente");
            erros.Add( 92, "Campo de descrição ausente");
            erros.Add( 93, "VU ou quantidade em zero");
            erros.Add( 94, "Item ainda não vendido");
            erros.Add( 95, "Desconto ou acréscimo não pode ser zero");
            erros.Add( 96, "Item já possui desconto ou acréscimo");
            erros.Add( 97, "Ítem cancelado");
            erros.Add( 98, "Operação inibida por configuração");
            erros.Add( 99, "Opção não suportada");
            erros.Add(100, "Desconto ou acréscimo supera valor bruto");
            erros.Add(101, "Desconto ou acréscimo final de valor zero");
            erros.Add(102, "Valor bruto zero");
            erros.Add(103, "Overflow no valor do item");
            erros.Add(104, "Overflou no valor do desconto ou acréscimo");
            erros.Add(105, "Overflow na capacidade do documento");
            erros.Add(106, "Overflow na capacidade do totalizador");
            erros.Add(107, "Item não possui desconto");
            erros.Add(108, "Item já possui desconto");
            erros.Add(109, "Quantidade possui mais de 2 decimais");
            erros.Add(110, "Valor unitário possui mais de 2 decimais");
            erros.Add(111, "Quantidade a cancelar deve ser inferior a total");
            erros.Add(112, "Campo de descrição deste item não mais presente na MT");
            erros.Add(113, "Subtotal não possui desconto ou acréscimo");
            erros.Add(114, "Não em fase de totalização");
            erros.Add(115, "Não em fase de venda ou totalização");
            erros.Add(116, "Mais de 1 desconto ou acréscimo não permitido");
            erros.Add(117, "Valor do desconto ou acréscimo supera subtotal");
            erros.Add(118, "Meio de pagamento não programado");
            erros.Add(119, "Não em fase de pagamento ou totalização");
            erros.Add(120, "Não em fase de finalização de documento");
            erros.Add(121, "Já emitiu mais CCDs que poderia estornar");
            erros.Add(122, "Último documento não é cancelável");
            erros.Add(123, "Estorne CCDs");
            erros.Add(124, "Último documento não foi CF");
            erros.Add(125, "Último documento não foi CNF");
            erros.Add(126, "Não pode cancelar");
            erros.Add(127, "Pagamento não mais na MT");
            erros.Add(128, "Já emitiu CCD deste pagamento");
            erros.Add(129, "RG não programado");
            erros.Add(130, "CNF não programado");
            erros.Add(131, "Cópia não disponível");
            erros.Add(132, "Já emitiu segunda via");
            erros.Add(133, "Já emitiu reimpressão");
            erros.Add(134, "Informações sobre o pagamento não disponíveis");
            erros.Add(135, "Já emitiu todas as parcelas");
            erros.Add(136, "Parcelamento somente na sequência");
            erros.Add(137, "CCD não encontrado");
            erros.Add(138, "Não pode utilizar SANGRIA ou SUPRIMENTO");
            erros.Add(139, "Pagamento não admite CCD");
            erros.Add(140, "Relógio inoperante");
            erros.Add(141, "Usuário sem CNPJ");
            erros.Add(142, "Usuário sem IM");
            erros.Add(143, "Não se passou 1 hora após o fechamento do último documento");
            erros.Add(144, "ECF OFF LINE");
            erros.Add(145, "Documento em emissão");
            erros.Add(146, "COO não coincide");
            erros.Add(147, "Erro na autenticação");
            erros.Add(148, "Erro na impressão de cheque");
            erros.Add(149, "Data não pertence ao século XXI");
            erros.Add(150, "Usuário já programado");
            erros.Add(151, "Descrição do pagamento já utilizada");
            erros.Add(152, "Descrição do totalizador já utilizada");
            erros.Add(153, "Descrição do RG já utilizada");
            erros.Add(154, "Já tem desconto após acréscimo ( ou vice versa )");
            erros.Add(155, "Já programou 15 totalizadores para ICMS");
            erros.Add(156, "Já programou 15 totalizadores para ISS");
            erros.Add(157, "MFD com problemas");
            erros.Add(158, "Razão social excede 48 caracteres");
            erros.Add(159, "Nome fantasia excede 48 caracteres");
            erros.Add(160, "Endereço excede 120 caracteres");
            erros.Add(161, "Identificação do programa aplicativo ausente");
            erros.Add(162, "Valor de desconto supera valor acumulado em totalizador");
            erros.Add(163, "Número de parcelas no pagamento não pode exceder 24");
            erros.Add(164, "MFD não cadastrada");
            erros.Add(165, "Excedeu limite de impressão de FD ( capacidade na MF esgotada )");
            erros.Add(166, "Efetivado é igual ao estornado");
            erros.Add(167, "Símbolo da moeda já programado");
            erros.Add(168, "UF inválida");
            erros.Add(169, "UF já programada");
            erros.Add(170, "Erro gravando UF");
            erros.Add(171, "Leitor CMC-7 não instalado");
            erros.Add(172, "Erro de leitura do código CMC-7");
            erros.Add(173, "Autenticação não permitida");
            erros.Add(174, "Operação somente com mecanismo matricial de impacto");
            erros.Add(175, "Coordenadas de cheque inválidas");
            erros.Add(176, "Impressão do verso do cheque somente após a impressão da frente");
            erros.Add(177, "Indice do bitmap inválido");
            erros.Add(178, "Bitmap de tamanho inválido");
            erros.Add(179, "Última RZ a mais de 30 dias. Comando de RZ deve informar data correta");
            erros.Add(184, "Parâmetro só pode ser “A” ou “T”");
            erros.Add(185, "Falta unidade doproduto");
            erros.Add(186, "Velocidade não permitida");
            erros.Add(187, "Código repetido");
            erros.Add(188, "Fora dos limites");
            erros.Add(189, "Já identificou o consumidor");
            erros.Add(190, "Número de Fabricação incorreto");
            erros.Add(191, "Informação disponível não corresponde a MF informada");
            erros.Add(192, "MF já em uso");
            erros.Add(193, "Falha não recuperável durante a operação");
            erros.Add(194, "Opção inválida");
            erros.Add(195, "Parâmetros inválidos");
            erros.Add(196, "Caracter HEXA inválido");
            erros.Add(197, "Valor insuficiente de pagamento");
            erros.Add(198, "IE inválido");
            erros.Add(199, "IM inválido");
            erros.Add(301, "CFBP Inibido");
            erros.Add(302, "Modalidade de Transporte inválida");
            erros.Add(303, "Categoria de Transporte inválida");
            erros.Add(304, "UF incompatível ");
            erros.Add(305, "Comando disponível apenas em CF genérico");
            erros.Add(400, "Chave não carregada");
            erros.Add(401, "Chave inválida");
            erros.Add(402, "Erro na decodificaçào");
            erros.Add(403, "Erro na codificação");
            #endregion

            //Erros da Tabela de Avisos
            #region
            avisos.Add(0, "Sem Aviso.");
            avisos.Add(1, "Papel acabando");
            avisos.Add(2, "Tampa aberta.");
            avisos.Add(4, "Bateria fraca.");
            avisos.Add(40, "Compactando.");
            #endregion
        }

        /// <summary>
        /// Contrutor utilizando um objeto DarumaMobile já inicializado.
        /// </summary>
        /// <param name="dmf">Objeto DarumaMobile já inicializado com parametros.</param>
        public DarumaDeveloper(DarumaMobile dmf)
        {
            darumaMobile = dmf;
            init();
        }

        /// <summary>
        /// Construtor que também inicia um objeto DarumaMobile próprio e privado.
        /// </summary>
        /// <param name="host">IP ou Nome na Rede da impressora.</param>
        /// <param name="port">Port na qual a conexão será efetuada.</param>
        /// <param name="timeout">Timeout utilizado para a conexão.</param>
         /// <param name="fechaconexao">Se verdadeiro, utiliza operações atomicas, ou seja, à cada operação será iniciada uma nova conexão.</param>
        public DarumaDeveloper(string host, int port, int timeout, bool fechaconexao = true)
        {
            strConexao =    "@socket(host=" + host +
                            ";port=" + port +
                            ";timeout=" + timeout +
                            ")@framework(trataexcecao=" + true + ";fechacomunicacao=" + fechaconexao + ");";

            darumaMobile = new DarumaMobile(strConexao);
            init();
        }
        
        /// <summary>
        /// Conecta à impressora e retorna informações essenciais para o funcionamento das impressoras fiscais. Se utilizar DUAL, não inicialize, somente conecte.
        /// </summary>
        public async Task inicializa()
        {
            await IniciarComunicacaoAsync();

            await DMF_UTIL_PreencheTabelas();

            await rLerDecimaisQuantidade_ECF_DarumaAsync();
            await rLerDecimaisValorUnitario_ECF_DarumaAsync();
            
            iSerieMFDE = await rRetornarInformacao_ECF_DarumaAsync("77", "");

            await FecharComunicacaoAsync();
        }

        #region Exposição de metodos da DarumaMobile

        /// <summary>
        /// Inicializa a conexão utilizando os parametros utilizados no objeto DarumaMobile já existe, ou passados no construtor.
        /// </summary>
        public async Task<AsyncVoidMethodBuilder> IniciarComunicacaoAsync()
        {
            try
            {
                await darumaMobile.IniciarComunicacaoAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return AsyncVoidMethodBuilder.Create();
        }

        /// <summary>
        /// Configura os Parametros da Conexao para reconectar à Impressora.
        /// </summary>
        /// <param name="host">IP ou Nome na Rede da impressora.</param>
        /// <param name="port">Port na qual a conexão será efetuada.</param>
        /// <param name="timeout">Timeout utilizado para a conexão.</param>
        /// <param name="trataexcecao">Se veridadeiro, lança exceções para o usuário tratar.</param>
        /// <param name="timeout">Se verdadeiro, utiliza operações atomicas, ou seja, à cada operação será iniciada uma nova conexão.</param>
        public bool ConfParametros(string host, string port, string timeout, bool trataexcecao, bool fechaconexao)
        {
            try
            {
                strConexao = "@socket(host=" + host +
                                           ";port=" + port +
                                           ";timeout=" + timeout +
                                           ")@framework(trataexcecao=" + trataexcecao + ";fechacomunicacao=" + fechaconexao + ");";

                return darumaMobile.ConfParametros(strConexao)==0 ? false : true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Encerra a conexão atual com a Impressora.
        /// </summary>
        public async Task<AsyncVoidMethodBuilder> FecharComunicacaoAsync()
        {
            try
            {
                await darumaMobile.FecharComunicacaoAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return AsyncVoidMethodBuilder.Create();
        }

        #endregion


        #region Metodos da Camada
        
        #region GRUPO CUPOM FISCAL
        
        /// <summary>
        /// Este método permite que você saiba qual é o número de casas decimais que está programado para a quantidade. 
        /// </summary>
        public async Task<int> rLerDecimaisQuantidade_ECF_DarumaAsync()
        {
            int cod = 200;
            string param = "139";
            string retorno = await darumaMobile.EnviarComando_FS_R_Async(cod.ToString() + param);

            retorno = DMF_UTIL_FormataResposta(retorno, cod, param);

            int valor = 0;

            if (!Int32.TryParse(retorno[0].ToString(), out valor)) DMF_UTIL_LancaException(-5);

            decimaisQuantidade = valor;
            
            return valor;
        }

        /// <summary>
        /// Este método permite que você saiba qual é o número de casas decimais que está programado para o valor dos itens. 
        /// </summary>
        public async Task<int> rLerDecimaisValorUnitario_ECF_DarumaAsync()
        {
            int cod = 200;
            string param = "139";
            string retorno = await darumaMobile.EnviarComando_FS_R_Async(cod.ToString() + param);

            retorno = DMF_UTIL_FormataResposta(retorno, cod, param);

            int valor = 0;

            if (!Int32.TryParse(retorno[1].ToString(), out valor)) DMF_UTIL_LancaException(-5);

            decimaisValorUn = valor;

            return valor;
        }

        /// <summary>
        /// Este método retorna as alíquotas cadastradas na impressora, separados por ponto-e-virgula (;).
        /// Exemplo de retorno: T0700;T1200;S0700;T0800;T0900;S0800;T0800;T0900;T1000;T1100;T1200;T1300;T1400; 
        /// T - Alíquota de ICMS  - Ex: 01700 - Alíquota de 17,00 de ICMS 
        /// S - Alíquota de ISS -   Ex: 11700 - Alíquota de 17,00 de ISS 
        /// </summary>
        public async Task<string> rLerAliquotas_ECF_DarumaAsync()
        {
            int cod = 200;
            string param = "125";
            string retorno = await darumaMobile.EnviarComando_FS_R_Async(cod.ToString() + param);

            retorno = DMF_UTIL_FormataResposta(retorno, cod, param);
            
            char[] aux = retorno.ToCharArray();
            string str = "";

            for (int i = 0; i < 16; i++)
            {
                char c = aux[i * 5];

                if (c != 255)
                {
                    //separa a letra S ou T
                    if (c == '0') str += 'T';
                    else if (c == '1') str += 'S';

                    //separa o número de 4 digitos
                    str += retorno.Substring(i * 5 + 1, 4) + ';';
                }
                else
                {
                    //fim
                    break;
                }
            }
            
            //Remove o ponto e virgula do final
            if(str.Length > 0)
                str = str.Remove(str.Length - 1, 1);

            //Atualiza lista de aliquotas
            aliquotas.Clear();
            aliquotas = str.Split(';').ToList();

            string end = ";TF1;TF2;TI1;TI2;TN1;TN2;SFS1;SFS2;SIS1;SIS2;SNS1;SNS2";
            string result = str + end;
            
            return result;
        }
        
        /// <summary>
        /// Este método retorna os meios de pagamentos cadastrados na impressora, separados por virgula (,).
        /// Exemplo de retorno: Dinheiro,  Duplicata, Cheque, Cartão 
        /// </summary>
        public async Task<string> rLerMeiosPagto_ECF_DarumaAsync()
        {
            int cod = 200;
            string param = "126";
            string retorno = await darumaMobile.EnviarComando_FS_R_Async(cod.ToString() + param);

            retorno = DMF_UTIL_FormataResposta(retorno, cod, param);
            retorno = DMF_UTIL_Limpa255(retorno);

            string str = "";

            for (int i = 0; i < 20; i++)
            {
                //separa o Meio de Pagamento
                string aux = retorno.Substring(i * 15, 15).TrimEnd();
                
                if (aux.Length != 0){
                    str += aux + ',' ;
                }
            }

            //remove a ultima virgula, 
            str = str.Remove(str.Length-1, 1);

            //Atualiza lista de de Meios de Pagamento
            meiosPagto.Clear();
            meiosPagto = str.Split(',').ToList() ;


            return str;
        }
        
        /// <summary>
        /// Este método retorna os nomes dos totalizadores não fiscais em sua impressora, separados por virgula (,). 
        /// Exemplo de retorno: Sangria, Suprimento, Conta de Luz, Conta de Telefone.
        /// </summary>
        public async Task<string> rLerCNF_ECF_DarumaAsync()
        {
            int cod = 200;
            string param = "127";
            string retorno = await darumaMobile.EnviarComando_FS_R_Async(cod.ToString() + param);

            retorno = DMF_UTIL_FormataResposta(retorno, cod, param);
            retorno = DMF_UTIL_Limpa255(retorno);

            string str = "";

            for (int i = 0; i < 20; i++)
            {
                //separa o Meio de Pagamento
                string aux = retorno.Substring(i * 15, 15).TrimEnd();

                if (aux.Length != 0)
                {
                    str += aux + ',';
                }
            }

            //remove a ultima virgula, 
            str = str.Remove(str.Length - 1, 1);

            //Atualiza lista de de Meios de Pagamento
            naoFiscais.Clear();
            naoFiscais = str.Split(',').ToList();


            return str;
        }

        /// <summary>
        /// Relatórios Gerenciais cadastrados impressora Fiscal. 
        /// </summary>
        /// <returns>Ex: Gerencial X, Conf. de mesa, Conf. de ficha,  Mesas em aberto, Fichas abertas, Transf. mesas </returns>
        public async Task<string> rLerRG_ECF_DarumaAsync()
        {
            int cod = 200;
            string param = "128";
            string retorno = await darumaMobile.EnviarComando_FS_R_Async(cod.ToString() + param);

            retorno = DMF_UTIL_FormataResposta(retorno, cod, param);
            retorno = DMF_UTIL_Limpa255(retorno);

            string str = "";

            for (int i = 0; i < 20; i++)
            {
                //separa o Meio de Pagamento
                string aux = retorno.Substring(i * 15, 15).TrimEnd();

                if (aux.Length != 0)
                {
                    str += aux + ',';
                }
            }

            //remove a ultima virgula, 
            str = str.Remove(str.Length - 1, 1);

            //Atualiza lista de de Meios de Pagamento
            relatoriosGerais.Clear();
            relatoriosGerais = str.Split(',').ToList();


            return str;
        }
        
        /// <summary>
        /// Este método abre um Cupom Fiscal, identificando consumidor. 
        /// </summary>
        /// <param name="CPF">CPF ou CNPJ Consumidor. 20 caracteres com máscara. Alfanumérico.</param>
        /// <param name="Nome">Nome Consumidor. 30 caracteres com máscara. Alfanumérico.</param>
        /// <param name="Endereco">Endereço Consumidor. 79 caracteres com máscara. Alfanumérico.</param>
        public async Task iCFAbrir_ECF_DarumaAsync(string CPF, string Nome, string Endereco)
        {
            int cod = 200;

            CPF = DMF_UTIL_Valida(CPF, 20, TipoDadoDaruma.AlfanumericoVariavel);
            Nome = DMF_UTIL_Valida(Nome, 30, TipoDadoDaruma.AlfanumericoVariavel);
            Endereco = DMF_UTIL_Valida(Endereco, 79, TipoDadoDaruma.AlfanumericoVariavel);

            string param = CPF + Nome + Endereco;
            string retorno = DMF_UTIL_FormataResposta( await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "" );

            iCOOCupomAberto = retorno.Substring(0, 6);
            iCCFCupomAberto = retorno.Substring(6, 6);
        }

        /// <summary>
        /// Este método abre um Cupom Fiscal. 
        /// </summary>
        public async Task iCFAbrirPadrao_ECF_DarumaAsync()
        {
            await iCFAbrir_ECF_DarumaAsync("", "", "");

        }

        /// <summary>
        /// Este método vende um item no Cupom Fiscal. 
        /// </summary>
        /// <param name="CargaTributaria">
        /// Alíquota do Item. A alíquota pode ser informada com a virgula (I07,00) ou sem a virgula (I0700).  
        /// Exemplos: 
        /// ICMS Não tributado: ("II" - Isento, "FF"- Substituição  tributária, "NN" - Não Tributária). 
        /// ICMS Tributado:( I07,00 , I18,00,  I0700 , I1800) 
        /// ISSQN Não tributado: ("ISS" - Isento, "FS"- Substituição tributária, "NS" - Não Tributária). 
        /// ISSQN Tributado: (S07,00 , S18,00, S0700 , S1800) 
        /// 5 caracteres. Alfanumérico.
        /// </param>
        /// <param name="Quantidade">Quantidade do Item. 7 caracteres. Real.</param>
        /// <param name="PrecoUnitario">Preço Unitário do Item. 7 caracteres. Real.</param>
        /// <param name="TipoDescAcresc">Tipo Acréscimo ou Desconto -  Exemplo -  
        /// D% - Desconto em Percentual 
        /// D$ - Desconto em Valor  
        /// A% - Acréscimo em Percentual  
        /// A$ - Acréscimo em Valor 
        /// 2 caracteres. Alfanumérico.</param>
        /// <param name="ValorDescAcresc">Valor do acréscimo ou Valor da porcentagem. 11 caracteres. Real. </param>
        /// <param name="TruncarOuArredondar"> Indicador de modo de cálculo 
        /// “T” para truncamento 
        /// “A” para arredondamento
        /// 1 caracteres. Alfanumérico.</param>
        /// <param name="CodigoItem">Código do Item. 14 caracteres. Alfanumérico.</param>
        /// <param name="UnidadeMedida">Unidade de medida.  3 caracteres. Alfanumérico.</param>
        /// <param name="DescricaoItem">Descrição do Item.  233 caracteres. Alfanumérico.</param>
        /// <param name="TamMinDescItem">Tamanho mínimo da descrição, no caso de impressão em 1 única linha. Se zero, não tenta imprimir em uma única linha.  2 caracteres. Numérico.</param>
        public async Task iCFVender_ECF_DarumaAsync(string CargaTributaria, string Quantidade, string PrecoUnitario, string TipoDescAcresc, string ValorDescAcresc, string TruncarOuArredondar, string CodigoItem, string UnidadeMedida, string DescricaoItem, string TamMinDescItem)
        {
            int cod = 207;

            DescricaoItem = DMF_UTIL_Valida(DescricaoItem, 233, TipoDadoDaruma.AlfanumericoVariavel);
            Quantidade = DMF_UTIL_Valida(Quantidade, 7, TipoDadoDaruma.Quantidade);
            PrecoUnitario = DMF_UTIL_Valida(PrecoUnitario, 8, TipoDadoDaruma.ValorUn);
            UnidadeMedida = DMF_UTIL_Valida(UnidadeMedida, 3, TipoDadoDaruma.Alfanumerico);
            TamMinDescItem = DMF_UTIL_Valida(TamMinDescItem, 2, TipoDadoDaruma.Numerico);
            CodigoItem = DMF_UTIL_Valida(CodigoItem, 14, TipoDadoDaruma.Alfanumerico);
            
            CargaTributaria = CargaTributaria.Replace(",", "");
            string ind = DMF_UTIL_ConsultaIndice_Aliquota(CargaTributaria);

            string AD = DMF_UTIL_ConsultaTipoAcrescimoDecrescimo(TipoDescAcresc);
            if( AD.Equals("0") || AD.Equals("2")  )
                ValorDescAcresc= DMF_UTIL_Valida(ValorDescAcresc, 11, TipoDadoDaruma.Porcentagem);
            else
                ValorDescAcresc= DMF_UTIL_Valida(ValorDescAcresc, 11, TipoDadoDaruma.Numerico);

            string param = ind+Quantidade+PrecoUnitario+AD+ValorDescAcresc+TamMinDescItem+CodigoItem+UnidadeMedida+TruncarOuArredondar+DescricaoItem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

            iNItemVendido = retorno.Substring(0, 3);
            iTipoDescontoItemVendido = retorno.Substring(3, 1);
            iTotalLiquidoItemVendido = retorno.Substring(4, 11);
        }

        /// <summary>
        /// Este método permite aplicar desconto no item do cupom fiscal antes da totalização.
        /// </summary>
        /// <param name="NumItem">Número do Item. 3 caracteres. Numérico.</param>
        /// <param name="TipoDesc">Tipo Acréscimo ou Desconto -  Exemplo -  
        /// D% - Desconto em Percentual 
        /// D$ - Desconto em Valor
        /// 2 caracteres. Alfanumérico.</param>
        /// <param name="ValorDesc">Valor do Desconto ou Valor da porcentagem. 11 caracteres. Real.</param>
        public async Task iCFLancarDescontoItem_ECF_DarumaAsync(string NumItem, string TipoDesc, string ValorDesc)
        {
            int cod = 202;

            NumItem = DMF_UTIL_Valida(NumItem, 3, TipoDadoDaruma.Numerico);

            string AD = DMF_UTIL_ConsultaTipoAcrescimoDecrescimo(TipoDesc);
            if (AD.Equals("0") || AD.Equals("2"))
                TipoDesc = DMF_UTIL_Valida(TipoDesc, 11, TipoDadoDaruma.Porcentagem);
            else
                TipoDesc = DMF_UTIL_Valida(TipoDesc, 11, TipoDadoDaruma.Numerico);

            string param = NumItem+AD+ValorDesc;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método permite o cancelamento total de item em cupom fiscal. 
        /// </summary>
        /// <param name="NumItem">Número do Item. 3 caracteres. Numerico. </param>
        public async Task iCFCancelarItem_ECF_DarumaAsync(string NumItem)
        {
            int cod = 203;

            NumItem = DMF_UTIL_Valida(NumItem, 3, TipoDadoDaruma.Numerico);

            string param = NumItem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }
        
        /// <summary>
        /// Este método cancela parcialmente um item. 
        /// </summary>
        /// <param name="NumItem">Número do Item. 3 caracteres. Numerico. </param>
        /// <param name="Quantidade">Quantidade a cancelar. 7 caracteres. Real.</param>
        public async Task iCFCancelarItemParcial_ECF_DarumaAsync(string NumItem, string Quantidade)
        {
            int cod = 204;

            //000 referencia ultimo item
            NumItem = DMF_UTIL_Valida(NumItem, 3, TipoDadoDaruma.Numerico);

            Quantidade = DMF_UTIL_Valida(Quantidade, 7, TipoDadoDaruma.Quantidade);

            string param = NumItem + Quantidade;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método cancela o desconto aplicado sobre um item vendido no cupom fiscal atual.
        /// </summary>
        /// <param name="NumItem">Número do Item. 3 caracteres. Numerico. </param>
        public async Task iCFCancelarDescontoItem_ECF_DarumaAsync(string NumItem)
        {
            int cod = 205;

            NumItem = DMF_UTIL_Valida(NumItem, 3, TipoDadoDaruma.Numerico);

            string param = NumItem ;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }
        
        /// <summary>
        /// Este método totaliza o cupom fiscal. 
        /// </summary>
        /// <param name="TipoDescAcresc">Tipo Acréscimo ou Desconto -  Exemplo -  
        /// A% - Acréscimo em Percentual  
        /// A$ - Acréscimo em Valor 
        /// D% - Desconto em Percentual 
        /// D$ - Desconto em Valor
        /// 2 caracteres. Alfanumerico.</param>
        /// <param name="ValorDescAcresc">Valor do acréscimo ou Valor da porcentagem. 11 caracteres. Real.</param>
        public async Task iCFTotalizarCupom_ECF_DarumaAsync(string TipoDescAcresc, string ValorDescAcresc)
        {
            int cod = 206;

            string AD = DMF_UTIL_ConsultaTipoAcrescimoDecrescimo(TipoDescAcresc);
            if (AD.Equals("0") || AD.Equals("2"))
                ValorDescAcresc = DMF_UTIL_Valida(ValorDescAcresc, 12, TipoDadoDaruma.Porcentagem);
            else
                ValorDescAcresc = DMF_UTIL_Valida(ValorDescAcresc, 12, TipoDadoDaruma.Numerico);

            string param = AD + ValorDescAcresc;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

            iSubTotalCupomTotalizado = retorno.Substring(0, 12);
        }

        /// <summary>
        /// Este método cancela o desconto aplicado sobre o subtotal do cupom fiscal atual. 
        /// </summary>
        public async Task iCFCancelarDescontoSubtotal_ECF_DarumaAsync()
        {
            int cod = 208;
            
            string param = "0";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método cancela o acréscimo aplicado sobre o subtotal do cupom fiscal atual. 
        /// </summary>
        public async Task iCFCancelarAcrescimoSubtotal_ECF_DarumaAsync()
        {
            int cod = 208;

            string param = "1";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método processa o pagamento do cupom fiscal. 
        /// </summary>
        /// <param name="FormaPgto">Descrição da forma de pagamento. 20 caracteres. Alfanumerico. </param>
        /// <param name="Valor">Valor da forma de pagamento. 12 caracteres. Numerico. 0 (zero) indica restante.</param>
        /// <param name="InfoAdicional">Informação Adicional. 84 caracteres. Alfanumerico.  </param>
        public async Task iCFEfetuarPagamento_ECF_DarumaAsync(string FormaPgto, string Valor, string InfoAdicional)
        {
            int cod = 209;

            string ind = DMF_UTIL_ConsultaIndice_MeiosPagto(FormaPgto);

            Valor = DMF_UTIL_Valida(Valor, 12, TipoDadoDaruma.Numerico);
            InfoAdicional = DMF_UTIL_Valida(InfoAdicional, 84, TipoDadoDaruma.AlfanumericoVariavel);

            string param = ind + Valor + InfoAdicional;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

            //ESPECIAL, tem o + ou - no começo!
            iSubTotalCupomTotalizado = retorno.Substring(0, 13);
        }

        /// <summary>
        /// Este método finaliza o cupom fiscal, com a opção de emitir cupom adicional ou não com mensagem promocional. 
        /// </summary>
        /// <param name="CupomAdicional">
        /// 0 - Não Imprime Cupom Adicional 
        /// 1 - Imprime Cupom Adicional Simplificado 
        /// 2 - Imprime Cupom Adicional Detalhado 
        /// 3 - Imprime Cupom Adicional DLL 
        /// 1 caracter. Numerico.
        /// </param>
        /// <param name="Mensagem">Mensagem promocional em até 8 linhas. 384 caracteres. Alfanumerico.</param>
        public async Task iCFEncerrar_ECF_DarumaAsync(string CupomAdicional, string Mensagem)
        {
            int cod = 210;

            CupomAdicional = DMF_UTIL_Valida(CupomAdicional, 1, TipoDadoDaruma.Numerico);
            Mensagem = DMF_UTIL_Valida(Mensagem, 384, TipoDadoDaruma.AlfanumericoVariavel);

            string param = CupomAdicional + Mensagem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

            iCCOCupomEncerrado = retorno.Substring(0, 6);
            iTotalLiquidoCupomEncerrado = retorno.Substring(6, 12);
        }

        /// <summary>
        /// Este método permite cancelar o cupom fiscal. 
        /// </summary>
        public async Task iCFCancelar_ECF_DarumaAsync()
        {
            int cod = 211;
            
            string param = "";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }
        
        #endregion

        #region GRUPO CCD Comprovante Não Fiscal para TEF
        
        /// <summary>
        /// Este método abre um Comprovante de Crédito ou Débito(TEF). 
        /// </summary>
        /// <param name="FormaPgto">Descritivo da forma da forma de pagamento. 20 caracteres. Alfanumerico. </param>
        /// <param name="numeroParcelas">Número de parcelas. 2 caracteres. Numerico. </param>
        /// <param name="DocOrigem">COO do documento vinculado. 6 caracteres. Numerico. </param>
        /// <param name="Valor">Valor do pagamento. 12 caracteres. Numerico. </param>
        /// <param name="CPF">CPF ou CNPJ Consumidor. 20 caracteres. Alfanumerico. </param>
        /// <param name="Nome">Nome Consumidor. 30 caracteres. Alfanumerico. </param>
        /// <param name="Endereco">Endereço Consumidor. 79 caracteres. Alfanumerico.</param>
        public async Task iCCDAbrir_ECF_DarumaAsync(string FormaPgto, string numeroParcelas, string DocOrigem, string Valor, string CPF, string Nome, string Endereco)
        {
            int cod = 212;

            string ind = DMF_UTIL_ConsultaIndice_MeiosPagto(FormaPgto);

            numeroParcelas = DMF_UTIL_Valida(numeroParcelas, 2, TipoDadoDaruma.Numerico);
            DocOrigem = DMF_UTIL_Valida(DocOrigem, 6, TipoDadoDaruma.Numerico);
            Valor = DMF_UTIL_Valida(Valor, 12, TipoDadoDaruma.Numerico);

            CPF = DMF_UTIL_Valida(CPF, 20, TipoDadoDaruma.AlfanumericoVariavel);
            Nome = DMF_UTIL_Valida(Nome, 30, TipoDadoDaruma.AlfanumericoVariavel);
            Endereco = DMF_UTIL_Valida(Endereco, 79, TipoDadoDaruma.AlfanumericoVariavel);

            string param = ind +numeroParcelas+DocOrigem+ Valor + CPF+Nome+Endereco;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método imprime texto do Comprovante de Crédito ou Débito(TEF).
        /// </summary>
        /// <param name="Texto">Texto do Comprovante de Crédito ou Débito. 619 caracteres. Alfanumérico.</param>
        public async Task iCCDImprimirTexto_ECF_DarumaAsync(string Texto)
        {
            int cod = 213;

            Texto = DMF_UTIL_Valida(Texto, 619, TipoDadoDaruma.AlfanumericoVariavel);

            string param = Texto;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método finaliza o Comprovante Crédito e Débito. 
        /// </summary>
        public async Task iCCDFechar_ECF_DarumaAsync()
        {
            int cod = 214;

            string param = "";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Abertura de via adicional de Comprovante de Crédito ou Débito
        /// </summary>
        /// <param name="CPF">CPF ou CNPJ Consumidor. 20 caracteres. Alfanumerico. </param>
        /// <param name="Nome">Nome Consumidor. 30 caracteres. Alfanumerico. </param>
        /// <param name="Endereco">Endereço Consumidor. 79 caracteres. Alfanumerico.</param>
        public async Task iCCDAbrirViaAdicional_ECF_DarumaAsync(string CPF, string Nome, string Endereco)
        {
            int cod = 215;

            CPF = DMF_UTIL_Valida(CPF, 20, TipoDadoDaruma.AlfanumericoVariavel);
            Nome = DMF_UTIL_Valida(Nome, 30, TipoDadoDaruma.AlfanumericoVariavel);
            Endereco = DMF_UTIL_Valida(Endereco, 79, TipoDadoDaruma.AlfanumericoVariavel);

            string param = CPF + Nome + Endereco;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método imprime a 2ª via do Comprovante de Crédito e Débito. 
        /// </summary>
        public async Task iCCDSegundaVia_ECF_DarumaAsync()
        {
            int cod = 216;
        
            string param = "";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Reimpressão de Comprovante de Crédito ou Débito
        /// </summary>
        public async Task iCCDReimpressao_ECF_DarumaAsync()
        {
            int cod = 217;

            string param = "";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método estorna Comprovante de Crédito ou Débito(TEF). 
        /// </summary>
        /// <param name="COO">COO do documento vinculado. 6 caracteres. Numerico. </param>
        /// <param name="CPF">CPF ou CNPJ Consumidor. 20 caracteres. Alfanumerico. </param>
        /// <param name="Nome">Nome Consumidor. 30 caracteres. Alfanumerico. </param>
        /// <param name="Endereco">Endereço Consumidor. 79 caracteres. Alfanumerico.</param>
        public async Task iCCDEstornar_ECF_DarumaAsync(string COO, string CPF, string Nome, string Endereco)
        {
            int cod = 218;

            COO = DMF_UTIL_Valida(COO, 6, TipoDadoDaruma.Numerico);

            CPF = DMF_UTIL_Valida(CPF, 20, TipoDadoDaruma.AlfanumericoVariavel);
            Nome = DMF_UTIL_Valida(Nome, 30, TipoDadoDaruma.AlfanumericoVariavel);
            Endereco = DMF_UTIL_Valida(Endereco, 79, TipoDadoDaruma.AlfanumericoVariavel);

            string param = COO + CPF + Nome + Endereco;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        #endregion

        #region GRUPO CNF Comprovante Recebimento Não Fiscal
        
        /// <summary>
        /// Este método abre um Comprovante Não Fiscal identificando consumidor. 
        /// </summary>
        /// <param name="CPF">CPF ou CNPJ Consumidor. 20 caracteres. Alfanumerico. </param>
        /// <param name="Nome">Nome Consumidor. 30 caracteres. Alfanumerico. </param>
        /// <param name="Endereco">Endereço Consumidor. 79 caracteres. Alfanumerico.</param>
        public async Task iCNFAbrir_ECF_DarumaAsync(string CPF, string Nome, string Endereco)
        {
            int cod = 219;

            CPF = DMF_UTIL_Valida(CPF, 20, TipoDadoDaruma.AlfanumericoVariavel);
            Nome = DMF_UTIL_Valida(Nome, 30, TipoDadoDaruma.AlfanumericoVariavel);
            Endereco = DMF_UTIL_Valida(Endereco, 79, TipoDadoDaruma.AlfanumericoVariavel);

            string param = CPF + Nome + Endereco;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método registra no Comprovante um item não-fiscal. 
        /// </summary>
        /// <param name="Indice">Indice do Totalizador Não Fiscal. 2 caracteres. Numérico. </param>
        /// <param name="Valor">Valor da forma de pagamento. 12 caracteres. Numerico. 0 (zero) indica restante.</param>
        /// <param name="TipoDescAcresc">Tipo Acréscimo ou Desconto -  Exemplo -  
        /// D% - Desconto em Percentual 
        /// D$ - Desconto em Valor  
        /// A% - Acréscimo em Percentual  
        /// A$ - Acréscimo em Valor 
        /// 2 caracteres. Alfanumérico.</param>
        /// <param name="ValorDescAcresc">Valor do acréscimo ou Valor da porcentagem. 11 caracteres. Real. </param>
        public async Task iCNFReceber_ECF_DarumaAsync(string Indice, string Valor, string TipoDescAcresc, string ValorDescAcresc)
        {
            int cod = 220;

            Indice = DMF_UTIL_Valida(Indice, 2, TipoDadoDaruma.Numerico);
            Valor = DMF_UTIL_Valida(Valor, 11, TipoDadoDaruma.Numerico);


            string AD = DMF_UTIL_ConsultaTipoAcrescimoDecrescimo(TipoDescAcresc);
            if (AD.Equals("0") || AD.Equals("2"))
                ValorDescAcresc = DMF_UTIL_Valida(ValorDescAcresc, 11, TipoDadoDaruma.Porcentagem);
            else
                ValorDescAcresc = DMF_UTIL_Valida(ValorDescAcresc, 11, TipoDadoDaruma.Numerico);


            string param = Indice + Valor + AD + ValorDescAcresc;
           
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método permite o cancelamento total de item no Comprovante Não Fiscal. 
        /// </summary>
        /// <param name="NumItem">Número do Item. 3 caracteres. Numerico. </param>
        public async Task iCNFCancelarItem_ECF_DarumaAsync(string NumItem)
        {
            int cod = 221;

            NumItem = DMF_UTIL_Valida(NumItem, 3, TipoDadoDaruma.Numerico);

            string param = NumItem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método cancela o desconto aplicado sobre um item vendido no comprovante não fiscal atual.
        /// </summary>
        /// <param name="NumItem">Número do Item. 3 caracteres. Numerico. </param>
        public async Task iCNFCancelarDescontoItem_ECF_DarumaAsync(string NumItem)
        {
            int cod = 222;

            NumItem = DMF_UTIL_Valida(NumItem, 3, TipoDadoDaruma.Numerico);

            string param = NumItem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método cancela o acréscimo aplicado sobre um item vendido no comprovante não fiscal. 
        /// </summary>
        /// <param name="NumItem">Número do Item. 3 caracteres. Numerico. </param>
        public async Task iCNFCancelarAcrescimoItem_ECF_DarumaAsync(string NumItem)
        {
            int cod = 222;

            NumItem = DMF_UTIL_Valida(NumItem, 3, TipoDadoDaruma.Numerico);

            string param = NumItem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método totaliza o comprovante não fiscal. 
        /// </summary>
        /// <param name="TipoDescAcresc">Tipo Acréscimo ou Desconto -  Exemplo -  
        /// D% - Desconto em Percentual 
        /// D$ - Desconto em Valor  
        /// A% - Acréscimo em Percentual  
        /// A$ - Acréscimo em Valor 
        /// 2 caracteres. Alfanumérico.</param>
        /// <param name="ValorDescAcresc">Valor do acréscimo ou Valor da porcentagem. 11 caracteres. Real. </param>
        public async Task iCNFTotalizarComprovante_ECF_DarumaAsync(string TipoDescAcresc, string ValorDescAcresc)
        {
            int cod = 223;

            string AD = DMF_UTIL_ConsultaTipoAcrescimoDecrescimo(TipoDescAcresc);
            if (AD.Equals("0") || AD.Equals("2"))
                ValorDescAcresc = DMF_UTIL_Valida(ValorDescAcresc, 11, TipoDadoDaruma.Porcentagem);
            else
                ValorDescAcresc = DMF_UTIL_Valida(ValorDescAcresc, 11, TipoDadoDaruma.Numerico);

            string param = AD + ValorDescAcresc;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }
        
        /// <summary>
        /// Este método cancela o desconto aplicado sobre o subtotal do comprovante não fiscal atual. 
        /// </summary>
        public async Task iCNFCancelarDescontoSubtotal_ECF_DarumaAsync()
        {
            int cod = 224;

            string param = "0";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método cancela o acréscimo aplicado sobre o subtotal do comprovante não fiscal atual. 
        /// </summary>
        public async Task iCNFCancelarAcrescimoSubtotal_ECF_DarumaAsync()
        {
            int cod = 224;

            string param = "1";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método processa o pagamento do comprovante não fiscal. 
        /// </summary>
        /// <param name="FormaPgto">Descrição da forma de pagamento. 20 caracteres. Alfanumerico. </param>
        /// <param name="Valor">Valor da forma de pagamento. 12 caracteres. Numerico. 0 (zero) indica restante.</param>
        /// <param name="InfoAdicional">Informação Adicional. 84 caracteres. Alfanumerico.  </param>
        public async Task iCNFEfetuarPagamento_ECF_DarumaAsync(string FormaPgto, string Valor, string InfoAdicional)
        {
            int cod = 225;

            string ind = DMF_UTIL_ConsultaIndice_MeiosPagto(FormaPgto);
            
            Valor = DMF_UTIL_Valida(Valor, 12, TipoDadoDaruma.Numerico);
            InfoAdicional = DMF_UTIL_Valida(InfoAdicional, 84, TipoDadoDaruma.AlfanumericoVariavel);

            string param = ind + Valor + InfoAdicional;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }
        
        /// <summary>
        /// Este método finaliza o cupom fiscal, com a opção de emitir cupom adicional ou não com mensagem promocional. 
        /// </summary>
        /// <param name="Mensagem">Mensagem promocional em até 8 linhas. 619 caracteres. Alfanumerico.</param>
        public async Task iCNFEncerrar_ECF_DarumaAsync(string Mensagem)
        {
            int cod = 226;

            Mensagem = DMF_UTIL_Valida(Mensagem, 384, TipoDadoDaruma.AlfanumericoVariavel);

            string param = Mensagem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método usado para retirar uma quantia de dinheiro do Caixa, e informando uma mensagem com texto livre.        
        /// </summary>
        /// <param name="Valor">Valor da forma de pagamento. 11 caracteres. Numerico. 0 (zero) indica restante.</param>
        /// <param name="Mensagem">Texto livre.</param>
        public async Task iSangria_ECF_DarumaAsync(string Valor, string Mensagem)
        {
            int cod = 227;

            Valor = DMF_UTIL_Valida(Valor, 12, TipoDadoDaruma.Numerico);
            Mensagem = DMF_UTIL_Valida(Mensagem, 619, TipoDadoDaruma.AlfanumericoVariavel);

            string param = Valor + Mensagem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Estorno de Meio de Pagamento
        /// </summary>
        /// <param name="FormaPagamentoEstornado">Forma de pagamento estornada. 20 caracteres. Alfanumerico. </param>
        /// <param name="FormaPagamentoEfetivada">Forma de pagamento efetivada. 20 caracteres. Alfanumerico. </param>
        /// <param name="ValorFormaPagamento">Valor da forma de pagamento. 12 caracteres. Numerico.</param>        /// 
        /// <param name="Mensagem">Texto livre. 619 caracteres. Alfanumerico.</param>
        public async Task iEstornoFormaPagamento_ECF_DarumaAsync(string FormaPagamentoEstornado, string FormaPagamentoEfetivada, string ValorFormaPagamento, string Mensagem)
        {
            int cod = 228;

            string indEstorno = DMF_UTIL_ConsultaIndice_MeiosPagto(FormaPagamentoEstornado);
            string indEfetivo = DMF_UTIL_ConsultaIndice_MeiosPagto(FormaPagamentoEfetivada);

            ValorFormaPagamento = DMF_UTIL_Valida(ValorFormaPagamento, 12, TipoDadoDaruma.Numerico);
            Mensagem = DMF_UTIL_Valida(Mensagem, 84, TipoDadoDaruma.AlfanumericoVariavel);

            string param = indEstorno + indEfetivo + ValorFormaPagamento + Mensagem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método permite cancelar o comprovante não  fiscal.
        /// </summary>
        public async Task iCNFCancelar_ECF_DarumaAsync()
        {
            int cod = 229;

            string param = "";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        #endregion

        #region GRUPO Relatório Gerencial

        /// <summary>
        /// Este método abre um Relatório Gerencial. 
        /// </summary>
        /// <param name="NomeRG">Nome do Relatório Gerencial. 15 caracteres. Alfanumérico.</param>
        public async Task iRGAbrir_ECF_DarumaAsync(string NomeRG)
        {
            int cod = 230;

            string ind = DMF_UTIL_ConsultaIndice_RG(NomeRG);

            string param = ind;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método abre um Relatório Gerencial X. 
        /// </summary>
        public async Task iRGAbrirPadrao_ECF_DarumaAsync()
        {
            int cod = 230;

            string ind = "01";

            string param = ind;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método imprime texto do Relatório Gerencial. 
        /// </summary>
        /// <param name="Texto">Texto do Relatório Gerencial.
        /// O texto informado pode ser formatado, para isso você pode utilizar uma das tags abaixo.
        /// 
        ///     <b></b> - Para sinalizar Negrito;  
        ///     <s></s> - Para sinalizar Sublinhado;  
        ///     <e></e> - Para sinalizar Expandido;  
        ///     <c></c> - Para sinalizar Condensado;  
        /// Também é possível a impressão de código de barras através de tags, para isso você pode utlizar as tags descritas no tópico Trabalhando com codigo de barras no ECF
        /// 619 caracteres. Alfanumérico.
        /// </param>
        public async Task iRGImprimirTexto_ECF_DarumaAsync(string Texto)
        {
            int cod = 231;

            Texto = DMF_UTIL_Valida(Texto, 619, TipoDadoDaruma.AlfanumericoVariavel);

            string param = Texto;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método finaliza o Relatório Gerencial. 
        /// </summary>
        public async Task iRGFechar_ECF_DarumaAsync()
        {
            int cod = 232;

            string param = "";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método imprime uma Leitura da  Memória Fiscal, por intevalo de Data ou CRZ.
        /// </summary>
        /// <param name="Inicial">DDMMAA ou DDMMAA ou 00ZZZZ ou ZZZZ  (início do período). 6 caracteres. Numerico.</param>
        /// <param name="Final">DDMMAA ou DDMMAA ou 00ZZZZ ou ZZZZ  (final do período). 6 caracteres. Numerico.</param>
        public async Task iMFLer_ECF_DarumaAsync(string Inicial, string Final)
        {
            int cod = 233;

            Inicial = DMF_UTIL_Valida(Inicial, 6, TipoDadoDaruma.Numerico);
            Final = DMF_UTIL_Valida(Final, 6, TipoDadoDaruma.Numerico);

            string param = "3"+Inicial+Final;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método emite a Redução Z e encerra a jornada fiscal. Informando a Data e Hora no momento da Redução Z, pode-se ajustar o relógio da impressora em até 5 minutos (adiantando ou atrasando)
        /// </summary>
        /// <param name="Data">Ddmmaa (Data atual). 6 caracteres. Numerico.</param>
        /// <param name="Hora">HHmmss (Hora atual). 6 caracteres. Numerico.</param>
        public async Task iReducaoZ_ECF_DarumaAsync(string Data, string Hora)
        {
            int cod = 234;

            Data = DMF_UTIL_Valida(Data, 6, TipoDadoDaruma.Numerico);
            Hora = DMF_UTIL_Valida(Hora, 6, TipoDadoDaruma.Numerico);

            string param = Data + Hora;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método imprimi uma Leitura X. 
        /// </summary>
        public async Task iLeituraX_ECF_DarumaAsync()
        {
            int cod = 235;

            string param = "0";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método usado para dar entrada de uma quantia de dinheiro do Caixa, e informando uma mensagem com texto livre. 
        /// </summary>
        /// <param name="Valor">Valor do suprimento.</param>
        /// <param name="Mensagem">Texto livre.</param>
        public async Task iSuprimento_ECF_DarumaAsync(string Valor, string Mensagem)
        {
            int cod = 236;

            Valor = DMF_UTIL_Valida(Valor, 11, TipoDadoDaruma.Numerico);
            Mensagem = DMF_UTIL_Valida(Mensagem, 619, TipoDadoDaruma.AlfanumericoVariavel);

            string param = Valor+Mensagem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        #endregion

        #region GRUPO Bilhete Passagem

        /// <summary>
        /// Este método abre um Cupom Fiscal Bilhete de Passagem. 
        /// </summary>
        /// <param name="Origem">Origem: localidade do embarque. 34 caracteres. Alfanumerico.</param>
        /// <param name="Destino">Destino: localidade de chegada. 34 caracteres. Alfanumerico. </param>
        /// <param name="UFDestino">UF destino. 2 caracteres. Alfanumerico.</param>
        /// <param name="Percurso">Percurso: percurso ou trajeto. 39 caracteres. Alfanumerico.</param>
        /// <param name="Prestadora">Prestadora do transporte. 48 caracteres. Alfanumerico.</param>
        /// <param name="Plataforma">Plataforma.  3 caracteres. Alfanumerico.</param>
        /// <param name="Poltrona">Poltrona. 2 caracteres. Alfanumerico.</param>
        /// <param name="ModalidadeTransp">Modalidade de transporte 
        /// 1 = Rodoviário
        /// 2 = Ferroviário
        /// 3 = Hidroviário
        /// 1 caracter. Numerico.</param>
        /// <param name="CategoriaTransp">Categoria do transporte    
        /// 1 = interestadual
        /// 2 = intermunicipal
        /// 3 = internacional
        /// 1 caracter. Numerico.</param>
        /// <param name="DataEmbarque">Data de embraque: DDMMAAAA hhmmss. 15 caracteres. Alfanumerico.</param>
        /// <param name="RGPassageiro">RG do passageiro (opcional). 29 caracteres. Alfanumerico.</param>
        /// <param name="NomePassageiro">Nome do passageiro (opcional). 30 caracteres. Alfanumerico.</param>
        /// <param name="EnderecoPassageiro">Endereço do passageiro (opcional). 79 caracteres. Alfanumerico.</param>
        public async Task iCFBPAbrir_ECF_DarumaAsync(string Origem, string Destino, string UFDestino, string Percurso,
            string Prestadora, string Plataforma, string Poltrona,
            string ModalidadeTransp, string CategoriaTransp, string DataEmbarque,
            string RGPassageiro, string NomePassageiro, string EnderecoPassageiro)
        {
            int cod = 237;

            Origem = DMF_UTIL_Valida(Origem, 34, TipoDadoDaruma.Alfanumerico);
            Destino = DMF_UTIL_Valida(Destino, 34, TipoDadoDaruma.Alfanumerico);
            Percurso = DMF_UTIL_Valida(Percurso, 39, TipoDadoDaruma.Alfanumerico);
            Plataforma = DMF_UTIL_Valida(Plataforma, 4, TipoDadoDaruma.Alfanumerico);
            Poltrona = DMF_UTIL_Valida(Poltrona, 3, TipoDadoDaruma.Alfanumerico);
            UFDestino = DMF_UTIL_Valida(UFDestino, 2, TipoDadoDaruma.Alfanumerico);
            Prestadora = DMF_UTIL_Valida(Prestadora, 48, TipoDadoDaruma.Alfanumerico);

            RGPassageiro = DMF_UTIL_Valida(RGPassageiro, 29, TipoDadoDaruma.AlfanumericoVariavel);
            NomePassageiro = DMF_UTIL_Valida(NomePassageiro, 30, TipoDadoDaruma.AlfanumericoVariavel);
            EnderecoPassageiro = DMF_UTIL_Valida(EnderecoPassageiro, 79, TipoDadoDaruma.AlfanumericoVariavel);

            DataEmbarque = DMF_UTIL_Valida(DataEmbarque, 15, TipoDadoDaruma.Alfanumerico);

            DMF_UTIL_Valida(ModalidadeTransp, 1, TipoDadoDaruma.Numerico);
            DMF_UTIL_Valida(CategoriaTransp, 1, TipoDadoDaruma.Numerico);

            if( !(ModalidadeTransp.Equals("1") && ModalidadeTransp.Equals("2") && ModalidadeTransp.Equals("3") && 
                  CategoriaTransp.Equals("1") && CategoriaTransp.Equals("2") && CategoriaTransp.Equals("3")) )
            {
                DMF_UTIL_LancaException(-3);
            }

            string param = Origem + Destino+ UFDestino+ Percurso+ Prestadora+ Plataforma+ Poltrona+ ModalidadeTransp+CategoriaTransp+ DataEmbarque+ RGPassageiro+ NomePassageiro+ EnderecoPassageiro;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");
        }
        
        /// <summary>
        /// Este método vende um item no Cupom Fiscal Bilhete de passagem. 
        /// </summary>
        /// <param name="CargaTributaria">
        /// Alíquota do Item. A alíquota pode ser informada com a virgula (I07,00) ou sem a virgula (I0700).  
        /// Exemplos: 
        /// ICMS Não tributado: ("II" - Isento, "FF"- Substituição  tributária, "NN" - Não Tributária). 
        /// ICMS Tributado:( I07,00 , I18,00,  I0700 , I1800) 
        /// ISSQN Não tributado: ("ISS" - Isento, "FS"- Substituição tributária, "NS" - Não Tributária). 
        /// ISSQN Tributado: (S07,00 , S18,00, S0700 , S1800) 
        /// </param>
        /// <param name="PrecoUnitario">Preço Unitário do Item. </param>
        /// <param name="TipoDescAcresc">Tipo Acréscimo ou Desconto -  Exemplo -  
        /// A% - Acréscimo em Percentual  
        /// A$ - Acréscimo em Valor 
        /// D% - Desconto em Percentual 
        /// D$ - Desconto em Valor  </param>
        /// <param name="ValorDescAcresc">Valor do acréscimo ou Valor da porcentagem. </param>
        /// <param name="DescricaoItem">Descrição do Item.  </param>
        public async Task iCFBPVender_ECF_DarumaAsync(string CargaTributaria, string PrecoUnitario, string TipoDescAcresc, string ValorDescAcresc, string DescricaoItem)
        {
            int cod = 238;

            DescricaoItem = DMF_UTIL_FechaStringCom255(DescricaoItem);

            CargaTributaria = CargaTributaria.Replace(",", "");
            string ind = DMF_UTIL_ConsultaIndice_Aliquota(CargaTributaria);

            PrecoUnitario = DMF_UTIL_PreencheZerosEsquerda(PrecoUnitario, 8);

            string AD = DMF_UTIL_ConsultaTipoAcrescimoDecrescimo(TipoDescAcresc);

            ValorDescAcresc = DMF_UTIL_PreencheZerosEsquerda(ValorDescAcresc, 11);
            
            string param = ind + PrecoUnitario + AD + ValorDescAcresc + DescricaoItem;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método configura o UF Origem do próximo Cupom Fiscal Bilhete de Passagem. 
        /// </summary>
        /// <param name="UF">UF de Origem do próximo CFBP. 2 caracteres. Alfanumerico.</param>
        public async Task confCFBPProgramarUF_ECF_DarumaAsync( string UF)
        {
            int cod = 239;

            UF = DMF_UTIL_Valida(UF, 2, TipoDadoDaruma.Alfanumerico);

            string param = UF;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_F_Async(cod.ToString() + param), cod, "");

        }

        #endregion

        #region GRUPO Retorna Informação

        /// <summary>
        /// Este método processa o pagamento do cupom fiscal. 
        /// </summary>
        /// <param name="Indice">Código da informação que deseja obter da impressora.
        /// Para recuperar mais de uma informação é necessário utilizar o  "+", dividindo os códigos das informações desejadas.
        /// Exemplo: "78+1+140" </param>
        /// <param name="Separador">se for “0” não utilizar separador ou seja, retornar informação sem separar </param>
        public async Task<string> rRetornarInformacao_ECF_DarumaAsync(string Indice, string Separador)
        {
            int cod = 200;
            string result = "";

            DMF_UTIL_Valida(Separador, 1, TipoDadoDaruma.Alfanumerico);
            Indice = DMF_UTIL_Valida(Indice, 3, TipoDadoDaruma.Numerico);

            foreach (var i in Indice.Split('+'))
            {
                string param = i;
                string retorno = await darumaMobile.EnviarComando_FS_R_Async(cod.ToString() + param);

                retorno = DMF_UTIL_FormataResposta(retorno, cod, param);

                result += retorno + (Separador == "0" ? "" : Separador);
            }

            return result;
        }

        #endregion

        #region GRUPO Funções de Status e Erro

        /// <summary>
        /// Este método permite que você saiba o status do ecf realizando a leitura de 14 bytes. 
        /// </summary>
        public async Task<string> rStatusImpressora_ECF_DarumaAsync()
        {
            string param = "\x001D\x0006"; //[GS][ACK]

            string retorno = await darumaMobile.EnviarResponderComandoAsync( param );

            retorno = DMF_UTIL_LimpaCR( retorno );

            return retorno;
        }

        #region BitFields
        //b3 S1
        const char ModoFiscal = (char)8;
        //b3 S2
        const char MDDisponivel = (char)8;
        //b2 S2
        const char MFDEDisponivel = (char)4;
        //b0 S3
        const char DiaFiscalNaoAberto = (char)1;
        //b3 S4
        const char AindaNaoRZHoje = (char)8;
        //b2 S4
        const char NaoRZPendente = (char)4;
        //b1 S4
        const char NearEndNaoDetectado = (char)2;
        //b0 S4
        const char BobinaPapelPresente = (char)1;
        //b3 S5
        const char GavetaFechada = (char)8;
        //b1 S5
        const char ChequePresente = (char)2;
        //b1 S6
        const char ECFOnLine = (char)2;
        //b2 S7
        const char NenhumDocumentoAberto = (char)4;
        //b3 S8
        const char PapelCarregado = (char)8;
        //b2 S8
        const char DocPosicionado = (char)4;
        //b1 S8
        const char ChequePosicionado = (char)2;
        //b0 S8
        const char NaoChequeDocObstruindo = (char)1;
        //b0 S9
        const char TampaFechada = (char)1;
        //b2 S10
        const char TampaCabecaTermicaFechada = (char)4;
        #endregion

        /// <summary>
        /// Este método permite que você saiba o status do ecf realizando a leitura de 14 bytes. 
        /// </summary>
        public async Task<string> rStatusImpressoraBinario_ECF_DarumaAsync()
        {
            string S = await rStatusImpressora_ECF_DarumaAsync();

            //Formatado
            string retorno = "";

            //Extrai os bits de seu respectivo byte
            #region
            retorno += (ModoFiscal & S[1 - 1])                  == 0 ? "0" : "1";

            retorno += (MDDisponivel & S[2 - 1])                == 0 ? "0" : "1";
            retorno += (MFDEDisponivel & S[2 - 1])              == 0 ? "0" : "1";

            retorno += (DiaFiscalNaoAberto & S[3 - 1])          == 0 ? "0" : "1";

            retorno += (AindaNaoRZHoje & S[4 - 1])              == 0 ? "0" : "1";
            retorno += (NaoRZPendente & S[4 - 1])               == 0 ? "0" : "1";
            retorno += (NearEndNaoDetectado & S[4 - 1])         == 0 ? "0" : "1";
            retorno += (BobinaPapelPresente & S[4 - 1])         == 0 ? "0" : "1";

            retorno += (GavetaFechada & S[5 - 1])               == 0 ? "0" : "1";
            retorno += (ChequePresente & S[5 - 1])              == 0 ? "0" : "1";

            retorno += (ECFOnLine & S[6 - 1])                   == 0 ? "0" : "1";

            retorno += (NenhumDocumentoAberto & S[7 - 1])       == 0 ? "0" : "1";

            retorno += (PapelCarregado & S[8 - 1])              == 0 ? "0" : "1";
            retorno += (DocPosicionado & S[8 - 1])              == 0 ? "0" : "1";
            retorno += (ChequePosicionado & S[8 - 1])           == 0 ? "0" : "1";
            retorno += (NaoChequeDocObstruindo & S[8 - 1])      == 0 ? "0" : "1";

            retorno += (TampaFechada & S[9 - 1])                == 0 ? "0" : "1";

            retorno += (TampaCabecaTermicaFechada & S[10 - 1])  == 0 ? "0" : "1";
            #endregion

            return retorno;
        }

        /// <summary>
        /// Retorna em variável do tipo inteiro: 1 ou 0 indicando se o índice informado esta habilitado ou não. 
        /// </summary>
        /// <param name="Indice">Indice da informação desejada. Entre 1 e 18. Numerico.</param>
        public async Task<int> rConsultaStatusImpressoraInt_ECF_DarumaAsync(int Indice)
        {
            string retorno = await rStatusImpressoraBinario_ECF_DarumaAsync();

            if (Indice < 1 || Indice > 18) DMF_UTIL_LancaException(-3);

            int i = 0;
            if (!Int32.TryParse(retorno[Indice-1].ToString(), out i))
                DMF_UTIL_LancaException(-1);

            return i;
        }

        /// <summary>
        /// Este método verifica se a impressora está ligada ou conectada no computador.   
        /// </summary>
        public async Task<bool> rVerificarImpressoraLigada_ECF_DarumaAsync()
        {
            string param = "\x001D\x0007"; //[GS][BEL]

            string retorno = await darumaMobile.EnviarResponderComandoAsync(param);

            retorno = retorno.Replace(":", "").Replace("\r", "");

            return true;
        }

        /// <summary>
        /// Busca o aviso e o erro emitidos pelo último comando executado da DarumaMobileFramework, e os informa através das variáveis passadas por referencia. 
        /// </summary>
        /// <param name="strErro">Codigo do ultimo erro no formato string.</param>
        /// <param name="strAviso">Código do aviso no formato string</param>
        /// <param name="intErro">Código do erro no formato inteiro.</param>
        /// <param name="intAviso">Código do aviso no formato inteiro.</param>
        public void rStatusUltimoCmd_ECF_Daruma(out string strErro, out string strAviso, out int intErro, out int intAviso) 
        {
            strErro = codigoUltimoErro.ToString();
            strAviso = codigoUltimoAviso.ToString();
            intErro = codigoUltimoErro;
            intAviso = codigoUltimoAviso;
        }

        /// <summary>
        /// Este método retorna os códigos referentes ao erro e aviso do último comando enviado pela DarumaMobileFramework, no formato de variável inteiro. 
        /// </summary>
        /// <param name="intErro">variável para retornar o código do erro referente ao último comando enviado.</param>
        /// <param name="intAviso">variável para retornar o código do aviso referente ao último comando enviado.</param>
        public void rStatusUltimoCmdInt_ECF_Daruma(out int intErro, out int intAviso)
        {
            intErro = codigoUltimoErro;
            intAviso = codigoUltimoAviso;
        }

        /// <summary>
        /// Busca o aviso e o erro emitidos pelo ultimo comando executado da DarumaMobileFramework, e os informa através das variáveis passadas por referencia no formato string
        /// </summary>
        /// <param name="strErro">Codigo do ultimo erro no formato string.</param>
        /// <param name="strAviso">Código do aviso no formato string</param>
        public void rStatusUltimoCmdStr_ECF_Daruma(out string strErro, out string strAviso)
        {
            strErro = codigoUltimoErro.ToString();
            strAviso = codigoUltimoAviso.ToString();
        }

        /// <summary>
        /// Através das variáveis passadas por referência, este método retorna a descrição do erro e do aviso referentes ao ultimo comando executado da DarumaMobileFramework, dispensando o tratamento de retorno pela aplicação. 
        /// </summary>
        /// <param name="Erro">Mensagem referente ao aviso</param>
        /// <param name="Aviso">Mensagem referente ao erro</param>
        public void eRetornarAvisoErroUltimoCMD_ECF_Daruma(out string Erro, out string Aviso)
        {
            erros.TryGetValue(codigoUltimoErro, out Erro);
            erros.TryGetValue(codigoUltimoAviso, out Aviso);
        }

        /// <summary>
        /// Este método permite obter  as informações estendidas (como COO, CCF, Sub Total, etc.) da resposta do ultimo comando de impressão enviado, sem a necessidade de executar outros métodos, otimizando a aplicação e facilitando o desenvolvimento da mesma. 
        /// </summary>
        /// <param name="Indice">Qual a resposta estendida que você deseja, os valores podem ser de 1 a 5. No help de cada método você encontra quais as respostas estendidas podem ser retornadas e os índices dessas respostas.</param>
        public string rInfoEstendida_ECF_Daruma(int Indice)
        {
            string retorno = "";

            switch (Indice)
            {
                case 1:
                    retorno += COOCupomAberto + CCFCupomAberto;
                    break;
                case 2:
                    retorno += NItemVendido + TipoDescontoItemVendido + TotalLiquidoCupomEncerrado;
                    break;
                case 3:
                    retorno += SubTotalCupomTotalizado;
                    break;
                case 4:
                    retorno += SaldoOuTrocoPagamentoEfetuado;
                    break;
                case 5:
                    retorno += COOCupomEncerrado + TotalLiquidoCupomEncerrado;
                    break;
                default: 
                    DMF_UTIL_LancaException(-4);
                    break;
            }

            return retorno;
        }

        /// <summary>
        /// Retorna a primeira informação adicional enviada pela impressora a respeito do ultimo comando executado. Somente os métodos de impressão possuem informações estendidas, que podem ser totalizadas em 5. 
        /// </summary>
        public string rInfoEstendida1_ECF_Daruma()
        {
            return rInfoEstendida_ECF_Daruma(1);
        }

        /// <summary>
        /// Retorna a segunda informação adicional enviada pela impressora a respeito do ultimo comando executado. Somente os métodos de impressão possuem informações estendidas, que podem ser totalizadas em 5.
        /// </summary>
        public string rInfoEstendida2_ECF_Daruma()
        {
            return rInfoEstendida_ECF_Daruma(2);
        }

        /// <summary>
        /// Retorna a terceira informação adicional enviada pela impressora a respeito do ultimo comando executado. Somente os métodos de impressão possuem informações estendidas, que podem ser totalizadas em 5. 
        /// </summary>
        public string rInfoEstendida3_ECF_Daruma()
        {
            return rInfoEstendida_ECF_Daruma(3);
        }

        /// <summary>
        /// Retorna a quarta informação adicional enviada pela impressora a respeito do ultimo comando executado. Somente os métodos de impressão possuem informações estendidas, que podem ser totalizadas em 5. 
        /// </summary>
        public string rInfoEstendida4_ECF_Daruma()
        {
            return rInfoEstendida_ECF_Daruma(4);
        }

        /// <summary>
        /// Retorna a quinta informação adicional enviada pela impressora a respeito do ultimo comando executado. Somente os métodos de impressão possuem informações estendidas, que podem ser totalizadas em 5. 
        /// </summary>
        public string rInfoEstendida5_ECF_Daruma()
        {
            return rInfoEstendida_ECF_Daruma(5);
        }

        #endregion

        #region GRUPO De Configuração

        /// <summary>
        /// Este método permite a configuração de Alíquotas.
        /// </summary>
        /// <param name="Valor">sDescrição a ser cadastrada. 6 caracteres. Alfanumerico.</param>
        public async Task confCadastrarAliquota_ECF_DarumaAsync(string Valor)
        {
            int cod = 202;

            //determina o indice a ser utilizado
            string ind = (aliquotas.Count + 1).ToString().PadLeft(2, '0');
            string Tipo = "0";

            //Trata somente o S pois o T eh padrão
            if ( Valor.Contains("S") )
            {
                Tipo = "1";
            }
            //Remove a virgula se existir
            Valor = Valor.Replace("S", "").Replace("T", "");

            Valor = DMF_UTIL_Valida(Valor, 6, TipoDadoDaruma.Porcentagem);

            string param = ind + Tipo + Valor;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método permite a configuração de Meios de Pagamento.
        /// </summary>
        /// <param name="Valor">sDescrição a ser cadastrada. 15 caracteres. Alfanumerico.</param>
        public async Task confCadastrarMeiosPagto_ECF_DarumaAsync(string Valor)
        {
            int cod = 203;

            //determina o indice a ser utilizado
            string ind = (meiosPagto.Count + 1).ToString().PadLeft(2, '0');

            Valor = DMF_UTIL_Valida(Valor, 15, TipoDadoDaruma.Alfanumerico);

            string param = ind + Valor;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método permite a configuração de Totalizadores Não Fiscais.
        /// </summary>
        /// <param name="Valor">sDescrição a ser cadastrada. 15 caracteres. Alfanumerico.</param>
        public async Task confCadastrarCNF_ECF_DarumaAsync(string Valor)
        {
            int cod = 204;

            //determina o indice a ser utilizado
            string ind = (naoFiscais.Count + 1).ToString().PadLeft(2, '0');

            Valor = DMF_UTIL_Valida(Valor, 15, TipoDadoDaruma.Alfanumerico);

            string param = ind + Valor;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método permite a configuração de Relatórios Gerenciais.
        /// </summary>
        /// <param name="Valor">sDescrição a ser cadastrada. 15 caracteres. Alfanumerico.</param>
        public async Task confCadastrarRG_ECF_DarumaAsync(string Valor)
        {
            int cod = 205;

            //determina o indice a ser utilizado
            string ind = (relatoriosGerais.Count + 1).ToString().PadLeft(2, '0');

            Valor = DMF_UTIL_Valida(Valor, 15, TipoDadoDaruma.Alfanumerico);

            string param = ind + Valor;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");

        }

        /// <summary>
        /// Este método permite a configuração de Alíquotas, Totalizadores Não Fiscais, Relatórios Gerenciais e Meios de Pagamento.
        /// </summary>
        /// <param name="Cadastrar">Tipo: ALIQUOTA,TNF,RG,FPGTO</param>
        /// <param name="Valor">sDescrição a ser cadastrada</param>
        /// <param name="Separador">Separador utilizado para separar a descrição</param>
        public async Task confCadastrar_ECF_DarumaAsync(string Cadastrar, string Valor, string Separador)
        {
            DMF_UTIL_Valida(Separador, 1, TipoDadoDaruma.Alfanumerico);

            foreach (var i in Valor.Split(Separador[0]))
            {
                if (Cadastrar.Equals("ALIQUOTA"))
                {
                    await confCadastrarAliquota_ECF_DarumaAsync(i);
                }
                else if (Cadastrar.Equals("TNF"))
                {
                    await confCadastrarCNF_ECF_DarumaAsync(i);
                }
                else if (Cadastrar.Equals("RG"))
                {
                    await confCadastrarRG_ECF_DarumaAsync(i);
                }
                else if (Cadastrar.Equals("FPGTO"))
                {
                    await confCadastrarMeiosPagto_ECF_DarumaAsync(i);
                }
                else DMF_UTIL_LancaException(-3);
            }
        }
        
        /// <summary>
        /// Este método desabilita o horário de verão.
        /// </summary>
        public async Task confDesabilitarHorarioVerao_ECF_DarumaAsync()
        {
            int cod = 200;

            string param = "0";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método habilita o horário de verão.
        /// </summary>
        public async Task confHabilitarHorarioVerao_ECF_DarumaAsync()
        {
            int cod = 200;

            string param = "1";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método desabilita o desconto em ISS 
        /// </summary>
        public async Task confDesabilitarDescontoISS_ECF_DarumaAsync()
        {
            int cod = 201;

            string param = "0";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método habilita o desconto em ISS 
        /// </summary>
        public async Task confHabilitarDescontoISS_ECF_DarumaAsync()
        {
            int cod = 201;

            string param = "1";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método desabilita a descrição em 1 linha
        /// </summary>
        public async Task confDesabilitarDescricao1Linha_ECF_DarumaAsync()
        {
            int cod = 206;

            string param = "0";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método habilita a descrição em 1 linha suprimindo valor unitário
        /// </summary>
        public async Task confHabilitarDescricao1LinhaSemVU_ECF_DarumaAsync()
        {
            int cod = 206;

            string param = "1";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }
        
        /// <summary>
        /// Este método habilita a descrição em 1 linha não suprimindo valor unitário
        /// </summary>
        public async Task confHabilitarDescricao1LinhaComVU_ECF_DarumaAsync()
        {
            int cod = 206;

            string param = "2";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método desabilita legenda INMETRO para o próximo item 
        /// </summary>
        public async Task confDesabilitarLegendaINMETRO_ECF_DarumaAsync()
        {
            int cod = 215;

            string param = "0";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método habilita legenda INMETRO para o próximo item 
        /// </summary>
        public async Task confHabilitarLegendaINMETRO_ECF_DarumaAsync()
        {
            int cod = 215;

            string param = "1";
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método modifica o modo de redução de decimais utilizando Truncamento ou Arredondamento
        /// </summary>
        /// <param name="Tipo">Tipo do modo de redução:
        /// A - Arredondamento
        /// T - Truncamento
        /// 1 caracter. Alfanumerico.
        /// </param>
        public async Task confModoReducaoDecimais_ECF_DarumaAsync(string Tipo)
        {
            int cod = 223;

            if (Tipo[0] == 'A') Tipo = "0";
            else if (Tipo[0] == 'T') Tipo = "1";
            else DMF_UTIL_LancaException(-3);

            string param = Tipo.ToString();
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método modifica o configurações de acordo com parametros (Cadastrar)
        /// </summary>
        /// <param name="Cadastrar">Tipo de cadastro: “HV” “DESCISS” “REGITEM” “INMETRO” “AT”</param>
        /// <param name="Valor">Valor para a configuração do parametro. 0, 1 ou 2 dependendo do cadastro. 1 caracter. Numerico.</param>
        public async Task confParametrizar_ECF_DarumaAsync(string Cadastrar, string Valor)
        {

            if (Cadastrar.Equals("HV"))
            {
                if (Valor.Equals("0")) await confDesabilitarHorarioVerao_ECF_DarumaAsync();
                else if (Valor.Equals("1")) await confHabilitarHorarioVerao_ECF_DarumaAsync();
                else DMF_UTIL_LancaException(-3);
            }
            else if (Cadastrar.Equals("DESCISS"))
            {
                if (Valor.Equals("0")) await confDesabilitarDescontoISS_ECF_DarumaAsync();
                else if (Valor.Equals("1")) await confHabilitarDescontoISS_ECF_DarumaAsync();
                else DMF_UTIL_LancaException(-3);
            }
            else if (Cadastrar.Equals("REGITEM"))
            {
                if (Valor.Equals("0")) await confDesabilitarDescricao1Linha_ECF_DarumaAsync();
                else if (Valor.Equals("1")) await confHabilitarDescricao1LinhaSemVU_ECF_DarumaAsync();
                else if (Valor.Equals("2")) await confHabilitarDescricao1LinhaComVU_ECF_DarumaAsync();
                else DMF_UTIL_LancaException(-3);
            }
            else if (Cadastrar.Equals("INMETRO"))
            {
                if (Valor.Equals("0")) await confDesabilitarLegendaINMETRO_ECF_DarumaAsync();
                else if (Valor.Equals("1")) await confHabilitarLegendaINMETRO_ECF_DarumaAsync();
                else DMF_UTIL_LancaException(-3);
            }
            else if (Cadastrar.Equals("AT"))
            {
                if (Valor.Equals("0")) await confModoReducaoDecimais_ECF_DarumaAsync("A");
                else if (Valor.Equals("1")) await confModoReducaoDecimais_ECF_DarumaAsync("T");
                else DMF_UTIL_LancaException(-3);
            }
            else DMF_UTIL_LancaException(-3);
        }

        /// <summary>
        /// Este método configura o número da Loja no ECF.
        /// </summary>
        /// <param name="Valor">Número da Loja. 4 caracteres. Alfanumérico.</param>
        public async Task confProgramarIDLoja_ECF_DarumaAsync(string Valor)
        {
            int cod = 208;

            Valor = DMF_UTIL_Valida(Valor, 4, TipoDadoDaruma.Alfanumerico);

            string param = Valor;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método programa o Operador no ECF, essa programação pode ser feita a qualquer momento do dia.
        /// </summary>
        /// <param name="Valor">Identificação do operador.</param>
        public async Task confProgramarIDOperador_ECF_DarumaAsync(string Valor)
        {
            int cod = 209;

            Valor = DMF_UTIL_Valida(Valor, 20, TipoDadoDaruma.Alfanumerico);

            string param = Valor;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método programa o Identificador de Aplicativo
        /// </summary>
        /// <param name="Valor">Identificação do programa aplicativo em 2 linhas de 42 caracteres cada. 84 caracteres. Alfanumérico.</param>
        public async Task confProgramarIDAplicativo_ECF_DarumaAsync(string Valor)
        {
            int cod = 214;

            Valor = DMF_UTIL_Valida(Valor, 84,TipoDadoDaruma.Alfanumerico);

            string param = Valor;
            string retorno = DMF_UTIL_FormataResposta(await darumaMobile.EnviarComando_FS_C_Async(cod.ToString() + param), cod, "");
        }

        /// <summary>
        /// Este método programa o Identificador de Aplicativo
        /// </summary>
        /// <param name="Cadastrar">tipo de cadastro podendo aceitar:“LOJA” “OPERADOR” “APLICATIVO”</param>
        /// <param name="Valor">Identificador a ser utilizado. Varia de acordo com o método utilizado.</param>
        public async Task confIdentificadores_ECF_DarumaAsync(string Cadastrar, string Valor)
        {

            if (Cadastrar.Equals("LOJA"))
            {
                await confProgramarIDLoja_ECF_DarumaAsync(Valor);
            }
            else if (Cadastrar.Equals("OPERADOR"))
            {
                await confProgramarIDOperador_ECF_DarumaAsync(Valor);
            }
            else if (Cadastrar.Equals("APLICATIVO"))
            {
                await confProgramarIDAplicativo_ECF_DarumaAsync(Valor);
            }
            else DMF_UTIL_LancaException(-3);
        }

        #endregion

        #region GRUPO MINI-IMPRESSORA

        /// <summary>
        /// Este método permite verificar os status da impressora no determinado momento, de acordo com o índice solicitado.
        /// </summary>
        /// <param name="Indice">Variável string informando índice que pode variar de 1 até 8 (veja tabela abaixo).
        /// Índice 	Retorno 0 	                Retorno 1
        /// 1 	    Impressão encerrada	        Impressão em andamento
        /// 2 	    Impressora operacional 	    Impressora em falha
        /// 3 	    Off Line 	                On Line
        /// 4 	    Papel OK 	                Fim de papel
        /// 5 	    Guilhotina não detectada 	Guilhotina detectada
        /// 6 	    Tampa térmica fechada 	    Tampa térmica aberta
        /// 7 	    Sem papel sobre o sensor 	Papel posicionado sobre o sensor
        /// 8 	    Gaveta fechada 	            Gaveta aberta
        /// </param>
        /// <param name="TipoRetorno">Variável string informando "0" para retorno do status em número e "1" para retorno do status em texto.</param>
        public async Task<string> rConsultaStatusImpressora_DUAL_DarumaAsync(string Indice, string TipoRetorno)
        {
            string GSENQ = "\x001D\x0005";  //[GS][ENQ]
            string ENQ = "\x0005";          //[ENQ]



            string retorno = await darumaMobile.EnviarResponderComandoAsync(ENQ);

            return retorno;

        }

        /// <summary>
        /// Método que possibilita envio de texto a serem impressos, permitindo formatação do mesmo, através das tags listadas mais abaixo, que chamamos de D-HTML, por ser semelhante à programação HTML.
        /// Com o uso deste método é possível enviar impressões linha a linha ou em blocos de linhas (buffer) de acordo com sua necessidade.
        /// Antes de utiliza-lo pela primeira vez na aplicação é importante que tenham sido configurados os parâmetros de comunicação da impressora, como Porta de Comunicação e Velocidade, para que os dados sejam enviados a impressora corretamente.
        /// </summary>
        /// <param name="String">Variável string com o texto e a(s) tag(s) que serão usadas em até 2000 caracteres.
        /// </param>
        /// <param name="Tam">Tamanho do texto que será impresso. Se o valor "0"(zero) for informado, a DarumaFramework calcula o tamanho automaticamente pra você.</param>
        public async Task iImprimirTexto_DUAL_DarumaAsync(string String, int Tam = 0)
        {
            D_HTMLParser parser = new D_HTMLParser();
            await darumaMobile.EnviarComandoAsync(parser.Parse(String));
        }

        #endregion

        #endregion


        #region Funções Publicas Auxiliares

        /// <summary>
        /// Retorna uma string espaços no lugar do caracter 255.
        /// </summary>
        /// <param name="retorno">String a ser utilizada como base.</param>
        private string DMF_UTIL_Limpa255(string retorno)
        {
            return retorno.Replace(((char)255).ToString(), " ");
        }

        /// <summary>
        /// Retorna uma string com zeros (0) preenchidos à esquerda.
        /// </summary>
        /// <param name="str">String a ser utilizada como base.</param>
        /// <param name="tam">Tamanho total que a string deverá ter depois de preenchida.</param>
        public string DMF_UTIL_PreencheZerosEsquerda(string str, int tam) 
        {
            return str.PadLeft(tam, '0');
        }

        /// <summary>
        /// Retorna uma string com zeros (0) preenchidos à direita.
        /// </summary>
        /// <param name="str">String a ser utilizada como base.</param>
        /// <param name="tam">Tamanho total que a string deverá ter depois de preenchida.</param>
        public string DMF_UTIL_PreencheZerosDireita(string str, int tam)
        {
            return str.PadRight(tam, '0');            
        }

        /// <summary>
        /// Retorna uma string com espaços ( ) preenchidos à esquerda.
        /// </summary>
        /// <param name="str">String a ser utilizada como base.</param>
        /// <param name="tam">Tamanho total que a string deverá ter depois de preenchida.</param>
        public string DMF_UTIL_PreencheEspacosEsquerda(string str, int tam)
        {
            return str.PadLeft(tam, ' ');
        }

        /// <summary>
        /// Retorna uma string com espaços ( ) preenchidos à direita.
        /// </summary>
        /// <param name="str">String a ser utilizada como base.</param>
        /// <param name="tam">Tamanho total que a string deverá ter depois de preenchida.</param>
        public string DMF_UTIL_PreencheEspacosDireita(string str, int tam)
        {
            return str.PadRight(tam, ' ');
        }

        /// <summary>
        /// Retorna uma string com o caracter 255 no final.
        /// </summary>
        /// <param name="str">String a ser utilizada como base.</param>
        public string DMF_UTIL_FechaStringCom255(string str)
        {
            return str + (char)255;
        }
        
        /// <summary>
        /// Valida string baseada na quantidade de caracteres e no tipo.
        /// </summary>
        /// <param name="str">String a ser utilizada como base.</param>
        /// <param name="tam">Tamanho total que a string deverá ter.</param>
        /// <param name="num">Indica se deverá ser alfanumerica, numerica comum, valor unitario, quantidade ou porcentagem</param>
        public string DMF_UTIL_Valida(string str, int tam, TipoDadoDaruma t)
        {
            if (str.Length > tam) DMF_UTIL_LancaException(-3);
            long a = 0;
            double b = 0f;

            if(t == TipoDadoDaruma.Numerico && !Int64.TryParse(str, out a))
                DMF_UTIL_LancaException(-4);
            if( ( t == TipoDadoDaruma.Porcentagem || t == TipoDadoDaruma.ValorUn || t == TipoDadoDaruma.Quantidade ) && !Double.TryParse(str, out b))
                DMF_UTIL_LancaException(-4);

            int decimaisAtuais = 0;
            int decimaisAdicionais = 0;

            string result = "";

            switch (t)
            {
                case TipoDadoDaruma.Alfanumerico:
                    result = DMF_UTIL_PreencheEspacosDireita(str,tam);
                    break;
                case TipoDadoDaruma.AlfanumericoVariavel:
                    result = DMF_UTIL_FechaStringCom255(str);
                    break;
                case TipoDadoDaruma.Numerico:
                    result = DMF_UTIL_PreencheZerosEsquerda(str, tam);
                    break;
                case TipoDadoDaruma.ValorUn:
                    if (str.IndexOf(",") != -1)
                    {
                        decimaisAtuais = str.Length - (str.IndexOf(",") + 1);
                        decimaisAdicionais = decimaisValorUn - decimaisAtuais;

                        //não pode ter mais que (decimaisQuantidade) casas antes da virgula
                        if (decimaisAdicionais < 0) DMF_UTIL_LancaException(-3);

                        //regulariza a quantidade de decimais
                        result = DMF_UTIL_PreencheZerosDireita(str, str.Length + decimaisAdicionais);
                        //Remove a virgula
                        result.Replace(",", "");
                    }
                    else
                    {
                        //regulariza a quantidade de decimais
                        if (decimaisValorUn > 2)
                            result = DMF_UTIL_PreencheZerosDireita(str, str.Length + decimaisValorUn - 2);
                        else
                            result = str;
                    }

                    if (str.Length > tam)
                        result = result.Substring(decimaisValorUn);

                    //regulariza o resto da string
                    result = DMF_UTIL_PreencheZerosEsquerda(result, tam);
                    break;
                case TipoDadoDaruma.Quantidade:
                    if (str.IndexOf(",") != -1)
                    {
                        decimaisAtuais = str.Length - (str.IndexOf(",") + 1);
                        decimaisAdicionais = decimaisQuantidade - decimaisAtuais;

                        //não pode ter mais que (decimaisQuantidade) casas antes da virgula
                        if (decimaisAdicionais < 0) DMF_UTIL_LancaException(-3);

                        //regulariza a quantidade de decimais
                        result = DMF_UTIL_PreencheZerosDireita(str, str.Length + decimaisAdicionais);
                        //Remove a virgula
                        result.Replace(",", "");
                    }
                    else
                    {
                        //regulariza a quantidade de decimais
                        result = DMF_UTIL_PreencheZerosDireita(str, str.Length + decimaisQuantidade);
                    }

                    if (str.Length > tam)
                        result = result.Substring(decimaisQuantidade);

                    //regulariza o resto da string
                    result = DMF_UTIL_PreencheZerosEsquerda(result, tam);
                    break;
                case TipoDadoDaruma.Porcentagem:

                    if (str.IndexOf(",") != -1)
                    {
                        decimaisAtuais = str.Length - (str.IndexOf(",") + 1);
                        decimaisAdicionais = 2 - decimaisAtuais;

                        //não pode ter mais que duas casas antes da virgula
                        if (decimaisAdicionais < 0 ) DMF_UTIL_LancaException(-3);

                        result = str.Substring(0, str.IndexOf(",") + decimaisAtuais);

                        //regulariza a quantidade de decimais
                        result = DMF_UTIL_PreencheZerosDireita(result, result.Length + decimaisAdicionais);
                        //regulariza a quantidade de inteiros
                        result = DMF_UTIL_PreencheZerosEsquerda(result, 5);
                        //Remove a virgula
                        result.Replace(",", "");
                    }
                    else
                    {
                        if(str.Length >  4)
                            result = str.Substring(0, 4);
                        else
                            //regulariza a quantidade de inteiros
                            result = DMF_UTIL_PreencheZerosEsquerda(str, 4);
                    }

                    //regulariza o resto da string
                    result = DMF_UTIL_PreencheZerosDireita(result, tam);
                    break;
            }
            
            return result;

        }
                
        /// <summary>
        /// Lança exceptions para ser tratado pelo usuário na Thread de UI
        /// </summary>
        /// <param name="cod">Código do Erro a ser lançado.</param>
        public void DMF_UTIL_LancaException(int cod)
        {
            Exception ex;
            string errorMessage = "";

            if (erros.TryGetValue(cod, out errorMessage))
            {
                ex = new Exception(errorMessage);
                ex.Data.Add("Code", cod);
            }
            else
            {
                ex = new Exception("Erro não identificado.");
                ex.Data.Add("Code", -1);
            }

            throw ex;
        }
        
        /// <summary>
        /// função auxiliar para consultar tipos de acrescimos ou decrescimos pois C# não suporta $ ou % em um enum.
        /// </summary>
        /// <param name="str">String contendo A$, A%, D$ ou D%</param>
        public string DMF_UTIL_ConsultaTipoAcrescimoDecrescimo(string str)
        {
            int ind = tiposAcrescimosDecrescimos.IndexOf(str);

            if (ind == -1)
            {
                //Erro de Aliquota não encontrada
                DMF_UTIL_LancaException(-2);
            }

            return ind.ToString();
        }

        /// <summary>
        /// Retorna o indice (int) de uma alíquota para consulta posterior.
        /// </summary>
        /// <param name="str">Descrição da Aliquota, por Ex. 010800 .</param>
        public string  DMF_UTIL_ConsultaIndice_Aliquota(string str)
        {
            int ind = aliquotas.IndexOf(str);

            if (ind == -1)
            {
                ind = aliquotasAux.IndexOf(str);

                if (ind == -1)
                {
                    //Erro de Aliquota não encontrada
                    DMF_UTIL_LancaException(-2);
                }

                ind += 16;
            }

            return (ind +1).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Retorna o indice (string) de um Meio de Pagamento para consulta posterior.
        /// </summary>
        /// <param name="str">Descrição do Meio de Pagamento, por Ex. Dinheiro.</param>
        public string DMF_UTIL_ConsultaIndice_MeiosPagto(string str)
        {
            int ind = meiosPagto.IndexOf(str);

            if (ind == -1)
            {
                //Erro de Aliquota não encontrada
                DMF_UTIL_LancaException(-2);
            }

            return (ind+1).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Retorna o indice (string) de um totalizador não fiscal para consulta posterior.
        /// </summary>
        /// <param name="str">Descrição do totalizador, por Ex. Dinheiro.</param>
        public string DMF_UTIL_ConsultaIndice_CNF(string str)
        {
            int ind = naoFiscais.IndexOf(str);

            if (ind == -1)
            {
                //Erro de Aliquota não encontrada
                DMF_UTIL_LancaException(-2);
            }

            return (ind + 1).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Retorna o indice (string) de um relatorio geral para consulta posterior.
        /// </summary>
        /// <param name="str">Descrição do totalizador, por Ex. Dinheiro.</param>
        public string DMF_UTIL_ConsultaIndice_RG(string str)
        {
            int ind = relatoriosGerais.IndexOf(str);

            if (ind == -1)
            {
                //Erro de Aliquota não encontrada
                DMF_UTIL_LancaException(-2);
            }

            return (ind + 1).ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// Carrega as tabelas de TOtalizadores Fiscais, Não Fiscais e Relatorios Gerais.
        /// </summary>
        public async Task<AsyncVoidMethodBuilder> DMF_UTIL_PreencheTabelas()
        {
            await rLerAliquotas_ECF_DarumaAsync();

            await rLerMeiosPagto_ECF_DarumaAsync();

            await rLerCNF_ECF_DarumaAsync();

            await rLerRG_ECF_DarumaAsync();

            return AsyncVoidMethodBuilder.Create();
        }

        /// <summary>
        /// Retira [:], confimarção do comando/parametros e [CR] da string de retorno e lança exceções se for identificado algum erro!
        /// </summary>
        /// <param name="resp">Resposta do comando</param>
        /// <param name="cod">Codigo do comando a ser executado</param>
        /// <param name="param">PArametros utilizados pelo comando.</param>
        public string DMF_UTIL_FormataResposta(string resp, int cod, string param)
        {
            resp = DMF_UTIL_LimpaCR(resp);

            //retira a confirmação
            int ind = resp.IndexOf(char.ConvertFromUtf32(cod));

            string message = resp.Substring(0, ind);

            //se possuir uma mensagem de erro e aviso
            if(message.Length>0)
            {
                //Extrai o erro no formato novo
                string strCodErr = message.Substring(2, 3);
                //extrai aviso
                string strCodAvi = message.Substring(5, 2);

                int codErr = int.Parse( strCodErr );
                int codAvi = int.Parse( strCodAvi );

                //Lança Exceção
                if (codErr != 0)
                {
                    DMF_UTIL_LancaException(codErr);
                }

                //Armazena o ultimo erro e o ultimo aviso
                codigoUltimoAviso = codAvi;
                codigoUltimoErro = codErr;
            }
            //monta a mensagem de confirmação
            string confirmacao = message + char.ConvertFromUtf32(cod) + param;
            string retorno = resp.Substring(confirmacao.Length);
            
            return retorno;
        }

        /// <summary>
        /// Retira [:] e [CR] da string de retorno!
        /// </summary>
        /// <param name="resp">Resposta do comando</param>
        public string DMF_UTIL_LimpaCR(string resp)
        {
            //remove o char : e o no final [CR]
            string retorno = resp.Substring(1);
            retorno = retorno.Remove(retorno.Length - 1);

            return retorno;
        }


        #endregion


    }
}
