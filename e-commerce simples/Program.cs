class Program
{
    static void Main(string[] args)
    {        
        Categoria categoria1 = new Categoria(1, "Eletrônicos");
        Categoria categoria2 = new Categoria(2, "Periféricos");
        
        Produto produto1 = new Produto(1, "Celular", "Bom Celular", 2000.00, 10, categoria1);
        Produto produto2 = new Produto(2, "Notebook", "Marca muito boa", 4000.00, 5, categoria1);
        Produto produto3 = new Produto(3, "Capinha Celular", "Muito legal", 19.00, 10, categoria2);
        
        Cliente cliente1 = new Cliente(1, "Zé", Convert.ToDateTime("01/01/1960"), "Na rua azul, 100,Centro, Cidade,Estado");
        Cliente cliente2 = new Cliente(2, "Maria", Convert.ToDateTime("10/05/1985"), "Na rua rosa, 100, Centro,Cidade,Estado");
        
        Pedido pedido1 = new Pedido(1, cliente1, Pedido.FormaPagamento.Cartao);
        pedido1.IniciarPedido();
        pedido1.AdicionarProduto(produto1);
        pedido1.AdicionarProduto(produto3);
        pedido1.FinalizarPedido();

        Pedido pedido2 = new Pedido(2, cliente2, Pedido.FormaPagamento.Boleto);
        pedido2.IniciarPedido();
        pedido2.AdicionarProduto(produto2);
        pedido2.CancelarPedido();
    }
}

class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }

    public Cliente(int id, string nome, DateTime dataNascimento, string endereco)
    {
        Id = id;
        Nome = nome;
        DataNascimento = dataNascimento;
        Endereco = endereco;
    }
}

class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public int Estoque { get; set; }
    public Categoria Categoria { get; set; }

    public Produto(int id, string nome, string descricao, double preco, int estoque, Categoria categoria)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Estoque = estoque;
        Categoria = categoria;
    }
}

class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public Categoria(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

class Pedido
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; }
    public List<Produto> Produtos { get; set; }
    public DateTime DataPedido { get; set; }
    public bool Iniciado { get; set; }
    public bool Finalizado { get; private set; }
    public bool Cancelado { get; private set; }
    public FormaPagamento Pagamento { get; private set; }

    public enum FormaPagamento
    {
        Cartao,
        Boleto
    }

    public Pedido(int id, Cliente cliente, FormaPagamento pagamento)
    {
        Id = id;
        Cliente = cliente;
        Produtos = new List<Produto>();
        DataPedido = DateTime.Now;
        Iniciado = false;
        Finalizado = false;
        Cancelado = false;
        Pagamento = pagamento;
    }

    

    public void IniciarPedido()
    {
        Console.WriteLine($"Pedido iniciado para o cliente {Cliente.Nome}.");
    }

    public void AdicionarProduto(Produto produto)
    {
        if (!Finalizado && !Cancelado)
        {
            Produtos.Add(produto);
        }
        else
        {
            Console.WriteLine("Não é possível adicionar produtos : pedido finalizado/cancelado.");
        }
    }

    public double CalcularTotal()
    {
        double total = 0;
        foreach (var produto in Produtos)
        {
            total += produto.Preco;
        }
        return total;
    }

    public void FinalizarPedido()
    {
        if (!Cancelado)
        {
            Finalizado = true;
            Console.WriteLine($"Pedido {Id} finalizado para o cliente {Cliente.Nome}. Total: {CalcularTotal():C}. Forma de pagamento: {Pagamento}");
        }
        else
        {
            Console.WriteLine("Falha, não é possível finalizar um pedido cancelado.");
        }
    }

    public void CancelarPedido()
    {
        if (!Finalizado && DateTime.Now.Subtract(DataPedido).Days <= 7 && CalcularTotal() > 100)
        {
            Cancelado = true;
            Console.WriteLine($"Pedido cancelado para o cliente {Cliente.Nome}.");
        }
        else
        {
            Console.WriteLine("Não é possível cancelar este pedido.");
        }
    }
}


