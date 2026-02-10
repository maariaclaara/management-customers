# Gestão de Clientes — API (.NET)

Este repositório contém a implementação de uma API para Gestão de Clientes, desenvolvida como parte de um desafio técnico com foco em Clean Architecture, Domain-Driven Design (DDD), CQRS e testes automatizados.

O objetivo da solução é demonstrar qualidade de código, clareza arquitetural e boas decisões técnicas, simulando um cenário real de desenvolvimento backend.

---

## 🎯 Escopo do Desafio

Implementar uma feature slice (fatia vertical) responsável por:

- Criar um cliente
- Consultar um cliente por ID

---

## 🚀 Tecnologias Utilizadas

- .NET 9
- ASP.NET Core Web API
- xUnit (testes unitários)
- Injeção de Dependência nativa
- REST Client (VS Code) para testes manuais dos endpoints

---

## 🏗️ Arquitetura

A solução segue os princípios da Clean Architecture, com separação clara de responsabilidades entre camadas.

A Clean Architecture foi adotada para garantir baixo acoplamento, alta testabilidade, independência de frameworks e facilidade de evolução do sistema.

---

## 🧠 Modelagem de Domínio

### Entidade: Cliente

Propriedades principais:
- Id (Guid)
- NomeFantasia
- Cnpj
- Ativo

A entidade protege suas invariantes de negócio, garantindo nome não vazio, CNPJ válido e estado consistente.

### Value Object: CNPJ

O CNPJ foi modelado como Value Object, sendo responsável por validação, normalização e garantia de integridade. Toda a regra relacionada ao CNPJ está encapsulada nesse objeto.

---

## 🔁 Padrão CQRS

A camada de aplicação utiliza CQRS, separando claramente responsabilidades.

### Commands
- CriaClienteCommand
- CriaClienteCommandHandler

### Queries
- ObtemClientePorIdQuery
- ObtemClientePorIdQueryHandler

Cada handler representa um caso de uso explícito, facilitando manutenção e testes.

---

## 💾 Persistência

Foi implementado um repositório em memória, respeitando o contrato IClienteRepository.

A persistência em memória foi escolhida para manter o foco do desafio na arquitetura, domínio e inversão de dependência, permitindo futura substituição por NHibernate, Entity Framework ou outro mecanismo de persistência sem impacto nas camadas superiores.

---

## 🧪 Testes Automatizados

Os testes foram desenvolvidos com xUnit, focando exclusivamente na camada de aplicação, sem dependência de infraestrutura ou API.

### Cenários testados

Criação de Cliente:
- Criação com dados válidos
- Erro ao tentar criar cliente com CNPJ duplicado
- Erro ao criar cliente com dados inválidos

Consulta por ID:
- Retorna cliente quando o ID existe
- Retorna não encontrado quando o ID não existe

Todos os testes passam com sucesso ao executar:

dotnet test

---

## 🔌 Endpoints da API

### Criar Cliente

POST /clientes  
Content-Type: application/json

Exemplo de payload:

{
  "nomeFantasia": "Empresa Teste LTDA",
  "cnpj": "12.345.678/0001-95"
}

### Consultar Cliente por ID

GET /clientes/{id}

---

## 🧪 Testes Manuais (REST Client)

A API foi validada manualmente utilizando a extensão REST Client do VS Code.

Arquivo disponível no projeto:
GestaoClientes.API.http

Cenários testados:
- Criação de cliente válida
- Criação com CNPJ duplicado
- Criação com dados inválidos
- Consulta por ID existente
- Consulta por ID inexistente

---

## 🎨 Sobre o Frontend

O frontend não foi incluído nesta entrega, pois não fazia parte do escopo do desafio.

A API foi completamente validada por testes automatizados e testes manuais via REST Client, mantendo o foco na qualidade do backend conforme os critérios avaliados.

---

## ▶️ Como Executar o Projeto

Executar a API:
dotnet run --project GestaoClientes.API

Executar os testes:
dotnet test

---

## 🧾 Considerações Finais

Esta solução prioriza código limpo, clareza arquitetural, domínio bem modelado, alta testabilidade e facilidade de manutenção.

O projeto está preparado para evoluir, seja com persistência real, novas funcionalidades ou integração com frontend.

Desenvolvido seguindo boas práticas adotadas em projetos reais de produção.
