#  Controle de Despesas Residenciais

Sistema fullstack para gerenciamento de despesas domésticas, desenvolvido com **.NET 8 (backend)** e **React + TypeScript (frontend)**.

---

##  Tecnologias

### Backend

* C#
* .NET 8
* Entity Framework Core
* SQLite
* Arquitetura em camadas (Controller, Service, Repository)

### Frontend

* React
* TypeScript
* Context API + useReducer
* Axios

---

##  Funcionalidades

### Pessoas

* Cadastro
* Edição
* Exclusão
* Listagem

### Categorias

* Cadastro
* Listagem
* Classificação por finalidade:

  * Despesa
  * Receita
  * Ambos

### Transações

* Cadastro
* Listagem
* Associação com pessoa e categoria
* Validação de regras de negócio

### Relatório

* Total de receitas e despesas por pessoa
* Saldo individual
* Total geral consolidado

---

##  Regras de Negócio

* Menores de idade **não podem possuir receitas**
* Categorias respeitam sua finalidade (Despesa/Receita)
* Ao excluir uma pessoa, **todas as suas transações são removidas automaticamente**

---

##  Decisões Técnicas

* Backend estruturado em camadas para separação de responsabilidades
* Uso de DTOs para controle de entrada e saída de dados
* Frontend dividido por domínio (People, Categories, Transactions, Reports)
* Estado global com **Context + useReducer** para sincronização entre componentes
* Ordenação alfabética de pessoas e categorias para melhor usabilidade
* Formatação monetária em padrão brasileiro (R$)

---

## Como executar o projeto

### Backend

```bash
cd ControleDespesasCasa.Api
dotnet run
```

### Frontend

```bash
cd frontend/controle-despesas-casa-front
npm install
npm run dev
```

---

##  Observações

Projeto desenvolvido como teste técnico, com foco em:

* Clareza de código
* Organização
* Regras de negócio bem definidas
* Integração entre frontend e backend
