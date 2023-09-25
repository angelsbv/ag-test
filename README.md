## Sobre arquitectura.

Para la creación del <b>software para gestionar clientes y sus tarjetas (tanto débito como crédito).</b> Se siguió la arquitectura N-Capas DDD la cual ayuda a crear código mas mantenible, escalable y testable.
Encontraremos 2 carpetas `src` que contiene todo el código fuente de la aplicación y `test` que contiene las pruebas de la aplicación:

DAL (Data Access Layer): Capa de acceso a datos, implementación de repositorios, `DbContext`.

BLL (Business Logic Layer): Contiene modelos de datos, contratos/interfaces, implementación de servicios.

Web: En esta capa encontraremos controladores (API), validaciones de input, y vistas.

Esta es una idea general, se puede extender... podríamos tener una capa de infraestructura y una para responsabilidades transversales (cross-cutting). Además, a nivel de `src` y `test` podríamos tener carpetas de `docs` y `tools` para tener herramientas de desarrollo como linters y para pruebas de estres, `config` para tener configuraciones respecto al entorno en que se ejecuta la aplicación.

## Sobre pruebas:

Aunque lo sugerido son pruebas unitarias, por temas de tiempo (en principio para el MVP) preferí realizar pruebas [e2e](https://microsoft.github.io/code-with-engineering-playbook/automated-testing/e2e-testing/), las cuales cubren más partes del código.

## Sobre planes futuros del proyecto:

1. El diseño se puede mejorar y facilmente ya que no hay dependencias ni acoplamiento sobre esto.

2. Versionado de API: La API debería versionarse para evitar romper lo que está funcionando correctamente en caso de actualizaciones grandes.
   
3. DTOs, objetos Response y Request: Actualmente no se están utilizando objetos distintos a los modelos principales que utiliza la capa `DAL`, considero que es una mejora técnica interesante pero en principio no fué un blocker para el MVP. Más que ayudarnos a separar objetos, nos ayuda a separar responsabilidades.

4. Pruebas unitarias.

5. Extender la implementación de los validadores, para que cubran mas partes de los inputs y de manera más segura.
