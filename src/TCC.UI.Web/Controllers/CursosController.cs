using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Interfaces;
using TCC.Domain.Models;

namespace TCC.UI.Web.Controllers
{
    public class CursosController : BaseController
    {
        private readonly ICursoAppService _cursoAppService;
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IProgressoAppService _progressoAppService;

        public CursosController(
            ICursoAppService cursoAppService,
            IUsuarioAppService usuarioAppService,
            IProgressoAppService progressoAppService
            )
        {
            _cursoAppService = cursoAppService;
            _usuarioAppService = usuarioAppService;
            _progressoAppService = progressoAppService;
        }

        [AllowAnonymous]
        [HttpGet("Cursos")]
        public async Task<IActionResult> Index()
        {
            return View(await _cursoAppService.GetAll());
        }

        [Authorize]
        [HttpGet("Cursos/{id:guid}")]               
        public async Task<IActionResult> Detalhes(Guid? id)
        {
            var cursoViewModel = await _cursoAppService.GetById(id.Value);

            if (cursoViewModel is null)
            {
                return NotFound();
            }

            return View(cursoViewModel);
        }

        [HttpGet("Cursos/IniciarCurso/{cursoId:guid}")]
        public async Task<IActionResult> IniciarCurso(Guid? cursoId)
        {
            var user = await _usuarioAppService.GetCurrentUser();
            var curso = await _cursoAppService.GetById(cursoId.Value);

            var progressos = await _progressoAppService.GetByCursoIdAndUserId(cursoId.Value, user.Id);

            if (progressos is null || !progressos.Any())
            {
                var aulaId = curso.Aulas.OrderBy(a => a.Number).First().Id;

                var newProgresso = new ProgressoAula
                {
                    UsuarioId = user.Id,
                    Status = Domain.Enums.StatusProgresso.EmAndamento,
                    AulaId = aulaId,
                    CursoId = curso.Id
                };

                await _progressoAppService.Add(newProgresso);
                return RedirectToAction("Detalhes", "Aulas", new { id = aulaId });
            }

            var primeiraAulaNaoConcluida = progressos
                .OrderBy(p => p.Aula.Number)
                .Last(a => a.Status == Domain.Enums.StatusProgresso.EmAndamento)
                .Aula;

            return RedirectToAction("Detalhes", "Aulas", new { id = primeiraAulaNaoConcluida.Id });
        }

        [HttpGet("Cursos/load")]
        public IActionResult Load()
        {
            //if (!Debugger.IsAttached)
            //{
            //    return NotFound();
            //}

            var cursos = new List<Curso>
            {
                GetVariavelCurso(),
                GetVetoresCurso(),
                GetTiposDeDadosCurso(),
                GetMatrizesCurso(),
                GetLacosCurso(),
                GetEstruturaDeDadosCurso()
            };

            foreach (var c in cursos)
            {
                _cursoAppService.Add(c);
                Thread.Sleep(100);
            }

            return Ok($"{cursos.Count} cursos adicionados!");
        }

