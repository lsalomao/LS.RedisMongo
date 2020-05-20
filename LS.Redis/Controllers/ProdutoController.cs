using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using LS.Redis.Domain.Entity;
using LS.Redis.Domain.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;


namespace LS.Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IDistributedCache cache;
        private readonly IProdutoRepository produtoRepository;

        public ProdutoController(IDistributedCache cache, IProdutoRepository produtoRepository)
        {
            this.cache = cache;
            this.produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("GetProdutoByMarca")]
        public async Task<IActionResult> GetProdutoByMarca(string marca)
        {
            try
            {
                var keyCache = $"Marcas-{marca}";

                var json = await cache.GetStringAsync(keyCache);

                if (string.IsNullOrEmpty(json))
                {
                    var result = await produtoRepository.GetByMarcaProdutos(marca);

                    DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
                    cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(Configuration.Controle.TimeExpiration));

                    await cache.SetStringAsync(keyCache, JsonSerializer.Serialize(result), cacheOptions);

                    return Ok(result);
                }

                return Ok(JsonSerializer.Deserialize<List<Produto>>(json));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
            }
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var json = await cache.GetStringAsync("Produtos");

                if (string.IsNullOrEmpty(json))
                {
                    var result = await produtoRepository.GetAllProdutos();

                    DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
                    cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(Configuration.Controle.TimeExpiration));

                    await cache.SetStringAsync("Produtos", JsonSerializer.Serialize(result), cacheOptions);

                    return Ok(result);
                }

                return Ok(json);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            try
            {
                var rng = new Random();

                for (int i = 0; i < 10000; i++)
                {
                    var produto1 = new Produto
                    {
                        Marca = Marcas[rng.Next(Marcas.Length)],
                        Nome = $" {Produtos[rng.Next(Produtos.Length)]} - {i * 5}",
                        Preco = rng.Next(10, 5000)
                    };

                    await produtoRepository.Inserir(produto1);
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro {ex.Message}");
            }
        }

        private static readonly string[] Marcas = new[]     {
            "Dell", "Aoc", "HP", "LG", "Samsung", "Motorola", "Renault", "GM", "Toyota", "Honda"
        };

        private static readonly string[] Produtos = new[]     {
            "Notebook Identity", "Mouse", "Monitor", "Teclado", "Logan", "Corola", "Gol", "Civic", "Impressoras", "Copo", "Saveiro"
        };
    }
}