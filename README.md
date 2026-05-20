# Gestão de Títulos em Atraso

Aplicação web para gerenciamento de títulos em atraso

## ⚡ Funcionalidades

- Cadastro, visualização e exclusão de títulos e parcelas
- Cálculo automático de juros, multa e dias em atraso

## 📋 Regras de negócio

- O título pode possuir uma ou mais parcelas
- Juros são calculados proporcionalmente aos dias em atraso
- Multa é aplicada sobre o valor total do título
- Apenas parcelas vencidas geram juros
- O valor atualizado considera:
  - Valor original
  - Multa
  - Juros acumulados

## 🛠️ Tecnologias e Padrões

- **Frontend:** Angular 21 + TypeScript
- **UI:** Angular Material
- **Backend:** .NET 8 Web API
- **ORM:** Entity Framework Core
- **Validações:** FluentValidation
- **Banco de dados:** PostgreSQL
- **Deploy:** Render

## 💡 Padrões e boas práticas

- **Arquitetura em camadas** (API, Application, Domain, Infrastructure)
- **Repository Pattern** para acesso a dados

## 💻 Acesse o projeto

🔗 Acesse o sistema diretamente no navegador clicando [**aqui**](https://gestao-titulos.onrender.com)