        #region CargaDeDados
        private Curso GetEstruturaDeDadosCurso()
        {
            var curso = new Curso(
                Guid.NewGuid(),
                "Estrutura de Dados",
                "Aprenda os fundamentos essenciais para utilizar diferentes estruturas de dados.",
                Domain.Enums.Nivel.Intermediario,
                iconUrl: "Estrutura_de_Dados.png"
                )
            {
                Aulas = new List<Aula>()
                {
                    new Aula()
                    {
                        Number = 1,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 1",
                        ContentUrl = "EstruturaDeDados/EstruturaDeDados1.pdf",
                        Nome = "Aula 1: O que são Estruturas de Dados?",
                        QtdMoedas = 10,
                        Xp = 1000,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Por que as estruturas de dados são importantes na programação?",
                                AlternativaA = "Para tornar os programas mais complicados.",
                                AlternativaB = "Para organizar informações de forma eficiente.",
                                AlternativaC = "Para criar jogos apenas.",
                                AlternativaD = "Para colorir o código.",
                                Resposta = "Para organizar informações de forma eficiente.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual das seguintes opções é um exemplo de estrutura de dados em programação?",
                                AlternativaA = "Um bloco de construção.",
                                AlternativaB = "Uma caixa de brinquedos.",
                                AlternativaC = "Uma lista de nomes de amigos.",
                                AlternativaD = "Uma bola de futebol.",
                                Resposta = "Uma lista de nomes de amigos.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 2,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 2",
                        ContentUrl = "EstruturaDeDados/EstruturaDeDados2.pdf",
                        Nome = "Aula 2: Arrays e Listas",
                        QtdMoedas = 10,
                        Xp = 1100,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "O que é uma característica das listas ligadas duplas em comparação com as listas ligadas simples?",
                                AlternativaA = "As listas ligadas duplas têm acesso mais rápido aos elementos.",
                                AlternativaB = "As listas ligadas duplas permitem percorrer a lista em apenas uma direção.",
                                AlternativaC = "As listas ligadas duplas não possuem referências aos elementos.",
                                AlternativaD = "As listas ligadas duplas ocupam menos espaço na memória.",
                                Resposta = "As listas ligadas duplas permitem percorrer a lista em apenas uma direção.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal característica de um array multidimensional?",
                                AlternativaA = "Pode conter elementos de tipos diferentes.",
                                AlternativaB = "Possui uma única dimensão para organizar os elementos.",
                                AlternativaC = "Permite organizar dados em múltiplas dimensões.",
                                AlternativaD = "Não é utilizado em programação.",
                                Resposta = "Permite organizar dados em múltiplas dimensões.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 3,
                         Id = Guid.NewGuid(),
                        Descricao = "Aula 3",
                        ContentUrl = "EstruturaDeDados/EstruturaDeDados3.pdf",
                        Nome = "Aula 3: Pilhas e Filas",
                        QtdMoedas = 10,
                        Xp = 1200,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Em uma pilha, qual operação permite adicionar um elemento no topo da pilha?",
                                AlternativaA = "Pop",
                                AlternativaB = "Push",
                                AlternativaC = "Enqueue",
                                AlternativaD = "Dequeue",
                                Resposta = "Push",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal diferença entre uma pilha e uma fila?",
                                AlternativaA = "Em uma fila, o último elemento adicionado é o primeiro a ser removido.",
                                AlternativaB = "Em uma pilha, o primeiro elemento adicionado é o primeiro a ser removido.",
                                AlternativaC = "Pilhas e filas são estruturas idênticas, não há diferença entre elas.",
                                AlternativaD = "Em uma fila, o primeiro elemento adicionado é o último a ser removido.",
                                Resposta = "Em uma fila, o primeiro elemento adicionado é o último a ser removido.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    }
                },
                Duracao = 200,
                QtdMoeda = 30,
                Xp = 5000
            };

            return curso;
        }

        private Curso GetLacosCurso()
        {
            var curso = new Curso(
                Guid.NewGuid(),
                "Laços de Repetição",
                "Aprenda os fundamentos essenciais para utilizar laços de repetição.",
                Domain.Enums.Nivel.Intermediario,
                iconUrl: "Laco_de_Repeticao.png"
                )
            {
                Aulas = new List<Aula>()
                {
                    new Aula()
                    {
                        Number = 1,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 1",
                        ContentUrl = "LacosDeRepeticao/LacosDeRepeticao1.pdf",
                        Nome = "Aula 1: Introdução aos Laços de Repetição",
                        QtdMoedas = 10,
                        Xp = 1000,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "O que são laços de repetição em programação?",
                                AlternativaA = "Uma estrutura que permite executar um conjunto de instruções uma única vez.",
                                AlternativaB = "Uma estrutura que permite executar um conjunto de instruções várias vezes.",
                                AlternativaC = "Uma estrutura que permite pular instruções em um programa.",
                                AlternativaD = "Uma estrutura usada apenas em Python.",
                                Resposta = "Uma estrutura que permite executar um conjunto de instruções várias vezes.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é o principal objetivo do laço \"for\" em Python?",
                                AlternativaA = "Executar um conjunto de instruções apenas uma vez.",
                                AlternativaB = "Realizar cálculos matemáticos complexos.",
                                AlternativaC = "Percorrer sequências e executar um conjunto de instruções para cada elemento.",
                                AlternativaD = "Controlar a execução de um programa.",
                                Resposta = "Percorrer sequências e executar um conjunto de instruções para cada elemento.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 2,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 2",
                        ContentUrl = "LacosDeRepeticao/LacosDeRepeticao2.pdf",
                        Nome = "Aula 2: Laço \"for\" em Python",
                        QtdMoedas = 10,
                        Xp = 1100,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual é a função principal do laço \"for\" em Python?",
                                AlternativaA = "Realizar operações matemáticas complexas.",
                                AlternativaB = "Executar um conjunto de instruções apenas uma vez.",
                                AlternativaC = "Percorrer sequências e executar um conjunto de instruções para cada elemento.",
                                AlternativaD = "Controlar a execução de um programa.",
                                Resposta = "Percorrer sequências e executar um conjunto de instruções para cada elemento.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "O que é necessário para usar um laço \"for\" em Python?",
                                AlternativaA = "Uma única instrução.",
                                AlternativaB = "Uma condição complexa.",
                                AlternativaC = "Uma sequência para percorrer.",
                                AlternativaD = "Um programa extenso.",
                                Resposta = "Uma sequência para percorrer.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 3,
                         Id = Guid.NewGuid(),
                        Descricao = "Aula 3",
                        ContentUrl = "LacosDeRepeticao/LacosDeRepeticao3.pdf",
                        Nome = "Aula 3: Laço \"while\" e Laço \"do-while\"",
                        QtdMoedas = 10,
                        Xp = 1200,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal característica do laço \"while\" em Python?",
                                AlternativaA = "Ele sempre executa o bloco de código pelo menos uma vez.",
                                AlternativaB = "Ele nunca executa o bloco de código.",
                                AlternativaC = "Ele é usado apenas para contar números.",
                                AlternativaD = "Ele só pode ser usado em situações complexas.",
                                Resposta = "Ele sempre executa o bloco de código pelo menos uma vez.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Como é possível simular um laço \"do-while\" em Python?",
                                AlternativaA = "Não é possível simular um laço \"do-while\" em Python.",
                                AlternativaB = "Usando um laço \"for\".",
                                AlternativaC = "Utilizando um laço \"while\" com uma condição de saída no início.",
                                AlternativaD = "Usando um laço \"if\" antes do \"while\".",
                                Resposta = "Utilizando um laço \"while\" com uma condição de saída no início.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    }
                },
                Duracao = 200,
                QtdMoeda = 30,
                Xp = 5000
            };

            return curso;
        }

        private Curso GetMatrizesCurso()
        {
            var curso = new Curso(
                Guid.NewGuid(),
                "Matrizes",
                "Aprenda os fundamentos essenciais para utilizar matrizes",
                Domain.Enums.Nivel.Avancado,
                iconUrl: "Matriz.png"
                )
            {
                Aulas = new List<Aula>()
                {
                    new Aula()
                    {
                        Number = 1,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 1",
                        ContentUrl = "Matrizes/Matrizes1.pdf",
                        Nome = "Aula 1: Introdução às Matrizes na Lógica de Programação",
                        QtdMoedas = 10,
                        Xp = 1000,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "O que são matrizes na programação?",
                                AlternativaA = "Caixas para armazenar informações em uma dimensão.",
                                AlternativaB = "Estruturas de dados bidimensionais que organizam informações em linhas e colunas.",
                                AlternativaC = "Funções matemáticas complexas.",
                                AlternativaD = "Nenhuma das alternativas.",
                                Resposta = "Estruturas de dados bidimensionais que organizam informações em linhas e colunas.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é a notação típica para acessar elementos de uma matriz?",
                                AlternativaA = "Usando nomes descritivos para os elementos.",
                                AlternativaB = "Usando números aleatórios.",
                                AlternativaC = "Usando índices de linha e coluna.",
                                AlternativaD = "Não é possível acessar elementos de uma matriz.",
                                Resposta = "Usando índices de linha e coluna.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 2,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 2",
                        ContentUrl = "Matrizes/Matrizes2.pdf",
                        Nome = "Aula 2: Declaração e Inicialização de Matrizes",
                        QtdMoedas = 10,
                        Xp = 1100,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual é o propósito da declaração de uma matriz em programação?",
                                AlternativaA = "Inicializar valores para a matriz.",
                                AlternativaB = "Reservar espaço de armazenamento para a matriz.",
                                AlternativaC = "Definir o número de elementos na matriz.",
                                AlternativaD = "Atribuir nomes aos elementos da matriz.",
                                Resposta = "Reservar espaço de armazenamento para a matriz.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "O que significa a inicialização de uma matriz?",
                                AlternativaA = "Declarar uma matriz com seu tamanho e tipo de dados.",
                                AlternativaB = "Reservar espaço de armazenamento para a matriz.",
                                AlternativaC = "Preencher a matriz com valores ou dados.",
                                AlternativaD = "Nomear os elementos da matriz.",
                                Resposta = "Preencher a matriz com valores ou dados.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 3,
                         Id = Guid.NewGuid(),
                        Descricao = "Aula 3",
                        ContentUrl = "Matrizes/Matrizes3.pdf",
                        Nome = "Aula 3: Operações com Matrizes",
                        QtdMoedas = 10,
                        Xp = 1200,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal diferença entre uma estrutura (registro) e uma enumeração em Python?",
                                AlternativaA = "Estruturas podem armazenar valores nomeados, enquanto enumerações são usadas para representar conjuntos de valores constantes.",
                                AlternativaB = "Estruturas são usadas para representar valores constantes, enquanto enumerações armazenam informações relacionadas.",
                                AlternativaC = "Não existe diferença entre estruturas e enumerações em Python.",
                                AlternativaD = "Nenhuma das alternativas.",
                                Resposta = "Estruturas podem armazenar valores nomeados, enquanto enumerações são usadas para representar conjuntos de valores constantes.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal vantagem de usar tipos definidos pelo usuário em Python?",
                                AlternativaA = "Tipos definidos pelo usuário são imutáveis e não podem ser modificados.",
                                AlternativaB = "Tipos definidos pelo usuário permitem criar estruturas de dados personalizadas para representar informações complexas.",
                                AlternativaC = "Tipos definidos pelo usuário consomem menos memória em comparação com os tipos embutidos.",
                                AlternativaD = "Nenhuma das alternativas.",
                                Resposta = "Tipos definidos pelo usuário permitem criar estruturas de dados personalizadas para representar informações complexas.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    }
                },
                Duracao = 200,
                QtdMoeda = 30,
                Xp = 5000
            };

            return curso;
        }

        private Curso GetTiposDeDadosCurso()
        {
            var curso = new Curso(
                Guid.NewGuid(),
                "Tipos de Dados",
                "Aprenda os fundamentos essenciais para utilizar vetores",
                Domain.Enums.Nivel.Avancado,
                iconUrl: "Tipos_de_Dados.png"
                )
            {
                Aulas = new List<Aula>()
                {
                    new Aula()
                    {
                        Number = 1,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 1",
                        ContentUrl = "TiposDeDados/TiposDeDados1.pdf",
                        Nome = "Aula 1: Estruturas e Registros em Python",
                        QtdMoedas = 10,
                        Xp = 1000,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual é a biblioteca em Python usada para criar registros (estruturas de dados nomeadas)?",
                                AlternativaA = "collections",
                                AlternativaB = "datastructure",
                                AlternativaC = "namedtuple",
                                AlternativaD = "Nenhuma das alternativas.",
                                Resposta = "collections",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Registros em Python são mutáveis, o que significa que seus campos podem ser alterados diretamente.",
                                AlternativaA = "Verdadeiro",
                                AlternativaB = "Falso",
                                AlternativaC = "",
                                AlternativaD = "",
                                Resposta = "Falso",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 2,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 2",
                        ContentUrl = "TiposDeDados/TiposDeDados2.pdf",
                        Nome = "Aula 2: Declaração e Inicialização de Vetores",
                        QtdMoedas = 10,
                        Xp = 1100,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual módulo em Python é usado para criar enumerações?",
                                AlternativaA = "collections",
                                AlternativaB = "enum",
                                AlternativaC = "types",
                                AlternativaD = "Nenhuma das alternativas.",
                                Resposta = "enum",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Os tipos definidos pelo usuário em Python são mutáveis, o que significa que seus campos podem ser alterados diretamente.",
                                AlternativaA = "Verdadeiro",
                                AlternativaB = "Falso",
                                AlternativaC = "",
                                AlternativaD = "",
                                Resposta = "Verdadeiro",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 3,
                         Id = Guid.NewGuid(),
                        Descricao = "Aula 3",
                        ContentUrl = "TiposDeDados/TiposDeDados3.pdf",
                        Nome = "Aula 3: Acesso a Elementos de Vetores",
                        QtdMoedas = 10,
                        Xp = 1200,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal diferença entre uma estrutura (registro) e uma enumeração em Python?",
                                AlternativaA = "Estruturas podem armazenar valores nomeados, enquanto enumerações são usadas para representar conjuntos de valores constantes.",
                                AlternativaB = "Estruturas são usadas para representar valores constantes, enquanto enumerações armazenam informações relacionadas.",
                                AlternativaC = "Não existe diferença entre estruturas e enumerações em Python.",
                                AlternativaD = "Nenhuma das alternativas.",
                                Resposta = "Estruturas podem armazenar valores nomeados, enquanto enumerações são usadas para representar conjuntos de valores constantes.",
                                Xp = 500,
                                QtdMoedas = 1
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal vantagem de usar tipos definidos pelo usuário em Python?",
                                AlternativaA = "Tipos definidos pelo usuário são imutáveis e não podem ser modificados.",
                                AlternativaB = "Tipos definidos pelo usuário permitem criar estruturas de dados personalizadas para representar informações complexas.",
                                AlternativaC = "Tipos definidos pelo usuário consomem menos memória em comparação com os tipos embutidos.",
                                AlternativaD = "Nenhuma das alternativas.",
                                Resposta = "Tipos definidos pelo usuário permitem criar estruturas de dados personalizadas para representar informações complexas.",
                                Xp = 500,
                                QtdMoedas = 1
                            }
                        }
                    }
                },
                Duracao = 200,
                QtdMoeda = 30,
                Xp = 5000
            };

            return curso;
        }

        private Curso GetVetoresCurso()
        {
            var curso = new Curso(
                Guid.NewGuid(),
                "Vetores",
                "Aprenda os fundamentos essenciais para utilizar vetores",
                Domain.Enums.Nivel.Iniciante,
                iconUrl: "Vetores.png"
                )
            {
                Aulas = new List<Aula>()
                {
                    new Aula()
                    {
                        Number = 1,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 1",
                        ContentUrl = "Vetores/Vetores1.pdf",
                        Nome = "Aula 1: Introdução a Vetores",
                        QtdMoedas = 10,
                        Xp = 1000,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Qual é a principal característica de um vetor em programação?",
                                AlternativaA = "Armazena apenas números inteiros.",
                                AlternativaB = "Permite armazenar múltiplos valores sob um único nome.",
                                AlternativaC = "Pode ser acessado apenas por meio de loops.",
                                AlternativaD = "Só pode conter elementos do mesmo tipo.",
                                Resposta = "Permite armazenar múltiplos valores sob um único nome.",
                                Xp = 500,
                                QtdMoedas = 100
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é a diferença entre um vetor e uma variável simples?",
                                AlternativaA = " Um vetor só pode armazenar números inteiros, enquanto uma variável pode armazenar qualquer tipo de dado.",
                                AlternativaB = "Uma variável é usada para armazenar um único valor, enquanto um vetor pode armazenar múltiplos valores.",
                                AlternativaC = "Vetores e variáveis são conceitos idênticos na programação.",
                                AlternativaD = "Vetores são usados apenas em linguagens de programação específicas.",
                                Resposta = "Uma variável é usada para armazenar um único valor, enquanto um vetor pode armazenar múltiplos valores.**",
                                Xp = 500,
                                QtdMoedas = 100
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 2,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 2",
                        ContentUrl = "Vetores/Vetores2.pdf",
                        Nome = "Aula 2: Declaração e Inicialização de Vetores",
                        QtdMoedas = 10,
                        Xp = 1100,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Como você declara e inicializa um vetor em Python?",
                                AlternativaA = "Usando chaves para envolver os elementos.",
                                AlternativaB = "Usando colchetes para envolver os elementos.",
                                AlternativaC = "Usando parênteses para envolver os elementos.",
                                AlternativaD = "Usando aspas para envolver os elementos.",
                                Resposta = "Usando colchetes para envolver os elementos.",
                                Xp = 500,
                                QtdMoedas = 100
                            },
                            new Exercicio()
                            {
                                Enunciado = "Como você acessa o primeiro elemento de um vetor em Python?",
                                AlternativaA = "Usando primeiro_elemento().",
                                AlternativaB = "Usando o número 1 como índice.",
                                AlternativaC = "Usando o índice 0.",
                                AlternativaD = "Usando o índice 2.",
                                Resposta = "Usando o índice 0.",
                                Xp = 500,
                                QtdMoedas = 100
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 3,
                         Id = Guid.NewGuid(),
                        Descricao = "Aula 3",
                        ContentUrl = "Vetores/Vetores3.pdf",
                        Nome = "Aula 3: Acesso a Elementos de Vetores",
                        QtdMoedas = 10,
                        Xp = 1200,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Suponha que você tenha o seguinte vetor em Python: idades = [25, 30, 35, 40, 45]. Qual é o resultado de idades[1:4]?",
                                AlternativaA = "[25, 30, 35, 40]",
                                AlternativaB = "[30, 35, 40]",
                                AlternativaC = "[30, 35]",
                                AlternativaD = "[35, 40, 45]",
                                Resposta = "[30, 35, 40]",
                                Xp = 500,
                                QtdMoedas = 100
                            },
                            new Exercicio()
                            {
                                Enunciado = "Qual é a utilidade do fatiamento (slicing) em vetores?",
                                AlternativaA = "Acessar apenas o primeiro elemento do vetor.",
                                AlternativaB = "Extrair partes específicas de um vetor.",
                                AlternativaC = "Calcular a média dos valores de um vetor.",
                                AlternativaD = "Adicionar elementos ao final do vetor.",
                                Resposta = "Extrair partes específicas de um vetor.",
                                Xp = 500,
                                QtdMoedas = 100
                            }
                        }
                    }
                },
                Duracao = 200,
                QtdMoeda = 30,
                Xp = 5000
            };

