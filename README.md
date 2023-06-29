<p align="center">
    <img src="docs/imgs/logo_complete.svg" alt="Dynamics 365 CE Base" />
</p>

# Dynamics 365 CE Base ![GitHub](https://img.shields.io/github/license/alangastaldi/Dynamics365CE-Base) ![Badge em Desenvolvimento](https://img.shields.io/badge/status-desenvolvimento-green)


## PT-BR

Gerenciar projetos de desenvolvimento em equipes para os ambientes *Dynamics 365 CE*/*Dataverse* da *Microsoft* pode ser um grande desafio. Por ser um sistema SaaS, criar um ambiente para cada desenvolvedor, ou branch, muitas vezes é algo inviável.

Por isso, criamos o **Dynamics 365 CE Base**, um projeto aberto e independente que oferece uma forma de organização, além de algumas ferramentas, que permitam que equipes atuem dentro de um mesmo ambiente de desenvolvimento, possibilitando uma maior agilidade e qualidade nas entregas.

O projeto propõe atender 3 grandes dificuldades:

- **Estrutura de Diretórios**: Criar um padrão de diretórios e arquivos que permita que você e sua equipe se localizem de forma simples e rápida dentro do projeto (veja mais detalhes na seção [STRUCTURE](STRUCTURE.md)).

- **Biblioteca Facilitadora**: Boa parte do desenvolvimento para o ambiente do Dynamics 365 CE/Dataverse é composto por uma série de inicializações de objetos e variáveis, validações e tratativas de erros. Não é nosso propósito alterar o SDK da Microsoft, apenas fornecer ferramentas para acelerar as etapas repetitivas do desenvolvimento.

- **Teste de Unidade**: A Microsoft não oferece uma maneira de realizar testes automatizados dentro do Dynamics 365 CE ou Dataverse ([mais detalhes](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/testing-tools-server)), deixando a cargo de algumas ferramentas da comunidade, como o [Fake Xrm Easy](https://dynamicsvalue.com/home). Porém, muitas vezes é necessário que os testes sejam executados no próprio ambiente, garantindo que as alterações de um projeto sejam testadas interagindo com os demais projetos. Para isso, pensamos em uma ferramenta simples, mas que auxilie na criação de registros dentro do seu ambiente.

### Como Usar

1. Crie um fork deste projeto.
2. Clone seu fork em sua máquina local.
3. Altere o arquivo `AssemblyKey.snk` na pasta `misc` para sua chave de assinatura.
4. Crie o arquivo `app.config`na pasta `%appdata%\Microsoft\UserSecrets\Dynamics365CEBase`.

**Obs**: Caso queira armazenar em uma nova pasta, altere a seguinte linha no arquivo `Tests.csproj`:

```
<None Include="$(AppData)\Microsoft\UserSecrets\Dynamics365CEBase\app.config" />
```
5. Caso queira instalar o Modelo de Projeto, copie o arquivo `Dynamics 365 Plugin.zip` da pasta `misc` para a pasta `C:\Users\Your-User\Documents\Visual Studio 2022\Templates\ProjectTemplates` (lembrando de alterar o caminho para seu usuário e sua versão do Visual Studio).
6. Agora basta criar novos projetos e iniciar seu desenvolvimento.

### Colabore com o Projeto

Este é um projeto aberto e toda contribuição é bem vinda, apenas lembre-se dos nossos objetivos:
- Este é um projeto genérico, pastas de cenários específicos devem ser criados em cada projeto individualmente.
- Não é nosso objetivo recriar o SDK da Microsoft.
- Na dúvida, utilize a ferramenta *Discussions* do GitHub.

### Licença

- [MIT](LICENSE.md)

## EN-US

Managing development projects in teams for *Microsoft* *Dynamics 365 CE*/*Dataverse* environments can be quite a challenge. As it is a SaaS system, creating an environment for each developer, or branch, is often unfeasible.

For this reason, we created **Dynamics 365 CE Base**, an open and independent project that offers a form of organization, in addition to some tools, that made teams work within the same development environment, allowing greater agility and delivery quality.

The project proposes to address 3 major difficulties:

- **Directory Structure**: Create a pattern of directories and files that allow you and your team to locate themselves in a simple and fast way within the project (see more details in the section [STRUCTURE](STRUCTURE.md)).

- **Facilitating Library**: Much of the development for the Dynamics 365 CE/Dataverse environment is made up of a series of object and variable initializations, validations and error handling. It's not our purpose to change the Microsoft SDK, just to provide tools to speed up repetitive development steps.

- **Unit Testing**: Microsoft does not provide a way to perform completed tests within Dynamics 365 CE or Dataverse ([more details](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/testing-tools-server)), leaving the load on some community tools like [Fake Xrm Easy](https://dynamicsvalue.com/home). However, it is often necessary for the tests to be executed in the environment itself, ensuring that changes in a project are tested by interacting with other projects. For this, think of a simple tool, but one that helps create records within your environment.

### How to use

1. Create a fork of this project.
2. Clone your fork on your local machine.
3. Change the `AssemblyKey.snk` file in the `misc` folder to your signing key.
4. Create the `app.config` file in the `%appdata%\Microsoft\UserSecrets\Dynamics365CEBase` folder.

**Note**: If you want to store it in a new folder, change the line below in the `Tests.csproj` file:

```
<None Include="$(AppData)\Microsoft\UserSecrets\Dynamics365CEBase\app.config" />
```
5. If you want to install the Project Template, copy the `Dynamics 365 Plugin.zip` file from the `misc` folder to the `C:\Users\Your-User\Documents\Visual Studio 2022\Templates\ProjectTemplates` folder (remembering to change the path for your user and your version of Visual Studio).
6. Now just create new projects and start its development.

### Collaborate with the Project

This is an open project and every contribution is welcome, just remember our goals:
- This is a generic project, specific scenario folders must be created in each individual project.
- It is not our goal to recreate the Microsoft SDK.
- When in doubt, use the *Discussions* tool from GitHub.

### License
- [MIT](LICENSE.md)
