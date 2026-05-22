# Consinco — Cadastro Complementar de Produtos

Sistema desktop desenvolvido como etapa final do processo seletivo para a **Consinco / TOTVS**, com o objetivo de gerenciar informações complementares de produtos já existentes no ERP, sem interferir na estrutura original do sistema.

---

## Sobre o Projeto

O ERP TOTVS Consinco já possui uma tabela de produtos consolidada. Este sistema foi desenvolvido para **estender** essas informações com dados complementares (lote de fabricação, data de criação e descrição resumida), mantendo rastreabilidade e integridade referencial com o banco de dados Oracle existente.

---

## Tecnologias Utilizadas

| Tecnologia | Versão | Uso |
|---|---|---|
| C# / .NET Framework | 4.8 | Plataforma principal |
| Windows Forms | — | Interface desktop |
| Oracle Database | XE 21c | Banco de dados |
| Oracle.ManagedDataAccess | 23.26.200 | Driver ADO.NET Oracle |
| Microsoft.Extensions.DependencyInjection | 10.0.8 | Injeção de dependência |
| Microsoft.Extensions.Logging | 8.0.0 | Logging estruturado |
| Docker | — | Ambiente Oracle local |

---

## Arquitetura

O projeto foi estruturado em **4 camadas** seguindo os princípios de separação de responsabilidades e boas práticas de desenvolvimento:

```
Consinco.Complemento.Produto.UI          → Windows Forms (.NET Framework 4.8)
Consinco.Complemento.Produto.Application → Serviços e regras de negócio (.NET Standard 2.0)
Consinco.Complemento.Produto.Data        → Repositórios e acesso ao banco (.NET Framework 4.8)
Consinco.Complemento.Produto.Domain      → Entidades, interfaces e contratos (.NET Standard 2.0)
```

### Fluxo de dependências

```
UI  →  Application  →  Data
 ↘         ↓            ↓
        Domain  ←←←←←←←←
```

O `Domain` não possui dependências externas — é o núcleo do sistema e define os contratos que todas as outras camadas respeitam.

### Padrões aplicados

- **Repository Pattern** — isolamento do acesso ao banco de dados
- **Service Pattern** — orquestração das regras de negócio
- **Dependency Injection** — desacoplamento entre camadas via `IServiceProvider`
- **Interface Segregation** — contratos definidos no Domain, implementados nas camadas corretas

---

## Banco de Dados

### Estrutura

```
CONSINCO.PRODUTOS          → Tabela existente do ERP (simulada)
CONSINCO.COMPLEMENTOS      → Tabela criada pelo sistema (FK → PRODUTOS)
```

### Objetos criados

| Objeto | Nome | Descrição |
|---|---|---|
| Tabela | `PRODUTOS` | Simulação da tabela do ERP com produtos ativos/inativos |
| Tabela | `COMPLEMENTOS` | Informações complementares com FK para PRODUTOS |
| Sequence | `SEQ_PRODUTOS` | Auto incremento para PRODUTOS |
| Sequence | `SEQ_COMPLEMENTOS` | Auto incremento para COMPLEMENTOS |
| Trigger | `TRG_PRODUTOS_BI` | Aplica a sequence no INSERT de PRODUTOS |
| Trigger | `TRG_COMPLEMENTOS_BI` | Aplica a sequence no INSERT de COMPLEMENTOS |
| Index | `IDX_COMPLEMENTOS_PRD_ID` | Performance nas consultas por produto |
| Procedure | `PRC_INSERIR_COMPLEMENTO` | Insere complemento com validação de produto ativo |
| Procedure | `PRC_ATUALIZAR_COMPLEMENTO` | Atualiza complemento com validação de existência |
| Procedure | `PRC_EXCLUIR_COMPLEMENTO` | Exclui complemento com validação de existência |
| Procedure | `PRC_CONSULTAR_COMPLEMENTOS` | Consulta com filtros dinâmicos e cursor de retorno |
| Procedure | `PRC_LISTAR_PRODUTOS_ATIVOS` | Lista produtos ativos para o ComboBox |
| View | `VW_PRODUTOS_COMPLEMENTOS` | Visão consolidada para consulta do DBA |

### Acesso ao banco via `Universal`

Todo o acesso ao banco de dados é centralizado na classe `Universal` (camada Data), que encapsula conexão, transação e tratamento de erros. Os repositórios apenas definem **o quê** chamar, delegando a infraestrutura ao `Universal`:

```
IUniversal.ExecuteNonQueryAsync  → INSERT, UPDATE, DELETE via Procedure
IUniversal.ExecuteDataTableAsync → SELECT via Procedure com SYS_REFCURSOR
IUniversal.ExecuteScalarAsync    → Consultas de valor único
IUniversal.ExecuteDataRowAsync   → Consulta de registro único
```

---

## Funcionalidades

### Tela de Listagem (`FrmListagem`)

- Filtros por: **Código do Produto**, **Descrição**, **Lote de Fabricação** e **período de Data de Criação**
- Pesquisa somente com **ao menos um filtro preenchido** (regra de negócio)
- Grid exibe: ID, Código, Descrição do Produto, Lote, Data de Criação, Descrição Resumida
- Ações por linha: **Editar** e **Excluir** (com confirmação)
- Botão **+ Novo** para inclusão
- Contador de registros encontrados
- Após qualquer operação (inserir, editar, excluir) a pesquisa é refeita automaticamente

### Tela de Cadastro (`FrmCadastro`)