            return curso;
        }

        private Curso GetVariavelCurso()
        {
            var curso = new Curso(
                Guid.NewGuid(),
                "Variáveis",
                "Aprenda os fundamentos essenciais para utilizar variáveis",
                Domain.Enums.Nivel.Iniciante,
                iconUrl: "Variaveis.png"
                )
            {
                Aulas = new List<Aula>()
                {
                    new Aula()
                    {
                        Number = 1,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 1",
                        ContentUrl = "Variaveis/Variaveis1.pdf",
                        Nome = "Aula 1: Introdução às Variáveis",
                        QtdMoedas = 10,
                        Xp = 1000,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Por que as variáveis são usadas na programação?",
                                AlternativaA = "Para tornar o código mais complicado.",
                                AlternativaB = "Para armazenar e manipular informações.",
                                AlternativaC = "Para esconder informações do programa.",
                                AlternativaD = "Para evitar o uso de números.",
                                Resposta = "Para armazenar e manipular informações.",
                                Xp = 500,
                                QtdMoedas = 100
                            },
                            new Exercicio()
                            {
                                Enunciado = "Por que as variáveis são usadas na programação?",
                                AlternativaA = "Para tornar o código mais complicado.",
                                AlternativaB = "Para armazenar e manipular informações.",
                                AlternativaC = "Para esconder informações do programa.",
                                AlternativaD = "Para evitar o uso de números.",
                                Resposta = "Para armazenar e manipular informações.",
                                Xp = 500,
                                QtdMoedas = 100
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 2,
                        Id = Guid.NewGuid(),
                        Descricao = "Aula 2",
                        ContentUrl = "Variaveis/Variaveis2.pdf",
                        Nome = "Aula 2: Declaração e Atribuição de Variáveis",
                        QtdMoedas = 10,
                        Xp = 1100,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Por que as variáveis são usadas na programação?",
                                AlternativaA = "Para tornar o código mais complicado.",
                                AlternativaB = "Para armazenar e manipular informações.",
                                AlternativaC = "Para esconder informações do programa.",
                                AlternativaD = "Para evitar o uso de números.",
                                Resposta = "Para armazenar e manipular informações.",
                                Xp = 500,
                                QtdMoedas = 100
                            },
                            new Exercicio()
                            {
                                Enunciado = "Por que as variáveis são usadas na programação?",
                                AlternativaA = "Para tornar o código mais complicado.",
                                AlternativaB = "Para armazenar e manipular informações.",
                                AlternativaC = "Para esconder informações do programa.",
                                AlternativaD = "Para evitar o uso de números.",
                                Resposta = "Para armazenar e manipular informações.",
                                Xp = 500,
                                QtdMoedas = 100
                            }
                        }
                    },
                    new Aula()
                    {
                        Number = 3,
                         Id = Guid.NewGuid(),
                        Descricao = "Aula 3",
                        ContentUrl = "Variaveis/Variaveis3.pdf",
                        Nome = "Aula 3: Escopo de Variáveis e Variáveis Estáticas",
                        QtdMoedas = 10,
                        Xp = 1200,
                        Exercicios = new List<Exercicio>()
                        {
                            new Exercicio()
                            {
                                Enunciado = "Por que as variáveis são usadas na programação?",
                                AlternativaA = "Para tornar o código mais complicado.",
                                AlternativaB = "Para armazenar e manipular informações.",
                                AlternativaC = "Para esconder informações do programa.",
                                AlternativaD = "Para evitar o uso de números.",
                                Resposta = "Para armazenar e manipular informações.",
                                Xp = 500,
                                QtdMoedas = 100
                            },
                            new Exercicio()
                            {
                                Enunciado = "Por que as variáveis são usadas na programação?",
                                AlternativaA = "Para tornar o código mais complicado.",
                                AlternativaB = "Para armazenar e manipular informações.",
                                AlternativaC = "Para esconder informações do programa.",
                                AlternativaD = "Para evitar o uso de números.",
                                Resposta = "Para armazenar e manipular informações.",
                                Xp = 500,
                                QtdMoedas = 100
                            }
                        }
                    }
                },
                Duracao = 200,
                QtdMoeda = 30,
                Xp = 5000
            };

            return curso;
        }
        #endregion

        [HttpGet("Cursos/deleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            var cursos = await _cursoAppService.GetAll();

            foreach (var curso in cursos)
            {
                await _cursoAppService.Remove(curso);
            }

            return Ok();
        }
    }
}
