# EasyOrder

Sistema de gerenciamento de pedidos desenvolvido em .NET 8 com arquitetura em camadas e messaging assíncrono.

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**:

```
src/
├── EasyOrder.API/           # Camada de Apresentação (Controllers, Swagger)
├── EasyOrder.Application/   # Camada de Aplicação (Services, DTOs, Consumers)
├── EasyOrder.Domain/        # Camada de Domínio (Entities, Events, Interfaces)
└── EasyOrder.Infrastructure/ # Camada de Infraestrutura (Repositories, Messaging)
```

### 📋 Funcionalidades

- ✅ Criação de pedidos
- ✅ Consulta de pedidos por ID e cliente
- ✅ Atualização e exclusão de pedidos
- ✅ Gerenciamento de clientes e produtos
- ✅ Messaging assíncrono com RabbitMQ
- ✅ Event-driven architecture

## 🛠️ Tecnologias e Bibliotecas

### Framework e Runtime
- **.NET 8.0** - Framework principal
- **ASP.NET Core** - Web API

### Bibliotecas Externas
- **MassTransit 8.5.2** - Framework de messaging
- **MassTransit.RabbitMQ 8.5.2** - Transporte RabbitMQ
- **Swashbuckle.AspNetCore 6.6.2** - Documentação Swagger/OpenAPI

### Ferramentas de Desenvolvimento
- **Docker** - Containerização do RabbitMQ
- **RabbitMQ** - Message broker
- **Swagger UI** - Documentação interativa da API

## 🚀 Como Executar

### Pré-requisitos

1. **.NET 8.0 SDK** instalado
2. **Docker** instalado (para RabbitMQ)
3. **RabbitMQ** rodando via Docker (porta 5672)
   - Usuário: `guest`
   - Senha: `guest`

### Instalação e Execução

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd EasyOrder
   ```

2. **Inicie o RabbitMQ com Docker**
   ```bash
   docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
   ```
   
   **Acesso ao Management UI**: `http://localhost:15672`
   - Usuário: `guest`
   - Senha: `guest`

3. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

4. **Execute o projeto**
   ```bash
   dotnet run --project src/EasyOrder.API/EasyOrder.API
   ```

5. **Acesse a API**
   - **Swagger UI**: `https://localhost:7000/swagger`
   - **API Base**: `https://localhost:7000/api`

## 📚 Endpoints da API

### Pedidos
- `POST /api/order` - Criar pedido
- `GET /api/order/{id}` - Buscar pedido por ID
- `GET /api/order/by-customer/{customerId}` - Buscar pedidos por cliente
- `PUT /api/order/{id}` - Atualizar pedido
- `DELETE /api/order/{id}` - Excluir pedido

### Clientes
- `POST /api/customer` - Criar cliente
- `GET /api/customer/{id}` - Buscar cliente por ID
- `GET /api/customer` - Listar todos os clientes
- `PUT /api/customer/{id}` - Atualizar cliente
- `DELETE /api/customer/{id}` - Excluir cliente

### Produtos
- `POST /api/product` - Criar produto
- `GET /api/product/{id}` - Buscar produto por ID
- `GET /api/product` - Listar todos os produtos
- `PUT /api/product/{id}` - Atualizar produto
- `DELETE /api/product/{id}` - Excluir produto

## 🔄 Event-Driven Architecture

O sistema utiliza eventos para comunicação assíncrona:

### Eventos
- **OrderCreatedEvent** - Disparado quando um pedido é criado
- **Consumer** - Processa eventos e atualiza status dos pedidos

### Fluxo
1. Pedido é criado via API
2. `OrderCreatedEvent` é publicado no RabbitMQ
3. `OrderCreatedEventConsumer` processa o evento
4. Status do pedido é atualizado automaticamente

## 📁 Estrutura de Pastas

```
src/EasyOrder.API/
├── Controllers/          # Controllers da API
├── Program.cs           # Configuração da aplicação
└── appsettings.json    # Configurações

src/EasyOrder.Application/
├── DTOs/               # Data Transfer Objects
├── Interfaces/         # Interfaces dos serviços
├── Services/           # Lógica de aplicação
└── Consumers/          # Event consumers

src/EasyOrder.Domain/
├── Entities/           # Entidades de domínio
├── Events/             # Eventos de domínio
├── Enums/              # Enumerações
└── Interfaces/         # Interfaces de repositório

src/EasyOrder.Infrastructure/
├── Repositories/       # Implementações dos repositórios
├── Extensions/         # Extensões de configuração
└── Messaging/          # Configuração de messaging
```

## 🧪 Exemplo de Uso

### Criar um Pedido

```json
POST /api/order
{
  "customerId": "123e4567-e89b-12d3-a456-426614174000",
  "items": [
    {
      "productId": "456e7890-e89b-12d3-a456-426614174001",
      "quantity": 2,
      "unitPrice": 25.50
    }
  ]
}
```

### Resposta
```json
{
  "orderId": "789abcde-e89b-12d3-a456-426614174002",
  "customerId": "123e4567-e89b-12d3-a456-426614174000",
  "orderDate": "2024-01-15T10:30:00Z",
  "status": "Created",
  "totalAmount": 51.00,
  "items": [
    {
      "orderItemId": "def12345-e89b-12d3-a456-426614174003",
      "productId": "456e7890-e89b-12d3-a456-426614174001",
      "quantity": 2,
      "unitPrice": 25.50,
      "subtotal": 51.00
    }
  ]
}
```

## 🔧 Configuração

### RabbitMQ
O sistema está configurado para conectar no RabbitMQ local:
- **Host**: `localhost:5672`
- **Usuário**: `guest`
- **Senha**: `guest`

Para alterar essas configurações, edite o arquivo `src/EasyOrder.Infrastructure/Extensions/AppExtensions.cs`.

### Docker Commands

**Iniciar RabbitMQ:**
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

**Parar RabbitMQ:**
```bash
docker stop rabbitmq
```

**Remover container:**
```bash
docker rm rabbitmq
```

**Ver logs do RabbitMQ:**
```bash
docker logs rabbitmq
```

## 📝 Notas

- O projeto utiliza repositórios em memória para simplicidade
- Todos os dados são perdidos ao reiniciar a aplicação
- Para produção, implemente persistência em banco de dados
- O sistema processa eventos de forma assíncrona com delay de 10 segundos

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request