- Opera em dois modos: **inclusão** (novo) e **edição** (existente)
- **ComboBox** de produtos carregado dinamicamente via `PRC_LISTAR_PRODUTOS_ATIVOS`
- Produto desabilitado no modo edição (integridade do vínculo)
- Validação de todos os campos obrigatórios com foco automático no campo inválido
- Retorna `DialogResult.OK` ao salvar, sinalizando ao `FrmListagem` para recarregar o grid

---

## Como Executar

### Pré-requisitos

- Visual Studio 2022
- Docker Desktop
- DBeaver (ou Oracle SQL Developer)
- .NET Framework 4.8

### 1. Subir o banco Oracle via Docker

```bash
docker-compose up -d
```

Aguardar a mensagem `DATABASE IS READY TO USE!` nos logs:

```bash
docker logs oracle-consinco
```

### 2. Conectar no DBeaver

| Campo | Valor |
|---|---|
| Host | `localhost` |
| Porta | `1521` |
| Service Name | `XEPDB1` |
| Usuário | `consinco` |
| Senha | `Admin1234` |

### 3. Executar os scripts SQL na ordem

```
Database/Tables/produtos.sql                            → Tabela de produtos (simulação ERP)
Database/Tables/complementos.sql                        → Tabela de complementos com FK
Database/Seeds/produto_seeds.sql                        → Carga inicial de 10 produtos
Database/StoredProcedures/PRC_INSERIR_COMPLEMENTO.sql   → Procedure de inserção
Database/StoredProcedures/PRC_ATUALIZAR_COMPLEMENTO.sql → Procedure de atualização
Database/StoredProcedures/PRC_EXCLUIR_COMPLEMENTO.sql   → Procedure de exclusão
Database/StoredProcedures/PRC_CONSULTAR_COMPLEMENTOS.sql→ Procedure de consulta com filtros
Database/StoredProcedures/PRC_LISTAR_PRODUTOS_ATIVOS.sql→ Procedure de listagem de produtos
Database/Views/vw_produtos_complementos.sql             → View consolidada para o DBA
```

### 4. Configurar a connection string

No arquivo `App.config` do projeto `UI`, a connection string já está configurada:

```xml
<connectionStrings>
  <add name="OracleConsinco"
       connectionString="User Id=consinco;Password=Admin1234;Data Source=localhost:1521/XEPDB1;"
       providerName="Oracle.ManagedDataAccess.Client" />
</connectionStrings>
```

### 5. Executar

Definir `Consinco.Complemento.Produto.UI` como **Startup Project** e pressionar `F5`.

---

## Estrutura de Pastas

```
📁 Solution
│
├── 📁 Database                             
│   ├── Seeds/
│   │   └── produto_seeds.sql
│   ├── StoredProcedures/
│   │   ├── PRC_INSERIR_COMPLEMENTO.sql
│   │   ├── PRC_ATUALIZAR_COMPLEMENTO.sql
│   │   ├── PRC_EXCLUIR_COMPLEMENTO.sql
│   │   └── PRC_CONSULTAR_COMPLEMENTOS.sql
│   ├── Tables/
│   │   ├── produtos.sql
│   │   └── complementos.sql
│   └── Views/
│       └── vw_produtos_complementos.sql
│
├── 📁 Consinco.Complemento.Produto.UI
│   ├── Forms/
│   │   ├── FrmListagem.cs
│   │   └── FrmCadastro.cs
│   ├── ViewModels/
│   │   └── ComplementoGridRowVM.cs
│   ├── Program.cs
│   └── App.config
│
├── 📁 Consinco.Complemento.Produto.Application
│   └── Services/
│       ├── ComplementoService.cs
│       └── ProdutoService.cs
│
├── 📁 Consinco.Complemento.Produto.Data
│   ├── Configuration/
│   │   ├── IUniversal.cs
│   │   └── Universal.cs
│   └── Repositories/
│       ├── ComplementoRepository.cs
│       └── ProdutoRepository.cs
│
└── 📁 Consinco.Complemento.Produto.Domain
    ├── Entities/
    │   ├── ComplementoModel.cs
    │   ├── ComplementoFiltroRequest.cs
    │   └── ProdutoModel.cs
    └── Interfaces/
        ├── Repositories/
        │   ├── IComplementoRepository.cs
        │   └── IProdutoRepository.cs
        └── Services/
            ├── IComplementoService.cs
            └── IProdutoService.cs
```

---

## Decisões Técnicas

**Por que sem API?**
O sistema é um cliente desktop consumindo um banco Oracle local. Adicionar uma camada de API REST introduziria complexidade sem benefício real para este contexto — dois processos rodando, serialização HTTP, gerenciamento de porta. A arquitetura em camadas com DI atinge o mesmo desacoplamento de forma mais direta.

**Por que Stored Procedures para tudo?**
Padrão adotado em ambientes Oracle corporativos. Centraliza as regras de acesso no banco, facilita auditoria, manutenção e controle de permissões — sem necessidade de recompilar o sistema para ajustes de query.

**Por que a classe `Universal`?**
Evita a repetição de código de abertura/fechamento de conexão, tratamento de transação e log de erro em cada repositório. Os repositórios focam apenas no contrato de dados, delegando a infraestrutura ao `Universal`.

**Por que `.NET Framework 4.8` no projeto `Data`?**
O driver `Oracle.ManagedDataAccess` tem suporte pleno ao .NET Framework. Manter o projeto `Data` em Framework evita warnings de compatibilidade e garante que o driver funcione sem adaptações.

---

*Desenvolvido como desafio técnico para processo seletivo — Consinco / TOTVS*
