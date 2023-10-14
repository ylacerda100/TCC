using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Interfaces;
using TCC.Domain.Models;

namespace TCC.UI.Web.Controllers
{
    public class CursosController : BaseController
    {
        private readonly ICursoAppService _cursoAppService;

        public CursosController(ICursoAppService cursoAppService)
        {
            _cursoAppService = cursoAppService;
        }
        
        [AllowAnonymous]
        [HttpGet("Cursos")]
        public async Task<IActionResult> Index()
        {
            return View(await _cursoAppService.GetAll());
        }

        [Authorize]
        [HttpGet("Cursos/{name}")]
        public async Task<IActionResult> Detalhes(string name)
        {
            if (string.IsNullOrEmpty(name)) 
            {
                return NotFound();
            }

            var cursoViewModel = await _cursoAppService.GetByName(name);

            if (cursoViewModel is null)
            {
                return NotFound();
            }

            return View(cursoViewModel);
        }

        [HttpGet("Cursos/load")]
        public IActionResult Load() 
        {
            var cursos = new List<Curso>
            {
                GetVariavelCurso(),
                GetVetoresCurso(),
                //GetTiposDeDadosCurso(),
                //GetMatrizesCurso(),
                //GetLacosCurso()
            };

            foreach (var c in cursos)
            {
                _cursoAppService.Add(c);
                Thread.Sleep(100);
            }

            return Ok($"{cursos.Count} cursos adicionados!");
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
                                QtdMoedas = 1
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
