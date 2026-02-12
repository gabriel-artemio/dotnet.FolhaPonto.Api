# API Folha de Ponto

## 📌 Sobre o projeto

Este projeto consiste no desenvolvimento de uma API RESTful para gerenciamento de folhas de pontos, construída com .NET e MySQL.

A API permite o controle completo dos horário de trabalho, incluindo operações de cadastro, consulta, atualização e exclusão, além de contar com um sistema de autenticação e autorização via JWT (JSON Web Token), garantindo segurança no acesso aos endpoints.

O projeto foi desenvolvido com foco em boas práticas, organização de código e facilidade de integração com aplicações front-end ou outros sistemas corporativos.

🛠️ Tecnologias utilizadas
<ul>
  <li>⚙️ .NET</li>
  <li>🗄️ MySQL</li>
  <li>🔐 JWT (JSON Web Token)</li>
</ul>

## 🔗 Endpoints da API

A API disponibiliza endpoints REST para o gerenciamento das folhas, permitindo operações de criação, consulta, atualização e exclusão (**CRUD**).

## ⚙️ Como usar?

Segue a rotina para usar a api de controle de folha de ponto:

<ol>
  <li>Faça a autenticação no sistema.</li>
  <li>Crie um produto.</li>
  <li>Registre uma entrada para o produto, é necessário inserir produtos no estoque.</li>
  <li>Dê um GET no estoque e veja o registro de estoque do produto cadastrado, totalmente automático.</li>
  <li>Registre uma saida para o produto para ver a operação de estoque funcionando.</li>
</ol>

---

### 🔐 Autorização

| Método        | Endpoint       | Descrição                                 |
|---------------|----------------|-------------------------------------------|
| 🟢 **POST**   | `/Auth`       | Realiza a autenticação do usuário na api  |

---

### 📝 Exemplo de POST (Auth)

```json
{
  "nm_usuario": "string",
  "senha": "string"
}
```
<!-- 
### 📦 Entrada Produto

| Método  | Endpoint                 | Descrição                                 |
|--------|---------------------------|-------------------------------------------|
| 🔵 **GET**    | `/EntradaProduto`         | Lista todas as entradas de produtos       |
| 🔵 **GET**    | `/EntradaProduto/id`      | Lista a entrada de produto por id         |
| 🟢 **POST**   | `/EntradaProduto`         | Cadastra uma nova entrada de produto      |

---

### 📝 Exemplo de POST (EntradaProduto)

```json
{
  "id_produto": int,
  "qtde": int,
  "valor_unitario": decimal,
  "data_entrada": DateTime
}
```

### 📦 Estoque

| Método  | Endpoint           | Descrição                                 |
|--------|---------------------|-------------------------------------------|
| 🔵 **GET**    | `/Estoque`          | Lista todos os registros do estoque       |
| 🔵 **GET**    | `/Estoque/id`       | Lista o registro de estoque por id        |

---

### 📦 Produto

| Método         | Endpoint           | Descrição                                 |
|----------------|---------------------|-------------------------------------------|
| 🔵 **GET**    | `/Produto`          | Lista todas os produtos                   |
| 🔵 **GET**    | `/Produto/id`       | Lista o produto por id                    |
| 🟢 **POST**   | `/Produto`          | Cadastra um novo produto                  |
| 🟡 **PUT**    | `/Produto/id`       | Edita um novo produto                     |
| 🔴 **DELETE** | `/Produto/id`       | Apaga um novo produto                     |

---

### 📝 Exemplo de POST (Produto)

```json
{
  "status": "string",
  "descricao": "string",
  "estoque_minino": 0,
  "estoque_maximo": 0
}
```

### 📝 Exemplo de PUT (Produto)

```json
{
  "status": "string",
  "descricao": "string",
  "estoque_minino": 0,
  "estoque_maximo": 0
}
```

### 📦 Saída Produto

| Método         | Endpoint             | Descrição                                 |
|----------------|-----------------------|-------------------------------------------|
| 🔵 **GET**    | `/SaidaProduto`       | Lista todas as saidas de produtos         |
| 🔵 **GET**    | `/SaidaProduto/id`    | Lista a saida de produto por id           |
| 🟢 **POST**   | `/SaidaProduto`       | Cadastra uma nova saida de produto        |

---

### 📝 Exemplo de POST (SaidaProduto)

```json
{
  "id_produto": int,
  "qtde": int,
  "valor_unitario": decimal,
  "data_entrada": DateTime
}
```

-->
📍 Observações

A API segue o padrão REST
